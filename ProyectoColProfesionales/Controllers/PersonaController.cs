using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoColProfesionales.Models.DB1;

using System.Text;
using System.Security.Cryptography;
using ProyectoColProfesionales.Servicios.Contrato;
using Microsoft.AspNetCore.Authorization;


namespace ProyectoColProfesionales.Controllers
{
    public class PersonaController : Controller
    {
        private readonly DBColProfessionalContext _context;
        private readonly IEmailSender _emailSender;


        public PersonaController(DBColProfessionalContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public IActionResult Create()
        {
            return View();
        }

        //encriptacion 
        public static String EncriptarClave(string password)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(password));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        ///Generar password y Username
        private string GeneratePassword(string baseString)
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            return baseString.Substring(0, Math.Min(4, baseString.Length)) + randomNumber.ToString();
        }
        private string GenerateUserName(string firstName, string lastName)
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 999);
            return $"{firstName.Substring(0, 1).ToLower()}{lastName.ToLower()}{randomNumber}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.PersonModel person)
        {
            if (ModelState.IsValid)
            {
                //var person = new Person
                //{
                //    Names = Names,
                //    Lastname = Lastname,
                //    SecondLastName = SecondLastName,
                //    IdentityNumber = IdentityNumber,
                //    PhoneNumber = PhoneNumber,
                //    Email = Email,
                //    RegisterDate = DateTime.Now,
                //    Status = 1
                //};
                // Generar el nombre de usuario y la contraseña
                string userName = GenerateUserName(person.Names, person.Lastname); // Usando el email como nombre de usuario
                string rawPassword = GeneratePassword(person.Names); // Generar una contraseña basada en el nombre
                string emailContent = $"Hola {person.Names},<br/>Tu nueva cuenta ha sido creada.<br/>Nombre de usuario: {userName}<br/>Contraseña: {rawPassword}";
                await _emailSender.SendEmailAsync(person.Email, "Detalles de tu cuenta", emailContent);

                Person personCreate = new Person
                {
                    Names = person.Names,
                    Lastname = person.Lastname,
                    SecondLastName = person.SecondLastName,
                    IdentityNumber = person.IdentityNumber,
                    PhoneNumber = person.PhoneNumber,
                    Email = person.Email,
                    RegisterDate = DateTime.Now,
                    Status = 1
                };

                _context.People.Add(personCreate);
                await _context.SaveChangesAsync();

                var user = new User
                {
                    UserName = userName, // O cualquier otra lógica para definir el UserName
                    Password = EncriptarClave(rawPassword), // Deberías tener una manera segura de establecer contraseñas
                    Role = person.Role,
                    IdPerson = personCreate.IdPerson,
                    RegisterDate = DateTime.Now,
                    Status = 1
                };

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Lista));
            }

            // Si llegas aquí, algo salió mal
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Names, string Lastname, string SecondLastName, string IdentityNumber, string PhoneNumber, string Email, string Role)
        {
            if (ModelState.IsValid)
            {
                var person = new Person
                {
                    Names = Names,
                    Lastname = Lastname,
                    SecondLastName = SecondLastName,
                    IdentityNumber = IdentityNumber,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    RegisterDate = DateTime.Now,
                    Status = 1
                };

                //_context.Persons.Add(person);
                _context.People.Add(person);
                await _context.SaveChangesAsync();

                var user = new User
                {
                    UserName = "Secretary", // O cualquier otra lógica para definir el UserName
                    Password = "123456", // Deberías tener una manera segura de establecer contraseñas
                    Role = Role,
                    IdPerson = person.IdPerson,
                    RegisterDate = DateTime.Now,
                    Status = 1
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Lista));
            }

            // Si llegas aquí, algo salió mal
            return View();
        }*/

        /* [HttpGet]
         public async Task<IActionResult> Lista()
         {
             //List<Person> person = await _context.Persons.ToListAsync();
             //return View(person);
             var list = await _context.People
          .Join(_context.Users,
                person => person.IdPerson,      // Presumiblemente tienes una propiedad Id en Person
                user => user.IdPerson,    // Suponiendo que IdPerson en User es una FK a Person
                (person, user) => new
                {
                    FullName = person.Names + " " + person.Lastname,
                    Email = person.Email,
                    Role = user.Role
                })
              .ToListAsync();

             return View(list);
         }*/
        [Authorize(Roles = "Principal,Administrador")]
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var list = await _context.People
                 .Join(_context.Users,
                       person => person.IdPerson,
                       user => user.IdPerson,
                       (person, user) => new
                       {
                           IdPerson = person.IdPerson,
                           FullName = person.Names + " " + person.Lastname,
                           Email = person.Email,
                           Role = user.Role,
                           Status = person.Status // Agrega el estado de la persona al resultado
                       })
                 .Where(x => x.Status == 1) // Filtrar por estado activo
                 .ToListAsync();

            return View(list);
        }
        //agregado
        // Nueva acción para ver las notificaciones asociadas a una persona
        public async Task<IActionResult> Notificaciones(int idPerson)
        {
            var person = await _context.People
                .Include(p => p.Notifications2) // Carga las notificaciones de la persona
                .FirstOrDefaultAsync(p => p.IdPerson == idPerson);

            if (person == null)
            {
                return NotFound();
            }

            ViewData["FullName"] = $"{person.Names} {person.Lastname}";
            return View(person.Notifications2); // Envía la lista de notificaciones a la vista
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            person.Status = 0; // Cambiar el estado a inactivo

            // Actualizar el estado del usuario asociado
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdPerson == id);
            if (user != null)
            {
                user.Status = 0;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }



    }
}

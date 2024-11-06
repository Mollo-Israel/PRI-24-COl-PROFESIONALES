using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoColProfesionales.Models;
using ProyectoColProfesionales.Models.DB1;

namespace ProyectoColProfesionales.Controllers
{
    public class ProfessionalController : Controller
    {
        private readonly DBColProfessionalContext _context;

        public ProfessionalController(DBColProfessionalContext context)
        {
            _context = context;
        }
        public IActionResult CreateP()
        {
            return View();
        }
        [Authorize(Roles = "Secundario,Principal,Administrador")]
        [HttpGet]
        public async Task<IActionResult> ListP()
        {
            //List<Person> person = await _context.Persons.ToListAsync();
            //return View(person);

            //var list = await _context.People
            //            .Join(_context.Professionals,
            //   person => person.IdPerson,      // Presumiblemente tienes una propiedad Id en Person
            //   professional => professional.IdPerson,    // Suponiendo que IdPerson en User es una FK a Person
            //   (person, professional) => new
            //   {
            //       FullNameP = person.Names + " " + person.Lastname,
            //       Email = person.Email,
            //       Carrera = professional.Career,
            //       Universidad = professional.University,
            //       IdProfessional = professional.IdProfessional
            //   })
            // .ToListAsync();

            //return View(list);
            var list = await _context.People
                        .Join(_context.Professionals,
               person => person.IdPerson,
               professional => professional.IdPerson,
               (person, professional) => new
               {
                   FullNameP = person.Names + " " + person.Lastname,
                   Email = person.Email,
                   Carrera = professional.Career,
                   Universidad = professional.University,
                   IdProfessional = professional.IdProfessional,
                   Status = professional.Status
               })
               .Where(p => p.Status == 1) // Solo obtener profesionales activos
               .ToListAsync();

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateP(Models.ProfesionalModel form)
        {
            if (ModelState.IsValid)
            {
                var person = new Models.DB1.Person
                {
                    Names = form.Names,
                    Lastname = form.Lastname,
                    SecondLastName = form.SecondLastName,
                    IdentityNumber = form.IdentityNumber,
                    PhoneNumber = form.PhoneNumber,
                    Email = form.Email,
                    RegisterDate = DateTime.Now,
                    Status = 1
                };

                _context.People.Add(person);
                await _context.SaveChangesAsync();

                var professional = new Models.DB1.Professional
                {
                    Code = form.Code, // O cualquier otra lógica para definir el UserName
                    University = form.University, // Deberías tener una manera segura de establecer contraseñas
                    Career = form.Career,
                    Specialty = form.Specialty,
                    IdPerson = person.IdPerson,
                    RegisterDate = DateTime.Now,
                    Status = 1
                };

                _context.Professionals.Add(professional);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ListP));
            }

            // Si llegas aquí, algo salió mal
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var professional = await _context.Professionals.FindAsync(id);
            if (professional == null)
            {
                return NotFound();
            }

            // Realizar eliminación lógica
            professional.Status = 0;
            _context.Professionals.Update(professional);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ListP));
        }


        public async Task<IActionResult> EditP(int idProfesional)
        {
            var profesional = await _context.Professionals
                                            .Where(p => p.IdProfessional == idProfesional)
                                            .Select(p => new ProfesionalModel
                                            {
                                                IdPerson = p.IdPerson,
                                                Names = p.IdPersonNavigation.Names,
                                                Lastname = p.IdPersonNavigation.Lastname,
                                                SecondLastName = p.IdPersonNavigation.SecondLastName,
                                                IdentityNumber = p.IdPersonNavigation.IdentityNumber,
                                                PhoneNumber = p.IdPersonNavigation.PhoneNumber,
                                                Email = p.IdPersonNavigation.Email,
                                                RegisterDate = p.RegisterDate,
                                                LastUpdate = p.LastUpdate,
                                                Status = p.Status,
                                                Code = p.Code,
                                                University = p.University,
                                                Career = p.Career,
                                                Specialty = p.Specialty
                                            }).FirstOrDefaultAsync();

            if (profesional == null)
            {
                return NotFound();
            }

            // Pasar el IdProfessional a la vista usando ViewBag
            ViewBag.IdProfessional = idProfesional;

            return View(profesional);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditP(Models.ProfesionalModel form, int idProfesional)
        {
            if (!ModelState.IsValid)  // Verificar si el ModelState es inválido
            {
                // Depurar los errores de ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // O puedes usar logging en tu aplicación para registrar los errores
                }

                // Devolver la vista con el modelo en caso de error
                return View(form);
            }

            // Si el ModelState es válido, procede a realizar la actualización
            var professional = await _context.Professionals
                                             .Include(p => p.IdPersonNavigation)
                                             .FirstOrDefaultAsync(p => p.IdProfessional == idProfesional);

            if (professional == null)
            {
                return NotFound();
            }

            // Actualizar los datos de la persona
            var person = professional.IdPersonNavigation;
            person.Names = form.Names;
            person.Lastname = form.Lastname;
            person.SecondLastName = form.SecondLastName;
            person.IdentityNumber = form.IdentityNumber;
            person.PhoneNumber = form.PhoneNumber; // Actualizar teléfono
            person.Email = form.Email; // Actualizar email
            person.LastUpdate = DateTime.Now;

            // Actualizar los datos del profesional
            professional.Code = form.Code;
            professional.University = form.University;
            professional.Career = form.Career;
            professional.Specialty = form.Specialty;
            professional.LastUpdate = DateTime.Now;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ListP));
        }




    }
}

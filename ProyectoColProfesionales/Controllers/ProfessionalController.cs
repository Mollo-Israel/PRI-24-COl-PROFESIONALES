using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var person = new Person
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

                var professional = new Professional
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
    }
}

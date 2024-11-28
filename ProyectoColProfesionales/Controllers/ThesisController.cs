using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoColProfesionales.Data;
using ProyectoColProfesionales.Models;
using ProyectoColProfesionales.Models.DB1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoColProfesionales.Controllers
{
    public class ThesisController : Controller
    {
        private readonly DBColProfessionalContext _context;

        public ThesisController(DBColProfessionalContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Secundario")]
        [Authorize(Roles = "Secundario,Principal,Administrador")]
        public IActionResult Index()
        {
            List<Thesis> activeTheses = _context.Theses.Where(t => t.Status == 1).ToList();
            return View(activeTheses);
        }

        [HttpGet]
        public async Task<IActionResult> EditThesis(int id)
        {
            // Recuperar la tesis con su archivo asociado
            var thesis = await _context.Theses
                .Include(t => t.ThesisFiles)
                .Where(t => t.IdThesis == id)
                .FirstOrDefaultAsync();

            if (thesis == null)
            {
                return NotFound();
            }

            var thesisFile = thesis.ThesisFiles.FirstOrDefault();

            // Crear el modelo para la vista
            var model = new ThesisModel
            {
                idThesis = thesis.IdThesis,
                type = thesis.Type,
                description = thesis.Description,
                student = thesis.Student,
                career = thesis.Career,
                thesisFileUrl = thesisFile != null ? $"/ThesisFiles/Download/{thesisFile.IdThesis}" : null // Generar URL de descarga
            };

            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditThesis(ThesisModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var thesis = await _context.Theses
                .Include(t => t.ThesisFiles)
                .Where(t => t.IdThesis == model.idThesis)
                .FirstOrDefaultAsync();

            if (thesis == null)
            {
                return NotFound();
            }

            thesis.Type = model.type;
            thesis.Description = model.description;
            thesis.Student = model.student;
            thesis.Career = model.career;
            thesis.LastUpdate = DateTime.Now;

            // Si se subió un nuevo archivo, actualizarlo
            if (model.thesisFile != null && model.thesisFile.Length > 0)
            {
                var thesisFile = thesis.ThesisFiles.FirstOrDefault();
                if (thesisFile != null)
                {
                    thesisFile.DataFile = await ConvertToByteArrayAsync(model.thesisFile);
                    thesisFile.NameFile = model.thesisFile.FileName;
                }
                else
                {
                    thesisFile = new ThesisFile
                    {
                        IdThesis = thesis.IdThesis,
                        DataFile = await ConvertToByteArrayAsync(model.thesisFile),
                        NameFile = model.thesisFile.FileName,
                        ThesisType = "tesis"
                    };
                    _context.ThesisFiles.Add(thesisFile);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Details(int id)
        {
            Thesis thesis = await _context.Theses.Where(x => x.IdThesis == id).FirstOrDefaultAsync();
            return View(thesis);
        }
        // GET: Thesis/Create
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThesisModel thesis1)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    thesis1.registerDate = DateTime.Now;
                    thesis1.lastUpdate = DateTime.Now;
                    // Convertir el archivo PDF a un formato adecuado para guardar en la base de datos

                    Models.DB1.Thesis thesis = new Models.DB1.Thesis
                    {
                        Type= thesis1.type,
                        Description= thesis1.description,
                        Student = thesis1.student,
                        Career = thesis1.career,
                        RegisterDate = thesis1.registerDate,
                        LastUpdate = thesis1.lastUpdate,
                        Status = thesis1.status

                    };
                    //var tesisFile = await ConvertToByteArrayAsync(thesis1.thesisFile),

                    // Guardar la tesis en la base de datos
                    _context.Theses.Add(thesis);
                    await _context.SaveChangesAsync();

                    //guardar la tesis
                    Models.DB1.ThesisFile thesisFile = new Models.DB1.ThesisFile
                    {
                        IdThesis = thesis.IdThesis,
                        DataFile = await ConvertToByteArrayAsync(thesis1.thesisFile),
                        NameFile = thesis1.thesisFile.FileName,
                        ThesisType = "tesis"
                    };
                    _context.ThesisFiles.Add(thesisFile);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra durante el proceso de guardado
                    ModelState.AddModelError("", "Ocurrió un error al guardar la tesis. Por favor, inténtalo de nuevo.");
                }
            }

            // Si hay errores de validación o el proceso de guardado falla, recargar la vista con los datos ingresados por el usuario
            return View(thesis1);
        }
        // Método para convertir el archivo PDF en un formato adecuado para guardar en la base de datos
        private async Task<byte[]> ConvertToByteArrayAsync(Microsoft.AspNetCore.Http.IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public async Task<IActionResult> Download(int id)
        {
            var thesisFile = _context.ThesisFiles.Where(x=>x.IdThesis==id).FirstOrDefault();
            if(thesisFile != null)
            {
                return File(thesisFile.DataFile, "APPLICATION/octet-stream", thesisFile.NameFile);
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var thesis = await _context.Theses.FindAsync(id);
            if (thesis == null)
            {
                return NotFound();
            }

            thesis.Status = 0; // Cambiar el estado a eliminado
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}

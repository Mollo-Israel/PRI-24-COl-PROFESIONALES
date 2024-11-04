using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoColProfesionales.Data;
using ProyectoColProfesionales.Models;
using ProyectoColProfesionales.Models.DB1;
using ProyectoColProfesionales.Servicios.Contrato;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ProyectoColProfesionales.Controllers
{
    public class ActivityController : Controller
    {
        private readonly DBColProfessionalContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _env;
        private IWordProcessor _wordProcessor;

        private readonly string _templateLetter;
        private readonly string _directoryPath;

        public ActivityController(DBColProfessionalContext context, IEmailSender emailSender, IHostingEnvironment env)
        {
            _context = context;
            _emailSender = emailSender;
            _env = env;
            _templateLetter = "template_carta.docx";
            _wordProcessor = new WordProcessor();
            _directoryPath = Path.Combine(_env.WebRootPath, "templates") + "\\";
        }

        // Nueva acción para cargar la vista de notificación de asistencia
        [HttpGet]
        public async Task<IActionResult> CreateNotification(int id)
        {
            var activity = await _context.Activities
                .Include(a => a.ActivityProfessionals)
                    .ThenInclude(ap => ap.IdProfessionalNavigation)
                        .ThenInclude(p => p.IdPersonNavigation)
                .FirstOrDefaultAsync(a => a.IdActivity == id);

            if (activity == null)
            {
                return NotFound();
            }

            var model = new NotificationModel
            {
                IdActivity = activity.IdActivity,
                DateActivity = activity.DateActivity,
                Description = activity.Description,
                ActivityProfessionals = activity.ActivityProfessionals.ToList()
            };

            return View(model);
        }

        // Método POST para manejar el envío de datos y la programación de notificaciones
        [HttpPost]
        public IActionResult CreateNotification(NotificationModel model, string mensajesRecordatorios)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "recordatorios.txt");

            // Guardar mensajes de recordatorios o mensaje personalizado en el archivo
            string content = !string.IsNullOrEmpty(mensajesRecordatorios) ? mensajesRecordatorios : model.customMessage ?? "No se seleccionaron recordatorios ni mensaje personalizado.";
            System.IO.File.WriteAllText(path, content);

            return RedirectToAction("Index", "Activity");
        }

        // Nueva acción para guardar mensaje personalizado
        [HttpPost]
        public IActionResult GuardarMensaje(string mensajePersonalizado)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "mensaje.txt");
            System.IO.File.WriteAllText(path, mensajePersonalizado);
            return Json(new { success = true, message = "Mensaje guardado correctamente." });
        }

        [Authorize(Roles = "Secundario,Principal,Administrador")]
        public IActionResult Index(string stateFilter)
        {
            IQueryable<Activity> activities = _context.Activities;

            if (!string.IsNullOrEmpty(stateFilter))
            {
                activities = activities.Where(a => a.StateActivity == stateFilter);
            }

            List<Activity> list = activities.Where(a => a.Status == 1).ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            var availableTheses = _context.Theses.Where(t => t.Status == 1).ToList();
            ViewBag.AvailableTheses = availableTheses;

            var professionalsAndPersons = (from p in _context.Professionals
                                           join per in _context.People on p.IdPerson equals per.IdPerson
                                           select new SelectListItem
                                           {
                                               Value = $"{p.IdProfessional}",
                                               Text = $"{per.Names} {per.Lastname}"
                                           }).ToList();

            ViewBag.ProfessionalsAndPersons = professionalsAndPersons;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityModel activity1)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    activity1.registerDate = DateTime.Now;
                    activity1.lastUpdate = DateTime.Now;

                    var activity = new Activity
                    {
                        Auditorium = activity1.auditorium,
                        DateActivity = activity1.dateActivity,
                        Description = activity1.description,
                        HasAssistance = activity1.hasAssistance,
                        HasPayment = activity1.hasPayment,
                        IdThesis = activity1.idThesis,
                        Latitude = activity1.latitude,
                        Longitude = activity1.longitude,
                        Status = activity1.status,
                        StateActivity = "Pendiente",
                        Place = activity1.place,
                        RegisterDate = DateTime.Now
                    };

                    _context.Activities.Add(activity);
                    await _context.SaveChangesAsync();

                    if (activity1.voucherAsistenceFile != null)
                    {
                        var activityVoucher = new Models.DB1.ActivityVoucher
                        {
                            VoucherType = "Comprobante de Asistencia",
                            VoucherFile = await ConvertToByteArrayAsync(activity1.voucherAsistenceFile),
                            NameFile = activity1.voucherAsistenceFile.FileName,
                            IdActivity = activity.IdActivity
                        };

                        _context.ActivityVouchers.Add(activityVoucher);
                        await _context.SaveChangesAsync();
                    }

                    if (activity1.voucherPayFile != null)
                    {
                        var activityVoucher1 = new Models.DB1.ActivityVoucher
                        {
                            VoucherType = "Comprobante de Pago",
                            VoucherFile = await ConvertToByteArrayAsync(activity1.voucherPayFile),
                            NameFile = activity1.voucherPayFile.FileName,
                            IdActivity = activity.IdActivity
                        };

                        _context.ActivityVouchers.Add(activityVoucher1);
                        await _context.SaveChangesAsync();
                    }

                    if (activity1.ProfessionalsAndPersons != null)
                    {
                        foreach (var id in activity1.ProfessionalsAndPersons)
                        {
                            var activityProfessional = new Models.DB1.ActivityProfessional
                            {
                                IdActivity = activity.IdActivity,
                                IdProfessional = id
                            };
                            _context.ActivityProfessionals.Add(activityProfessional);
                            await _context.SaveChangesAsync();
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al guardar la actividad.");
                }
            }

            var availableTheses = _context.Theses.Where(t => t.Status == 1).ToList();
            ViewBag.AvailableTheses = availableTheses;
            return View(activity1);
        }

        private async Task<byte[]> ConvertToByteArrayAsync(Microsoft.AspNetCore.Http.IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(id);
                if (activity != null)
                {
                    activity.Status = 0;
                    activity.StateActivity = "Reprogramado";
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar la actividad.");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

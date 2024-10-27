using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoColProfesionales.Data;
using ProyectoColProfesionales.Models;
using ProyectoColProfesionales.Models.DB1;
using ProyectoColProfesionales.Servicios.Contrato;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace ProyectoColProfesionales.Controllers
{
    public class ActivityController : Controller
    {
        string _templateLetter;
        string _directoryPath;
        //private readonly DataDbContext _context;
        private readonly DBColProfessionalContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _env;
        private IWordProcessor _wordProcessor;

   

        public ActivityController(DBColProfessionalContext context, IEmailSender emailSender, IHostingEnvironment env)
        {
            _context = context;
            _emailSender = emailSender;
            _env = env;
            _templateLetter = "template_carta.docx";
            _wordProcessor = new WordProcessor();
            _directoryPath = Path.Combine(_env.WebRootPath, "templates") + "\\";
        }
        // Nueva acción para mostrar la vista de Notificación de Asistencia
        [HttpPost]
        public async Task<IActionResult> CreateNotification(int id)
        {
            var activity = await _context.Activities
                .Include(a => a.ActivityProfessionals)
                .ThenInclude(ap => ap.IdProfessionalNavigation)
                .ThenInclude(p => p.IdPersonNavigation) // Incluyendo los datos de la persona
                .FirstOrDefaultAsync(a => a.IdActivity == id);

            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }


        [HttpPost]
        public IActionResult SaveActivity(ActivityModel model)
        {
            // Lógica para guardar los datos del formulario y la notificación
            return RedirectToAction("Index");
        }

        //public IActionResult Index()
        //{
        //    List<Activity> list = new List<Activity>();
        //    list = _context.Activities.Where(a => a.Status == 1).ToList();
        //    //  list = _context.Activities.ToList();
        //    return View(list);

        //}
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

        // GET: Activity/Create
        public IActionResult Create()
        {
            // Obtener las tesis disponibles
            List<Thesis> availableTheses = _context.Theses.Where(t => t.Status == 1).ToList();
            ViewBag.AvailableTheses = availableTheses;

            // Crear la lista de profesionales y personas
            List<SelectListItem> professionalsAndPersons = (from p in _context.Professionals
                                                            join per in _context.People on p.IdPerson equals per.IdPerson
                                                            select new SelectListItem
                                                            {
                                                                Value = $"{p.IdProfessional}", // Usamos el Id del Profesional como valor
                                                                Text = $"{per.Names} {per.Lastname}" // Concatenamos el nombre del Profesional y de la Persona
                                                            }).ToList();

            // Asignar la lista a ViewBag
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
                    // Establecer la fecha de registro y actualización
                    activity1.registerDate = DateTime.Now;
                    activity1.lastUpdate = DateTime.Now;

                    // Crear una nueva actividad
                    Activity activity = new Activity
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

                    // Guardar la actividad en la base de datos
                    _context.Activities.Add(activity);
                    await _context.SaveChangesAsync();

                    // Guardar el voucher asociado a la actividad
                    if (activity1.voucherAsistenceFile != null && activity1.voucherAsistenceFile.Length > 0)
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

                    // Guardar el voucher asociado al pago
                    if (activity1.voucherPayFile != null && activity1.voucherPayFile.Length > 0)
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

                    // Guardar los profesionales asociados a la actividad
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

                    // Obtener la lista de profesionales
                    var professionalIds = activity1.ProfessionalsAndPersons;
                    var profesionalList = await (from p in _context.Professionals
                                                 join per in _context.People on p.IdPerson equals per.IdPerson
                                                 where professionalIds.Contains(p.IdProfessional)
                                                 select per).ToListAsync();

                    // Obtener información del estudiante (tesis)
                    var thesisId = activity1.idThesis;
                    var student = await _context.Theses.Where(x => x.IdThesis == thesisId).FirstOrDefaultAsync();
                    string studentName = student?.Student ?? "";
                    string studentCareer = student?.Career ?? "";

                    // Preparar contenido del correo electrónico
                    string emailContent = $"Buenas tardes,<br/><br/>Te informamos que has sido seleccionado/a para formar parte del tribunal en la próxima actividad.<br/>Fecha y hora de la actividad: {activity1.dateActivity}.<br/>Agradecemos tu colaboración y compromiso.<br/><br/>Saludos cordiales.";

                    // Enviar correos electrónicos a los profesionales
                    foreach (var itemProfesional in profesionalList)
                    {
                        var streamTemp = _wordProcessor.ModelLetter(_directoryPath, _templateLetter, new AttributeReplaceLetter
                        {
                            Carrera = studentCareer,
                            FechaCarta = GetDateSpanish(),
                            FechaPresentacion = GetDateTimeSpanish(activity1.dateActivity),
                            NombrePostulante = studentName,
                            Profesional = itemProfesional.Names + " " + itemProfesional.Lastname
                        });

                        await _emailSender.SendEmailAsync(itemProfesional.Email, "Detalles de tu cuenta", emailContent, streamTemp.memoryStream, streamTemp.NameFile);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra durante el proceso de guardado
                    ModelState.AddModelError("", "Ocurrió un error al guardar la actividad. Por favor, inténtalo de nuevo.");
                }
            }

            // Si hay errores de validación, recargar la vista con los datos ingresados por el usuario
            List<Thesis> availableTheses = _context.Theses.Where(t => t.Status == 1).ToList();
            ViewBag.AvailableTheses = new SelectList(availableTheses, "IdThesis", "Description");

            // Recargar el combo de Profesionales y Personas
            List<SelectListItem> professionalsAndPersons = await (from p in _context.Professionals
                                                                  join per in _context.People on p.IdPerson equals per.IdPerson
                                                                  select new SelectListItem
                                                                  {
                                                                      Value = $"{p.IdProfessional}",
                                                                      Text = $"{per.Names} {per.Lastname}"
                                                                  }).ToListAsync();

            ViewBag.ProfessionalsAndPersons = professionalsAndPersons;

            return View(activity1);
        }


        private string GetDateSpanish()
        {
            //"12 de mayo 2024"
            DateTime currentDate = DateTime.Now;
            string dateString = string.Format("{0} de {1} {2}", currentDate.ToString("dd"),
                currentDate.ToString("MMMM", new System.Globalization.CultureInfo("es-ES")),
                currentDate.ToString("yyyy")
                );
            return dateString;
        }
        private string GetDateTimeSpanish(DateTime currentDate)
        {
            //24 de mayo 2024 a horas 12:30
            string dateString = string.Format("{0} de {1} {2} a horas {3}", currentDate.ToString("dd"),
                currentDate.ToString("MMMM", new System.Globalization.CultureInfo("es-ES")),
                currentDate.ToString("yyyy"),
                currentDate.ToString("HH:mm")
                );
            return dateString;
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
                // Obtener la actividad por su ID usando el DbContext inyectado
                var activity = await _context.Activities.FindAsync(id);

                if (activity != null)
                {
                    // Cambiar el estado de la actividad a inactivo (status = 0) y reprogramado
                    activity.Status = 0;
                    activity.StateActivity = "Reprogramado";

                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // Redirigir a la acción Index después de la eliminación
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si no se encuentra la actividad, devolver un error 404
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante el proceso de eliminación
                ModelState.AddModelError("", "Ocurrió un error al eliminar la actividad. Por favor, inténtalo de nuevo.");
                // Registrar el error (puedes usar un logger si lo tienes configurado)
                //_logger.LogError(ex, "Error al eliminar la actividad.");

                // Redirigir a Index para evitar que la aplicación se quede en un estado incorrecto
                return RedirectToAction(nameof(Index));
            }
        }


        //NTOFICACION
        private async Task ScheduleNotification(Activity activity)
        {
            var timeUntilNotification = activity.DateActivity - DateTime.Now - TimeSpan.FromSeconds(5);
            if (timeUntilNotification.TotalMilliseconds > 0)
            {
                await Task.Delay(timeUntilNotification);
                await SendNotification(activity);
            }
        }

        private async Task SendNotification(Activity activity)
        {
            var professionalIds = await _context.ActivityProfessionals
                .Where(ap => ap.IdActivity == activity.IdActivity)
                .Select(ap => ap.IdProfessional)
                .ToListAsync();

            var profesionalList = await (from p in _context.Professionals
                                         join per in _context.People on p.IdPerson equals per.IdPerson
                                         where professionalIds.Contains(p.IdProfessional)
                                         select per
                                        ).ToListAsync();

            string emailContent = $"TIENES DEFENSA el {activity.DateActivity:dd/MM/yyyy HH:mm}.";

            foreach (var itemProfesional in profesionalList)
            {
                await _emailSender.SendEmailAsync(itemProfesional.Email, "Recordatorio de Defensa", emailContent);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            try
            {
                // Obtener la actividad por su ID usando el DbContext inyectado
                var activity = await _context.Activities.FindAsync(id);

                if (activity != null)
                {
                    // Cambiar el estado de la actividad a "Finalizado"
                    activity.StateActivity = "Finalizado";

                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // Redirigir a la acción Index después del cambio de estado
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si no se encuentra la actividad, devolver un error 404
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante el proceso de actualización
                ModelState.AddModelError("", "Ocurrió un error al cambiar el estado de la actividad. Por favor, inténtalo de nuevo.");
                // Puedes agregar registros de errores a tus registros de aplicación para su posterior análisis
                //_logger.LogError(ex, "Error al cambiar el estado de la actividad.");

                return RedirectToAction(nameof(Index));
            }
        }


        // GET: Activity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            // Mapear los datos de la actividad al modelo de vista ActivityModel
            var activityModel = new ActivityEditModel
            {
                idActivity = activity.IdActivity,
                description = activity.Description,
                hasAssistance = false, // Convertir a bool, si es nulo, se asignará false
                hasPayment = false,
                // Agrega más mapeos si es necesario
            };

            

            return View(activityModel);
        }
      

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ActivityEditModel activity)
        {
            if (activity.idActivity<=0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new DBColProfessionalContext())
                    {
                        var existingActivity = await db.Activities.FindAsync(activity.idActivity);
                        if (existingActivity == null)
                        {
                            return NotFound();
                        }

                        existingActivity.HasAssistance = activity.hasAssistance;
                        existingActivity.HasPayment = activity.hasPayment;
                        existingActivity.LastUpdate = DateTime.Now;

                        db.Entry(existingActivity).State = EntityState.Modified;
                        await db.SaveChangesAsync();


                        // Guardar el voucher asociado a la actividad
                        if (activity.voucherAsistenceFile != null && activity.voucherAsistenceFile.Length > 0)
                        {
                            var activityVoucher = new ProyectoColProfesionales.Models.DB1.ActivityVoucher
                            {
                                VoucherType = "Comprobante de Asistencia", // Puedes establecer un tipo de comprobante aquí si es relevante
                                VoucherFile = await ConvertToByteArrayAsync(activity.voucherAsistenceFile), // Convertir el archivo a un array de bytes
                                NameFile = activity.voucherAsistenceFile.FileName,
                                IdActivity = activity.idActivity // Asociar el comprobante con la actividad recién creada
                            };

                            db.ActivityVouchers.Add(activityVoucher);
                            await db.SaveChangesAsync();
                        }
                        // Guardar el voucher asociado a la actividad
                        if (activity.voucherPayFile != null && activity.voucherPayFile.Length > 0)
                        {
                            var activityVoucher1 = new ProyectoColProfesionales.Models.DB1.ActivityVoucher
                            {
                                VoucherType = "Comprobante de Pago", // Puedes establecer un tipo de comprobante aquí si es relevante
                                VoucherFile = await ConvertToByteArrayAsync(activity.voucherPayFile), // Convertir el archivo a un array de bytes
                                NameFile = activity.voucherPayFile.FileName,
                                IdActivity = activity.idActivity // Asociar el comprobante con la actividad recién creada
                            };

                            db.ActivityVouchers.Add(activityVoucher1);
                            await db.SaveChangesAsync();
                        }

                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al guardar los cambios de la actividad. Por favor, inténtalo de nuevo.");
                    return View(activity);
                }
            }

           // return View(activity);
            return RedirectToAction(nameof(Index));
        }
        


    }
}

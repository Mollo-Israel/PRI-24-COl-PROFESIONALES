using Microsoft.AspNetCore.Mvc;
using ProyectoColProfesionales.Models;
using System;

namespace ProyectoColProfesionales.Controllers
{
    public class NotificationsController : Controller
    {
        [HttpPost]
        public IActionResult SendWhatsAppMessage(NotificationModel model)
        {
            // Combinar la fecha y la hora seleccionadas para obtener la fecha y hora completas
            DateTime scheduledDateTime = model.ScheduledDate.Add(model.ScheduledTime);

            // Validar que la fecha y hora combinadas no sean menores a la fecha y hora actual
            if (scheduledDateTime < DateTime.Now)
            {
                ModelState.AddModelError("ScheduledDate", "La fecha y hora deben ser mayores o iguales a la fecha y hora actual.");
            }

            // Validar el modelo y continuar si no hay errores
            if (!ModelState.IsValid)
            {
                return View("CreateNotification", model);
            }

            // Lógica para concatenar el mensaje según los valores seleccionados
            string message = $"{model.SelectedReminder} ";
            if (!string.IsNullOrEmpty(model.CustomMessage))
            {
                message += $"{model.CustomMessage} ";
            }
            message += $"Fecha y Hora: {scheduledDateTime:dd/MM/yyyy HH:mm}";

            // Concatenar el número con el código de país
            string fullPhoneNumber = $"{model.CountryCode}{model.Phone}";

            // Redirigir a WhatsApp Web con el mensaje completo
            string whatsappUrl = $"https://wa.me/{fullPhoneNumber}?text={Uri.EscapeDataString(message)}";
            return Redirect(whatsappUrl);
        }
    }
}

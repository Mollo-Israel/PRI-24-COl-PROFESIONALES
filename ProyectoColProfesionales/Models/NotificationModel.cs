using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models
{
    public class NotificationModel
    {
        [Required(ErrorMessage = "Por favor selecciona un código de país.")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Por favor ingresa un número de teléfono.")]
        [Phone(ErrorMessage = "Por favor ingresa un número de teléfono válido.")]
        public string Phone { get; set; }

        public string CustomMessage { get; set; }

        [Required(ErrorMessage = "Por favor selecciona una fecha.")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "La fecha debe ser mayor o igual a la fecha actual.")]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "Por favor selecciona una hora.")]
        [DataType(DataType.Time)]
        public TimeSpan ScheduledTime { get; set; }

        public string SelectedReminder { get; set; }

        // Método para obtener la fecha y hora combinadas
        public DateTime GetScheduledDateTime()
        {
            return ScheduledDate.Add(ScheduledTime);
        }
    }
}

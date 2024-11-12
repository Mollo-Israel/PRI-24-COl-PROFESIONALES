using ProyectoColProfesionales.Models.DB1;
using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models.DB1
{
    public class ActivityNotification2
    {
        public int IdActivity {  get; set; }
        public Activity? Activity { get; set; }
        public Notification2? Notification2 { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "El mensaje debe tener entre 5 y 250 caracteres")]
        [Display(Name = "Mensaje a Enviar")]
        public string Message { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        [Display(Name = "Fecha Programada 1")]
        public DateTime? Date1 { get; set; } = DateTime.Now;

        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        [Display(Name = "Fecha Programada 2")]
        public DateTime? Date2 { get; set; } = DateTime.Now;

        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        [Display(Name = "Fecha Programada 3")]
        public DateTime? Date3 { get; set; } = DateTime.Now;

        public int Status { get; set; } = 1;

        [Required(ErrorMessage = "La Persona es obligatoria")]
        [Display(Name = "Profesional")]
        public int IdPerson { get; set; }

    }
}

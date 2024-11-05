using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoColProfesionales.Models.DB1
{
    public class Notification2
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "El mensaje debe tener mínimo entre 5 a 250 caracteres")]
        public string Message { get; set; }


        [Required(ErrorMessage = "Al menos una fecha es obligatoria")]
        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        public DateTime? Date1 { get; set; }


        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        public DateTime? Date2 { get; set; }


        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        public DateTime? Date3 { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Status { get; set; } = 1;

        [Required(ErrorMessage = "La Persona es obligatoria")]
        public int IdPerson { get; set; }
        public Person? person { get; set; }

      
    }
}

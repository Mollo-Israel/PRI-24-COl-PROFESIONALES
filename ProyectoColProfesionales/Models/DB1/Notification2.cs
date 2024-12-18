using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models.DB1
{
    public class Notification2 : IValidatableObject
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "El mensaje debe tener entre 5 y 250 caracteres")]
        [Display(Name = "Mensaje a Enviar")]
        public string Message { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        [Display(Name = "Programar Fecha 1")]
        public DateTime? Date1 { get; set; } = DateTime.Now;

        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        [Display(Name = "Programar Fecha 2")]
        public DateTime? Date2 { get; set; } = DateTime.Now;

        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida")]
        [Display(Name = "Programar Fecha 3")]
        public DateTime? Date3 { get; set; } = DateTime.Now;

        public int Status { get; set; } = 1;

        [Required(ErrorMessage = "La Persona es obligatoria")]
        [Display(Name = "Programar Fecha 3")]
        public int IdPerson { get; set; }

        public Person? Person { get; set; }

        // Validación personalizada
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Date1.HasValue && !Date2.HasValue && !Date3.HasValue)
            {
                yield return new ValidationResult("Debe ingresar al menos una fecha.", new[] { nameof(Date1), nameof(Date2), nameof(Date3) });
            }
        }
    }
}

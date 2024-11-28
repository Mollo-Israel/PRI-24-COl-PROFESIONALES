using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models;

public class ActivityModel
{
    public int idActivity { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(50, ErrorMessage = "La descripción no puede tener más de 50 caracteres.")]
    [RegularExpression(@"^(?!.*\s{2,})(?!\s)[a-zA-Z0-9\sáéíóúÁÉÍÓÚüÜñÑ.,]+(?<!\s)$",
    ErrorMessage = "La descripción no debe comenzar ni terminar con espacios, no debe contener caracteres especiales excepto puntos y debe permitir acentos.")]
    public string description { get; set; }

    [Required(ErrorMessage = "La fecha y hora de la defensa es obligatorio.")]
    [Display(Name = "Fecha de Actividad")]
    [DataType(DataType.DateTime)]
    //[FutureDate(ErrorMessage = "La fecha de actividad debe ser posterior o igual a la fecha actual.")]
    public DateTime dateActivity { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El auditorio es obligatorio.")]
    [StringLength(50, ErrorMessage = "El auditorio no puede tener más de 50 caracteres.")]
    [RegularExpression(@"^(?!.*\s{2,})(?!\s)[a-zA-Z0-9\sáéíóúÁÉÍÓÚüÜñÑ.,]+(?<!\s)$",
    ErrorMessage = "El auditorio no debe comenzar ni terminar con espacios, no debe contener caracteres especiales excepto puntos y debe permitir acentos.")]
    public string auditorium { get; set; }

    [Required(ErrorMessage = "La ubicación es obligatoria.")]
    [StringLength(50, ErrorMessage = "La ubicación no puede tener más de 50 caracteres.")]
    [RegularExpression(@"^(?!.*\s{2,})(?!\s)[a-zA-Z0-9\sáéíóúÁÉÍÓÚüÜñÑ.,]+(?<!\s)$",
    ErrorMessage = "La ubicación no debe comenzar ni terminar con espacios, no debe contener caracteres especiales excepto puntos y debe permitir acentos.")]
    public string place { get; set; }

    [Required(ErrorMessage = "El proyecto es obligatorio.")]
    public int idThesis { get; set; }

    public DateTime registerDate { get; set; }
    public DateTime lastUpdate { get; set; }
    public byte status { get; set; }
    public int idUserRegister { get; set; }
    public int idUserLastUpdate { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    public string latitude { get; set; }


    [Required(ErrorMessage = "El campo es obligatorio.")]
    public string longitude { get; set; }

    public string? stateActivity { get; set; }

    //[Required(ErrorMessage = "El campo es obligatorio.")]
    public bool hasAssistance { get; set; }
    //[Required(ErrorMessage = "El campo es obligatorio.")]
    public bool hasPayment { get; set; }

    public int[]? ProfessionalsAndPersons { get; set; }
    public int Professionals { get; set; }
    //[Required(ErrorMessage = "El comprobante de asistencia es obligatorio.")]
    public Microsoft.AspNetCore.Http.IFormFile? voucherAsistenceFile { get; set; }
    //[Required(ErrorMessage = "El comprobante de pago es obligatorio.")]
    public Microsoft.AspNetCore.Http.IFormFile? voucherPayFile { get; set; }
}

public class ActivityEditModel
{
    public int idActivity { get; set; }
    public string description { get; set; }
    public bool hasAssistance { get; set; }
    //[Required(ErrorMessage = "El campo es obligatorio.")]
    public bool hasPayment { get; set; }
    public Microsoft.AspNetCore.Http.IFormFile? voucherAsistenceFile { get; set; }
    //[Required(ErrorMessage = "El comprobante de pago es obligatorio.")]
    public Microsoft.AspNetCore.Http.IFormFile? voucherPayFile { get; set; }
}

public class FutureDateAttribute : ValidationAttribute
{
    public FutureDateAttribute()
    {
        ErrorMessage = "La fecha de actividad debe ser posterior o igual a la fecha actual.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            if (dateValue < DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}
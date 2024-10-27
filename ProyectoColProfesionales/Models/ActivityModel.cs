using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models;

public class ActivityModel
{
    public int idActivity { get; set; }

    [Required(ErrorMessage = "La descripción es obligatorio.")]
    public string description { get; set; }

    [Required(ErrorMessage = "La fecha y hora de la defensa es obligatorio.")]
    [Display(Name = "Fecha de Actividad")]
    [DataType(DataType.DateTime)]
    //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    //[FutureDate(ErrorMessage = "La fecha de actividad debe ser posterior o igual a la fecha actual.")]
    public DateTime dateActivity { get; set; } //= DateTime.Now;

    [Required(ErrorMessage = "El auditorio es obligatorio.")]
    public string auditorium { get; set; }

    [Required(ErrorMessage = "La ubicación es obligatorio.")]
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

    public string stateActivity { get; set; }

    //[Required(ErrorMessage = "El campo es obligatorio.")]
    public bool hasAssistance { get; set; }
    //[Required(ErrorMessage = "El campo es obligatorio.")]
    public bool hasPayment { get; set; }
    public int[] ProfessionalsAndPersons { get; set; }
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
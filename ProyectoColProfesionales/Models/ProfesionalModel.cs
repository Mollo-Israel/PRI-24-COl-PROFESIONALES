using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models;

public class ProfesionalModel
{
    [Column("idPerson")]
    public int IdPerson { get; set; }

    [Column("names")]
    [StringLength(60)]
    [Unicode(false)]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [RegularExpression(@"^(?!.*\s{2,})([a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+)$", ErrorMessage = "El nombre solo puede contener letras y espacios, y no puede tener espacios adicionales.")]
    public string Names { get; set; } = null!;

    [Column("lastname")]
    [StringLength(35)]
    [Unicode(false)]
    [Required(ErrorMessage = "El primer apellido es obligatorio.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "El apellido solo puede contener letras sin espacios.")]
    public string Lastname { get; set; } = null!;

    [Column("secondLastName")]
    [StringLength(35)]
    [Unicode(false)]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "El segundo apellido solo puede contener letras sin espacios.")]
    public string? SecondLastName { get; set; }
    [Column("identityNumber")]
    [StringLength(15)]
    [Unicode(false)]
    [Required(ErrorMessage = "El número de C.I. es obligatorio.")]
    [RegularExpression(@"^(?!.*\s{2,})([a-zA-Z0-9]+)$", ErrorMessage = "El número de C.I. solo puede contener letras y números, y no puede tener espacios adicionales.")]
    public string IdentityNumber { get; set; } = null!;

    [Column("phoneNumber")]
    [StringLength(20)]
    [Unicode(false)]
    [Required(ErrorMessage = "El número de celular es obligatorio.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "El número de celular solo puede contener números.")]
    public string PhoneNumber { get; set; } = null!;

    [Column("email")]
    [StringLength(60)]
    [Unicode(false)]
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [RegularExpression(@"^(?!.*\s{2,})([a-zA-Z0-9@.]+)$", ErrorMessage = "El correo electrónico solo puede contener letras, números, @ y puntos, y no puede tener espacios adicionales.")]
    public string? Email { get; set; }

    [Column("registerDate", TypeName = "datetime")]
    public DateTime RegisterDate { get; set; }

    [Column("lastUpdate", TypeName = "datetime")]
    public DateTime? LastUpdate { get; set; }

    [Column("status")]
    public byte Status { get; set; }

    [Column("idUserRegister")]
    public int? IdUserRegister { get; set; }

    [Column("idUserLastUpdate")]
    public int? IdUserLastUpdate { get; set; }

    [Column("code")]
    [StringLength(50)]
    [Unicode(false)]
    [Required(ErrorMessage = "El código es obligatorio.")]
    [RegularExpression(@"^(?!.*\s{2,})([a-zA-Z0-9\s]+)$", ErrorMessage = "El código solo puede contener letras y números, y no puede tener espacios adicionales.")]
    public string Code { get; set; } = null!;

    [Column("university")]
    [StringLength(100)]
    [Unicode(false)]
    [Required(ErrorMessage = "La universidad es obligatoria.")]
    [RegularExpression(@"^(?!.*\s{2,})([a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+)$", ErrorMessage = "La universidad solo puede contener letras y espacios, y no puede tener espacios adicionales.")]
    public string? University { get; set; }

    [Column("career")]
    [StringLength(100)]
    [Unicode(false)]
    [Required(ErrorMessage = "La carrera es obligatoria.")]
    [RegularExpression(@"^(?!.*\s{2,})([a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+)$", ErrorMessage = "La carrera solo puede contener letras y espacios, y no puede tener espacios adicionales.")]
    public string? Career { get; set; }

    [Column("specialty")]
    [StringLength(50)]
    [Unicode(false)]
    [Required(ErrorMessage = "La especialidad es obligatoria.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+(?:\s[a-zA-ZáéíóúÁÉÍÓÚñÑ]+)*$", ErrorMessage = "La especialidad solo puede contener letras y espacios simples entre palabras.")]
    public string Specialty { get; set; } = null!;

}

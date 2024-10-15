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
    public string Names { get; set; } = null!;
    [Column("lastname")]
    [StringLength(35)]
    [Unicode(false)]
    [Required(ErrorMessage = "El primer apellido es obligatorio.")]
    public string Lastname { get; set; } = null!;

    [Column("secondLastName")]
    [StringLength(35)]
    [Unicode(false)]
    public string? SecondLastName { get; set; }

    [Column("identityNumber")]
    [StringLength(15)]
    [Unicode(false)]
    [Required(ErrorMessage = "El número de C.I. es obligatorio.")]
    public string IdentityNumber { get; set; } = null!;

    [Column("phoneNumber")]
    [StringLength(20)]
    [Unicode(false)]
    [Required(ErrorMessage = "El número de celular es obligatorio.")]
    public string PhoneNumber { get; set; } = null!;

    [Column("email")]
    [StringLength(60)]
    [Unicode(false)]
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
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
    public string Code { get; set; } = null!;

    [Column("university")]
    [StringLength(100)]
    [Unicode(false)]
    [Required(ErrorMessage = "La universidad es obligatorio.")]
    public string? University { get; set; }
    [Column("career")]
    [StringLength(100)]
    [Unicode(false)]
    [Required(ErrorMessage = "La carrera es obligatorio.")]
    public string? Career { get; set; }

    [Column("specialty")]
    [StringLength(50)]
    [Unicode(false)]
    [Required(ErrorMessage = "La especialidad es obligatorio.")]
    public string Specialty { get; set; } = null!;
}

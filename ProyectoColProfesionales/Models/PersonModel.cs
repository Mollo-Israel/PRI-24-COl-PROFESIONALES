using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models
{
    public class PersonModel
    {

        public int IdPerson { get; set; }
        [StringLength(60)]
        [Unicode(false)]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Names { get; set; } = null!;
        [StringLength(35)]
        [Unicode(false)]
        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        public string Lastname { get; set; } = null!;

        [StringLength(35)]
        [Unicode(false)]
        public string? SecondLastName { get; set; }

        [StringLength(15)]
        [Unicode(false)]
        [Required(ErrorMessage = "El número de C.I. es obligatorio.")]
        public string IdentityNumber { get; set; } = null!;

        [StringLength(20)]
        [Unicode(false)]
        [Required(ErrorMessage = "El número de celular es obligatorio.")]
        public string PhoneNumber { get; set; } = null!;

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

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Role { get; set; }
    }
}

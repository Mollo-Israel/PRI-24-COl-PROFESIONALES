using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(35)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [ForeignKey("Person")]
        public int IdPerson { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public DateTime? LastUpdate { get; set; }

        [Required]
        public byte Status { get; set; } = 1; // Asumiendo que el estado 'activo' es 1 por defecto

        public int? IdUserRegister { get; set; }

        public int? IdUserLastUpdate { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models
{
    [Table("Notification")]
    public class Notification
    {
        //DNSC1SVKCL5PZ9R94DTL96NV link whatsapp para enviar mensaje
        [Key]
        public int IdNotification { get; set; }

        [Required]
        [StringLength(50)]
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [ForeignKey("Professional")]
        public int IdProfessional { get; set; }

        [ForeignKey("Letter")]
        public int IdLetter { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdate { get; set; }

        [Required]
        public byte Status { get; set; }

        public int? IdUserRegister { get; set; }

        public int? IdUserLastUpdate { get; set; }
    }
}

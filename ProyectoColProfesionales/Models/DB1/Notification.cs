using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        [Column("idNotification")]
        public int IdNotification { get; set; }
        [Column("message")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Message { get; set; }
        [Column("dateTime", TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("idProfessional")]
        public int? IdProfessional { get; set; }
        [Column("idLetter")]
        public int? IdLetter { get; set; }
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

        [StringLength(100)]
        public string? MessageType { get; set; } // Actualizado a 100 caracteres

        [ForeignKey("IdLetter")]
        [InverseProperty("Notifications")]
        public virtual Letter? IdLetterNavigation { get; set; }
        [ForeignKey("IdProfessional")]
        [InverseProperty("Notifications")]
        public virtual Professional? IdProfessionalNavigation { get; set; }
    }
}

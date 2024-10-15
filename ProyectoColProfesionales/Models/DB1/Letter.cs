using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("Letter")]
    public partial class Letter
    {
        public Letter()
        {
            Notifications = new HashSet<Notification>();
        }

        [Key]
        [Column("idLetter")]
        public int IdLetter { get; set; }
        [Column("idProfessional")]
        public int? IdProfessional { get; set; }
        [Column("format")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Format { get; set; }
        [Column("idActivity")]
        public int? IdActivity { get; set; }
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

        [ForeignKey("IdActivity")]
        [InverseProperty("Letters")]
        public virtual Activity? IdActivityNavigation { get; set; }
        [ForeignKey("IdProfessional")]
        [InverseProperty("Letters")]
        public virtual Professional? IdProfessionalNavigation { get; set; }
        [InverseProperty("IdLetterNavigation")]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}

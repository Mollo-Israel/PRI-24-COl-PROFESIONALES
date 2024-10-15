using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("Professional")]
    public partial class Professional
    {
        public Professional()
        {
            ActivityProfessionals = new HashSet<ActivityProfessional>();
            Letters = new HashSet<Letter>();
            Notifications = new HashSet<Notification>();
        }

        [Key]
        [Column("idProfessional")]
        public int IdProfessional { get; set; }
        [Column("specialty")]
        [StringLength(50)]
        [Unicode(false)]
        [Required(ErrorMessage = "La especialidad es obligatorio.")]
        public string Specialty { get; set; } = null!;
        [Column("code")]
        [StringLength(50)]
        [Unicode(false)]
        public string Code { get; set; } = null!;
        [Column("idPerson")]
        public int IdPerson { get; set; }
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
        [Column("university")]
        [StringLength(100)]
        [Unicode(false)]
        public string? University { get; set; }
        [Column("career")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Career { get; set; }

        [ForeignKey("IdPerson")]
        [InverseProperty("Professionals")]
        public virtual Person IdPersonNavigation { get; set; } = null!;
        [InverseProperty("IdProfessionalNavigation")]
        public virtual ICollection<ActivityProfessional> ActivityProfessionals { get; set; }
        [InverseProperty("IdProfessionalNavigation")]
        public virtual ICollection<Letter> Letters { get; set; }
        [InverseProperty("IdProfessionalNavigation")]
        public virtual ICollection<Notification> Notifications { get; set; }

    }
}

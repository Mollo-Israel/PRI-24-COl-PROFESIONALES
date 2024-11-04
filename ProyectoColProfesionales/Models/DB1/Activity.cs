using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("Activity")]
    public partial class Activity
    {
        public Activity()
        {
            ActivityProfessionals = new HashSet<ActivityProfessional>();
            ActivityVouchers = new HashSet<ActivityVoucher>();
            Letters = new HashSet<Letter>();
        }

        [Key]
        [Column("idActivity")]
        public int IdActivity { get; set; }
        [Column("description")]
        [StringLength(50)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
        [Column("dateActivity", TypeName = "datetime")]
        public DateTime DateActivity { get; set; }
        [Column("auditorium")]
        [StringLength(50)]
        [Unicode(false)]
        public string Auditorium { get; set; } = null!;
        [Column("place")]
        [StringLength(50)]
        [Unicode(false)]
        public string Place { get; set; } = null!;
        [Column("idThesis")]
        public int IdThesis { get; set; }
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
        [Column("latitude")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Latitude { get; set; }
        [Column("longitude")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Longitude { get; set; }
        [Column("stateActivity")]
        [StringLength(50)]
        [Unicode(false)]
        public string? StateActivity { get; set; }
        [Column("hasAssistance")]
        public bool? HasAssistance { get; set; }
        [Column("hasPayment")]
        public bool? HasPayment { get; set; }

        [ForeignKey("IdThesis")]
        [InverseProperty("Activities")]
        public virtual Thesis IdThesisNavigation { get; set; } = null!;
        [InverseProperty("IdActivityNavigation")]
        public virtual ICollection<ActivityProfessional> ActivityProfessionals { get; set; }
        [InverseProperty("IdActivityNavigation")]
        public virtual ICollection<ActivityVoucher> ActivityVouchers { get; set; }
        [InverseProperty("IdActivityNavigation")]
        public virtual ICollection<Letter> Letters { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("Thesis")]
    public partial class Thesis
    {
        public Thesis()
        {
            Activities = new HashSet<Activity>();
            ThesisFiles = new HashSet<ThesisFile>();
        }

        [Key]
        [Column("idThesis")]
        public int IdThesis { get; set; }
        [Column("type")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Type { get; set; }
        [Column("description")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("student")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Student { get; set; }
        [Column("career")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Career { get; set; }
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

        [InverseProperty("IdThesisNavigation")]
        public virtual ICollection<Activity> Activities { get; set; }
        [InverseProperty("IdThesisNavigation")]
        public virtual ICollection<ThesisFile> ThesisFiles { get; set; }
    }
}

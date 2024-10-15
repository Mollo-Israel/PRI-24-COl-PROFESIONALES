using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("ThesisFile")]
    public partial class ThesisFile
    {
        [Key]
        [Column("idThesisFile")]
        public int IdThesisFile { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ThesisType { get; set; } = null!;
        public byte[] DataFile { get; set; } = null!;
        [Column("nameFile")]
        [StringLength(255)]
        [Unicode(false)]
        public string NameFile { get; set; } = null!;
        [Column("idThesis")]
        public int IdThesis { get; set; }

        [ForeignKey("IdThesis")]
        [InverseProperty("ThesisFiles")]
        public virtual Thesis IdThesisNavigation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("ActivityProfessional")]
    public partial class ActivityProfessional
    {
        [Key]
        [Column("idActivityProfessional")]
        public int IdActivityProfessional { get; set; }
        [Column("idProfessional")]
        public int IdProfessional { get; set; }
        [Column("idActivity")]
        public int IdActivity { get; set; }

        [ForeignKey("IdActivity")]
        [InverseProperty("ActivityProfessionals")]
        public virtual Activity IdActivityNavigation { get; set; } = null!;
        [ForeignKey("IdProfessional")]
        [InverseProperty("ActivityProfessionals")]
        public virtual Professional IdProfessionalNavigation { get; set; } = null!;
    }
}

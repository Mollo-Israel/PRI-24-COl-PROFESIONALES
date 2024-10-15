using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoColProfesionales.Models
{
    public class ActivityProfessional
    {
        [Key]
        public int idActivityProfessional { get; set; }
        public int idProfessional { get; set; }
        public int idActivity { get; set; }
    }
}

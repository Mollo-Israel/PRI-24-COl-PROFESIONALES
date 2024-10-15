using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoColProfesionales.Models
{
    public class Professional
    {
        [Key]
        public int idProfessional { get; set; }
        [Required(ErrorMessage = "La especialidad es obligatorio.")]
        public string specialty { get; set; }
        public string code { get; set; }
        public int idPerson { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public byte status { get; set; }
        public int idUserRegister { get; set; }
        public int idUserLastUpdate { get; set; }
        public string universidad { get; set; }
        public string carrera { get; set; }
    }
}

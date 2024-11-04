using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoColProfesionales.Models
{
    public class Activity1
    {
        [Key]
        public int idActivity { get; set; }
        public string description { get; set; }
        public DateTime dateActivity { get; set; }
        public string auditorium { get; set; }
        public string place { get; set; }
        public int idThesis { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public byte status { get; set; }
        public int idUserRegister { get; set; }
        public int idUserLastUpdate { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string stateActivity { get; set; }
        public bool hasAssistance { get; set; }
        public bool hasPayment { get; set; }
    }
}

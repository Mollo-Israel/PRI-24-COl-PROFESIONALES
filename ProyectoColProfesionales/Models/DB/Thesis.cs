using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class Thesis
    {
        public Thesis()
        {
            Activities = new HashSet<Activity>();
        }

        public int IdThesis { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Student { get; set; }
        public string? Career { get; set; }
        public byte[] ThesisPdf { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public byte Status { get; set; }
        public int? IdUserRegister { get; set; }
        public int? IdUserLastUpdate { get; set; }

        public virtual User? IdUserLastUpdateNavigation { get; set; }
        public virtual User? IdUserRegisterNavigation { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}

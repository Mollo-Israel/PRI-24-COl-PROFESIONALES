using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class Activity
    {
        public Activity()
        {
            ActivityProfessionals = new HashSet<ActivityProfessional>();
            ActivityVouchers = new HashSet<ActivityVoucher>();
            Letters = new HashSet<Letter>();
        }

        public int IdActivity { get; set; }
        public string Description { get; set; } = null!;
        public DateTime DateActivity { get; set; }
        public string Auditorium { get; set; } = null!;
        public string Place { get; set; } = null!;
        public int IdThesis { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public byte Status { get; set; }
        public int? IdUserRegister { get; set; }
        public int? IdUserLastUpdate { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? StateActivity { get; set; }
        public bool? HasAssistance { get; set; }
        public bool? HasPayment { get; set; }

        public virtual Thesis IdThesisNavigation { get; set; } = null!;
        public virtual User? IdUserLastUpdateNavigation { get; set; }
        public virtual User? IdUserRegisterNavigation { get; set; }
        public virtual ICollection<ActivityProfessional> ActivityProfessionals { get; set; }
        public virtual ICollection<ActivityVoucher> ActivityVouchers { get; set; }
        public virtual ICollection<Letter> Letters { get; set; }
    }
}

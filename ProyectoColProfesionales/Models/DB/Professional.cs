using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class Professional
    {
        public Professional()
        {
            ActivityProfessionals = new HashSet<ActivityProfessional>();
            Letters = new HashSet<Letter>();
            Notifications = new HashSet<Notification>();
        }

        public int IdProfessional { get; set; }
        public string Specialty { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int IdPerson { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public byte Status { get; set; }
        public int? IdUserRegister { get; set; }
        public int? IdUserLastUpdate { get; set; }
        public string? Universidad { get; set; }
        public string? Carrera { get; set; }

        public virtual Person IdPersonNavigation { get; set; } = null!;
        public virtual User? IdUserLastUpdateNavigation { get; set; }
        public virtual User? IdUserRegisterNavigation { get; set; }
        public virtual ICollection<ActivityProfessional> ActivityProfessionals { get; set; }
        public virtual ICollection<Letter> Letters { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class Letter
    {
        public Letter()
        {
            Notifications = new HashSet<Notification>();
        }

        public int IdLetter { get; set; }
        public int? IdProfessional { get; set; }
        public string? Format { get; set; }
        public int? IdActivity { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public byte Status { get; set; }
        public int? IdUserRegister { get; set; }
        public int? IdUserLastUpdate { get; set; }

        public virtual Activity? IdActivityNavigation { get; set; }
        public virtual Professional? IdProfessionalNavigation { get; set; }
        public virtual User? IdUserLastUpdateNavigation { get; set; }
        public virtual User? IdUserRegisterNavigation { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}

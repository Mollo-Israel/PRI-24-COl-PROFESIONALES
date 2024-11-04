using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class Notification
    {
        public int IdNotification { get; set; }
        public string? Message { get; set; }
        public DateTime? DateTime { get; set; }
        public int? IdProfessional { get; set; }
        public int? IdLetter { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public byte Status { get; set; }
        public int? IdUserRegister { get; set; }
        public int? IdUserLastUpdate { get; set; }

        public virtual Letter? IdLetterNavigation { get; set; }
        public virtual Professional? IdProfessionalNavigation { get; set; }
        public virtual User? IdUserLastUpdateNavigation { get; set; }
        public virtual User? IdUserRegisterNavigation { get; set; }
    }
}

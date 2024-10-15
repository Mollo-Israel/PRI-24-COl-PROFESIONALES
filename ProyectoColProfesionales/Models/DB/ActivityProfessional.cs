using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class ActivityProfessional
    {
        public int IdActivityProfessional { get; set; }
        public int IdProfessional { get; set; }
        public int IdActivity { get; set; }

        public virtual Activity IdActivityNavigation { get; set; }
        public virtual Professional IdProfessionalNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class Person
    {
        public Person()
        {
            Professionals = new HashSet<Professional>();
            Users = new HashSet<User>();
        }

        public int IdPerson { get; set; }
        public string Names { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? SecondLastName { get; set; }
        public string IdentityNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public byte Status { get; set; }
        public int? IdUserRegister { get; set; }
        public int? IdUserLastUpdate { get; set; }

        public virtual User? IdUserLastUpdateNavigation { get; set; }
        public virtual User? IdUserRegisterNavigation { get; set; }
        public virtual ICollection<Professional> Professionals { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

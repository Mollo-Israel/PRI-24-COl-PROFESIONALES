using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class User
    {
        public User()
        {
            ActivityIdUserLastUpdateNavigations = new HashSet<Activity>();
            ActivityIdUserRegisterNavigations = new HashSet<Activity>();
            LetterIdUserLastUpdateNavigations = new HashSet<Letter>();
            LetterIdUserRegisterNavigations = new HashSet<Letter>();
            NotificationIdUserLastUpdateNavigations = new HashSet<Notification>();
            NotificationIdUserRegisterNavigations = new HashSet<Notification>();
            PersonIdUserLastUpdateNavigations = new HashSet<Person>();
            PersonIdUserRegisterNavigations = new HashSet<Person>();
            ProfessionalIdUserLastUpdateNavigations = new HashSet<Professional>();
            ProfessionalIdUserRegisterNavigations = new HashSet<Professional>();
            ThesisIdUserLastUpdateNavigations = new HashSet<Thesis>();
            ThesisIdUserRegisterNavigations = new HashSet<Thesis>();
        }

        public int IdUser { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int IdPerson { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public byte Status { get; set; }
        public int? IdUserRegister { get; set; }
        public int? IdUserLastUpdate { get; set; }

        public virtual Person IdPersonNavigation { get; set; } = null!;
        public virtual ICollection<Activity> ActivityIdUserLastUpdateNavigations { get; set; }
        public virtual ICollection<Activity> ActivityIdUserRegisterNavigations { get; set; }
        public virtual ICollection<Letter> LetterIdUserLastUpdateNavigations { get; set; }
        public virtual ICollection<Letter> LetterIdUserRegisterNavigations { get; set; }
        public virtual ICollection<Notification> NotificationIdUserLastUpdateNavigations { get; set; }
        public virtual ICollection<Notification> NotificationIdUserRegisterNavigations { get; set; }
        public virtual ICollection<Person> PersonIdUserLastUpdateNavigations { get; set; }
        public virtual ICollection<Person> PersonIdUserRegisterNavigations { get; set; }
        public virtual ICollection<Professional> ProfessionalIdUserLastUpdateNavigations { get; set; }
        public virtual ICollection<Professional> ProfessionalIdUserRegisterNavigations { get; set; }
        public virtual ICollection<Thesis> ThesisIdUserLastUpdateNavigations { get; set; }
        public virtual ICollection<Thesis> ThesisIdUserRegisterNavigations { get; set; }
    }
}

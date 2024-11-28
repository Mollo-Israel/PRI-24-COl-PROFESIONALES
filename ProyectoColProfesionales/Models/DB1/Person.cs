using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("Person")]
    public partial class Person
    {
        public Person()
        {
            Professionals = new HashSet<Professional>();
            Users = new HashSet<User>();
        }

        [Key]
        [Column("idPerson")]
        public int IdPerson { get; set; }
        [Column("names")]
        [StringLength(60)]
        [Unicode(false)]
        public string Names { get; set; } = null!;
        [Column("lastname")]
        [StringLength(35)]
        [Unicode(false)]
        public string Lastname { get; set; } = null!;
        [Column("secondLastName")]
        [StringLength(35)]
        [Unicode(false)]
        public string? SecondLastName { get; set; }
        [Column("identityNumber")]
        [StringLength(15)]
        [Unicode(false)]
        public string IdentityNumber { get; set; } = null!;
        [Column("phoneNumber")]
        [StringLength(20)]
        [Unicode(false)]
        public string PhoneNumber { get; set; } = null!;
        [Column("email")]
        [StringLength(60)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("registerDate", TypeName = "datetime")]
        public DateTime RegisterDate { get; set; }
        [Column("lastUpdate", TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("status")]
        public byte Status { get; set; }
        [Column("idUserRegister")]
        public int? IdUserRegister { get; set; }
        [Column("idUserLastUpdate")]
        public int? IdUserLastUpdate { get; set; }

        [InverseProperty("IdPersonNavigation")]
        public virtual ICollection<Professional> Professionals { get; set; }
        [InverseProperty("IdPersonNavigation")]
        public virtual ICollection<User> Users { get; set; }
        //agregado
        public virtual ICollection<Notification2> Notifications2 { get; set; }




    }
}

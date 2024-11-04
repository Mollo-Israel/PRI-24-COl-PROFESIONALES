using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("User")]
    public partial class User
    {
        [Key]
        [Column("idUser")]
        public int IdUser { get; set; }
        [Column("userName")]
        [StringLength(35)]
        [Unicode(false)]
        public string UserName { get; set; } = null!;
        [Column("password")]
        [StringLength(25)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [Column("role")]
        [StringLength(50)]
        [Unicode(false)]
        public string Role { get; set; } = null!;
        [Column("idPerson")]
        public int IdPerson { get; set; }
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

        [ForeignKey("IdPerson")]
        [InverseProperty("Users")]
        public virtual Person IdPersonNavigation { get; set; } = null!;
    }
}

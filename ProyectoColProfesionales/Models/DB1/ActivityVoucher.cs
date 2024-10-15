using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoColProfesionales.Models.DB1
{
    [Table("ActivityVoucher")]
    public partial class ActivityVoucher
    {
        [Key]
        [Column("idActivityVoucher")]
        public int IdActivityVoucher { get; set; }
        [Column("voucherType")]
        [StringLength(30)]
        [Unicode(false)]
        public string VoucherType { get; set; } = null!;
        [Column("voucherFile")]
        public byte[] VoucherFile { get; set; } = null!;
        [Column("nameFile")]
        [StringLength(255)]
        [Unicode(false)]
        public string NameFile { get; set; } = null!;
        [Column("idActivity")]
        public int IdActivity { get; set; }

        [ForeignKey("IdActivity")]
        [InverseProperty("ActivityVouchers")]
        public virtual Activity IdActivityNavigation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class ActivityVoucher
    {
        public int IdActivityVoucher { get; set; }
        public string VoucherType { get; set; } = null!;
        public byte[] VoucherFile { get; set; } = null!;
        public string NameFile { get; set; } = null!;
        public int IdActivity { get; set; }

        public virtual Activity IdActivityNavigation { get; set; } = null!;
    }
}

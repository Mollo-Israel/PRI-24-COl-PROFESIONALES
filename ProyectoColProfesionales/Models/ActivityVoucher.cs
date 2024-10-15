using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoColProfesionales.Models
{
    public class ActivityVoucher
    {
        [Key]
        public int idActivityVoucher { get; set; }
        public string voucherType { get; set; }
        public byte[] voucherFile { get; set; }
        public string nameFile { get; set; }
        public int idActivity { get; set; }
    }
}

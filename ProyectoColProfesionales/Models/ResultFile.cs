using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoColProfesionales.Models
{
    public class ResultFile
    {
        public string NameFile { get; set; }
        public MemoryStream memoryStream { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}

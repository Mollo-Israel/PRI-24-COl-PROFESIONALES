using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoColProfesionales.Models
{
    interface IWordProcessor
    {
        ResultFile ModelLetter(string path, string filePath, AttributeReplaceLetter attribute);
    }
}

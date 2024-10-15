using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ProyectoColProfesionales.Models
{
    public class ThesisModel 
    {
        public int idThesis { get; set; }
        [Required(ErrorMessage = "El tipo es obligatorio.")]
        public string type { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string description { get; set; }
        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        public string student { get; set; }
        [Required(ErrorMessage = "La carrera es obligatoria.")]
        public string career { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public byte status { get; set; }
        public int idUserRegister { get; set; }
        public int idUserLastUpdate { get; set; }
        [Required(ErrorMessage = "El archivo de tesis es obligatorio.")]
        //[FileType(new string[] { ".pdf", ".doc", ".docx" }, ErrorMessage = "Solo se permiten archivos PDF o Word.")]
        public Microsoft.AspNetCore.Http.IFormFile thesisFile { get; set; }
    }
}

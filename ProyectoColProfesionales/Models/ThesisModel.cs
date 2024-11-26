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
        [StringLength(90, ErrorMessage = "La descripción no puede tener más de 90 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,})(?!\s)[a-zA-Z0-9\sáéíóúÁÉÍÓÚüÜñÑ.,]+(?<!\s)$",
        ErrorMessage = "La descripción no debe comenzar ni terminar con espacios, no debe contener caracteres especiales excepto puntos y debe permitir acentos.")]
        public string description { get; set; }


        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$",
            ErrorMessage = "El nombre del estudiante no debe contener números ni caracteres especiales.")]
        [StringLength(100, ErrorMessage = "El nombre del estudiante no puede tener más de 100 caracteres.")]

        public string student { get; set; }

        [Required(ErrorMessage = "La carrera es obligatoria.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$",
         ErrorMessage = "La carrera no debe contener números ni caracteres especiales.")]
        [StringLength(100, ErrorMessage = "La carrera no puede tener más de 100 caracteres.")]
        public string career { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public byte status { get; set; }
        public int idUserRegister { get; set; }
        public int idUserLastUpdate { get; set; }
        //[FileType(new string[] { ".pdf", ".doc", ".docx" }, ErrorMessage = "Solo se permiten archivos PDF o Word.")]
        [Required(ErrorMessage = "El archivo de tesis es obligatorio.")]
        public Microsoft.AspNetCore.Http.IFormFile? thesisFile { get; set; }

        // Campo adicional para la URL del archivo existente
        public string? thesisFileUrl { get; set; }

    }
}

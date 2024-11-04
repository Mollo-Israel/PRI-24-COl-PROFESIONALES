namespace ProyectoColProfesionales.Models
{
    public class NotificationModel
    {
        public int IdActivity { get; set; } // Propiedad para almacenar el Id de la actividad
        public DateTime DateActivity { get; set; } // Propiedad para almacenar la fecha de la actividad
        public string? Description { get; set; } // Propiedad para la descripción de la actividad    }
        public string? customMessage { get; set; }
        public List<ProyectoColProfesionales.Models.DB1.ActivityProfessional> ActivityProfessionals { get; set; } = new List<ProyectoColProfesionales.Models.DB1.ActivityProfessional>();

    }
}

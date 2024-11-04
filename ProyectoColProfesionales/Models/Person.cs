using System.ComponentModel.DataAnnotations;

namespace ProyectoColProfesionales.Models
{
    public class Person
    {
        [Key]
        public int IdPerson { get; set; }
        public string? Names { get; set; }
        public string? LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? IdentityNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public byte Status { get; set; }
        public int IdUserRegister { get; set; }
        public int IdUserLastUpdate { get; set; }
    }
}

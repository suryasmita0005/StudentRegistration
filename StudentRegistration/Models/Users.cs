using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Models
{
    public class Users
    {
        public string? Id { get; set; } 
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? DisplayName { get; set; } 
        public string? Role { get; set; } 



    }
}

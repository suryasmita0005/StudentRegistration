namespace StudentRegistration.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string UserType { get; set; }


    }
}

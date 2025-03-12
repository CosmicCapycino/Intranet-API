namespace Intranet_API.Models.Data
{
    public class User
    {
        public int Id { get; set; }
        public required string Forename { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string Role { get; set;}
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}

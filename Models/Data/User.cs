namespace Intranet_API.Models.Data
{
    public class User
    {
        public int Id { get; set; }
        public required string Forename { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}

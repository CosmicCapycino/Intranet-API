using System.Text.Json.Serialization;

namespace Intranet_API.Models.Data
{
    public class RefreshToken
    {
        public int Id { get; set; }
        [JsonIgnore] public User User { get; set; } = null;
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
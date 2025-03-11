using Newtonsoft.Json;

namespace Intranet_API.Models.Requests
{
    public class LoginUser
    {
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
    }
}

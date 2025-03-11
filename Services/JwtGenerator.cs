using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Intranet_API.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Intranet_API.Services;

public class JwtGenerator {
    public static string GenerateJwt(Profile userProfile) {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userProfile.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(type: "profile", value: JsonConvert.SerializeObject(userProfile), JsonClaimValueTypes.Json)
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("15b9722f46ab79c34be75be40438bf4f0b80962bb1c9a0921ace4fc561b24740"));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:7198",
            audience: "http://localhost:8080",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
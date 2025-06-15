using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MiraGames.Server.Entities;
using MiraGames.Server.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace MiraGames.Server.Services
{
    public class TokenService(IConfiguration config) : ITokenService<User>
    {
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"])
            );

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(config["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

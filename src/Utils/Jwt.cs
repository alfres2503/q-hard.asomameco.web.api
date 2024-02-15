using Microsoft.IdentityModel.Tokens;
using src.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace src.Utils
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }

        public static async Task<Jwt> Generate(Member member, Jwt jwtConfig)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwtConfig.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", member.Id.ToString()),
                new Claim("email", member.Email),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signIn
            );

            return new Jwt
            {
                Key = jwtConfig.Key,
                Issuer = jwtConfig.Issuer,
                Audience = jwtConfig.Audience,
                Subject = jwtConfig.Subject,
                Expires = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }


        
    }
}

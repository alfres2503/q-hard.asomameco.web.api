using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using src.Models;
using src.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly AppDBContext _context;
        public IConfiguration _configuration;

        public MemberController(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Member.ToListAsync();
        }

        // login method
        [HttpPost]
        [Route("login")]
        public dynamic LogIn([FromBody] Object requestBody)
        {
            var credentials = JsonConvert.DeserializeObject<dynamic>(requestBody.ToString());

            string email = credentials.email;
            string password = credentials.password;

            // get member from database (real world application you should use a service)
            var member = _context.Member.FirstOrDefault(m => m.Email == email && m.Password == password);

            if (member == null)
                return Unauthorized(new {success = false, status = 401, message = "Invalid email or password" });

            if(!member.IsActive)
                return Unauthorized(new { success = false, status = 401, message = "Inactive member" });

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", member.Id.ToString()),
                new Claim("email", member.Email),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signIn
            );

            return Ok(new
            {
                success = true,
                status = 200,
                message = "Login successful",
                data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    member_id = member.Id
                }
            });
        }
    }
}

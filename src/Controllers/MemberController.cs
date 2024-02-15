using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using src.Models;
using src.Services;
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
        public IConfiguration _configuration;
        private readonly IMemberService _memberService;

        public MemberController(AppDBContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _memberService = new MemberService(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            try
            {
                return Ok(await _memberService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            Member member = null;
            try
            {
                member = await _memberService.GetByID(id);

                if (member == null)
                    return NotFound(new { success = false, status = 404, message = "User not found" });

                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<dynamic> LogIn([FromBody] Object requestBody)
        {
            var credentials = JsonConvert.DeserializeObject<dynamic>(requestBody.ToString());

            string email = credentials.email;
            string password = credentials.password;

            // get member from service
            Member member = await _memberService.GetByEmail(email);

            if (member == null)
                return Unauthorized(new { success = false, status = 401, message = "Invalid email or password" });

            if (!member.IsActive)
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        public IMemberService _memberService;
        private IAuthService _authService;

        public AuthController(IMemberService memberService, IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _memberService = memberService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Object requestBody)
        {
            try
            {
                var credentials = JsonConvert.DeserializeObject<dynamic>(requestBody.ToString());

                string email = credentials.email;
                string password = credentials.password;

                Member member = await _memberService.GetByEmail(email);
                
                // Validations 
                if (member == null)
                    return Unauthorized(new { success = false, status = 401, message = "Email not found" });

                if (!member.IsActive)
                    return Unauthorized(new { success = false, status = 401, message = "Inactive member" });

                bool isAuth = await _authService.Authenticate(member, password);

                if (!isAuth)
                    return Unauthorized(new { success = false, status = 401, message = "Invalid password" });

                // Generate token
                var jwtConfig = _configuration.GetSection("Jwt").Get<Jwt>();
                var token = await Jwt.Generate(member, jwtConfig);

                return Ok(new
                {
                    success = true,
                    status = 200,
                    message = "Login successful",
                    data = new
                    {
                        token = token.Token,
                        expiration = token.Expires,
                        member_id = member.Id
                    }
                });
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, status = 500, message = e.Message });
            }
            
        }
    }
}

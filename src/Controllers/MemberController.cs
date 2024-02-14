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
        private readonly AppDBContext _context;

        public MemberController(AppDBContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _memberService = new MemberService(context);
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        { 
            return Ok(await _memberService.GetAllAsync());
            //return await _context.Member.ToListAsync();
        }
    }
}

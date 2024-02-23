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
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // 404 is only for when there is no such endpoint or resource, not for when there is no data
        // 204 is the correct status code for when there is no data 
        // to return 204, use NoContent() instead of Ok(null)

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _memberService.GetAll(pageNumber, pageSize).ConfigureAwait(false);
                return response != null ? Ok(response) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            try
            {
                if(id <= 0)
                    return BadRequest(new { success = false, status = 400, message = "Invalid ID" });

                var response = await _memberService.GetByID(id).ConfigureAwait(false);

                return response != null ? Ok(response) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember([FromBody] Member member)
        {
            try
            {
                if (member == null)
                    return BadRequest(new { success = false, status = 400, message = "Invalid member" });

                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, status = 400, message = "Invalid member" });

                var response = await _memberService.Create(member).ConfigureAwait(false);

                return CreatedAtRoute(nameof(GetMemberById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

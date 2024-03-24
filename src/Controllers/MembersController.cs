using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Services;
using src.Utils;

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
        public async Task<ActionResult<PagedResult<Member>>> GetMembers(int pageNumber = 1, int pageSize = 10, string searchTerm = null, string orderBy = null)
        {
            try
            {
                var list = await _memberService.GetAll(pageNumber, pageSize, searchTerm, orderBy).ConfigureAwait(false);

                if(list == null)
                    return NoContent();

                var total = await _memberService.GetCount(searchTerm).ConfigureAwait(false);
                var totalPages = (int)Math.Ceiling((double)total / (double)pageSize);

                var result = new PagedResult<Member>
                {
                    List = list,
                    TotalPages = totalPages,
                    TotalRecords = total
                };

                return Ok(result);
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
                if (id <= 0)
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

                var memberByEmail = await _memberService.GetByEmail(member.Email).ConfigureAwait(false);

                if (memberByEmail != null)
                    return BadRequest(new { success = false, status = 400, message = "Email already exists" });

                var memberByIDCard = await _memberService.GetByIdCard(member.IdCard).ConfigureAwait(false);

                if (memberByIDCard != null)
                    return BadRequest(new { success = false, status = 400, message = "ID Card already exists" });

                var response = await _memberService.Create(member).ConfigureAwait(false);

                return response != null ? CreatedAtAction(nameof(GetMemberById), new { id = response.Id }, response) : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create member");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Member>> UpdateMember(int id, [FromBody] Member _member)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { success = false, status = 400, message = "Invalid ID" });

                if (_member == null)
                    return BadRequest(new { success = false, status = 400, message = "Invalid member" });

                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, status = 400, message = "Invalid member" });

                var memberByEmail = await _memberService.GetByEmail(_member.Email).ConfigureAwait(false);

                if (memberByEmail != null && memberByEmail.Id != _member.Id)
                    return BadRequest(new { success = false, status = 400, message = "Email already exists" });

                var memberByIDCard = await _memberService.GetByIdCard(_member.IdCard).ConfigureAwait(false);

                if (memberByIDCard != null && memberByIDCard.Id != _member.Id)
                    return BadRequest(new { success = false, status = 400, message = "ID Card already exists" });

                var response = await _memberService.Update(id, _member).ConfigureAwait(false);

                return response != null ? AcceptedAtAction(nameof(GetMemberById), new { id = response.Id }, response) : StatusCode(StatusCodes.Status500InternalServerError, "Failed to update member");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("state/{id}")]
        public async Task<ActionResult<Member>> ChangeState(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { success = false, status = 400, message = "Invalid ID" });

                var response = await _memberService.ChangeState(id).ConfigureAwait(false);

                return response != null ? AcceptedAtAction(nameof(GetMemberById), new { id = response.Id }, response) : StatusCode(StatusCodes.Status500InternalServerError, "Failed to change state");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Services;
using src.Utils;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // 404 is only for when there is no such endpoint or resource, not for when there is no data
        // 204 is the correct status code for when there is no data 
        // to return 204, use NoContent() instead of Ok(null)

        [HttpGet("event/{id}")]
        public async Task<ActionResult<Attendance>> GetAttendanceByIdEvent(int id, int pageNumber = 1, int pageSize = 10, string searchTerm = null, string orderBy = null)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { success = false, status = 400, message = "Invalid Event ID" });

                var response = await _attendanceService.GetByIdEvent(id,pageNumber,pageSize,searchTerm, orderBy).ConfigureAwait(false);

                return response != null ? Ok(response) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private async Task<ActionResult<Attendance>> GetByIdEventIdAssociate(int idEvent, int idAssociate)
        {
            try
            {
                if (idEvent <= 0 && idAssociate <= 0)
                    return BadRequest(new { success = false, status = 400, message = "Invalid Associate ID" });

                var response = await _attendanceService.GetByIdEventIdAssociate(idEvent,idAssociate).ConfigureAwait(false);

                return response != null ? Ok(response) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Attendance>> CreateAttendance([FromBody] Attendance attendance)
        {
            try
            {
                if (attendance == null)
                    return BadRequest(new { success = false, status = 400, message = "Invalid attendance" });

                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, status = 400, message = "Invalid event" });

                var attendanceByEventIdAssociate = await _attendanceService.GetByIdEventIdAssociate(attendance.IdEvent, attendance.IdAssociate).ConfigureAwait(false);

                if (attendanceByEventIdAssociate != null)
                    return BadRequest(new { success = false, status = 400, message = "Attendace already exists" });

                var response = await _attendanceService.Create(attendance).ConfigureAwait(false);

                return CreatedAtAction(nameof(GetByIdEventIdAssociate), new { idEvent = response.IdEvent, idAssociate = response.IdAssociate }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}

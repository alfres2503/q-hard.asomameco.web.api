using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using src.Models.Reports;
using src.Services;
using src.Utils;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // 404 is only for when there is no such endpoint or resource, not for when there is no data
        // 204 is the correct status code for when there is no data 
        // to return 204, use NoContent() instead of Ok(null)

        [HttpGet]
        public async Task<ActionResult<AttendancePercentageReport>> GetAttendancePercentageData()
        {
            try
            {
                var response = await _reportService.GetAttendancePercentageData().ConfigureAwait(false);
                return response != null ? Ok(response) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

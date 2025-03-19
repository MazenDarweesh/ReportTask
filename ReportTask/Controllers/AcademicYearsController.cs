using Application.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReportTask.Controllers
{
    [ApiController]
    [Route("api/academicyears")]
    public class AcademicYearsController : ControllerBase
    {
        private readonly IAcademicYearService _academicYearService;

        public AcademicYearsController(IAcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }

        [HttpGet("{schoolId}")]
        public async Task<IActionResult> GetAcademicYears(string schoolId)
        {
            var academicYears = await _academicYearService.GetAcademicYearsAsync(schoolId);
            return Ok(academicYears);
        }
    }
}

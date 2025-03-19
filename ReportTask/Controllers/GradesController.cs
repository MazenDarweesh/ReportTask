using Application.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReportTask.Controllers
{
    [ApiController]
    [Route("api/grades")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var grades = await _gradeService.GetGradesAsync();
            return Ok(grades);
        }
    }
}

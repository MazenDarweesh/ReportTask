using Application.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReportTask.Controllers
{
    [ApiController]
    [Route("api/classrooms")]
    public class ClassroomsController : ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomsController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClassrooms([FromQuery] string gradeId, [FromQuery] string academicYearId)
        {
            var classrooms = await _classroomService.GetClassroomsAsync(gradeId, academicYearId);
            return Ok(classrooms);
        }
    }
}

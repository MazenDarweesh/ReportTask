using Application.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReportTask.Controllers
{
    [ApiController]
    [Route("api/schools")]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            var schools = await _schoolService.GetSchoolsAsync();
            return Ok(schools);
        }
    }
}

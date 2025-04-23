using Ensek_Remote_Technical_Test.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ensek_Remote_Technical_Test.Controllers
{
    [ApiController]
    [Route("api/meter-reading-uploads")]
    public class MeterReadingUploadController : ControllerBase
    {
        private readonly IMeterReadingUploadService _uploadService;

        public MeterReadingUploadController(IMeterReadingUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                var result = await _uploadService.ProcessMeterReadingsCsvAsync(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Upload controller is reachable");
        }
    }
}

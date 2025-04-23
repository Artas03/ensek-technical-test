using Ensek_Remote_Technical_Test.DTO;

namespace Ensek_Remote_Technical_Test.Services
{
    public interface IMeterReadingUploadService
    {
        Task<MeterReadingUploadResultDto> ProcessMeterReadingsCsvAsync(IFormFile file);
    }
}

using Ensek_Remote_Technical_Test.Models;

namespace Ensek_Remote_Technical_Test.Repositories
{
    public interface IMeterReadingRepository
    {
        Task<List<int>> GetValidAccountIdsAsync();
        Task<bool> IsDuplicateReadingAsync(int accountId, DateTime readingDateTime);
        Task AddMeterReadingsAsync(List<MeterReading> readings);
    }
        
}

using Ensek_Remote_Technical_Test.Data;
using Ensek_Remote_Technical_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Ensek_Remote_Technical_Test.Repositories
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly AppDbContext _context;

        public MeterReadingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetValidAccountIdsAsync()
        {
            return await _context.Accounts.Select(a => a.AccountId).ToListAsync();
        }

        public async Task<bool> IsDuplicateReadingAsync(int accountId, DateTime readingDateTime)
        {
            return await _context.MeterReadings.AnyAsync(m => m.AccountId == accountId && m.MeterReadingDateTime == readingDateTime);
        }

        public async Task AddMeterReadingsAsync(List<MeterReading> readings)
        {
            await _context.MeterReadings.AddRangeAsync(readings);
            await _context.SaveChangesAsync();
        }
    }
}

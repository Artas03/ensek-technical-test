using CsvHelper;
using CsvHelper.Configuration;
using Ensek_Remote_Technical_Test.DTO;
using Ensek_Remote_Technical_Test.Models;
using Ensek_Remote_Technical_Test.Repositories;
using System.Globalization;

namespace Ensek_Remote_Technical_Test.Services
{
    public class MeterReadingUploadService : IMeterReadingUploadService
    {
        private readonly IMeterReadingRepository _repository;

        public MeterReadingUploadService(IMeterReadingRepository repository)
        {
            _repository = repository;
        }

        public async Task<MeterReadingUploadResultDto> ProcessMeterReadingsCsvAsync(IFormFile file)
        {
            var successCount = 0;
            var failureCount = 0;
            var validReadings = new List<MeterReading>();

            var validAccountIds = await _repository.GetValidAccountIdsAsync();

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var rows = csv.GetRecords<dynamic>();

            foreach (var row in rows)
            {
                try
                {
                    int accountId = int.Parse(row.AccountId);
                    DateTime readingDateTime = DateTime.Parse(row.MeterReadingDateTime);
                    string readingValue = row.MeterReadValue;

                    bool isValidAccount = validAccountIds.Contains(accountId);
                    bool isCorrectFormat = System.Text.RegularExpressions.Regex.IsMatch(readingValue, @"^\d{3,5}$");
                    bool isDuplicate = await _repository.IsDuplicateReadingAsync(accountId, readingDateTime);

                    Console.WriteLine($"Row -> AccountId: {accountId}, DateTime: {readingDateTime}, Value: {readingValue}");
                    Console.WriteLine($"ValidAccount: {isValidAccount}, FormatValid: {isCorrectFormat}, Duplicate: {isDuplicate}");

                    if (isValidAccount && isCorrectFormat && !isDuplicate)
                    {
                        validReadings.Add(new MeterReading
                        {
                            AccountId = accountId,
                            MeterReadingDateTime = readingDateTime,
                            MeterReadValue = readingValue
                        });

                        successCount++;
                    }
                    else
                    {
                        failureCount++;
                    }
                }
                catch
                {
                    failureCount++;
                }
            }

            await _repository.AddMeterReadingsAsync(validReadings);

            return new MeterReadingUploadResultDto
            {
                SuccessCount = successCount,
                FailureCount = failureCount
            };
        }
    }
}

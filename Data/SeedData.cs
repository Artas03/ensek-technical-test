using CsvHelper;
using CsvHelper.Configuration;
using Ensek_Remote_Technical_Test.Models;
using System.Globalization;

namespace Ensek_Remote_Technical_Test.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context, string csvFilePath)
        {
            if (context.Accounts.Any())
            {
                return; //Already seeded
            }

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var records = csv.GetRecords<Account>().ToList();
            context.Accounts.AddRange(records);
            context.SaveChanges();
        }
    }
}

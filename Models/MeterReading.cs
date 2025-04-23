using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ensek_Remote_Technical_Test.Models
{
    public class MeterReading
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [Required]
        public DateTime MeterReadingDateTime { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Reading value must be exactly 5 digits")]
        public string MeterReadValue { get; set; }
    }
}

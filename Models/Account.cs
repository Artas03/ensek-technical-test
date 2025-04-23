using System.ComponentModel.DataAnnotations;

namespace Ensek_Remote_Technical_Test.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}

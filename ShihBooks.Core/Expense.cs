using SQLite;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.Core
{
    // All the code in this file is included in all platforms.
    [Table("Expenses")]
    public class Expense
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        public int? MerchantId { get; set; }

        public int? ExpenseTypeId { get; set; }

        public int? TagId { get; set; }

        public string Note { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
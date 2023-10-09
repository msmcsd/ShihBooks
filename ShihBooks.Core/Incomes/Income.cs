using SQLite;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.Core.Incomes
{
    [Table("Incomes")]
    public class Income
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public int SourceId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime IncomeDate { get; set; }
    }
}

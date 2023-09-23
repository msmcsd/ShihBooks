using SQLite;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.Core
{
    [Table("ExpenseEvents")]
    public class ExpenseEvent
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        [Unique]
        public string Name { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}

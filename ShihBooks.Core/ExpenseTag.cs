using SQLite;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.Core
{
    [Table("ExpenseTags")]
    public class ExpenseTag
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

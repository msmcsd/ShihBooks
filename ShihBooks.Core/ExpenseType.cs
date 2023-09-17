using SQLite;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.Core
{
    [Table("ExpenseTypes")]
    public class ExpenseType
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

using SQLite;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.Core
{
    [Table("Merchants")]
    public class Merchant
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}

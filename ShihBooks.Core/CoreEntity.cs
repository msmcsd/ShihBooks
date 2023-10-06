using SQLite;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.Core
{
    public class CoreEntity
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

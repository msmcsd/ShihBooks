using System.ComponentModel.DataAnnotations;

namespace ShihBooks.WebApi.Models
{
    public class CoreEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}

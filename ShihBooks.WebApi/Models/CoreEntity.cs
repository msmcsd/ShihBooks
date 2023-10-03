using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.WebApi.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class CoreEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}

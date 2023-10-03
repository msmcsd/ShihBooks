using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShihBooks.WebApi.Models
{
    public class Expense
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        public double Amount { get; set; }

        public int? MerchantId { get; set; }

        public int? ExpenseTypeId { get; set; }

        public int? TagId { get; set; }

        public int? EventId { get; set; }

        [StringLength(50)]
        public string? Note { get; set; }

        [Required]
        [StringLength(25)]
        public string Description { get; set; } = string.Empty;
    }
}

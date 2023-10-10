using System.ComponentModel.DataAnnotations.Schema;

namespace ShihBooks.Core
{
    [Table("IncomeSources")]
    public class IncomeSource : CoreEntity
    {
        public string ImageUrl { get; set; }
    }
}

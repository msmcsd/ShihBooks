namespace ShihBooks.Core
{
    // All the code in this file is included in all platforms.
    public class Expense
    {
        public int Id { get; set; }

        public DateTime ExpenseDate { get; set; }

        public string ExpenseType { get; set; }

        public string Merchant { get; set; }

        public int MerchantId { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public string MerchantImageUrl { get; set; }
    }
}
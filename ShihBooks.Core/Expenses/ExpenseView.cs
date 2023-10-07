using ShihBooks.Core.Expenses;

namespace ShihBooks.Core
{
    /// <summary>
    /// This class contains all columns from Expense plus additional columns from
    /// joining other tables. This is the model used to display on UI.
    /// </summary>
    public class ExpenseView : Expense
    {
        public string MerchantImageUrl { get; set; }
    }
}

using ShihBooks.Core;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class ExpenseSourceInMemory : IExpenseSource
    {
        private List<Expense> _expenses;
        public ExpenseSourceInMemory()
        {
            _expenses = new List<Expense>()
            {
                new Expense()
                {
                    Id = 1,
                    Description = "Grocery",
                    Amount = 100.23,
                    ExpenseDate = new DateTime(2023, 3, 4),
                    MerchaneImageUrl="https://www.costco.com/wcsstore/CostcoUSBCCatalogAssetStore/feature-pages/16w0126-media-request-logo.jpg",
                    Merchant = "Costco"
                },
                new Expense()
                {
                    Id = 1,
                    Description = "Grocery",
                    Amount = 51.34,
                    ExpenseDate = new DateTime(2023, 8, 4),
                    MerchaneImageUrl="https://www.costco.com/wcsstore/CostcoUSBCCatalogAssetStore/feature-pages/16w0126-media-request-logo.jpg",
                    Merchant = "Costco"
                },
                new Expense()
                {
                    Id = 1,
                    Description = "Grocery",
                    Amount = 12.99,
                    ExpenseDate = new DateTime(2023, 8, 1),
                    MerchaneImageUrl="https://www.costco.com/wcsstore/CostcoUSBCCatalogAssetStore/feature-pages/16w0126-media-request-logo.jpg",
                    Merchant = "Costco"
                },
            };
        }

        public Task<List<Expense>> GetExpenses(int year, int month)
        {
            var expenses = new List<Expense>();

            if (year < 0 || year > 9999) return Task.FromResult(expenses);
            if (month <= 0 || month > 12) return Task.FromResult(expenses);

            expenses = _expenses.Where(e => e.ExpenseDate.Year == year && e.ExpenseDate.Month == month).ToList();

            return Task.FromResult<List<Expense>>(expenses);
        }
    }
}
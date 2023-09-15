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
                    Note = "Meals",
                    Amount = 100.23,
                    ExpenseDate = new DateTime(2023, 3, 4),
                    MerchantImageUrl="https://www.costco.com/wcsstore/CostcoUSBCCatalogAssetStore/feature-pages/16w0126-media-request-logo.jpg",
                    Merchant = "Costco",
                    MerchantId = 1,
                    ExpenseType = "Grocery",
                    CategoryId = 1,
                },
                new Expense()
                {
                    Id = 1,
                    Note = "Gaming laptop",
                    Amount = 1251.34,
                    ExpenseDate = new DateTime(2023, DateTime.Now.Month, 4),
                    MerchantImageUrl="https://www.costco.com/wcsstore/CostcoUSBCCatalogAssetStore/feature-pages/16w0126-media-request-logo.jpg",
                    Merchant = "Amazon",
                    MerchantId = 2,
                    ExpenseType = "Electronics",
                    CategoryId = 2,
                },
                new Expense()
                {
                    Id = 1,
                    Note = "Daily shopping",
                    Amount = 12.99,
                    ExpenseDate = new DateTime(2023, DateTime.Now.Month, 1),
                    MerchantImageUrl="https://www.costco.com/wcsstore/CostcoUSBCCatalogAssetStore/feature-pages/16w0126-media-request-logo.jpg",
                    Merchant = "Costco",
                    MerchantId = 1,
                    ExpenseType="Grocery",
                    CategoryId = 1
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
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
                    Description = "Meals",
                    Amount = 100.23,
                    ExpenseDate = new DateTime(2023, 3, 4),
                    MerchantImageUrl="https://play-lh.googleusercontent.com/gqOziTbVWioRJtHh7OvfOq07NCTcAHKWBYPQKJOZqNcczpOz5hdrnQNY7i2OatJxmuY=w240-h480-rw",
                    MerchantId = 1,
                    ExpenseTypeId = 1,
                },
                new Expense()
                {
                    Id = 2,
                    Description = "Gaming laptop from Amazon",
                    Amount = 1251.34,
                    ExpenseDate = new DateTime(2023, DateTime.Now.Month, 4),
                    MerchantImageUrl="https://www.amazon.com/favicon.ico",
                    MerchantId = 2,
                    ExpenseTypeId = 2,
                    Note = "For gaming"
                },
                new Expense()
                {
                    Id = 3,
                    Description = "Daily shopping",
                    Amount = 12.99,
                    ExpenseDate = new DateTime(2023, DateTime.Now.Month, 1),
                    MerchantImageUrl="https://play-lh.googleusercontent.com/gqOziTbVWioRJtHh7OvfOq07NCTcAHKWBYPQKJOZqNcczpOz5hdrnQNY7i2OatJxmuY=w240-h480-rw",
                    MerchantId = 1,
                    ExpenseTypeId = 1
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

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
           return await Task.FromResult(new List<ExpenseTag>()
           {
               new ExpenseTag {Id = 1, TagName = "Kids"},
               new ExpenseTag {Id = 2, TagName = "One Time"}
           });
        }

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await Task.FromResult(new List<ExpenseType>()
            {
                new ExpenseType { Id = 1, Name = "Grocery" },
                new ExpenseType { Id = 2, Name = "Electronics"}
            });
        }

        public Task<List<Merchant>> GetMerchantsAsync()
        {
            return Task.FromResult(new List<Merchant>()
            {
                new Merchant
                {
                    Id = 1,
                    Name = "Costco"
                },
                new Merchant
                {
                    Id = 2,
                    Name = "Amazon"
                }
            });
        }
    }
}
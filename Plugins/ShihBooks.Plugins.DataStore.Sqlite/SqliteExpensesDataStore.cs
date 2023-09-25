using ShihBooks.Core;
using ShihBooks.UseCases.PluginInterfaces;
using SQLite;

namespace ShihBooks.Plugins.DataStore.Sqlite
{
    // All the code in this file is included in all platforms.
    public class SqliteExpensesDataStore : IExpensesDataStore
    {
        private SQLiteAsyncConnection _db;

        public SqliteExpensesDataStore()
        {
            var dbExists = File.Exists(Constants.DatabasePath);
            _db = new SQLiteAsyncConnection(Constants.DatabasePath);

            _db.CreateTableAsync<ExpenseTag>();
            _db.CreateTableAsync<Merchant>();
            _db.CreateTableAsync<ExpenseType>();
            _db.CreateTableAsync<Expense>();
            _db.CreateTableAsync<ExpenseEvent>();

            if (!dbExists)
                InsertSampleExpenses();
        }

        private void InsertSampleExpenses()
        {
            _db.InsertAllAsync(new List<ExpenseTag>()
            {
                new ExpenseTag {Id = 1, Name = "Kids", DateAdded = DateTime.Now},
                new ExpenseTag {Id = 2, Name = "One Time", DateAdded = DateTime.Now}
            });
                      
            _db.InsertAllAsync(new List<ExpenseEvent>()
            {
                new ExpenseEvent {Id = 1, Name = "Travel"},
                new ExpenseEvent {Id = 2, Name = "New semester"}
            });

            _db.InsertAllAsync(new List<ExpenseType>()
            {
                new ExpenseType { Id = 1, Name = "Grocery", DateAdded = DateTime.Now },
                new ExpenseType { Id = 2, Name = "Electronics", DateAdded = DateTime.Now}
            });

            _db.InsertAllAsync(new List<Merchant>()
            {
                new Merchant { Id = 1, Name = "Costco", ImageUrl = "https://play-lh.googleusercontent.com/gqOziTbVWioRJtHh7OvfOq07NCTcAHKWBYPQKJOZqNcczpOz5hdrnQNY7i2OatJxmuY=w240-h480-rw", DateAdded = DateTime.Now},
                new Merchant { Id = 2, Name = "Amazon", ImageUrl = "https://www.amazon.com/favicon.ico", DateAdded = DateTime.Now}
            });

            _db.InsertAllAsync(new List<Expense>()
            {
                new Expense()
                {
                    Id = 1,
                    Description = "Meals",
                    Amount = 100.23,
                    ExpenseDate = new DateTime(2023, 3, 4),
                    MerchantId = 1,
                    ExpenseTypeId = 1,
                },
                new Expense()
                {
                    Id = 2,
                    Description = "Gaming laptop from Amazon",
                    Amount = 1251.34,
                    ExpenseDate = new DateTime(2023, DateTime.Now.Month, 4),
                    MerchantId = 2,
                    ExpenseTypeId = 2,
                    Note = "For gaming",
                    TagId = 1,
                    EventId = 2
                },
                new Expense()
                {
                    Id = 3,
                    Description = "Daily shopping",
                    Amount = 12.99,
                    ExpenseDate = new DateTime(2023, DateTime.Now.Month, 1),
                    MerchantId = 1,
                    ExpenseTypeId = 1
                },
            });

        }

        public async Task<List<ExpenseView>> GetExpenses(int year, int month)
        {
            var expenses = (from e in await _db.Table<Expense>().ToListAsync()
                            join m in await _db.Table<Merchant>().ToListAsync()
                            on e.MerchantId equals m.Id
                            where e.ExpenseDate.Year == year && e.ExpenseDate.Month == month
                            select new ExpenseView
                            {
                                Id = e.Id,
                                Description = e.Description,
                                Amount = e.Amount,
                                ExpenseDate = e.ExpenseDate,
                                MerchantId = e.MerchantId,
                                ExpenseTypeId = e.ExpenseTypeId,
                                MerchantImageUrl = m.ImageUrl,
                                TagId = e.TagId,
                                EventId = e.EventId,
                                Note = e.Note
                            }).ToList();
            
            return expenses;
        }

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return await _db.Table<ExpenseTag>().ToListAsync();
        }

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await _db.Table<ExpenseType>().ToListAsync();
        }

        public async Task<List<Merchant>> GetMerchantsAsync()
        {
            return await _db.Table<Merchant>().ToListAsync();
        }

        public Task SavExpenseTag(string tagName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateExpenseTag(int tagId, string tagName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateExpense(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExpenseEvent>> GetExpenseEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteExpense(int expenseId)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteExpenseTag(int tagId)
        {
            throw new NotImplementedException();
        }
    }
}
using ShihBooks.Core;
using ShihBooks.Core.Expenses;
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
            if (dbExists)
            {
                //File.Delete(Constants.DatabasePath);
            }

            _db = new SQLiteAsyncConnection(Constants.DatabasePath);

            _db.CreateTableAsync<Expense>();
            _db.CreateTableAsync<ExpenseTag>();
            _db.CreateTableAsync<Merchant>();
            _db.CreateTableAsync<ExpenseType>();
            _db.CreateTableAsync<ExpenseEvent>();
            _db.CreateTableAsync<IncomeSource>();

            //InsertSampleExpenses();
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

        #region Expense

        public async Task<List<ExpenseView>> GetExpensesAsync(int year, int month)
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
                            }).OrderByDescending(e => e.ExpenseDate).ToList();
            
            return expenses;
        }

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            if (expense == null) return false;

            await _db.InsertAsync(expense);

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(Expense expense)
        {
            await _db.UpdateAsync(expense);

            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId)
        {
            await _db.DeleteAsync(new Expense
            {
                Id = expenseId,
            });

            return true;
        }

        #endregion

        #region Merchant

        public async Task<List<Merchant>> GetMerchantsAsync()
        {
            return await _db.Table<Merchant>().OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<bool> AddMerchantAsync(string merchantName, string imageUrl)
        {
            await _db.InsertAsync(new Merchant
            {
                Name = merchantName,
                ImageUrl = imageUrl
            });

            return true;
        }

        public async Task<bool> UpdateMerchantAsync(int id, string merchantName, string imageUrl)
        {
            await _db.UpdateAsync(new Merchant
            {
                Id = id,
                Name = merchantName,
                ImageUrl = imageUrl
            });

            return true;
        }

        public async Task<int> DeleteMerchantAsync(int id)
        {
            await _db.DeleteAsync(new Merchant
            {
                Id = id,
            });

            return id;
        }

        #endregion

        #region Expense Tag

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return await _db.Table<ExpenseTag>().OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<bool> AddExpenseTagAsync(string tagName)
        {
            await _db.InsertAsync(new ExpenseTag
            {
                Name = tagName,
            });

            return true;
        }

        public async Task<bool> UpdateExpenseTagAsync(int tagId, string tagName)
        {
            await _db.UpdateAsync(new ExpenseTag
            {
                Id = tagId,
                Name = tagName,
            });

            return true;
        }

        public async Task<int> DeleteExpenseTagAsync(int tagId)
        {
            await _db.DeleteAsync(new ExpenseTag
            {
                Id = tagId,
            });

            return tagId;
        }

        #endregion

        #region Expense Type

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await _db.Table<ExpenseType>().OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<bool> AddExpenseTypeAsync(string name)
        {
            await _db.InsertAsync(new ExpenseType
            {
                Name = name,
            });

            return true;
        }

        public async Task<bool> UpdateExpenseTypeAsync(int id, string name)
        {
            await _db.UpdateAsync(new ExpenseType
            {
                Id = id,
                Name = name,
            });

            return true;
        }

        public async Task<int> DeleteExpenseTypeAsync(int id)
        {
            await _db.DeleteAsync(new ExpenseType
            {
                Id = id,
            });

            return id;
        }

        #endregion

        #region Expense Event

        public async Task<List<ExpenseEvent>> GetExpenseEventsAsync()
        {
            return await _db.Table<ExpenseEvent>().OrderBy(e => e.Name).ToListAsync();
        }

        public async Task<bool> AddExpenseEventAsync(string eventName)
        {
            await _db.InsertAsync(new ExpenseEvent
            {
                Name = eventName,
            });

            return true;
        }

        public async Task<bool> UpdateExpenseEventAsync(int id, string newEventName)
        {
            await _db.UpdateAsync(new ExpenseEvent
            {
                Id = id,
                Name = newEventName,
            });

            return true;
        }

        public async Task<int> DeleteExpenseEventAsync(int id)
        {
            await _db.DeleteAsync(new ExpenseEvent
            {
                Id = id,
            });

            return id;
        }

        #endregion

        #region Income Source

        public async Task<List<IncomeSource>> GetIncomeSourcesAsync()
        {
            return await _db.Table<IncomeSource>().OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<bool> AddIncomeSourceAsync(string sourceName)
        {
            await _db.InsertAsync(new IncomeSource
            {
                Name = sourceName,
            });

            return true;
        }

        public async Task<bool> UpdateIncomeSourceAsync(int id, string newSourceName)
        {
            await _db.UpdateAsync(new IncomeSource
            {
                Id = id,
                Name = newSourceName,
            });

            return true;
        }
 
        public async Task<int> DeleteIncomeSourceAsync(int id)
        {
            await _db.DeleteAsync(new IncomeSource
            {
                Id = id,
            });

            return id;
        }

        #endregion
    }
}
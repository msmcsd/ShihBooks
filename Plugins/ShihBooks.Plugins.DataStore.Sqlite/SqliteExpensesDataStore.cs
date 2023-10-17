using ShihBooks.Core;
using ShihBooks.Core.Expenses;
using ShihBooks.Core.Incomes;
using ShihBooks.Core.StatusResponses;
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

            _db.CreateTableAsync<Income>();
            _db.CreateTableAsync<IncomeSource>();
            _db.CreateTableAsync<IncomeRecipient>();

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

        public async Task<StatusResponse> AddExpenseAsync(Expense expense)
        {
            if (expense == null) 
                return new StatusResponse(StatusCode.InvalidEntity);

            await _db.InsertAsync(expense);

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateExpenseAsync(Expense expense)
        {
            if (expense == null)
                return new StatusResponse(StatusCode.InvalidEntity);

            await _db.UpdateAsync(expense);

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteExpenseAsync(int expenseId)
        {
            await _db.DeleteAsync(new Expense
            {
                Id = expenseId,
            });

            return new StatusResponse(StatusCode.Success);
        }

        #endregion

        #region Merchant

        public async Task<List<Merchant>> GetMerchantsAsync()
        {
            return await _db.Table<Merchant>().OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<StatusResponse> AddMerchantAsync(string merchantName, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(merchantName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.InsertAsync(new Merchant
            {
                Name = merchantName,
                ImageUrl = imageUrl
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateMerchantAsync(int id, string merchantName, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(merchantName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.UpdateAsync(new Merchant
            {
                Id = id,
                Name = merchantName,
                ImageUrl = imageUrl
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteMerchantAsync(int id)
        {
            await _db.DeleteAsync(new Merchant
            {
                Id = id,
            });

            return new StatusResponse(StatusCode.Success);
        }

        #endregion

        #region Expense Tag

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return await _db.Table<ExpenseTag>().OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<StatusResponse> AddExpenseTagAsync(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.InsertAsync(new ExpenseTag
            {
                Name = tagName,
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateExpenseTagAsync(int tagId, string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.UpdateAsync(new ExpenseTag
            {
                Id = tagId,
                Name = tagName,
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteExpenseTagAsync(int tagId)
        {
            await _db.DeleteAsync(new ExpenseTag
            {
                Id = tagId,
            });

            return new StatusResponse(StatusCode.Success);
        }

        #endregion

        #region Expense Type

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await _db.Table<ExpenseType>().OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<StatusResponse> AddExpenseTypeAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.InsertAsync(new ExpenseType
            {
                Name = name,
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateExpenseTypeAsync(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.UpdateAsync(new ExpenseType
            {
                Id = id,
                Name = name,
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteExpenseTypeAsync(int id)
        {
            await _db.DeleteAsync(new ExpenseType
            {
                Id = id,
            });

            return new StatusResponse(StatusCode.Success);
        }

        #endregion

        #region Expense Event

        public async Task<List<ExpenseEvent>> GetExpenseEventsAsync()
        {
            return await _db.Table<ExpenseEvent>().OrderBy(e => e.Name).ToListAsync();
        }

        public async Task<StatusResponse> AddExpenseEventAsync(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.InsertAsync(new ExpenseEvent
            {
                Name = eventName,
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateExpenseEventAsync(int id, string newEventName)
        {
            if (string.IsNullOrWhiteSpace(newEventName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.UpdateAsync(new ExpenseEvent
            {
                Id = id,
                Name = newEventName,
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteExpenseEventAsync(int id)
        {
            await _db.DeleteAsync(new ExpenseEvent
            {
                Id = id,
            });

            return new StatusResponse(StatusCode.Success);
        }

        #endregion

        #region Income Source

        public async Task<List<IncomeSource>> GetIncomeSourcesAsync()
        {
            return await _db.Table<IncomeSource>().OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<StatusResponse> AddIncomeSourceAsync(string sourceName, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(sourceName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.InsertAsync(new IncomeSource
            {
                Name = sourceName,
                ImageUrl = imageUrl
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateIncomeSourceAsync(int id, string newSourceName, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(newSourceName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.UpdateAsync(new IncomeSource
            {
                Id = id,
                Name = newSourceName,
                ImageUrl = imageUrl
            });

            return new StatusResponse(StatusCode.Success);
        }
 
        public async Task<StatusResponse> DeleteIncomeSourceAsync(int id)
        {
            await _db.DeleteAsync(new IncomeSource
            {
                Id = id,
            });

            return new StatusResponse(StatusCode.Success);
        }

        #endregion

        #region Income

        public async Task<List<IncomeDetails>> GetIncomesAsync(int year, int month)
        {
            var incomes = new List<IncomeDetails>();

            if (year < 0 || year > 9999) return incomes;
            if (month <= 0 || month > 12) return incomes;

            incomes = (from i in await _db.Table<Income>().ToListAsync()
                       join r in await _db.Table<IncomeRecipient>().ToListAsync() on i.RecipientId equals r.Id
                       join s in await _db.Table<IncomeSource>().ToListAsync() on i.SourceId equals s.Id
                       where i.IncomeDate.Year == year && i.IncomeDate.Month == month
                       select new IncomeDetails
                       {
                           Id = i.Id,
                           Description = i.Description,
                           Amount = i.Amount,
                           SourceId = i.SourceId,
                           RecipientId = i.RecipientId,
                           IncomeDate = i.IncomeDate,
                           Recipient = r.Name,
                           IncomeSourceImageUrl = s.ImageUrl
                       }).OrderByDescending(d => d.IncomeDate).ToList();

            return incomes;
        }


        public async Task<StatusResponse> AddIncomeAsync(Income income)
        {
            if (income == null)
                return new StatusResponse(StatusCode.InvalidEntity);

            await _db.InsertAsync(income);

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateIncomeAsync(Income income)
        {
            if (income == null)
                return new StatusResponse(StatusCode.InvalidEntity);

            await _db.UpdateAsync(income);

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteIncomeAsync(int id)
        {
            await _db.DeleteAsync(new Income { Id = id });

            return new StatusResponse(StatusCode.Success);
        }

        #endregion

        #region Income Recipient

        public async Task<List<IncomeRecipient>> GetIncomeRecipients()
        {
            return await _db.Table<IncomeRecipient>().ToListAsync();
        }

        public async Task<StatusResponse> AddIncomeRecipientAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.InsertAsync(new IncomeRecipient { Name = name });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateIncomeRecipientAsync(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            await _db.UpdateAsync(new IncomeRecipient { Id = id, Name = name });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteIncomeRecipientAsync(int id)
        {
            await _db.DeleteAsync(new IncomeRecipient { Id = id });

            return new StatusResponse(StatusCode.Success);
        }

        #endregion
    }
}
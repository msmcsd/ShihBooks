using ShihBooks.Core;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class InMemoryExpensesDataStore : IExpensesDataStore
    {
        private List<ExpenseTag> _expenseTags = new List<ExpenseTag>()
        {
            new ExpenseTag {Id = 1, Name = "Kids"},
            new ExpenseTag {Id = 2, Name = "One Time"}
        };

        private List<ExpenseType> _expenseTypes = new List<ExpenseType>()
        {
            new ExpenseType { Id = 1, Name = "Grocery" },
            new ExpenseType { Id = 2, Name = "Electronics"}
        };

        private List<Merchant> _merchants =>new List<Merchant>()
        {
            new Merchant { Id = 1, Name = "Costco", ImageUrl = "https://play-lh.googleusercontent.com/gqOziTbVWioRJtHh7OvfOq07NCTcAHKWBYPQKJOZqNcczpOz5hdrnQNY7i2OatJxmuY=w240-h480-rw"},
            new Merchant { Id = 2, Name = "Amazon", ImageUrl = "https://www.amazon.com/favicon.ico"}
        };

        private List<ExpenseEvent> _expenseEvents = new List<ExpenseEvent>()
        {
            new ExpenseEvent {Id = 1, Name = "Travel"},
            new ExpenseEvent {Id = 2, Name = "New semester"}
        };

        private List<Expense> _expenses = new List<Expense>()
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
        };

        public Task<List<ExpenseView>> GetExpenses(int year, int month)
        {
            var expenses = new List<ExpenseView>();

            if (year < 0 || year > 9999) return Task.FromResult(expenses);
            if (month <= 0 || month > 12) return Task.FromResult(expenses);

            expenses = (from e in _expenses
                        join m in _merchants
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
                       

            return Task.FromResult(expenses);
        }

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
           return await Task.FromResult(_expenseTags.OrderBy(t=>t.Name).ToList());
        }

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await Task.FromResult(_expenseTypes);
        }

        public Task<List<Merchant>> GetMerchantsAsync()
        {
            return Task.FromResult(_merchants);
        }

        public async Task<bool> AddExpenseTag(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                return false;
            }

            _expenseTags.Add(new ExpenseTag
            { 
                Id = _expenseTags.Count() + 1, 
                Name = tagName 
            });

            return true;
        }

        public async Task<bool> UpdateExpenseTag(int tagId, string tagName)
        {
            var tag = _expenseTags.FirstOrDefault(t => t.Id == tagId);
            if (tag != null)
            {
                tag.Name = tagName;
            }

            return true;
        }

        public async Task<bool> UpdateExpense(Expense expense)
        {
            var exp = _expenses.FirstOrDefault(e => e.Id == expense.Id);
            if (exp != null)
            {
                exp.ExpenseTypeId = expense.ExpenseTypeId;
                exp.Note = expense.Note;
                exp.ExpenseDate = expense.ExpenseDate;
                exp.Description = expense.Description;
                exp.Amount = expense.Amount;
                exp.MerchantId = expense.MerchantId;
                exp.TagId = expense.TagId;
                exp.EventId = expense.EventId;
            }

            return true;
        }

        public async Task<List<ExpenseEvent>> GetExpenseEventsAsync()
        {
            return _expenseEvents;
        }

        public Task<bool> DeleteExpense(int expenseId)
        {
            var e = _expenses.FirstOrDefault(e => e.Id == expenseId);
            if (e != null) _expenses.Remove(e);

            return Task.FromResult(true);
        }

        public Task<int> DeleteExpenseTagAsync(int tagId)
        {
            var tag = _expenseTags.FirstOrDefault(e => e.Id == tagId);
            if (tag == null) return Task.FromResult(-1);

            var expense = _expenses.FirstOrDefault(e => e.TagId == tagId);
            if (expense == null)
            {
                _expenseTags.Remove(tag);
                return Task.FromResult(tagId);
            }

            // return Task.FromResult($"Expense on {expense.ExpenseDate.ToString("MM/dd/yyyy")} is using tag {tag.Name}.");
            return Task.FromResult(0);
        }

        public Task<bool> AddExpenseType(string name)
        {
            var type = _expenseTypes.FirstOrDefault(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (type != null) return Task.FromResult(false);

            _expenseTypes.Add(new ExpenseType() 
            { 
                Id = _expenseTypes.Count() + 1,
                Name = name 
            });

            return Task.FromResult(true);
        }

        public async Task<bool> UpdateExpenseType(int id, string newTypeName)
        {
            var type = _expenseTypes.FirstOrDefault(t => t.Id == id);
            if (type == null) return false;

            type.Name = newTypeName;
            return true;
        }

        public async Task<int> DeleteExpenseTypeAsync(int id)
        {
            var type = _expenseTypes.FirstOrDefault(t => t.Id == id);
            if (type != null)
            {
                _expenseTypes.Remove(type);
                return 0;
            }

            return id;
        }
    }
}
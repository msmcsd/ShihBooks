using ShihBooks.Core;
using ShihBooks.Core.Expenses;
using ShihBooks.Core.Incomes;
using ShihBooks.UseCases.PluginInterfaces;
using System.Xml.Linq;

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

        private List<Merchant> _merchants => new List<Merchant>()
        {
            new Merchant { Id = 1, Name = "Costco", ImageUrl = "https://play-lh.googleusercontent.com/gqOziTbVWioRJtHh7OvfOq07NCTcAHKWBYPQKJOZqNcczpOz5hdrnQNY7i2OatJxmuY=w240-h480-rw"},
            new Merchant { Id = 2, Name = "Amazon", ImageUrl = "https://www.amazon.com/favicon.ico"}
        };

        private List<ExpenseEvent> _expenseEvents = new List<ExpenseEvent>()
        {
            new ExpenseEvent {Id = 1, Name = "Travel"},
            new ExpenseEvent {Id = 2, Name = "New semester"}
        };

        private List<IncomeSource> _incomeSources = new List<IncomeSource>()
        {
            new IncomeSource {Id = 1, Name = "Interest"},
            new IncomeSource {Id = 2, Name = "Lottery"}
        };

        private List<Income> _incomes = new List<Income>
        {
            new Income 
            {
                Id = 1, 
                Description = "Band of Americas", 
                Amount=100, 
                IncomeDate=new DateTime(2023, DateTime.Now.Month, 4), 
                SourceId = 1,
                RecipientId = 1
            },
            new Income
            {
                Id = 2, 
                Description = "Won lottery", 
                Amount=123.45, 
                IncomeDate=new DateTime(2023, DateTime.Now.Month, 18), 
                SourceId = 2,
                RecipientId = 2
            }
        };

        private List<IncomeRecipient> _incomeRecipients = new List<IncomeRecipient>()
        {
            new IncomeRecipient {Id = 1, Name = "John1"},
            new IncomeRecipient {Id = 2, Name = "Adam2"}
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

        public Task<List<ExpenseView>> GetExpensesAsync(int year, int month)
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
                         }).OrderByDescending(e => e.ExpenseDate).ToList();
                       

            return Task.FromResult(expenses);
        }

        public Task<bool> AddExpenseAsync(Expense expense)
        {
            expense.Id = _expenses.Count + 1;
            _expenses.Add(expense);

            return Task.FromResult(true);
        }

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return await Task.FromResult(_expenseTags.OrderBy(t => t.Name).ToList());
        }

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await Task.FromResult(_expenseTypes.OrderBy(t => t.Name).ToList());
        }

        public async Task<bool> AddExpenseTagAsync(string tagName)
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

        public async Task<bool> UpdateExpenseTagAsync(int tagId, string tagName)
        {
            var tag = _expenseTags.FirstOrDefault(t => t.Id == tagId);
            if (tag != null)
            {
                tag.Name = tagName;
            }

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(Expense expense)
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

        public Task<bool> DeleteExpenseAsync(int expenseId)
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

        public Task<bool> AddExpenseTypeAsync(string name)
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

        public async Task<bool> UpdateExpenseTypeAsync(int id, string newTypeName)
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

        public async Task<bool> AddExpenseEventAsync(string eventName)
        {
            var ev = _expenseEvents.FirstOrDefault(t => t.Name.Equals(eventName, StringComparison.InvariantCultureIgnoreCase));
            if (ev != null) return false;

            _expenseEvents.Add(new ExpenseEvent
            {
                Id = _expenseTypes.Count() + 1,
                Name = eventName
            });

            return true;
        }

        public async Task<bool> UpdateExpenseEventAsync(int id, string newEventName)
        {
            var ev = _expenseEvents.FirstOrDefault(t => t.Id == id);
            if (ev == null) return false;

            ev.Name = newEventName;
            return true;
        }

        public async Task<int> DeleteExpenseEventAsync(int id)
        {
            var ev = _expenseEvents.FirstOrDefault(t => t.Id == id);
            if (ev != null)
            {
                _expenseEvents.Remove(ev);
                return 0;
            }

            return id;
        }

        public async Task<bool> AddIncomeSourceAsync(string sourceName)
        {
            var source = _incomeSources.FirstOrDefault(t => t.Name.Equals(sourceName, StringComparison.InvariantCultureIgnoreCase));
            if (source != null) return false;

            _incomeSources.Add(new IncomeSource
            {
                Id = _expenseTypes.Count() + 1,
                Name = sourceName
            });

            return true;
        }

        public async Task<int> DeleteIncomeSourceAsync(int id)
        {
            var source = _incomeSources.FirstOrDefault(t => t.Id == id);
            if (source != null)
            {
                _incomeSources.Remove(source);
                return 0;
            }

            return id;
        }

        public async Task<bool> UpdateIncomeSourceAsync(int id, string newSourceName)
        {
            var source = _incomeSources.FirstOrDefault(t => t.Id == id);
            if (source == null) return false;

            source.Name = newSourceName;
            return true;
        }

        public Task<List<IncomeSource>> GetIncomeSourcesAsync()
        {
            return Task.FromResult(_incomeSources.OrderBy(t => t.Name).ToList());
        }

        public Task<List<Merchant>> GetMerchantsAsync()
        {
            return Task.FromResult(_merchants.OrderBy(t => t.Name).ToList());
        }

        public async Task<bool> AddMerchantAsync(string merchantName, string imageUrl)
        {
            var m = _merchants.FirstOrDefault(t => t.Name.Equals(merchantName, StringComparison.InvariantCultureIgnoreCase));
            if (m != null) return false;

            _merchants.Add(new Merchant
            {
                Id = _merchants.Count + 1,
                Name = merchantName,
                ImageUrl = imageUrl,
            });

            return true;
        }

        public async Task<bool> UpdateMerchantAsync(int id, string name, string imageUrl)
        {
            var m = _merchants.FirstOrDefault(t => t.Id == id);
            if (m == null) return false;

            m.Name = name;
            m.ImageUrl = imageUrl;

            return true;
        }

        public async Task<int> DeleteMerchantAsync(int id)
        {
            var m = _merchants.FirstOrDefault(t => t.Id == id);
            if (m != null)
            {
                _merchants.Remove(m);
                return 0;
            }

            return id;
        }

        public async Task<List<IncomeDetails>> GetIncomesAsync(int year, int month)
        {
            var incomes = new List<IncomeDetails>();

            if (year < 0 || year > 9999) return incomes;
            if (month <= 0 || month > 12) return incomes;

            incomes = (from i in _incomes
                       join r in _incomeRecipients on i.RecipientId equals r.Id
                       join s in _incomeSources on i.SourceId equals s.Id
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
                           IncomeSourceImageUrl = s.ImageUrl,
                           Note = i.Note
                       }).OrderByDescending(d => d.IncomeDate).ToList();

            return incomes;
        }

        public async Task<bool> AddIncomeAsync(Income income)
        {
            if (income == null) return false;

            income.Id = _incomes.Count + 1;
            _incomes.Add(income);

            return true;
        }

        public async Task<bool> UpdateIncomeAsync(Income income)
        {
            if (income == null) return false;

            var i = _incomes.FirstOrDefault(a => a.Id == income.Id);
            if (i != null)
            {
                i.Amount = income.Amount;
                i.IncomeDate = income.IncomeDate;
                i.SourceId = income.SourceId;
                i.RecipientId = income.RecipientId;
                i.Description = income.Description;
                i.Note = income.Note;
            }

            return true;
        }

        public async Task<int> DeleteIncomeAsync(int id)
        {
            var income = _incomes.FirstOrDefault(i => i.Id == id);
            if (income != null)
            {
                _incomes.Remove(income);
                return id;
            }

            return -1;
        }

        #region Income Recipient

        public async Task<List<IncomeRecipient>> GetIncomeRecipients()
        {
            return _incomeRecipients.OrderBy(r => r.Name).ToList();
        }

        public async Task<bool> AddIncomeRecipientAsync(string name)
        {
            var recipient = _incomeRecipients.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
            if (recipient != null) return true;

            _incomeRecipients.Add(new IncomeRecipient { Id = _incomeRecipients.Count + 1, Name = name });
            return true;
        }

        public async Task<bool> UpdateIncomeRecipientAsync(int id, string name)
        {
            var recipient = _incomeRecipients.FirstOrDefault(i => i.Id == id);
            if (recipient != null)
            {
                recipient.Name = name;
            }

            return true;
        }

        public async Task<int> DeleteIncomeRecipientAsync(int id)
        {
            var income = _incomes.FirstOrDefault(i => i.RecipientId == id);
            if (income != null) return -1;

            var recipient = _incomeRecipients.FirstOrDefault(i => i.Id == id);
            if (recipient != null)
            {
                _incomeRecipients.Remove(recipient);
            }

            return id;
        }

        #endregion
    }
}
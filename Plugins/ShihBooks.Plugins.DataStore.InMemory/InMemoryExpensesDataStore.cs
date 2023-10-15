using ShihBooks.Core;
using ShihBooks.Core.Expenses;
using ShihBooks.Core.Incomes;
using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class InMemoryExpensesDataStore : IExpensesDataStore
    {
        #region Sample Data

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

        #endregion

        #region Expense

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

        public async Task<StatusResponse> AddExpenseAsync(Expense expense)
        {
            expense.Id = _expenses.Count + 1;
            _expenses.Add(expense);

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateExpenseAsync(Expense expense)
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

                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.ExpenseNotFound);
        }

        public async Task<StatusResponse> DeleteExpenseAsync(int expenseId)
        {
            var e = _expenses.FirstOrDefault(e => e.Id == expenseId);
            if (e != null)
            {
                _expenses.Remove(e);
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.ExpenseNotFound);
        }

        #endregion

        #region Expense Tag

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return _expenseTags.OrderBy(t => t.Name).ToList();
        }

        public async Task<StatusResponse> AddExpenseTagAsync(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                return new StatusResponse(StatusCode.InvalidEntityName);
            }

            _expenseTags.Add(new ExpenseTag
            { 
                Id = _expenseTags.Count() + 1, 
                Name = tagName 
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateExpenseTagAsync(int tagId, string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                return new StatusResponse(StatusCode.InvalidEntityName);
            }

            var tag = _expenseTags.FirstOrDefault(t => t.Id == tagId);
            if (tag != null)
            {
                tag.Name = tagName;
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.EntityNotFound);
        }        
        
        public async Task<StatusResponse> DeleteExpenseTagAsync(int tagId)
        {
            var tag = _expenseTags.FirstOrDefault(e => e.Id == tagId);
            if (tag == null) return new StatusResponse(StatusCode.EntityNotFound);

            var expense = _expenses.FirstOrDefault(e => e.TagId == tagId);
            if (expense == null)
            {
                _expenseTags.Remove(tag);
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.ExpenseNotFound);
        }

        #endregion

        #region Expense Type

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await Task.FromResult(_expenseTypes.OrderBy(t => t.Name).ToList());
        }

        public async Task<StatusResponse> AddExpenseTypeAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var type = _expenseTypes.FirstOrDefault(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (type != null) return new StatusResponse(StatusCode.EntityExists);

            _expenseTypes.Add(new ExpenseType() 
            { 
                Id = _expenseTypes.Count() + 1,
                Name = name 
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateExpenseTypeAsync(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var type = _expenseTypes.FirstOrDefault(t => t.Id == id);
            if (type == null) 
                return new StatusResponse(StatusCode.EntityNotFound);

            type.Name = name;
            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteExpenseTypeAsync(int id)
        {
            var e = _expenses.FirstOrDefault(e => e.ExpenseTypeId  == id);
            if (e != null) return new StatusResponse(StatusCode.EntityInUse);

            var type = _expenseTypes.FirstOrDefault(t => t.Id == id);
            if (type != null)
            {
                _expenseTypes.Remove(type);
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.EntityNotFound);
        }

        #endregion

        #region Expense Event

        public async Task<List<ExpenseEvent>> GetExpenseEventsAsync()
        {
            return _expenseEvents.OrderBy(e => e.Name).ToList();
        }

        public async Task<StatusResponse> AddExpenseEventAsync(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var ev = _expenseEvents.FirstOrDefault(t => t.Name.Equals(eventName, StringComparison.InvariantCultureIgnoreCase));
            if (ev != null) 
                return new StatusResponse(StatusCode.EntityExists);

            _expenseEvents.Add(new ExpenseEvent
            {
                Id = _expenseTypes.Count() + 1,
                Name = eventName
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateExpenseEventAsync(int id, string newEventName)
        {
            if (string.IsNullOrWhiteSpace(newEventName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var ev = _expenseEvents.FirstOrDefault(t => t.Id == id);
            if (ev == null) 
                return new StatusResponse(StatusCode.EntityNotFound);

            ev.Name = newEventName;
            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteExpenseEventAsync(int id)
        {
            var e = _expenses.FirstOrDefault(e => e.EventId == id);
            if (e != null) return new StatusResponse(StatusCode.EntityInUse);

            var ev = _expenseEvents.FirstOrDefault(t => t.Id == id);
            if (ev != null)
            {
                _expenseEvents.Remove(ev);
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.EntityNotFound);
        }

        #endregion

        #region Income Source

        public async Task<List<IncomeSource>> GetIncomeSourcesAsync()
        {
            return _incomeSources.OrderBy(t => t.Name).ToList();
        }

        public async Task<StatusResponse> AddIncomeSourceAsync(string sourceName)
        {
            if (string.IsNullOrWhiteSpace(sourceName)) 
                return new StatusResponse(StatusCode.InvalidEntityName);

            var source = _incomeSources.FirstOrDefault(t => t.Name.Equals(sourceName, StringComparison.InvariantCultureIgnoreCase));
            if (source != null) return new StatusResponse(StatusCode.EntityExists);

            _incomeSources.Add(new IncomeSource
            {
                Id = _expenseTypes.Count() + 1,
                Name = sourceName
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateIncomeSourceAsync(int id, string newSourceName)
        {
            if (string.IsNullOrEmpty(newSourceName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var source = _incomeSources.FirstOrDefault(t => t.Id == id);
            if (source == null) return new StatusResponse(StatusCode.EntityNotFound);

            source.Name = newSourceName;
            return new StatusResponse(StatusCode.Success);
        }        
        
        public async Task<StatusResponse> DeleteIncomeSourceAsync(int id)
        {
            var e = _incomes.FirstOrDefault(e => e.SourceId == id);
            if (e != null)
                return new StatusResponse(StatusCode.EntityInUse);

            var source = _incomeSources.FirstOrDefault(t => t.Id == id);
            if (source != null)
            {
                _incomeSources.Remove(source);
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.EntityNotFound);
        }

        #endregion

        #region Merchant

        public async Task<List<Merchant>> GetMerchantsAsync()
        {
            return _merchants.OrderBy(t => t.Name).ToList();
        }

        public async Task<StatusResponse> AddMerchantAsync(string merchantName, string imageUrl)
        {
            if (string.IsNullOrEmpty(merchantName))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var m = _merchants.FirstOrDefault(t => t.Name.Equals(merchantName, StringComparison.InvariantCultureIgnoreCase));
            if (m != null) return new StatusResponse(StatusCode.EntityExists);

            _merchants.Add(new Merchant
            {
                Id = _merchants.Count + 1,
                Name = merchantName,
                ImageUrl = imageUrl,
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateMerchantAsync(int id, string name, string imageUrl)
        {
            if (string.IsNullOrEmpty(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var m = _merchants.FirstOrDefault(t => t.Id == id);
            if (m == null) return new StatusResponse(StatusCode.EntityNotFound);

            m.Name = name;
            m.ImageUrl = imageUrl;

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> DeleteMerchantAsync(int id)
        {
            var e = _expenses.FirstOrDefault(t => t.MerchantId == id);
            if (e != null)
                return new StatusResponse(StatusCode.EntityInUse);

            var m = _merchants.FirstOrDefault(t => t.Id == id);
            if (m != null)
            {
                _merchants.Remove(m);
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.EntityNotFound);
        }

        #endregion

        #region Income

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

        public async Task<StatusResponse> AddIncomeAsync(Income income)
        {
            if (income == null) 
                return new StatusResponse(StatusCode.InvalidEntity);

            income.Id = _incomes.Count + 1;
            _incomes.Add(income);

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateIncomeAsync(Income income)
        {
            if (income == null) 
                return new StatusResponse(StatusCode.InvalidEntity);

            var i = _incomes.FirstOrDefault(a => a.Id == income.Id);
            if (i != null)
            {
                i.Amount = income.Amount;
                i.IncomeDate = income.IncomeDate;
                i.SourceId = income.SourceId;
                i.RecipientId = income.RecipientId;
                i.Description = income.Description;
                i.Note = income.Note;

                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.IncomeNotFound);
        }

        public async Task<StatusResponse> DeleteIncomeAsync(int id)
        {
            var income = _incomes.FirstOrDefault(i => i.Id == id);
            if (income != null)
            {
                _incomes.Remove(income);
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.IncomeNotFound);
        }

        #endregion

        #region Income Recipient

        public async Task<List<IncomeRecipient>> GetIncomeRecipients()
        {
            return _incomeRecipients.OrderBy(r => r.Name).ToList();
        }

        public async Task<StatusResponse> AddIncomeRecipientAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var recipient = _incomeRecipients.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
            if (recipient != null) 
                return new StatusResponse(StatusCode.EntityExists);

            _incomeRecipients.Add(new IncomeRecipient 
            { 
                Id = _incomeRecipients.Count + 1, 
                Name = name 
            });

            return new StatusResponse(StatusCode.Success);
        }

        public async Task<StatusResponse> UpdateIncomeRecipientAsync(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new StatusResponse(StatusCode.InvalidEntityName);

            var recipient = _incomeRecipients.FirstOrDefault(i => i.Id == id);
            if (recipient != null)
            {
                recipient.Name = name;
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.EntityNotFound);
        }

        public async Task<StatusResponse> DeleteIncomeRecipientAsync(int id)
        {
            var income = _incomes.FirstOrDefault(i => i.RecipientId == id);
            if (income != null) 
                return new StatusResponse(StatusCode.EntityInUse);

            var recipient = _incomeRecipients.FirstOrDefault(i => i.Id == id);
            if (recipient != null)
            {
                _incomeRecipients.Remove(recipient);
                return new StatusResponse(StatusCode.Success);
            }

            return new StatusResponse(StatusCode.EntityNotFound);
        }

        #endregion
    }
}
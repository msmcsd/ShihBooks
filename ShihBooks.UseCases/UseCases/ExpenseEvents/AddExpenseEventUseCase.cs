using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.UseCases.ExpenseEvents
{
    public class AddExpenseEventUseCase : IAddExpenseEventUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddExpenseEventUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public Task<bool> ExecuteAsync(string eventName)
        {
            return _expensesDataStore.AddExpenseEventAsync(eventName);
        }
    }
}

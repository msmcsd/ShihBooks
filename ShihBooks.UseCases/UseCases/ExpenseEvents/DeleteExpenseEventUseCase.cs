using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.UseCases.ExpenseEvents
{
    public class DeleteExpenseEventUseCase : IDeleteExpenseEventUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteExpenseEventUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<int> ExecuteAsync(int id)
        {
            return await _expensesDataStore.DeleteExpenseEventAsync(id);
        }
    }
}

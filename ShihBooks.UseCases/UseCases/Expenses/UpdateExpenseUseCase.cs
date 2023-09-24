using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.Expenses;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.UseCases.Expenses
{
    public class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateExpenseUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(Expense expense)
        {
            if (expense == null) return false;

            return await _expensesDataStore.UpdateExpense(expense);
        }
    }
}

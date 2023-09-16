using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases
{
    public class ViewExpenseTypesUseCase : IViewExpenseTypesUseCase
    {
        private readonly IExpensesDataStore _expenseSource;

        public ViewExpenseTypesUseCase(IExpensesDataStore expenseSource)
        {
            _expenseSource = expenseSource;
        }
        public async Task<List<ExpenseType>> ExecuteAsync()
        {
            return await _expenseSource.GetExpenseTypesAsync();
        }
    }
}

using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.UseCases.ExpenseTypes
{
    public class UpdateExpenseTypeUseCase : IUpdateExpenseTypeUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateExpenseTypeUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(int id, string newTypeName)
        {
            return await _expensesDataStore.UpdateExpenseTypeAsync(id, newTypeName);
        }
    }
}

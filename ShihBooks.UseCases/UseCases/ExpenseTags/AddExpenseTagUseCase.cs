using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.UseCases.ExpenseTags
{
    public class AddExpenseTagUseCase : IAddExpenseTagUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddExpenseTagUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task ExecuteAsync(string tagName)
        {
            if (tagName == null)
            {
                return;
            }

            await _expensesDataStore.SavExpenseTag(tagName);
        }
    }
}

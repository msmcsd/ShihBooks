using ShihBooks.UseCases.Interfaces.IncomeSources;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.UseCases.IncomeSources
{
    public class UpdateIncomeSourceUseCase : IUpdateIncomeSourceUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateIncomeSourceUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(int id, string newSourceName)
        {
            return await _expensesDataStore.UpdateIncomeSourceAsync(id, newSourceName);
        }
    }
}

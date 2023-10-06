using ShihBooks.UseCases.Interfaces.Merchants;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.UseCases.Merchants
{
    public class UpdateMerchantUseCase : IUpdateMerchantUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateMerchantUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(int id, string newMerchantName, string imageUrl)
        {
            return await _expensesDataStore.UpdateMerchantAsync(id, newMerchantName, imageUrl);
        }
    }
}

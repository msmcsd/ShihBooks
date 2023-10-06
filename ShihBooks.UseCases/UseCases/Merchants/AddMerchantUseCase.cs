using ShihBooks.UseCases.Interfaces.Merchants;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.UseCases.Merchants
{
    public class AddMerchantUseCase : IAddMerchantUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddMerchantUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(string merchantName)
        {
            return await _expensesDataStore.AddMerchantAsync(merchantName, null);
        }

        public async Task<bool> ExecuteAsync(string merchantName, string imageUrl)
        {
            return await _expensesDataStore.AddMerchantAsync(merchantName, imageUrl);
        }
    }
}

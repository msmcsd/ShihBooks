using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.Merchants;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.Merchants
{
    public class AddMerchantUseCase : IAddMerchantUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddMerchantUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(string merchantName)
        {
            return await _expensesDataStore.AddMerchantAsync(merchantName, null);
        }

        public async Task<StatusResponse> ExecuteAsync(string merchantName, string imageUrl)
        {
            return await _expensesDataStore.AddMerchantAsync(merchantName, imageUrl);
        }
    }
}

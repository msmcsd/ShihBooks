using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.Merchants;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.Merchants
{
    public class UpdateMerchantUseCase : IUpdateMerchantUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateMerchantUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int id, string newMerchantName, string imageUrl)
        {
            return await _expensesDataStore.UpdateMerchantAsync(id, newMerchantName, imageUrl);
        }
    }
}

using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.Merchants;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.Merchants
{
    public class DeleteMerchantUseCase : IDeleteMerchantUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteMerchantUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int id)
        {
            return await _expensesDataStore.DeleteMerchantAsync(id);
        }
    }
}

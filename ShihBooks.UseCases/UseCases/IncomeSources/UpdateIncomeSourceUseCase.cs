using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.IncomeSources;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeSources
{
    public class UpdateIncomeSourceUseCase : IUpdateIncomeSourceUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateIncomeSourceUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int id, string newSourceName)
        {
            return await _expensesDataStore.UpdateIncomeSourceAsync(id, newSourceName);
        }
    }
}

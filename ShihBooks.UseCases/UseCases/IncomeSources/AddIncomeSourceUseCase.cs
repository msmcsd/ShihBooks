using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.IncomeSources;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeSources
{
    public class AddIncomeSourceUseCase : IAddIncomeSourceUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddIncomeSourceUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(string sourceName)
        {
            return await _expensesDataStore.AddIncomeSourceAsync(sourceName, null);
        }

        public async Task<StatusResponse> ExecuteAsync(string sourceName, string imageUrl)
        {
            return await _expensesDataStore.AddIncomeSourceAsync(sourceName, imageUrl);
        }
    }
}

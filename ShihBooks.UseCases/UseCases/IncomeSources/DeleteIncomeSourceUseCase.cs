using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.IncomeSources;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeSources
{
    public class DeleteIncomeSourceUseCase : IDeleteIncomeSourceUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteIncomeSourceUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int id)
        {
            return await _expensesDataStore.DeleteIncomeSourceAsync(id);
        }
    }
}

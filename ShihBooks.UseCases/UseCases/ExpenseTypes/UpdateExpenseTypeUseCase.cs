using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.UseCases.ExpenseTypes
{
    public class UpdateExpenseTypeUseCase : IUpdateExpenseTypeUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateExpenseTypeUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int id, string newTypeName)
        {
            return await _expensesDataStore.UpdateExpenseTypeAsync(id, newTypeName);
        }
    }
}

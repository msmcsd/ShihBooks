using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseEvents
{
    public class DeleteExpenseEventUseCase : IDeleteExpenseEventUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteExpenseEventUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int id)
        {
            return await _expensesDataStore.DeleteExpenseEventAsync(id);
        }
    }
}

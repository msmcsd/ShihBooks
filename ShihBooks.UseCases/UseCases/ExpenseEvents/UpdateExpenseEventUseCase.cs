using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.UseCases.ExpenseEvents
{
    public class UpdateExpenseEventUseCase : IUpdateExpenseEventUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateExpenseEventUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int id, string newEventName)
        {
            return await _expensesDataStore.UpdateExpenseEventAsync(id, newEventName);
        }
    }
}

using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseEvents
{
    public class AddExpenseEventUseCase : IAddExpenseEventUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddExpenseEventUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public Task<StatusResponse> ExecuteAsync(string eventName)
        {
            return _expensesDataStore.AddExpenseEventAsync(eventName);
        }
    }
}

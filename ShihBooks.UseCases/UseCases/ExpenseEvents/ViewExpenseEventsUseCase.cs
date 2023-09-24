using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseEvents
{
    public class ViewExpenseEventsUseCase : IViewExpenseEventsUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public ViewExpenseEventsUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<List<ExpenseEvent>> ExecuteAsync()
        {
            return await _expensesDataStore.GetExpenseEventsAsync();
        }
    }
}

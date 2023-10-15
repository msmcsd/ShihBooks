using ShihBooks.Core.Expenses;
using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.Expenses;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.Expenses
{
    public class AddExpenseUseCase : IAddExpenseUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddExpenseUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(Expense expense)
        {
            return await _expensesDataStore.AddExpenseAsync(expense);
        }
    }
}

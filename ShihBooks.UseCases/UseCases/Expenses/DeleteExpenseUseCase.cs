using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.Expenses;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.Expenses
{
    public class DeleteExpenseUseCase : IDeleteExpenseUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteExpenseUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int expenseId)
        {
            return await _expensesDataStore.DeleteExpenseAsync(expenseId);
        }
    }
}

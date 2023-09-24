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

        public async Task<bool> ExecuteAsync(int expenseId)
        {
            return await _expensesDataStore.DeleteExpense(expenseId);
        }
    }
}

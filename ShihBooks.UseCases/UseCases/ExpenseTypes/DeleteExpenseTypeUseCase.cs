using ShihBooks.UseCases.Interfaces.ExpenseTypes;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseTypes
{
    public class DeleteExpenseTypeUseCase : IDeleteExpenseTypeUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteExpenseTypeUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<int> ExecuteAsync(int id)
        {
            return await _expensesDataStore.DeleteExpenseTypeAsync(id);
        }
    }
}

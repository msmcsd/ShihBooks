using ShihBooks.UseCases.Interfaces.ExpenseTypes;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseTypes
{
    public class AddExpenseTypeUseCase : IAddExpenseTypeUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddExpenseTypeUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(string name)
        {
            return await _expensesDataStore.AddExpenseTypeAsync(name);
        }
    }
}

using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseTags
{
    public class AddExpenseTagUseCase : IAddExpenseTagUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddExpenseTagUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(string tagName)
        {
            if (tagName == null)
            {
                return false;
            }

            return await _expensesDataStore.AddExpenseTagAsync(tagName);
        }
    }
}

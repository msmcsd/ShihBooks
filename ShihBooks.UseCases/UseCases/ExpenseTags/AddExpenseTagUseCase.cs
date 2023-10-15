using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.UseCases.ExpenseTags
{
    public class AddExpenseTagUseCase : IAddExpenseTagUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddExpenseTagUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(string tagName)
        {
            return await _expensesDataStore.AddExpenseTagAsync(tagName);
        }
    }
}

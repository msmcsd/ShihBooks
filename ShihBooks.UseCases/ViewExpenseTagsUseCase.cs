using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases
{
    public class ViewExpenseTagsUseCase : IViewExpenseTagsUseCase
    {
        private readonly IExpensesDataStore _expenseSource;

        public ViewExpenseTagsUseCase(IExpensesDataStore expenseSource)
        {
            _expenseSource = expenseSource;
        }

        public async Task<List<ExpenseTag>> ExecuteAsync()
        {
            return await _expenseSource.GetExpenseTagsAsync();
        }
    }
}

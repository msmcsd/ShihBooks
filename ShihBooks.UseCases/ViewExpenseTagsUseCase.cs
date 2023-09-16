using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases
{
    public class ViewExpenseTagsUseCase : IViewExpenseTagsUseCase
    {
        private readonly IExpenseSource _expenseSource;

        public ViewExpenseTagsUseCase(IExpenseSource expenseSource)
        {
            _expenseSource = expenseSource;
        }

        public async Task<List<ExpenseTag>> ExecuteAsync()
        {
            return await _expenseSource.GetExpenseTagsAsync();
        }
    }
}

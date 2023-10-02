using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseTags
{
    public class ViewExpenseTagsUseCase : IViewExpenseTagsUseCase
    {
        private readonly IExpensesDataStore _expenseSource;

        public ViewExpenseTagsUseCase(IExpensesDataStore expenseSource)
        {
            _expenseSource = expenseSource;
        }

        public async Task<List<ExpenseEntity>> ExecuteAsync()
        {
            var ret = await _expenseSource.GetExpenseTagsAsync();
            return ret.ConvertAll(t => new ExpenseEntity
            {
                Id = t.Id,
                Name = t.Name
            });
        }
    }
}

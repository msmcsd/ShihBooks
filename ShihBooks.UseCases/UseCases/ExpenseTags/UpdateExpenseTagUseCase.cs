using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseTags
{
    public class UpdateExpenseTagUseCase : IUpdateExpenseTagUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateExpenseTagUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(int tagId, string tagName)
        {
            return await _expensesDataStore.UpdateExpenseTag(tagId, tagName);
        }
    }
}

using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.UseCases.UseCases.ExpenseTags;

namespace ShihBooks.UseCases.Interfaces.ExpenseTags
{
    public class DeleteExpenseTagUseCase : IDeleteExpenseTagUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteExpenseTagUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<string> ExecuteAsync(int tagId)
        {
            return await _expensesDataStore.DeleteExpenseTag(tagId);
        }
    }
}

using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseTags
{
    public class DeleteExpenseTagUseCase : IDeleteExpenseTagUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteExpenseTagUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(int tagId)
        {
            return await _expensesDataStore.DeleteExpenseTagAsync(tagId);
        }

    }
}
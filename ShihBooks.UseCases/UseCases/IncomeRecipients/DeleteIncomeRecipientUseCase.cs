using ShihBooks.UseCases.Interfaces.IncomeRecipients;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeRecipients
{
    public class DeleteIncomeRecipientUseCase : IDeleteIncomeRecipientUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteIncomeRecipientUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<int> ExecuteAsync(int id)
        {
            return await _expensesDataStore.DeleteIncomeRecipientAsync(id);
        }
    }
}

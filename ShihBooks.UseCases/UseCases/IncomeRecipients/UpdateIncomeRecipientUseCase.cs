using ShihBooks.UseCases.Interfaces.IncomeRecipients;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeRecipients
{
    public class UpdateIncomeRecipientUseCase : IUpdateIncomeRecipientUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateIncomeRecipientUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(int id, string name)
        {
            return await _expensesDataStore.UpdateIncomeRecipientAsync(id, name);
        }
    }
}

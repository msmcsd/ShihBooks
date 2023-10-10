using ShihBooks.UseCases.Interfaces.IncomeRecipients;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeRecipients
{
    public class AddIncomeRecipientUseCase : IAddIncomeRecipientUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public AddIncomeRecipientUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<bool> ExecuteAsync(string name)
        {
            return await _expensesDataStore.AddIncomeRecipientAsync(name);
        }
    }
}

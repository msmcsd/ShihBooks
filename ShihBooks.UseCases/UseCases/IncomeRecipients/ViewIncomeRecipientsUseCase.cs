using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.IncomeRecipients;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeRecipients
{
    public class ViewIncomeRecipientsUseCase : IViewIncomeRecipientsUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public ViewIncomeRecipientsUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<List<CoreEntity>> ExecuteAsync()
        {
            var ret = await _expensesDataStore.GetIncomeRecipients();
            return ret.ConvertAll(r => new CoreEntity
            {
                Id = r.Id,
                Name = r.Name,
                DateAdded = r.DateAdded
            });
        }
    }
}

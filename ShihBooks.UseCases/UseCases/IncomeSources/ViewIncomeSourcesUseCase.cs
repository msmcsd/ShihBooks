using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.IncomeSources;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeSources
{
    public class ViewIncomeSourcesUseCase : IViewIncomeSourcesUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public ViewIncomeSourcesUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<List<IncomeSource>> ExecuteAsync()
        {
            return await _expensesDataStore.GetIncomeSourcesAsync();
        }
    }
}

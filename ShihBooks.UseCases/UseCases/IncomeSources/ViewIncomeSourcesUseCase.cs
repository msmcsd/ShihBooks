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

        public async Task<List<CoreEntityWithUrl>> ExecuteAsync()
        {
            var ret = await _expensesDataStore.GetIncomeSourcesAsync();
            return ret.ConvertAll(s => new CoreEntityWithUrl
            {
                Id = s.Id,
                Name = s.Name,
                ImageUrl = s.ImageUrl
            });
        }
    }
}

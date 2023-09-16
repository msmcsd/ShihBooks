using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases
{
    public class ViewMerchantsUseCase : IViewMerchantsUseCase
    {
        private readonly IExpensesDataStore _expenseSource;

        public ViewMerchantsUseCase(IExpensesDataStore expenseSource)
        {
            _expenseSource = expenseSource;
        }
        public async Task<List<Merchant>> ExecuteAsync()
        {
            return await _expenseSource.GetMerchantsAsync();
        }
    }
}

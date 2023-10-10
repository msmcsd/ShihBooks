using ShihBooks.UseCases.Interfaces.Incomes;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.Core.Incomes;

namespace ShihBooks.UseCases.UseCases.Incomes
{
    public class ViewIncomesByMonthUseCase : IViewIncomesByMonthUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public ViewIncomesByMonthUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<List<IncomeDetails>> ExecuteAsync(int year, int month)
        {
            return await _expensesDataStore.GetIncomesAsync(year, month);
        }
    }
}

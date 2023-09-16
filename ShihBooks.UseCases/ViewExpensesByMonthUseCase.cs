using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases
{
    // All the code in this file is included in all platforms.
    public class ViewExpensesByMonthUseCase : IViewExpensesByMonthUseCase
    {
        private readonly IExpensesDataStore _expensesSource;

        public ViewExpensesByMonthUseCase(IExpensesDataStore expensesSource)
        {
            _expensesSource = expensesSource;
        }

        public async Task<List<Expense>> ExecuteAsync(int year, int month)
        {
            return await _expensesSource.GetExpenses(year, month);
        }
    }
}
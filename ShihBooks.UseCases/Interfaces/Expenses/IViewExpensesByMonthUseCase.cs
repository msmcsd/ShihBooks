using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces.Expenses
{
    public interface IViewExpensesByMonthUseCase
    {
        Task<List<ExpenseView>> ExecuteAsync(int year, int month);
    }
}
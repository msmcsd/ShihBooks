using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces
{
    public interface IViewExpensesByMonthUseCase
    {
        Task<List<ExpenseView>> ExecuteAsync(int year, int month);
    }
}
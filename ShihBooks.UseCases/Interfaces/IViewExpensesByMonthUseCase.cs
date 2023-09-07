using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces
{
    public interface IViewExpensesByMonthUseCase
    {
        Task<List<Expense>> ExecuteAsync(int year, int month);
    }
}
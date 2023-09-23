using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces
{
    public interface IViewExpenseEventsUseCase
    {
        Task<List<ExpenseEvent>> ExecuteAsync();
    }
}
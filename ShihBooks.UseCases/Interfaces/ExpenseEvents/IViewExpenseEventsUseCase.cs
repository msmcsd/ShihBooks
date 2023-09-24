using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces.ExpenseEvents
{
    public interface IViewExpenseEventsUseCase
    {
        Task<List<ExpenseEvent>> ExecuteAsync();
    }
}
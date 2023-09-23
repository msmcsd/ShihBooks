using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces
{
    public interface IUpdateExpenseUseCase
    {
        Task<bool> ExecuteAsync(Expense expense);
    }
}
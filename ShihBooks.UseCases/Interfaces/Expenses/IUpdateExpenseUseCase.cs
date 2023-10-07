using ShihBooks.Core.Expenses;

namespace ShihBooks.UseCases.Interfaces.Expenses
{
    public interface IUpdateExpenseUseCase
    {
        Task<bool> ExecuteAsync(Expense expense);
    }
}
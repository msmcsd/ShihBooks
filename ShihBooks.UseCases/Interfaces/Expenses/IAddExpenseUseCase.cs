using ShihBooks.Core.Expenses;

namespace ShihBooks.UseCases.Interfaces.Expenses
{
    public interface IAddExpenseUseCase
    {
        Task<bool> ExecuteAsync(Expense expense);
    }
}
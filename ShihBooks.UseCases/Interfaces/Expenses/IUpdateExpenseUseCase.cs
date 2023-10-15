using ShihBooks.Core.Expenses;
using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Expenses
{
    public interface IUpdateExpenseUseCase
    {
        Task<StatusResponse> ExecuteAsync(Expense expense);
    }
}
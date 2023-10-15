using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Expenses
{
    public interface IDeleteExpenseUseCase
    {
        Task<StatusResponse> ExecuteAsync(int expenseId);
    }
}
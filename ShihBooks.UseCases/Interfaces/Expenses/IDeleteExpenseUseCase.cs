namespace ShihBooks.UseCases.Interfaces.Expenses
{
    public interface IDeleteExpenseUseCase
    {
        Task<bool> ExecuteAsync(int expenseId);
    }
}
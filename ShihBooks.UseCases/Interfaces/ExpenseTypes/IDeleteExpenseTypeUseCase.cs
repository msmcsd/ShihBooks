namespace ShihBooks.UseCases.Interfaces.ExpenseTypes
{
    public interface IDeleteExpenseTypeUseCase
    {
        Task<int> ExecuteAsync(int id);
    }
}
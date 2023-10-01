namespace ShihBooks.UseCases.UseCases.ExpenseTypes
{
    public interface IUpdateExpenseTypeUseCase
    {
        Task<bool> ExecuteAsync(int id, string name);
    }
}
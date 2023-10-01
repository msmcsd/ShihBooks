namespace ShihBooks.UseCases.Interfaces.ExpenseTypes
{
    public interface IAddExpenseTypeUseCase
    {
        Task<bool> ExecuteAsync(string name);
    }
}
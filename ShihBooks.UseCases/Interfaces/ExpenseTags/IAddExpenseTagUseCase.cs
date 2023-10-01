namespace ShihBooks.UseCases.Interfaces.ExpenseTags
{
    public interface IAddExpenseTagUseCase
    {
        Task ExecuteAsync(string tagName);
    }
}
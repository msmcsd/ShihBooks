namespace ShihBooks.UseCases.Interfaces.ExpenseTags
{
    public interface ISaveExpenseTagUseCase
    {
        Task ExecuteAsync(string tagName);
    }
}
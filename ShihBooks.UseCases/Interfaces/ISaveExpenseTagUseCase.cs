namespace ShihBooks.UseCases.Interfaces
{
    public interface ISaveExpenseTagUseCase
    {
        Task ExecuteAsync(string tagName);
    }
}
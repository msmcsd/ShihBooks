namespace ShihBooks.UseCases.Interfaces
{
    public interface IUpdateExpenseTagUseCase
    {
        Task<bool> ExecuteAsync(int tagId, string tagName);
    }
}
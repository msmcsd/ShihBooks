namespace ShihBooks.UseCases.Interfaces.ExpenseTags
{
    public interface IUpdateExpenseTagUseCase
    {
        Task<bool> ExecuteAsync(int tagId, string tagName);
    }
}
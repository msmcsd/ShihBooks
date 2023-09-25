namespace ShihBooks.UseCases.UseCases.ExpenseTags
{
    public interface IDeleteExpenseTagUseCase
    {
        Task<string> ExecuteAsync(int tagId);
    }
}
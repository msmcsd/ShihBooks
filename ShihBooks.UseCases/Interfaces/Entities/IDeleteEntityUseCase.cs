namespace ShihBooks.UseCases.Interfaces.Entities
{
    public interface IDeleteEntityUseCase
    {
        Task<int> ExecuteAsync(int id);
    }
}

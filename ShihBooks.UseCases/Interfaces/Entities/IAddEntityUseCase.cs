namespace ShihBooks.UseCases.Interfaces.Entities
{
    public interface IAddEntityUseCase
    {
        Task<bool> ExecuteAsync(string entityName);
    }
}

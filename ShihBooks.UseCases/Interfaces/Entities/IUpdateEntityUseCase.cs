namespace ShihBooks.UseCases.Interfaces.Entities
{
    public interface IUpdateEntityUseCase
    {
        Task <bool> ExecuteAsync(int id, string newEntityName);
    }
}

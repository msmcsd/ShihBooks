using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Entities
{
    public interface IUpdateEntityUseCase
    {
        Task <StatusResponse> ExecuteAsync(int id, string newEntityName);
    }
}

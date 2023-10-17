using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.EntityWithUrls
{
    public interface IUpdateEntityWithUrlUseCase
    {
        Task<StatusResponse> ExecuteAsync(int id, string newEntityName, string imageUrl);
    }
}

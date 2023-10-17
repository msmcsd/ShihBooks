using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.EntityWithUrls
{
    public interface IAddEntityWithUrlUseCase
    {
        Task<StatusResponse> ExecuteAsync(string entityName, string imageUrl);
    }
}

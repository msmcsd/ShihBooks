using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Entities
{
    public interface IAddEntityUseCase
    {
        Task<StatusResponse> ExecuteAsync(string entityName);
    }
}

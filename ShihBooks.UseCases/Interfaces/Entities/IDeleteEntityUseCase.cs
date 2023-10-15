using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Entities
{
    public interface IDeleteEntityUseCase
    {
        Task<StatusResponse> ExecuteAsync(int id);
    }
}

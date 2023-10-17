using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces.EntityWithUrls
{
    public interface IViewEntitiesWithUrlUseCase
    {
        Task<List<CoreEntityWithUrl>> ExecuteAsync();
    }
}

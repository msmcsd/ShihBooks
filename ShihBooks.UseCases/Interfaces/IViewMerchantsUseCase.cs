using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces
{
    public interface IViewMerchantsUseCase
    {
        Task<List<Merchant>> ExecuteAsync();
    }
}
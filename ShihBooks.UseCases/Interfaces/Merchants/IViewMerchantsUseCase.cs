using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces.Merchants
{
    public interface IViewMerchantsUseCase
    {
        Task<List<Merchant>> ExecuteAsync();
    }
}
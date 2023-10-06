using ShihBooks.UseCases.Interfaces.Entities;

namespace ShihBooks.UseCases.Interfaces.Merchants
{
    public interface IUpdateMerchantUseCase 
    {
        Task<bool> ExecuteAsync(int id, string merchantName, string imageUrl);
    }
}

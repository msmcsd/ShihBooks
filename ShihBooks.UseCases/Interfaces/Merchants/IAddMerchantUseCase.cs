using ShihBooks.UseCases.Interfaces.Entities;

namespace ShihBooks.UseCases.Interfaces.Merchants
{
    public interface IAddMerchantUseCase
    {
        Task<bool> ExecuteAsync(string name, string imageUrl);
    }
}

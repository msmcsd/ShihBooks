using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Merchants
{
    public interface IUpdateMerchantUseCase 
    {
        Task<StatusResponse> ExecuteAsync(int id, string merchantName, string imageUrl);
    }
}

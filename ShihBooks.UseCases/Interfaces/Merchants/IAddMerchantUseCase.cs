using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Merchants
{
    public interface IAddMerchantUseCase
    {
        Task<StatusResponse> ExecuteAsync(string name, string imageUrl);
    }
}

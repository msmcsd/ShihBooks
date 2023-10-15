using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.IncomeSources
{
    public interface IUpdateIncomeSourceUseCase
    {
        Task<StatusResponse> ExecuteAsync(int id, string name, string imageurl);
    }
}

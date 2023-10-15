using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Incomes
{
    public interface IDeleteIncomeUseCase
    {
        Task<StatusResponse> ExecuteAsync(int id);
    }
}
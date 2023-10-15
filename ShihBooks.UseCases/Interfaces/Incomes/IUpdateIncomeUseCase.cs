using ShihBooks.Core.Incomes;
using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Incomes
{
    public interface IUpdateIncomeUseCase
    {
        Task<StatusResponse> ExecuteAsync(Income income);
    }
}
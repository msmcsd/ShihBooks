using ShihBooks.Core.Incomes;
using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.Interfaces.Incomes
{
    public interface IAddIncomeUseCase
    {
        Task<StatusResponse> ExecuteAsync(Income income);
    }
}
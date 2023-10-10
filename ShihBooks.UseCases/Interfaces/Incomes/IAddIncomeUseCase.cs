using ShihBooks.Core.Incomes;

namespace ShihBooks.UseCases.Interfaces.Incomes
{
    public interface IAddIncomeUseCase
    {
        Task<bool> ExecuteAsync(Income income);
    }
}
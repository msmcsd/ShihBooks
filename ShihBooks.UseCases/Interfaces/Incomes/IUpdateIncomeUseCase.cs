using ShihBooks.Core.Incomes;

namespace ShihBooks.UseCases.Interfaces.Incomes
{
    public interface IUpdateIncomeUseCase
    {
        Task<bool> ExecuteAsync(Income income);
    }
}
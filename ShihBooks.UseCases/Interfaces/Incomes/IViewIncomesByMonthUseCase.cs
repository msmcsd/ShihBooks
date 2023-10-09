using ShihBooks.Core.Incomes;

namespace ShihBooks.UseCases.Interfaces.Incomes
{
    public interface IViewIncomesByMonthUseCase
    {
        Task<List<Income>> ExecuteAsync(int year, int month);
    }
}
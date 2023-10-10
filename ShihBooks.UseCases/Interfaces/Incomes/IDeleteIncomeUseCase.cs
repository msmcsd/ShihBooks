namespace ShihBooks.UseCases.Interfaces.Incomes
{
    public interface IDeleteIncomeUseCase
    {
        Task<int> ExecuteAsync(int id);
    }
}
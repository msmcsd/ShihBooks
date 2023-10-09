namespace ShihBooks.UseCases.Interfaces.Incomes
{
    public interface IDeleteIncomeUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }
}
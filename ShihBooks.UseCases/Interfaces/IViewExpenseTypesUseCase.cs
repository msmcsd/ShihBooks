using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces
{
    public interface IViewExpenseTypesUseCase
    {
        Task<List<ExpenseType>> ExecuteAsync();
    }
}
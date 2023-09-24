using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces.ExpenseTypes
{
    public interface IViewExpenseTypesUseCase
    {
        Task<List<ExpenseType>> ExecuteAsync();
    }
}
using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces
{
    public interface IViewExpenseTagsUseCase
    {
        Task<List<ExpenseTag>> ExecuteAsync();
    }
}
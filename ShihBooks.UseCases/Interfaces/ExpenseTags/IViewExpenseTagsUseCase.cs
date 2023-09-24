using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces.ExpenseTags
{
    public interface IViewExpenseTagsUseCase
    {
        Task<List<ExpenseTag>> ExecuteAsync();
    }
}
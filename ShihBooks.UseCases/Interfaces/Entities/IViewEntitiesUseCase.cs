using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces.Entities
{
    public interface IViewEntitiesUseCase
    {
        Task<List<ExpenseEntity>> ExecuteAsync();
    }
}

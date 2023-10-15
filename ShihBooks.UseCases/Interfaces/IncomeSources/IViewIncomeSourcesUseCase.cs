using ShihBooks.Core;

namespace ShihBooks.UseCases.Interfaces.IncomeSources
{
    public interface IViewIncomeSourcesUseCase 
    {
        Task<List<IncomeSource>> ExecuteAsync();
    }
}

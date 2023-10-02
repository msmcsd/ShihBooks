using ShihBooks.UseCases.Interfaces.IncomeSources;

namespace ShihBooks.ViewModels
{
    public partial class ManageIncomeSourcesViewModel : ManageEntitiesViewModel
    {
        public ManageIncomeSourcesViewModel(IViewIncomeSourcesUseCase viewIncomeSourcesUseCase,
                                            IAddIncomeSourceUseCase addIncomeSourceUseCase,
                                            IUpdateIncomeSourceUseCase updateIncomeSourceUseCase,
                                            IDeleteIncomeSourceUseCase deleteIncomeSourceUseCase) :
            base(viewIncomeSourcesUseCase,
                 addIncomeSourceUseCase,
                 updateIncomeSourceUseCase,
                 deleteIncomeSourceUseCase)
        {
        }
    }
}

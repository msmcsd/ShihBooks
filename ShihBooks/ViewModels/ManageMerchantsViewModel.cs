using ShihBooks.UseCases.Interfaces.Merchants;

namespace ShihBooks.ViewModels
{
    public partial class ManageMerchantsViewModel : ManageEntitiesWithUrlViewModel
    {
        public ManageMerchantsViewModel(IViewMerchantsUseCase viewMerchantsUseCase,
                                        IAddMerchantUseCase addMerchantUseCase,
                                        IUpdateMerchantUseCase updateMerchantUseCase,
                                        IDeleteMerchantUseCase deleteMerchantUseCase) :
            base(viewMerchantsUseCase, addMerchantUseCase, updateMerchantUseCase, deleteMerchantUseCase)
        {
        }
    }
}

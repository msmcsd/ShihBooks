using ShihBooks.UseCases.Interfaces.IncomeRecipients;

namespace ShihBooks.ViewModels
{
    public class ManageIncomeRecipientsViewModel : ManageEntitiesViewModel
    {
        public ManageIncomeRecipientsViewModel(IViewIncomeRecipientsUseCase viewIncomeRecipientsUseCase,
                                               IAddIncomeRecipientUseCase addIncomeRecipientUseCase,
                                               IUpdateIncomeRecipientUseCase updateIncomeRecipientUseCase,
                                               IDeleteIncomeRecipientUseCase deleteIncomeRecipientUseCase) : 
            base(viewIncomeRecipientsUseCase, addIncomeRecipientUseCase, 
                 updateIncomeRecipientUseCase, deleteIncomeRecipientUseCase)
        {
        }
    }
}

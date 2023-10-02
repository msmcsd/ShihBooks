using ShihBooks.UseCases.Interfaces.ExpenseEvents;

namespace ShihBooks.ViewModels
{
    public partial class ManageExpenseEventsViewModel : ManageEntitiesViewModel
    {
        public ManageExpenseEventsViewModel(IViewExpenseEventsUseCase viewExpenseEventsUseCase,
                                            IAddExpenseEventUseCase addExpenseEventUseCase,
                                            IUpdateExpenseEventUseCase updateExpenseEventUseCase,
                                            IDeleteExpenseEventUseCase deleteExpenseEventUseCase) :
            base(viewExpenseEventsUseCase,
                 addExpenseEventUseCase,
                 updateExpenseEventUseCase,
                 deleteExpenseEventUseCase)
        {
            
        }
    }
}

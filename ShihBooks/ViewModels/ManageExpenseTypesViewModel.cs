using ShihBooks.UseCases.Interfaces.ExpenseTypes;
using ShihBooks.UseCases.UseCases.ExpenseTypes;

namespace ShihBooks.ViewModels
{
    public partial class ManageExpenseTypesViewModel : ManageEntitiesViewModel
    {
        public ManageExpenseTypesViewModel(IViewExpenseTypesUseCase viewExpenseTypesUseCase,
                                           IAddExpenseTypeUseCase addExpenseTypeUseCase,
                                           IUpdateExpenseTypeUseCase updateExpenseTypeUseCase,
                                           IDeleteExpenseTypeUseCase deleteExpenseTypeUseCase) : 
            base(viewExpenseTypesUseCase, 
                 addExpenseTypeUseCase,
                 deleteExpenseTypeUseCase,
                 updateExpenseTypeUseCase)
        {
        }
    }
}

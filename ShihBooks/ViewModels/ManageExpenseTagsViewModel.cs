using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.UseCases.ExpenseTags;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShihBooks.ViewModels
{
    public partial class ManageExpenseTagsViewModel : ManageEntitiesViewModel
    {
        public ManageExpenseTagsViewModel(IViewExpenseTagsUseCase viewExpenseTagUseCase,
                                          IAddExpenseTagUseCase addExpenseTagUseCase,
                                          IUpdateExpenseTagUseCase updateExpenseTagUseCase,
                                          IDeleteExpenseTagUseCase deleteExpenseTagUseCase) :
            base(viewExpenseTagUseCase,
                 addExpenseTagUseCase,
                 updateExpenseTagUseCase,
                 deleteExpenseTagUseCase)
        {
        }
    }
}

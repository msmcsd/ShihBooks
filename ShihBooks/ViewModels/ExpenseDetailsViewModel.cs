using CommunityToolkit.Mvvm.ComponentModel;
using ShihBooks.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.ViewModels
{
    [QueryProperty("Expense", "Expense")]
    public partial class ExpenseDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Expense _expense;
    }
}

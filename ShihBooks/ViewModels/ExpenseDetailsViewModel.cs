using CommunityToolkit.Mvvm.ComponentModel;
using ShihBooks.Core;

namespace ShihBooks.ViewModels
{
    [QueryProperty("Expense", "Expense")]
    public partial class ExpenseDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Expense _expense;

        public List<Merchant> Merchants { get; set; } = new();

        [ObservableProperty]
        private Merchant _selectedMerchant;

        public ExpenseDetailsViewModel()
        {
            Merchants = new List<Merchant>()
            {
                new Merchant
                {
                    Id = 1,
                    Name = "Costco"
                },
                new Merchant
                {
                    Id = 2,
                    Name = "Amazon"
                }
            };
        }
    }
}

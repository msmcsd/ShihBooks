using CommunityToolkit.Mvvm.ComponentModel;
using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;

namespace ShihBooks.ViewModels
{
    [QueryProperty("Expense", "Expense")]
    public partial class ExpenseDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Expense _expense;

        public List<Merchant> Merchants { get; set; } = new();

        public List<ExpenseType> ExpenseTypes { get; set; } = new();

        public List<ExpenseTag> ExpenseTags{ get; set; } = new();

        [ObservableProperty]
        private Merchant _selectedMerchant;

        [ObservableProperty]
        private ExpenseType _selectedType;

        [ObservableProperty]
        private ExpenseTag _selectedTag;
        private readonly IViewExpenseTypesUseCase _viewExpenseTypesUseCase;

        public ExpenseDetailsViewModel(IViewExpenseTypesUseCase viewExpenseTypesUseCase)
        {
            _viewExpenseTypesUseCase = viewExpenseTypesUseCase;

            Task.Run(LoadSelectionList);
        }

        private async Task LoadSelectionList()
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

            ExpenseTypes = await _viewExpenseTypesUseCase.ExecuteAsync();

            ExpenseTags = new List<ExpenseTag>()
            {
                null,
                new ExpenseTag
                {
                    Id = 1,
                    TagName = "Kids"
                },
                new ExpenseTag
                {
                    Id = 2,
                    TagName = "One Time"
                }
            };
        }
    }
}

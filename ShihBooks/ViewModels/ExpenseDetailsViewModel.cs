
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;

namespace ShihBooks.ViewModels
{
    [QueryProperty("Expense", "Expense")]
    public partial class ExpenseDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ExpenseView _expense;

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
        private readonly IViewMerchantsUseCase _viewMerchantsUseCase;
        private readonly IViewExpenseTagsUseCase _viewExpenseTagsUseCase;
        private readonly IUpdateExpenseUseCase _updateExpenseUseCase;

        public ExpenseDetailsViewModel(IViewExpenseTypesUseCase viewExpenseTypesUseCase,
                                       IViewMerchantsUseCase viewMerchantsUseCase,
                                       IViewExpenseTagsUseCase viewExpenseTagsUseCase,
                                       IUpdateExpenseUseCase updateExpenseUseCase)
        {
            _viewExpenseTypesUseCase = viewExpenseTypesUseCase;
            _viewMerchantsUseCase = viewMerchantsUseCase;
            _viewExpenseTagsUseCase = viewExpenseTagsUseCase;
            _updateExpenseUseCase = updateExpenseUseCase;
            Task.Run(LoadSelectionList);
        }

        private async Task LoadSelectionList()
        {
            Merchants = await _viewMerchantsUseCase.ExecuteAsync();
            ExpenseTypes = await _viewExpenseTypesUseCase.ExecuteAsync();
            ExpenseTags = await _viewExpenseTagsUseCase.ExecuteAsync();
        }

        [RelayCommand]
        public async Task UpdateExpense()
        {
            var ret = await _updateExpenseUseCase.ExecuteAsync(Expense);
            if (ret)
                await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task CancelUpdateExpense()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

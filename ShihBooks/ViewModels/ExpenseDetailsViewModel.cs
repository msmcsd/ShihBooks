using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
using ShihBooks.Core.Expenses;
using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.Interfaces.Expenses;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.Interfaces.ExpenseTypes;
using ShihBooks.UseCases.Interfaces.Merchants;

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

        public List<ExpenseEvent> ExpenseEvents{ get; set; } = new();

        [ObservableProperty]
        private Merchant _selectedMerchant;

        [ObservableProperty]
        private ExpenseType _selectedType;

        [ObservableProperty]
        private ExpenseTag _selectedTag;

        [ObservableProperty]
        private ExpenseEvent _selectedEvent;

        //[ObservableProperty]
        //private DateTime _selectedDate;

        [ObservableProperty]
        private bool _isNewExpense;

        private readonly IViewExpenseTypesUseCase _viewExpenseTypesUseCase;
        private readonly IViewMerchantsUseCase _viewMerchantsUseCase;
        private readonly IViewExpenseTagsUseCase _viewExpenseTagsUseCase;
        private readonly IUpdateExpenseUseCase _updateExpenseUseCase;
        private readonly IViewExpenseEventsUseCase _viewExpenseEventsUseCase;
        private readonly IAddExpenseUseCase _addExpenseUseCase;

        public ExpenseDetailsViewModel(IViewExpenseTypesUseCase viewExpenseTypesUseCase,
                                       IViewMerchantsUseCase viewMerchantsUseCase,
                                       IViewExpenseTagsUseCase viewExpenseTagsUseCase,
                                       IUpdateExpenseUseCase updateExpenseUseCase,
                                       IViewExpenseEventsUseCase viewExpenseEventsUseCase,
                                       IAddExpenseUseCase addExpenseUseCase)
        {
            _viewExpenseTypesUseCase = viewExpenseTypesUseCase;
            _viewMerchantsUseCase = viewMerchantsUseCase;
            _viewExpenseTagsUseCase = viewExpenseTagsUseCase;
            _updateExpenseUseCase = updateExpenseUseCase;
            _viewExpenseEventsUseCase = viewExpenseEventsUseCase;
            _addExpenseUseCase = addExpenseUseCase;
            Task.Run(GetEntitiesAsync);
        }

        public override async Task GetEntitiesAsync()
        {
            Merchants = await _viewMerchantsUseCase.ExecuteAsync();

            var ret = await _viewExpenseTypesUseCase.ExecuteAsync();
            ExpenseTypes = ret.ConvertAll(t => new ExpenseType
            {
                Id = t.Id,
                Name = t.Name,
            });
            ret = await _viewExpenseTagsUseCase.ExecuteAsync();
            ExpenseTags = ret.ConvertAll(t => new ExpenseTag
            {
                Id = t.Id,
                Name = t.Name,
            });

            ret = await _viewExpenseEventsUseCase.ExecuteAsync();
            ExpenseEvents = ret.ConvertAll(t=>new ExpenseEvent
            {
                Id = t.Id,
                Name = t.Name
            });
        }

        public override async Task UpdateEntityAsync()
        {
            if (IsNewExpense)
            {
                await AddEntityAsync();
                return;
            }

            var ret = await _updateExpenseUseCase.ExecuteAsync(new Expense
            {
                Id = Expense.Id,
                Amount = Expense.Amount,
                Description = Expense.Description,
                ExpenseDate = Expense.ExpenseDate,
                ExpenseTypeId = Expense.ExpenseTypeId,
                TagId = Expense.TagId,
                EventId = Expense.EventId,
                Note = Expense.Note,
                MerchantId = Expense.MerchantId
            });

            if (ret.IsSuccess)
                await Shell.Current.GoToAsync("..");
        }

        public override async Task AddEntityAsync()
        {
            var ret = await _addExpenseUseCase.ExecuteAsync(new Expense
            {
                Amount = Expense.Amount,
                Description = Expense.Description,
                ExpenseDate = Expense.ExpenseDate,
                ExpenseTypeId = Expense.ExpenseTypeId,
                TagId = Expense.TagId,
                EventId = Expense.EventId,
                Note = Expense.Note,
                MerchantId = Expense.MerchantId
            });

            if (ret.IsSuccess)
                await Shell.Current.GoToAsync("..");
        }

        public override async Task SearchEntityAsync()
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        public async Task CancelUpdateExpense()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

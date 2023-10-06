﻿
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
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

        private readonly IViewExpenseTypesUseCase _viewExpenseTypesUseCase;
        private readonly IViewMerchantsUseCase _viewMerchantsUseCase;
        private readonly IViewExpenseTagsUseCase _viewExpenseTagsUseCase;
        private readonly IUpdateExpenseUseCase _updateExpenseUseCase;
        private readonly IViewExpenseEventsUseCase _viewExpenseEventsUseCase;

        public ExpenseDetailsViewModel(IViewExpenseTypesUseCase viewExpenseTypesUseCase,
                                       IViewMerchantsUseCase viewMerchantsUseCase,
                                       IViewExpenseTagsUseCase viewExpenseTagsUseCase,
                                       IUpdateExpenseUseCase updateExpenseUseCase,
                                       IViewExpenseEventsUseCase viewExpenseEventsUseCase)
        {
            _viewExpenseTypesUseCase = viewExpenseTypesUseCase;
            _viewMerchantsUseCase = viewMerchantsUseCase;
            _viewExpenseTagsUseCase = viewExpenseTagsUseCase;
            _updateExpenseUseCase = updateExpenseUseCase;
            _viewExpenseEventsUseCase = viewExpenseEventsUseCase;

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
            var ret = await _updateExpenseUseCase.ExecuteAsync(Expense);
            if (ret)
                await Shell.Current.GoToAsync("..");
        }

        public override async Task AddEntityAsync()
        {

        }

        public override async Task SearchEntityAsync()
        {

        }

        [RelayCommand]
        public async Task CancelUpdateExpense()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

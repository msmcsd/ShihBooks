using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ShihBooks.Core;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.UseCases.Interfaces.ExpenseTypes;
using System.Diagnostics;
using ShihBooks.UseCases.UseCases.ExpenseTypes;
using CommunityToolkit.Mvvm.Input;

namespace ShihBooks.ViewModels
{
    public partial class ManageExpenseTypesViewModel : BaseViewModel
    {
        private readonly IViewExpenseTypesUseCase _viewExpenseTypesUseCase;
        private readonly IAddExpenseTypeUseCase _addExpenseTypeUseCase;
        private readonly IUpdateExpenseTypeUseCase _updateExpenseTypeUseCase;
        private readonly IDeleteExpenseTypeUseCase _deleteExpenseTypeUseCase;

        public List<ExpenseType> ExpenseTypes { get; set; } = new();

        public ObservableCollection<ExpenseType> FilteredItems { get; set; } = new();

        [ObservableProperty]
        private string _searchText;

        public ManageExpenseTypesViewModel(IViewExpenseTypesUseCase viewExpenseTypesUseCase,
                                           IAddExpenseTypeUseCase addExpenseTypeUseCase,
                                           IUpdateExpenseTypeUseCase updateExpenseTypeUseCase,
                                           IDeleteExpenseTypeUseCase deleteExpenseTypeUseCase)
        {
            _viewExpenseTypesUseCase = viewExpenseTypesUseCase;
            _addExpenseTypeUseCase = addExpenseTypeUseCase;
            _updateExpenseTypeUseCase = updateExpenseTypeUseCase;
            _deleteExpenseTypeUseCase = deleteExpenseTypeUseCase;
        }

        public async Task GetAllExpenseTypesAsync()
        {
            if (IsBusy) return;

            if (FilteredItems.Count > 0)
            {
                FilteredItems.Clear();
            }

            try
            {
                IsBusy = true;

                ExpenseTypes = await _viewExpenseTypesUseCase.ExecuteAsync();
                if (ExpenseTypes?.Count > 0)
                {
                    foreach (var tag in ExpenseTypes)
                    {
                        FilteredItems.Add(tag);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

        internal async Task AddExpenseType(string name)
        {
            var ret = await _addExpenseTypeUseCase.ExecuteAsync(name);
            if (ret)
            {
                await GetAllExpenseTypesAsync();
            }
        }

        internal async Task UpdateExpenseType(int id, string name)
        {
            var ret = await _updateExpenseTypeUseCase.ExecuteAsync(id, name);
            if (ret)
            {
                await GetAllExpenseTypesAsync();
            }
        }

        [RelayCommand]
        public async Task DeleteExpenseTypeAsync(ExpenseType expenseType)
        {
            var ret = await _deleteExpenseTypeUseCase.ExecuteAsync(expenseType.Id);
            if (ret == 0)
                FilteredItems.Remove(expenseType);
        }

        public async Task PerformSearchAsync()
        {
            if (SearchText == null) return;

            var list = SearchText.Length <= 0 ? ExpenseTypes :
                                                ExpenseTypes.Where(t => t.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

            if (FilteredItems.Count() > 0) FilteredItems.Clear();
            foreach (var t in list) FilteredItems.Add(t);
        }
    }
}

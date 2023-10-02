using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.Entities;
using ShihBooks.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShihBooks.ViewModels
{
    public partial class ManageEntitiesViewModel : BaseViewModel
    {
        private readonly IViewEntitiesUseCase _viewEntitiesUseCase;
        private readonly IAddEntityUseCase _addEntityUseCase;
        private readonly IUpdateEntityUseCase _updateEntityUseCase;
        private readonly IDeleteEntityUseCase _deleteEntityUseCase;

        private List<ExpenseEntity> _cachedEntities { get; set; } = new();

        public ObservableCollection<ExpenseEntity> FilteredEntities { get; set; } = new();

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private ExpenseEntity _selectedEntity;

        public ManageEntitiesViewModel(IViewEntitiesUseCase viewEntitiesUseCase,
                                       IAddEntityUseCase addEntityUseCase,
                                       IUpdateEntityUseCase updateEntityUseCase,
                                       IDeleteEntityUseCase deleteEntityUseCase)
        {                                     
            _viewEntitiesUseCase = viewEntitiesUseCase;
            _addEntityUseCase = addEntityUseCase;
            _updateEntityUseCase = updateEntityUseCase;
            _deleteEntityUseCase = deleteEntityUseCase;
        }

        public async Task GetExpenseEntitiesAsync()
        {
            if (IsBusy) return;

            if (FilteredEntities.Count > 0)
            {
                FilteredEntities.Clear();
            }

            try
            {
                IsBusy = true;

                _cachedEntities = await _viewEntitiesUseCase.ExecuteAsync();
                if (_cachedEntities?.Count() > 0)
                {
                    foreach (var tag in _cachedEntities)
                    {
                        FilteredEntities.Add(tag);
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

        [RelayCommand]
        public async Task DeleteEntityAsync(ExpenseEntity expenseEntity)
        {
            var ret = await _deleteEntityUseCase.ExecuteAsync(expenseEntity.Id);
            if (ret == 0)
                FilteredEntities.Remove(expenseEntity);
        }

        [RelayCommand]
        public async Task AddEntity()
        {
            var name = await Shell.Current.CurrentPage.ShowPopupAsync(new ManageItemPopupPage(true, "Enter name:", ""));
            if (name is null)
            {
                return;
            }

            var ret = await _addEntityUseCase.ExecuteAsync(name as string);
            if (ret)
            {
                await GetExpenseEntitiesAsync();
                SelectedEntity = null;
            }
        }

        [RelayCommand]
        public async Task UpdateEntity()
        {
            if (SelectedEntity == null) return;

            var origEntityName = SelectedEntity.Name;

            var name = await Shell.Current.CurrentPage.ShowPopupAsync(new ManageItemPopupPage(false, "Enter new name:", origEntityName));
            if (name is null)
            {
                return;
            }

            var newEntityName = name as string;
            if (newEntityName != origEntityName)
            {
                var origSearchText = SearchText;

                var ret = await _updateEntityUseCase.ExecuteAsync(SelectedEntity.Id, newEntityName);
                if (ret)
                {
                    await GetExpenseEntitiesAsync();
                }

                if (!string.IsNullOrEmpty(origSearchText))
                {
                    SearchText = "";
                }
            }
        }

        [RelayCommand]
        public async Task SearchEntity()
        {
            var list = SearchText?.Length <= 0 ? 
                _cachedEntities :
                _cachedEntities.Where(t => t.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            if (FilteredEntities.Count > 0) FilteredEntities.Clear();

            foreach (var t in list)
            {
                FilteredEntities.Add(t);
            }
        }
    }
}

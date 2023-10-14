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

        private List<CoreEntity> _cachedEntities { get; set; } = new();

        public ObservableCollection<CoreEntity> FilteredEntities { get; set; } = new();

        //[ObservableProperty]
        //private string _searchText;

        [ObservableProperty]
        private CoreEntity _selectedEntity;

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

        public override async Task GetEntitiesAsync()
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
                if (_cachedEntities?.Count > 0)
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
        public async Task DeleteEntityAsync(CoreEntity expenseEntity)
        {
            var ret = await _deleteEntityUseCase.ExecuteAsync(expenseEntity.Id);
            if (ret > 0)
            {
                _cachedEntities?.Remove(expenseEntity);
                FilteredEntities.Remove(expenseEntity);
            }
        }

        public override async Task AddEntityAsync()
        {
            var name = await Shell.Current.CurrentPage.ShowPopupAsync(new ManageItemPopupPage(true, "Enter name:", ""));
            if (name is null)
            {
                return;
            }

            var ret = await _addEntityUseCase.ExecuteAsync(name as string);
            if (ret)
            {
                await GetEntitiesAsync();
                SelectedEntity = null;
            }
        }

        public override async Task UpdateEntityAsync()
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
                    await GetEntitiesAsync();
                }

                if (!string.IsNullOrEmpty(origSearchText))
                {
                    SearchText = "";
                }
            }
        }

        public override async Task SearchEntityAsync()
        {
            var list = SearchText?.Length <= 0 ? 
                _cachedEntities :
                _cachedEntities.Where(t => t.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase));

            if (FilteredEntities.Count > 0) FilteredEntities.Clear();

            foreach (var t in list)
            {
                FilteredEntities.Add(t);
            }
        }
    }
}

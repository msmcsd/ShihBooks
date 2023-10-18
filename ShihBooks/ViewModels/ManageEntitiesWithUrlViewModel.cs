using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.Entities;
using ShihBooks.UseCases.Interfaces.EntityWithUrls;
using ShihBooks.Views;
using System.Collections.ObjectModel;

namespace ShihBooks.ViewModels
{
    public partial class ManageEntitiesWithUrlViewModel : BaseViewModel
    {
        private readonly IViewEntitiesWithUrlUseCase _viewEntitiesWithUrlUseCase;
        private readonly IAddEntityWithUrlUseCase _addEntityWithUrlUseCase;
        private readonly IUpdateEntityWithUrlUseCase _updateEntityWithUrlUseCase;
        private readonly IDeleteEntityUseCase _deleteEntityUseCase;
        private readonly string _entityName;

        private List<CoreEntityWithUrl> _cachedEntities { get; set; } = new();

        public ObservableCollection<CoreEntityWithUrl> FilteredEntities { get; set; } = new();

        [ObservableProperty]
        private CoreEntityWithUrl _selectedEntity;


        public ManageEntitiesWithUrlViewModel(IViewEntitiesWithUrlUseCase viewEntitiesWithUrlUseCase,
                                              IAddEntityWithUrlUseCase addEntityWithUrlUseCase,
                                              IUpdateEntityWithUrlUseCase updateEntityWithUrlUseCase,
                                              IDeleteEntityUseCase  deleteEntityUseCase, 
                                              string entityName)
        {
            _viewEntitiesWithUrlUseCase = viewEntitiesWithUrlUseCase;
            _addEntityWithUrlUseCase = addEntityWithUrlUseCase;
            _updateEntityWithUrlUseCase = updateEntityWithUrlUseCase;
            _deleteEntityUseCase = deleteEntityUseCase;
            _entityName = entityName;
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

                _cachedEntities = await _viewEntitiesWithUrlUseCase.ExecuteAsync();
                if (_cachedEntities?.Count > 0)
                {
                    foreach (var source in _cachedEntities)
                    {
                        FilteredEntities.Add(source);
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task DeleteEntityAsync(CoreEntityWithUrl entity)
        {
            if (entity == null)
                return;

            var ret = await _deleteEntityUseCase.ExecuteAsync(entity.Id);
            if (ret.IsSuccess)
            {
                _cachedEntities?.Remove(entity);
                FilteredEntities.Remove(entity);
            }
        }

        public override async Task AddEntityAsync()
        {
            var m = (CoreEntityWithUrl)await Shell.Current.CurrentPage.ShowPopupAsync(new ManageEntityWithUrlPopupPage(_entityName));
            if (m is null)
            {
                return;
            }

            var ret = await _addEntityWithUrlUseCase.ExecuteAsync(m.Name, m.ImageUrl);
            if (ret.IsSuccess)
            {
                await GetEntitiesAsync();
                SelectedEntity = null;
            }
        }

        public override async Task UpdateEntityAsync()
        {
            if (SelectedEntity == null) return;

            var origMerchantName = SelectedEntity.Name;
            var origImageUrl = SelectedEntity.ImageUrl;

            var newSource = (CoreEntityWithUrl)await Shell.Current.CurrentPage.ShowPopupAsync(new ManageEntityWithUrlPopupPage(_entityName, SelectedEntity.Name, SelectedEntity.ImageUrl));
            if (newSource is null)
            {
                return;
            }

            if (newSource.Name != origMerchantName ||
               (string.IsNullOrWhiteSpace(newSource.ImageUrl) && !string.IsNullOrWhiteSpace(origImageUrl)) ||
               (!string.IsNullOrWhiteSpace(newSource.ImageUrl) && string.IsNullOrWhiteSpace(origImageUrl)) ||
               (newSource.ImageUrl != null && origImageUrl != null && newSource.ImageUrl.ToLower() != origImageUrl.ToLower()))
            {
                var origSearchText = SearchText;

                var ret = await _updateEntityWithUrlUseCase.ExecuteAsync(SelectedEntity.Id, newSource.Name, newSource.ImageUrl);
                if (ret.IsSuccess)
                {
                    await GetEntitiesAsync();
                    SelectedEntity = null;
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

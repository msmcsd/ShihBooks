using CommunityToolkit.Maui.Views;
using ShihBooks.UseCases.Interfaces.Merchants;
using ShihBooks.UseCases.Interfaces.Entities;
using ShihBooks.Views;
using ShihBooks.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ShihBooks.ViewModels
{
    public partial class ManageMerchantsViewModel : BaseViewModel
    {
        private readonly IViewMerchantsUseCase _viewMerchantsUseCase;
        private readonly IAddMerchantUseCase _addMerchantUseCase;
        private readonly IUpdateMerchantUseCase _updateMerchantUseCase;
        private readonly IDeleteMerchantUseCase _deleteMerchantUseCase;

        private List<Merchant> _cachedEntities { get; set; } = new();

        public ObservableCollection<Merchant> FilteredEntities { get; set; } = new();

        [ObservableProperty]
        private Merchant _selectedMerchant;

        public ManageMerchantsViewModel(IViewMerchantsUseCase viewMerchantsUseCase,
                                        IAddMerchantUseCase addMerchantUseCase,
                                        IUpdateMerchantUseCase updateMerchantUseCase,
                                        IDeleteMerchantUseCase deleteMerchantUseCase)
        {
            _viewMerchantsUseCase = viewMerchantsUseCase;
            _addMerchantUseCase = addMerchantUseCase;
            _updateMerchantUseCase = updateMerchantUseCase;
            _deleteMerchantUseCase = deleteMerchantUseCase;
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

                _cachedEntities = await _viewMerchantsUseCase.ExecuteAsync();
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
                //Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task DeleteEntityAsync(Merchant merchant)
        {
            var ret = await _deleteMerchantUseCase.ExecuteAsync(merchant.Id);
            if (ret.IsSuccess)
                FilteredEntities.Remove(merchant);
        }

        public override async Task AddEntityAsync()
        {
            var m = (Merchant)await Shell.Current.CurrentPage.ShowPopupAsync(new ManageMerchantPopupPage());
            if (m is null)
            {
                return;
            }

            var ret = await _addMerchantUseCase.ExecuteAsync(m.Name, m.ImageUrl);
            if (ret.IsSuccess)
            {
                await GetEntitiesAsync();
                SelectedMerchant = null;
            }
        }

        public override async Task UpdateEntityAsync()
        {
            if (SelectedMerchant == null) return;

            var origMerchantName = SelectedMerchant.Name;
            var origImageUrl = SelectedMerchant.ImageUrl;

            var newMerchant = (Merchant)await Shell.Current.CurrentPage.ShowPopupAsync(new ManageMerchantPopupPage(SelectedMerchant.Name, SelectedMerchant.ImageUrl));
            if (newMerchant is null)
            {
                return;
            }

            if (newMerchant.Name != origMerchantName ||
               (string.IsNullOrWhiteSpace(newMerchant.ImageUrl) && !string.IsNullOrWhiteSpace(origImageUrl)) ||
               (!string.IsNullOrWhiteSpace(newMerchant.ImageUrl) && string.IsNullOrWhiteSpace(origImageUrl)) ||
               (newMerchant.ImageUrl.ToLower() != origImageUrl.ToLower()))
            {
                var origSearchText = SearchText;

                var ret = await _updateMerchantUseCase.ExecuteAsync(SelectedMerchant.Id, newMerchant.Name, newMerchant.ImageUrl);
                if (ret.IsSuccess)
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

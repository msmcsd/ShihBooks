using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ShihBooks.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty]
        private string _searchText;

        public bool IsNotBusy => !IsBusy;

        public BaseViewModel()
        {
            
        }

        public virtual Task GetEntitiesAsync()
        {
            return null;
        }

        [RelayCommand]
        public virtual Task AddEntityAsync()
        {
            return null;
        }

        [RelayCommand]
        public virtual Task UpdateEntityAsync()
        {
            return null;
        }

        [RelayCommand]
        public virtual Task SearchEntityAsync()
        {
            return null;
        }

    }
}

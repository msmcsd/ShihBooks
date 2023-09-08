using CommunityToolkit.Mvvm.ComponentModel;

namespace ShihBooks.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        public bool IsNotBusy => !IsBusy;

        public BaseViewModel()
        {
            
        }
    }
}

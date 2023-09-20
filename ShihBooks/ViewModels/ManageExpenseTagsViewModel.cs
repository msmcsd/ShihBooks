using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShihBooks.ViewModels
{
    public class ManageExpenseTagsViewModel : BaseViewModel
    {
        private readonly IViewExpenseTagsUseCase _viewExpenseTagsUseCase;

        public ObservableCollection<ExpenseTag> ExpenseTags { get; set; } = new();

        public ManageExpenseTagsViewModel(IViewExpenseTagsUseCase viewExpenseTagsUseCase)
        {
            _viewExpenseTagsUseCase = viewExpenseTagsUseCase;
        }

        public async Task GetAllExpenseTagsAsync()
        {
            if (IsBusy) return;

            if (ExpenseTags.Count > 0)
            {
                ExpenseTags.Clear();
            }

            try
            {
                IsBusy = true;

                var tags = await _viewExpenseTagsUseCase.ExecuteAsync();
                if (tags?.Count > 0)
                {
                    foreach (var tag in tags)
                    {
                        ExpenseTags.Add(tag);
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
    }
}

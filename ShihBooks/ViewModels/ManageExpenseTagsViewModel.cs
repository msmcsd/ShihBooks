using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShihBooks.ViewModels
{
    public class ManageExpenseTagsViewModel : BaseViewModel
    {
        private readonly IViewExpenseTagsUseCase _viewExpenseTagsUseCase;
        private readonly ISaveExpenseTagUseCase _saveExpenseTagUseCase;
        private readonly IUpdateExpenseTagUseCase _updateExpenseTagUseCase;

        public ObservableCollection<ExpenseTag> ExpenseTags { get; set; } = new();

        public ManageExpenseTagsViewModel(IViewExpenseTagsUseCase viewExpenseTagsUseCase,
                                          ISaveExpenseTagUseCase saveExpenseTagUseCase,
                                          IUpdateExpenseTagUseCase updateExpenseTagUseCase)
        {
            _viewExpenseTagsUseCase = viewExpenseTagsUseCase;
            _saveExpenseTagUseCase = saveExpenseTagUseCase;
            _updateExpenseTagUseCase = updateExpenseTagUseCase;
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

        public async Task SaveExpenseTag(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName)) return;

            await _saveExpenseTagUseCase.ExecuteAsync(tagName);

            await GetAllExpenseTagsAsync();
        }

        public async Task<bool> UpdateExpenseTag(int tagId, string tagName)
        {
            bool ret = await _updateExpenseTagUseCase.ExecuteAsync(tagId, tagName);
            if (ret)
                await GetAllExpenseTagsAsync();

            return ret;
        }
    }
}

using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.UseCases.ExpenseTags;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShihBooks.ViewModels
{
    public partial class ManageExpenseTagsViewModel : BaseViewModel
    {
        private readonly IViewExpenseTagsUseCase _viewExpenseTagsUseCase;
        private readonly ISaveExpenseTagUseCase _saveExpenseTagUseCase;
        private readonly IUpdateExpenseTagUseCase _updateExpenseTagUseCase;
        private readonly IDeleteExpenseTagUseCase _deleteExpenseTagUseCase;

        public ObservableCollection<ExpenseTag> ExpenseTags { get; set; } = new();

        public ManageExpenseTagsViewModel(IViewExpenseTagsUseCase viewExpenseTagsUseCase,
                                          ISaveExpenseTagUseCase saveExpenseTagUseCase,
                                          IUpdateExpenseTagUseCase updateExpenseTagUseCase,
                                          IDeleteExpenseTagUseCase deleteExpenseTagUseCase)
        {
            _viewExpenseTagsUseCase = viewExpenseTagsUseCase;
            _saveExpenseTagUseCase = saveExpenseTagUseCase;
            _updateExpenseTagUseCase = updateExpenseTagUseCase;
            _deleteExpenseTagUseCase = deleteExpenseTagUseCase;
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

        [RelayCommand]
        public async Task DeleteExpenseTagAsync(ExpenseTag expenseTag)
        {
            var ret = await _deleteExpenseTagUseCase.ExecuteAsync(expenseTag.Id);
            if (string.IsNullOrEmpty(ret))
                ExpenseTags.Remove(expenseTag);
        }
    }
}

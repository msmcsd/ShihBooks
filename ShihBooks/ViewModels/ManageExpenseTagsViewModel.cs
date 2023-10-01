using CommunityToolkit.Mvvm.ComponentModel;
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
        private readonly IAddExpenseTagUseCase _saveExpenseTagUseCase;
        private readonly IUpdateExpenseTagUseCase _updateExpenseTagUseCase;
        private readonly IDeleteExpenseTagUseCase _deleteExpenseTagUseCase;

        public List<ExpenseTag> ExpenseTags { get; set; } = new();

        public ObservableCollection<ExpenseTag> FilteredTags { get; set; } = new();

        [ObservableProperty]
        private string _searchText;

        public ManageExpenseTagsViewModel(IViewExpenseTagsUseCase viewExpenseTagsUseCase,
                                          IAddExpenseTagUseCase saveExpenseTagUseCase,
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

            if (FilteredTags.Count > 0)
            {
                FilteredTags.Clear();
            }

            try
            {
                IsBusy = true;

                ExpenseTags = await _viewExpenseTagsUseCase.ExecuteAsync();
                if (ExpenseTags?.Count > 0)
                {
                    foreach (var tag in ExpenseTags)
                    {
                        FilteredTags.Add(tag);
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

        public async Task AddExpenseTag(string tagName)
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
                FilteredTags.Remove(expenseTag);
        }

        public async Task PerformSearchAsync()
        {
            if (SearchText == null) return;

            var list = SearchText.Length <= 0 ? ExpenseTags : 
                                                ExpenseTags.Where(t => t.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
            
            if (FilteredTags.Count() > 0) FilteredTags.Clear();
            foreach(var t in list) FilteredTags.Add(t);
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using ShihBooks.Core;
using ShihBooks.Core.Incomes;
using ShihBooks.UseCases.Interfaces.IncomeRecipients;
using ShihBooks.UseCases.Interfaces.Incomes;
using ShihBooks.UseCases.Interfaces.IncomeSources;

namespace ShihBooks.ViewModels
{
    [QueryProperty("Income", "Income")]
    public partial class IncomeDetailsViewModel : BaseViewModel
    {
        private readonly IViewIncomeSourcesUseCase _viewIncomeSourcesUseCase;
        private readonly IViewIncomeRecipientsUseCase _viewIncomeRecipientsUseCase;
        private readonly IAddIncomeUseCase _addInomeUseCase;
        private readonly IUpdateIncomeUseCase _updateIncomeUseCase;
        private readonly IDeleteIncomeUseCase _deleteIncomeUseCase;
        [ObservableProperty]
        private IncomeSource _selectedIncomeSource;

        [ObservableProperty]
        private IncomeRecipient _selectedIncomeRecipient;

        [ObservableProperty]
        private IncomeDetails _income;

        [ObservableProperty]
        private bool _isNewIncome;

        public List<IncomeSource> IncomeSources { get; set; } = new();

        public List<IncomeRecipient> IncomeRecipients { get; set; } = new();

        public IncomeDetailsViewModel(IViewIncomeSourcesUseCase viewIncomeSourcesUseCase,
                                      IViewIncomeRecipientsUseCase viewIncomeRecipientsUseCase,
                                      IAddIncomeUseCase addInomeUseCase,
                                      IUpdateIncomeUseCase updateIncomeUseCase,
                                      IDeleteIncomeUseCase deleteIncomeUseCase)
        {
            _viewIncomeSourcesUseCase = viewIncomeSourcesUseCase;
            _viewIncomeRecipientsUseCase = viewIncomeRecipientsUseCase;
            _addInomeUseCase = addInomeUseCase;
            _updateIncomeUseCase = updateIncomeUseCase;
            _deleteIncomeUseCase = deleteIncomeUseCase;

            Task.Run(GetEntitiesAsync);
        }

        public async override Task GetEntitiesAsync()
        {
            var sources = await _viewIncomeSourcesUseCase.ExecuteAsync();
            IncomeSources = sources.ConvertAll(s => new IncomeSource
            {
                Id = s.Id,
                Name = s.Name,
                ImageUrl = s.ImageUrl
            });
            
            var ret = await _viewIncomeRecipientsUseCase.ExecuteAsync();
            IncomeRecipients = ret.ConvertAll(s => new IncomeRecipient
            {
                Id = s.Id,
                Name = s.Name
            });
        }

        public override async Task AddEntityAsync()
        {
            var ret = await _addInomeUseCase.ExecuteAsync(new Income
            {
                Amount = Income.Amount,
                IncomeDate = Income.IncomeDate,
                Description = Income.Description,
                SourceId = Income.SourceId,
                RecipientId = Income.RecipientId,
                Note = Income.Note,
            });

            if (ret.IsSuccess)
                await Shell.Current.GoToAsync("..");
        }

        public override async Task UpdateEntityAsync()
        {
            if (IsNewIncome)
            {
                await AddEntityAsync();
                return;
            }
          
            var ret = await _updateIncomeUseCase.ExecuteAsync(Income);

            if (ret.IsSuccess)
                await Shell.Current.GoToAsync("..");
        }
    }
}

using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.ViewModels
{
    public class ExpenseViewModel
    {
        private readonly IViewExpensesByMonthUseCase _viewExpensesByMonthUseCase;

        public ObservableCollection<Expense> Expenses { get; set; } = new();

        public ExpenseViewModel(IViewExpensesByMonthUseCase viewExpensesByMonthUseCase)
        {
            _viewExpensesByMonthUseCase = viewExpensesByMonthUseCase;
        }

        public async Task LoadExpensesByMonthAsync(int year, int month)
        {
            Expenses.Clear();
            var expenses = await _viewExpensesByMonthUseCase.ExecuteAsync(year, month);

            if (expenses?.Count > 0)
            {
                foreach (var e in expenses)
                {
                    Expenses.Add(e);
                }
            }
        }
    }
}

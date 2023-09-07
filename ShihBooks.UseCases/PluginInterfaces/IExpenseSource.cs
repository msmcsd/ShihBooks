using ShihBooks.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShihBooks.UseCases.PluginInterfaces
{
    public interface IExpenseSource
    {
        Task<List<Expense>> GetExpenses(int year, int month);
    }
}

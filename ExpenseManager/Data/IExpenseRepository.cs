using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Data
{
    public interface IExpenseRepository
    {
        Task<Expense> GetExenseByIdAsync(string id);
        Task<IEnumerable<Expense>> GetAllExpenseAsync();
        Task CreateExpenseAsync(Expense expense);
        //Task CreateBulkExpenseAsync(List<Expense> expenses);
        Task EditExpenseAsync(string id, Expense expense);
        Task DeleteExpenseAsync(string id);
    }
}

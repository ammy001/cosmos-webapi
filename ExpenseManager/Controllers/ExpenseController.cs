using ExpenseManager.Data;
using ExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            return Ok(await _expenseRepository.GetAllExpenseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(string id)
        {
            return Ok(await _expenseRepository.GetExenseByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
        {
            expense.Id = Guid.NewGuid().ToString();
            await _expenseRepository.CreateExpenseAsync(expense);
            return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateBulkExpense([FromBody] List<Expense> expenses)
        //{
        //    await _expenseRepository.CreateBulkExpenseAsync(expenses);
        //    return Ok();
        //    //return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
        //}

        [HttpPut]
        public async Task<IActionResult> EditExpense(Expense expense)
        {
            await _expenseRepository.EditExpenseAsync(expense.Id, expense);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(string id)
        {
            await _expenseRepository.DeleteExpenseAsync(id);
            return NoContent();
        }
    }
}

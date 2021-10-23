using ExpenseManager.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Data
{
    public class ExpenseRepository : IExpenseRepository
    {
        private Container _container;

        public ExpenseRepository(CosmosClient client, string databaseName, string containerName)
        {
            _container = client.GetContainer(databaseName, containerName);
        }

        public async Task CreateExpenseAsync(Expense expense)
        {
            try
            {
                await _container.CreateItemAsync<Expense>(expense, new PartitionKey(expense.Id), new ItemRequestOptions { PreTriggers = new List<string> { "Addtimestamp" } });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateBulkExpenseAsync(List<Expense> expenses)
        {
            try
            {
                List<Task> _tasks = new List<Task>();

                foreach (Expense expense in expenses)
                {
                    expense.Id = Guid.NewGuid().ToString();
                    _tasks.Add(_container.CreateItemAsync<Expense>(expense, new PartitionKey(expense.Id)));
                }

                await Task.WhenAll(_tasks);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteExpenseAsync(string id)
        {
            await _container.DeleteItemAsync<Expense>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<Expense>> GetAllExpenseAsync()
        {
            var query = _container.GetItemQueryIterator<Expense>(new QueryDefinition("SELECT * FROM c"));

            var result = new List<Expense>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response.ToList());
            }

            return result;
        }

        public async Task<Expense> GetExenseByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Expense>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task EditExpenseAsync(string id, Expense expense)
        {
            await _container.UpsertItemAsync(expense, new PartitionKey(id));
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<List<TodoItem>> SearchByNameAsync(string searchString);
        Task<TodoItem> GetAsync(long id);

        Task  AddAsync(TodoItem todoItem);
        Task  UpdateAsync(TodoItem todoItem);
        Task  RemoveAsync(TodoItem todoItem);
    }
}
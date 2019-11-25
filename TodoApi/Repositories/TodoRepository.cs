using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> GetAsync(long id)
        {
            var todoItem = await _context.TodoItems.Include(x => x.Responsible).FirstOrDefaultAsync(i => i.Id ==id);
            return todoItem;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            var todoItems =await _context.TodoItems.Include(x => x.Responsible).ToListAsync();
            return todoItems;
        }

        public async Task AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem item)
        {
             _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TodoItem>> SearchByNameAsync(string searchString)
        {
            return await _context.TodoItems.Where(x => x.Name.ToLowerInvariant().Contains(searchString.ToLowerInvariant())).ToListAsync();
        }
    }
}
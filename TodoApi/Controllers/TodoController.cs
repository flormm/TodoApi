using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Dto;
using TodoApi.Errors;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITodoRepository _todoRepository;

        public TodoController(IMapper mapper,
                        UserManager<ApplicationUser> userManager, ITodoRepository todoRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _todoRepository =todoRepository;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var todoItems =await _todoRepository.GetAllAsync();
            return Ok(todoItems);
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            var todoItem = await _todoRepository.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound(new ErrorMessage("Invalid id"));
            }

            return Ok(todoItem);
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(NewTodoItemDTO itemDTO)
        {
            var item =_mapper.Map<TodoItem>(itemDTO);
            await _todoRepository.AddAsync(item);

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            await _todoRepository.UpdateAsync(item);
            return NoContent();
        }
        // PATCH: api/Todo/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTodoItem(long id, UpdateTodoItemDTO changes)
        {
            var todoItem = await _todoRepository.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound(new ErrorMessage("Invalid id"));
            }
            switch (changes.Field)
            {
                case "Name":
                    todoItem.Name =changes.Value;
                    break;

                case "IsCompleted":
                    todoItem.IsComplete = bool.Parse(changes.Value);
                    break;

                case "Responsible":
                    var user = await _userManager.Users.Where( u => u.Id == changes.Value).FirstOrDefaultAsync();
                    if(user !=null)
                    {
                        todoItem.Responsible =user;
                    }else
                    {
                        return BadRequest(new ErrorMessage("User not found"));
                        //throw new System.Exception("Se pudrio todo");
                    }
                    
                    break;                
                case "Id":
                    return Forbid();
                default :
                    return BadRequest(new ErrorMessage("Invalid field"));
            }
            await _todoRepository.UpdateAsync(todoItem);

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoRepository.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoRepository.RemoveAsync(todoItem);

            return NoContent();
        }
        
        [HttpGet("search")]
        public async Task<IActionResult> GetAsync(string searchString)
        {
            List<TodoItem> result = null;
            if(searchString ==null)
            {
                result = await _todoRepository.GetAllAsync();
            }else
            {
                result = await _todoRepository.SearchByNameAsync(searchString);
            }
            return Ok(result);
        }
    }
}
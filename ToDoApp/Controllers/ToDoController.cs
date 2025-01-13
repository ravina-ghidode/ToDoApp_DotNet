using Database;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToDoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _unitOfWork.TodoItems.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<TodoItem?> GetById(int id)
        {
            var item = await _unitOfWork.TodoItems.GetByIdAsync(id);
            
            return item;
        }
        [HttpPost]
        public void AddData([FromBody]TodoItem item)
        {
            _unitOfWork.TodoItems.Add(item);
             _unitOfWork.Complete();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoItem(int id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest("Invalid data.");
            }
            var todo = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if(todo == null)
            {
                return NotFound();
            }
            todo.Title = item.Title;
            todo.DateCreated = item.DateCreated;
            todo.IsCompleted = item.IsCompleted;

            _unitOfWork.TodoItems.Update(todo);
            _unitOfWork.Complete();
            return Ok(todo);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var todo = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if(todo == null)
            {
                return NotFound();
            }
            _unitOfWork.TodoItems.Remove(todo);
            _unitOfWork.Complete();
            return NoContent();

        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedTodos([FromQuery] PagingParameters pagingParameters)
        {
            var todos = await _unitOfWork.TodoItems.GetPagedDataAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
            return Ok(todos);
        }
    }
}

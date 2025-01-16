using Database;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Utility;

namespace ToDoApp.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<ServiceResponse<IEnumerable<TodoItem>>>> GetAll()
        {
            var response = new ServiceResponse<IEnumerable<TodoItem>>();
            var allItems = await _unitOfWork.TodoItems.GetAllToDoItemsAsync();
            var serviceResponse = response.GetResponse(allItems, "All todos Retrived successfully", true);
            return serviceResponse.IsSuccessful ? Ok(serviceResponse) : NoContent();
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<TodoItem?>>> GetById(int id)
        {
            var response = new ServiceResponse<TodoItem?>();
            var item = await _unitOfWork.TodoItems.GetTodoByIdAsync(id);
            if(item is null)
            {
                return NotFound(response.GetResponse(null, "todoId Not found", false));
            }
            var serviceResponse = response.GetResponse(item, "A Todo Retrived Successfully", true);
            return serviceResponse.IsSuccessful ? Ok(serviceResponse) : NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<TodoItem>>> AddData(TodoItem item)
        {
            var response = new ServiceResponse<TodoItem>();
            if(item is null || string.IsNullOrEmpty(item.Title))
            {
                return BadRequest(response.GetResponse(item, "Item or title cannot be left empty"));
            }
            var todo = await _unitOfWork.TodoItems.AddTodoItemAsync(item);
            await _unitOfWork.CompleteAsync();

            return Ok(response.GetResponse(todo, "Todo item added successfully", true));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<TodoItem>>> UpdateToDoItem(int id, TodoItem item)
        {
            var response = new ServiceResponse<TodoItem>();
            if (item == null || string.IsNullOrEmpty(item.Title))
            {
                return BadRequest(response.GetResponse(null, "Invalid data"));
            }
            var todo = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if(todo == null)
            {
                return NotFound(response.GetResponse(null, "Todo Item not found"));
            }
            todo.Title = item.Title;
            todo.DateCreated = item.DateCreated;
            todo.IsCompleted = item.IsCompleted;

            var updatedTodo =  _unitOfWork.TodoItems.UpdateTodoItem(todo);
            await _unitOfWork.CompleteAsync();
            return Ok(response.GetResponse(updatedTodo, "Todo Item updated successfully", true));

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<TodoItem>>> DeleteToDoItem(int id)
        {
            var response = new ServiceResponse<TodoItem>();
            var itemFromDb = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if(itemFromDb == null)
            {
                return NotFound(response.GetResponse(itemFromDb, "Id not found"));
            }

            var deletedItem =  _unitOfWork.TodoItems.DeleteTodoItem(itemFromDb);
            await _unitOfWork.CompleteAsync();
            return Ok(response.GetResponse(deletedItem, "Todo Item deleted Successfully", true));

        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedTodos([FromQuery] PagingParameters pagingParameters)
        {
            var todos = await _unitOfWork.TodoItems.GetPagedDataAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
            return Ok(todos);
        }
    }
}

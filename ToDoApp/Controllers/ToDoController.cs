using AutoMapper;
using Database;
using Entities.Dto_s;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Implementation;
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
        private readonly IMapper _mapper;

        public ToDoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetTodoItemDto>>>> GetAll()
        {
            var response = new ServiceResponse<IEnumerable<GetTodoItemDto>>();
            var allItems = await _unitOfWork.TodoItems.GetAllToDoItemsAsync();
            var itemsToReturn = allItems.Select(item => _mapper.Map<GetTodoItemDto>(item));
            var serviceResponse = response.GetResponse(itemsToReturn, "All todos Retrived successfully", true);
            return serviceResponse.IsSuccessful ? Ok(serviceResponse) : NoContent();
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoItemDto?>>> GetById(int id)
        {
            var response = new ServiceResponse<GetTodoItemDto?>();
            var item = await _unitOfWork.TodoItems.GetTodoByIdAsync(id);
            if(item is null)
            {
                return NotFound(response.GetResponse(null, "todoId Not found", false));
            }
            var itemToReturn = _mapper.Map<GetTodoItemDto>(item);
            var serviceResponse = response.GetResponse(itemToReturn, "A Todo Retrived Successfully", true);
            return serviceResponse.IsSuccessful ? Ok(serviceResponse) : NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetTodoItemDto>>> AddData(TodoItem item)
        {
            var response = new ServiceResponse<GetTodoItemDto>();
            if(item is null || string.IsNullOrEmpty(item.Title))
            {
                return BadRequest(response.GetResponse(null, "Item or title cannot be left empty"));
            }
            var addedItem = await _unitOfWork.TodoItems.AddTodoItemAsync(item);
            await _unitOfWork.CompleteAsync();
            var itemToReturn = _mapper.Map<GetTodoItemDto>(addedItem);
            return Ok(response.GetResponse(itemToReturn, "Todo item added successfully", true));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoItemDto>>> UpdateToDoItem(int id, TodoItem item)
        {
            var response = new ServiceResponse<GetTodoItemDto>();
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

            _unitOfWork.TodoItems.UpdateTodoItem(todo);
            await _unitOfWork.CompleteAsync();

            var updatedItemDto = _mapper.Map<GetTodoItemDto>(todo);
            return Ok(response.GetResponse(updatedItemDto, "Todo Item updated successfully", true));

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoItemDto>>> DeleteToDoItem(int id)
        {
            var response = new ServiceResponse<GetTodoItemDto>();
            var itemFromDb = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if(itemFromDb == null)
            {
                return NotFound(response.GetResponse(null, "Id not found"));
            }

            var deletedItem =  _unitOfWork.TodoItems.DeleteTodoItem(itemFromDb);
            var itemToReturn = _mapper.Map<GetTodoItemDto>(deletedItem);
            await _unitOfWork.CompleteAsync();
            return Ok(response.GetResponse(itemToReturn, "Todo Item deleted Successfully", true));

        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedTodos([FromQuery] PagingParameters pagingParameters)
        {
            var todos = await _unitOfWork.TodoItems.GetPagedDataAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
            return Ok(todos);
        }
    }
}

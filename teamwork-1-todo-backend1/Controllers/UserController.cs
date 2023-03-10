using teamwork_1_todo_backend1.Models;
using teamwork_1_todo_backend1.Services;
using Microsoft.AspNetCore.Mvc;
using teamwork_1_todo_backend1.Dtos;
using Microsoft.AspNetCore.Identity;

namespace teamwork_1_todo_backend1.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ToDoService _todoService;

    public UserController(ToDoService todoService) =>
        _todoService = todoService;

    //[HttpGet]
    //public async Task<List<User>> Get() =>
    //    await _todoService.GetAsync();

    [HttpGet("{userName}", Name = "Getbook")]
    public async Task<ActionResult<User>> Get(string userName)
    {
        var todo = await _todoService.GetAsync(userName);

        if (todo is null)
        {
            return NotFound();
        }

        return todo;
    }

    [HttpPost("SignInUser")]
    public async Task<User> GetUser([FromForm] UserDto newUser)
    {
        var user =  await _todoService.GetAsync1(newUser);
        if (user is null)
        {
            return null;
        }
        return user;
    }

    [HttpPost("SignUpUser")]
    public async Task<IActionResult> Post([FromForm] UserDto newUser)
    {
        await _todoService.CreateAsync(newUser);
        return CreatedAtRoute("Getbook", new { userName = newUser.UserName }, newUser);
    }

    [HttpPost("UpdateTodoList")]
    public bool AddTodoList(ToDoDto data)
    {

        return _todoService.AddToDoList(data);
    }

    //[HttpPut("{userID}")]
    //public async Task<IActionResult> Update(User updatedUser)
    //{
    //    var update = _todoService.UpdateAsync(updatedUser);

    //    if (update is null)
    //    {
    //        return NotFound();
    //    }

    //    return NoContent();
    //}

    [HttpDelete("DeleteUser")]
    public IActionResult Delete([FromQuery] string id)
    {
        var todo = _todoService.GetAsync(id);

        if (todo is null)
        {
            return NotFound();
        }
        else
        {
            _todoService.RemoveAsync(id);
            return Ok();
        }
    }
}
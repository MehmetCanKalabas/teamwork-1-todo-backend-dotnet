using teamwork_1_todo_backend1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using teamwork_1_todo_backend1.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace teamwork_1_todo_backend1.Services;

public class ToDoService
{
    private readonly IMongoCollection<User> _todoCollection;

    public ToDoService(
        IOptions<ToDoDatabaseSettings> toDoDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            toDoDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            toDoDatabaseSettings.Value.DatabaseName);

        _todoCollection = mongoDatabase.GetCollection<User>(
            toDoDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<User>> GetAsync() =>
        await _todoCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string userName) =>
        await _todoCollection.Find(x => x.userName == userName).FirstOrDefaultAsync();

    public async Task<User> GetAsync1(UserDto newUser) =>
       await _todoCollection.Find(x => x.userName == newUser.UserName).FirstOrDefaultAsync();



    //public bool GetUser(UserDto model)
    //{
    //    var search = _todoCollection.Find(x => x.userName == model.UserName).FirstOrDefaultAsync();

    //    if (search != null)
    //    {
    //        return search;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}

    public async Task CreateAsync(UserDto newUser)
    {
        if (newUser != null)
        {
            User user = new User();
            user.userName = newUser.UserName;
            user.password = newUser.Password;
            await _todoCollection.InsertOneAsync(user);
        }
    }
    public bool AddToDoList(ToDoDto model)
    {
        if (model != null)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                var user = _todoCollection.Find(f => f.userID == model.Id).FirstOrDefault();
                if (user != null)
                {
                    if (model.TodoList != null && model.TodoList.Count > 0)
                    {
                        user.todolist = new List<Todolist>();
                        foreach (var item in model.TodoList)
                        {
                            user.todolist.Add(new Todolist()
                            {
                                todoID = item.TodoID,
                                todoTitle = item.Title,
                                isDone = item.IsDone
                            });
                        }
                        _todoCollection.ReplaceOne(x => x.userID == model.Id, user);
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    //public async Task UpdateAsync(User updatedUser) =>
    //    await _todoCollection.ReplaceOneAsync(x => x.userID == updatedUser.userID, updatedUser);


    public void RemoveAsync(string id)
    {
        _todoCollection.DeleteOne(x => x.userID == id);
    }
}
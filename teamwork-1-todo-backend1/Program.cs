using teamwork_1_todo_backend1.Models;
using teamwork_1_todo_backend1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    //options.AddPolicy("Policy1",
    //    policy =>
    //    {
    //        policy.WithOrigins("http://example.com",
    //                            "http://www.contoso.com");
    //    });

    options.AddPolicy("AnotherPolicy",
        policy =>
        {
            policy.WithOrigins("http://www.contoso.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.Configure<ToDoDatabaseSettings>(
    builder.Configuration.GetSection("ToDoDatabase"));

builder.Services.AddSingleton<ToDoService>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

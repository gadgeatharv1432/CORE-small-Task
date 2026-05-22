using DataTask.Context;
using Microsoft.EntityFrameworkCore;
using TaskRepository.Repository.Implementations;
using TaskRepository.Repository.Interfaces;

using TaskService.Services.Implementations;
using TaskService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaskRepository,TaskRepository.Repository.Implementations.TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskService, TaskService.Services.Implementations.TaskService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", 
        policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

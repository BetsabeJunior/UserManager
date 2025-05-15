using Microsoft.EntityFrameworkCore;
using UserManager.Infrastructure.Data;
using UserManager.Domain.Interfaces;
using UserManager.Infrastructure.Repositories;
using UserManager.Application.Interfaces;
using UserManager.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API_PROGRAMACION_DE_SOFTWARE.DAOs;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Utilities;
using API_PROGRAMACION_DE_SOFTWARE.Services;

//using Library.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// This is the default connection string for SQL Server LocalDB.
var connectionString = builder.Configuration.GetConnectionString("SQLServerConnection") ?? throw new InvalidOperationException("Connection string 'SQLServerConnection' is not configured.");

// Configure the SQLServerConfiguration with the connection string
builder.Services.Configure<SQLServerConfiguration>(options =>
{
    options.ConnectionString = connectionString;
});
// Register the DbContext with the connection string
builder.Services.AddDbContext<DAOsContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserDAO, UserDAO>();
builder.Services.AddScoped<IUserService, UserService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();

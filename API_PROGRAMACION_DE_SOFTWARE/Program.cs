using API_PROGRAMACION_DE_SOFTWARE.DAOs;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Services;
using API_PROGRAMACION_DE_SOFTWARE.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;


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

    builder.Services.AddScoped<IMaterialDAO, MaterialDAO>();
    builder.Services.AddScoped<IMaterialService, MaterialService>();

    builder.Services.AddScoped<IReservationDAO, ReservationDAO>();
    builder.Services.AddScoped<IReservationService, ReservationService>();

    builder.Services.AddScoped<ILoanDAO, LoanDAO>();
    builder.Services.AddScoped<ILoanService, LoanService>();

    builder.Services.AddScoped<ILoginDAO, LoginDAO>();
    builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services
    .AddControllers();

/*builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });*/
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, new DefaultJsonTypeInfoResolver());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:7150", "http://localhost:5035")
              .AllowAnyHeader()
              .AllowAnyMethod();
        
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowBlazorApp");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

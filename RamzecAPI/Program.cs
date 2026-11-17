using Microsoft.EntityFrameworkCore;
using RamzecAPI.Data;
using RamzecAPI.Services.Implementations;
using RamzecAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Подключение к PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервисов
builder.Services.AddScoped<IFurnitureService, FurnitureService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddControllers();

// Регистрация генератора Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Включаем Swagger (без условия if, чтобы работал при любом запуске)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ramzec Furniture API v1");
    // Делает Swagger стартовой страницей (корень сайта "/")
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
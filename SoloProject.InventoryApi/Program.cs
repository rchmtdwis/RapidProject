using Microsoft.EntityFrameworkCore;
using SoloProject.InventoryApi.Models;
using SoloProject.InventoryApi.Repositories;
using SoloProject.InventoryApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var productDb = builder.Configuration.GetConnectionString("ProjectDb");

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(productDb)
);

builder.Services.AddScoped<IProductRepository, ProductServices>();
builder.Services.AddScoped<ITransactionRepository, TransactionServices>();

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

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupleStore.Automappers;
using SupleStore.DTOs;
using SupleStore.Models;
using SupleStore.Repository;
using SupleStore.Services;
using SupleStore.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddKeyedScoped<ICommonService<ProductosDto, ProductoInsertDto, ProductoUpdateDto>, ProductoService>("productService");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Repository

builder.Services.AddScoped<IRepository<Productos>, ProductRepository>();

//Entity Framework

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Validators

builder.Services.AddScoped<IValidator<ProductoInsertDto>, ProductInsertValidator>();
builder.Services.AddScoped<IValidator<ProductoUpdateDto>, ProductUpdateValidator>();


//Mappers

builder.Services.AddAutoMapper(typeof(MappingProfile));





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

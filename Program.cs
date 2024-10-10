using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupleStore.DTOs;
using SupleStore.Models;
using SupleStore.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Entity Framework

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Validators

builder.Services.AddScoped<IValidator<ProductoInsertDto>, ProductInsertValidator>();
builder.Services.AddScoped<IValidator<ProductoUpdateDto>, ProductUpdateValidator>();








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

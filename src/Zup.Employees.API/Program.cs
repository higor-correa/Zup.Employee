using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Zup.Employees.Application.Services.EmployeeContacts;
using Zup.Employees.Application.Services.Employees;
using Zup.Employees.Application.Validations;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;
using Zup.Employees.Domain.EmployeeContacts.Services;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Domain.Employees.Services;
using Zup.Employees.Infra;
using Zup.Employees.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<EmployeeValidation>());

builder.Services.AddScoped<IEmployeeFacade, EmployeeFacade>();
builder.Services.AddScoped<IEmployeeContactFacade, EmployeeContactFacade>();

builder.Services.AddScoped<IEmployeeCreator, EmployeeCreator>();
builder.Services.AddScoped<IEmployeeRemover, EmployeeRemover>();
builder.Services.AddScoped<IEmployeeSearcher, EmployeeSearcher>();
builder.Services.AddScoped<IEmployeeUpdater, EmployeeUpdater>();

builder.Services.AddScoped<IEmployeeContactCreator, ContactCreator>();
builder.Services.AddScoped<IEmployeeContactRemover, ContactRemover>();
builder.Services.AddScoped<IEmployeeContactSearcher, ContactSearcher>();
builder.Services.AddScoped<IEmployeeContactUpdater, ContactUpdater>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeContactRepository, EmployeeContactRepository>();

builder.Services.AddDbContext<EmployeeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeContextConnection")));

var app = builder.Build();

using var provider = app.Services.CreateScope();
var context = provider.ServiceProvider.GetRequiredService<EmployeeContext>();
context.Database.Migrate();

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

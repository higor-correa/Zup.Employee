using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zup.Employees.API.Configurations;
using Zup.Employees.Application.Services.EmployeeContacts;
using Zup.Employees.Application.Services.Employees;
using Zup.Employees.Application.Services.Security;
using Zup.Employees.Application.Validations;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;
using Zup.Employees.Domain.EmployeeContacts.Services;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Domain.Employees.Services;
using Zup.Employees.Infra;
using Zup.Employees.Infra.Repositories;
using Zup.Employees.Security.Domain;
using Zup.Employees.Security.Domain.Configurations;
using Zup.Employees.Security.Domain.Interfaces;
using Zup.Employees.Security.Domain.Tokens.Interfaces;
using Zup.Employees.Security.Domain.Tokens.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<EmployeeValidation>());

#region Microservice DI

builder.Services.AddOptions<JwtSettings>().Bind(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<ContextMiddleware>();

builder.Services.AddScoped<ILoginFacade, LoginFacade>();
builder.Services.AddScoped<IEmployeeFacade, EmployeeFacade>();
builder.Services.AddScoped<IEmployeeContactFacade, EmployeeContactFacade>();

builder.Services.AddScoped<ILoginService, LoginService>();

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

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

#endregion

builder.Services.AddDbContext<EmployeeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeContextConnection")));

#region AuthorizationConfigs

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => opt.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                });

#endregion

var app = builder.Build();

#region Database Migrations

using var provider = app.Services.CreateScope();
var context = provider.ServiceProvider.GetRequiredService<EmployeeContext>();
context.Database.Migrate();

#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Authorization Configs

app.UseAuthentication();
app.UseAuthorization();

#endregion

app.UseMiddleware<ContextMiddleware>();

app.MapControllers();

app.Run();

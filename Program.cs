global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using Expense_Tracker.Models;
global using Expense_Tracker.Data.DTOs;
global using Expense_Tracker.Data;
global using Expense_Tracker.Services.Interfaces;
global using Microsoft.EntityFrameworkCore;
global using Expense_Tracker.Data;
global using Microsoft.EntityFrameworkCore;
global using Expense_Tracker.Services.Interfaces;
using Expense_Tracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Repositories DI
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
builder.Services.AddScoped<IAdmin , Admin>();
builder.Services.AddScoped<IAccount , Account>();
builder.Services.AddScoped<ICategory , CategoryRepo>();
builder.Services.AddScoped<IClaims , Claims>();
builder.Services.AddScoped<IExpenses , ExpensesRepo>();
#endregion

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;            // No digit required in the password
    options.Password.RequiredLength = 6;              // Minimum password length
    options.Password.RequireNonAlphanumeric = false;  // No non-alphanumeric character required
    options.Password.RequireUppercase = false;        // No uppercase character required
    options.Password.RequireLowercase = false;        // No lowercase character required
    options.Password.RequiredUniqueChars = 1;         // Minimum unique characters in the password
}).AddDefaultTokenProviders()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

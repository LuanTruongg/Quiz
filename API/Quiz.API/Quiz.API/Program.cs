using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Repository;
using Quiz.Repository.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizDb")));
builder.Services.AddIdentity<User, IdentityRole<string>>()
    .AddEntityFrameworkStores<QuizDbContext>()
    .AddDefaultTokenProviders();
//builder.Services.AddTransient<UserManager<User>, UserManager<User>>();
//builder.Services.AddTransient<SignInManager<User>, SignInManager<User>>();
//builder.Services.AddTransient<RoleManager<IdentityRole<string>>, RoleManager<IdentityRole<string>>>();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.


app.Run();


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Repository;
using Quiz.Repository.Model;
using Quiz.Service;
using Quiz.Service.Implements;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizDb")));
builder.Services.AddIdentity<User, IdentityRole<string>>()
    .AddEntityFrameworkStores<QuizDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ISubjectManagementService, SubjectManagementService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();


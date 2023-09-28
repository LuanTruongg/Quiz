using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Infrastructure.Http;
using Quiz.Infrastructure.Middlewares;
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
builder.Services.AddScoped<IModuleManagementService, ModuleManagementService>();
builder.Services.AddScoped<IQuestionManagementService, QuestionManagementService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();


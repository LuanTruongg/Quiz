using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddScoped<ITestStructureManagementService, TestStructureManagementService>();
builder.Services.AddScoped<ITestSubjectManagementService, TestSubjectManagementService>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();

string issuer = builder.Configuration.GetValue<string>("JwtTokens:Issuer");
string signingKey = builder.Configuration.GetValue<string>("JwtTokens:Key");
byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = issuer,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = System.TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
    };
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


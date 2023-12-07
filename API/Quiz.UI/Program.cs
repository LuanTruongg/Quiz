
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Infrastructure.Middlewares;
using Quiz.Repository;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizDb")));
builder.Services.AddHttpClient();

builder.Services.AddIdentity<User, IdentityRole<string>>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<QuizDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
});
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//    {
//        options.Cookie.Name = ".Cookies.NetCoreSocialLogin";
//        options.LoginPath = new PathString("/signin-google");

//    })
//    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
//    {
//        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//        options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
//        options.CallbackPath = new PathString("/signin-google");
//    });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Login/Index";
    options.AccessDeniedPath = "/";
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ILoginServiceClient, LoginServiceClient>();
builder.Services.AddScoped<IHomeServiceClient, HomeServiceClient>();
builder.Services.AddScoped<ITestStructureServiceClient, TestStructureServiceClient>();
builder.Services.AddScoped<ITestSubjectServiceClient, TestSubjectServiceClient>();
builder.Services.AddScoped<ISubjectServiceClient, SubjectServiceClient>();
builder.Services.AddScoped<IQuestionServiceClient, QuestionServiceClient>();
builder.Services.AddScoped<IUserManagementServiceClient, UserManagementServiceClient>();
builder.Services.AddScoped<IUserTestManagementServiceClient, UserTestManagementServiceClient>();

var host = WebApplication.CreateBuilder(args).Services.BuildServiceProvider();
Thread.Sleep(5000);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();

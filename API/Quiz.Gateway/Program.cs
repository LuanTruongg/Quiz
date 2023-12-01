
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Quiz.Gateway.Ocelot;
using Quiz.Gateway.Services;
using Quiz.Gateway.SwaggerExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddJsonFile("ocelot.json")
          .AddJsonFile($"ocelot.{env.EnvironmentName}.json")
          .AddJsonFile("swagger.json")
          .AddJsonFile($"swagger.{env.EnvironmentName}.json");
    config.AddEnvironmentVariables();
});
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddSingleton<SwaggerCollectionDownload>();
builder.Services.AddSingleton<MergeSwagger>();
builder.Services.AddSingleton<ISwaggerService, SwaggerService>();

var host = WebApplication.CreateBuilder(args).Services.BuildServiceProvider();
Thread.Sleep(3000);

var app = builder.Build();

var download = app.Services.GetRequiredService<SwaggerCollectionDownload>();
var merge = app.Services.GetRequiredService<MergeSwagger>();
var mergeSwagger = app.Services.GetRequiredService<ISwaggerService>();
// Configure the HTTP request pipeline.

if (download.IsDownload == false)
{
    await download.Download(app.Environment.EnvironmentName);
    merge.Merge();
}
app.UseSwagger();

app.UseSwaggerForOcelotUI()
    .UseOcelot(OcelotConfigure.Create(mergeSwagger))
    .Wait();
app.UseAuthorization();

app.MapControllers();

app.Run();

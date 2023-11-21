using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzucareraPomalca.Api.Middlewares;
using AzucareraPomalca.Application.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//var logger = new LoggerConfiguration()
//    .WriteTo.Console(LogEventLevel.Information)
//    .WriteTo.File(
//        ".." + Path.DirectorySeparatorChar + "logapi.log",
//        LogEventLevel.Warning,
//        rollingInterval: RollingInterval.Day
//    )
//    .CreateLogger();

//builder.Logging.AddSerilog(logger);
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(new ValidationFilter());

//    AuthorizationPolicy authorizationPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .Build();

//    options.Filters.Add(new AuthorizeFilter());
//});

// Route Options
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;

});

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure
builder.Services.addInfrastructureServices(builder.Configuration);


// Applications
builder.Services.AddApplicationServices();

// Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(options =>
    {
        options.RegisterModule(new InfrastructureAutofacModule());
        options.RegisterModule(new ApplicationAutofacModule());
    });

// API
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .Build();
});

app.UseAuthorization();

app.MapControllers();

app.Run();

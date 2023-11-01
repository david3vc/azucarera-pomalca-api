using Autofac.Extensions.DependencyInjection;
using Autofac;
using AzucareraPomalca.Infrastructure.Core.Paginations.Abstractions;
using AzucareraPomalca.Infrastructure.Core.Paginations.Implementations;
using System.Reflection;
using AzucareraPomalca.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Route Options
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;

});

// Data base context
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IPaginator<>), typeof(Paginator<>));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(options =>
    {
        options.RegisterAssemblyTypes(Assembly.Load("AzucareraPomalca.Infrastructure"))
        .Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

        options.RegisterAssemblyTypes(Assembly.Load("AzucareraPomalca.Application"))
        .Where(t => t.Name.EndsWith("Service"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    });

builder.Services.AddAutoMapper(Assembly.Load("AzucareraPomalca.Application"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

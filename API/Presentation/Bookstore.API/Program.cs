using Bookstore.API.Extensions;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Logics.CategoryLogics;
using Bookstore.Application.Mappers;
using Bookstore.Application.Services;
using Bookstore.Infrastructure.Context;
using Bookstore.Infrastructure.Interfaces;
using Bookstore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// NLog configuration
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BookstoreContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"),
        b => b.MigrationsAssembly("Bookstore.API"));
});
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining(typeof(CreateCategory)));
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.ConfigureCors();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

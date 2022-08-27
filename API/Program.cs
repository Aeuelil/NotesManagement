using Microsoft.EntityFrameworkCore;
using NotesManagement.Data;
using System.Text.Json.Serialization;
using Serilog;
using API.Middleware;

// Setup Serilog to be able to save to files and specify location
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs\\logfile.txt"
                  , rollingInterval: RollingInterval.Day
                  , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NoteDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers(options => 
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Ignore JSON cycling erorrs.
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Host.UseSerilog();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<NoteDbContext>();
}

// Allow CORS in case the web project is under a different URL, so no conflicts.
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

// Capture every request and log details
app.UseMiddleware<LoggingMiddleware>();

app.Run();

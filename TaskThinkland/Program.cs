using Serilog.Events;
using Serilog;
using TaskThinkland.Api.Extensions;
using TaskThinkland.Api.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.File(@"Loggers\Errors.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtValidation(builder.Configuration);
builder.Services.AddSwaggerWithToken();
builder.Services.AddThinklandServices();
builder.Services.AddThinklanDbContext(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.UseErrorHandlerMiddleware();

app.MapControllers();

app.Run();

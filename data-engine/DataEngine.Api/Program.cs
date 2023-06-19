using DataEngine.Core;
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();

services.AddCore();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

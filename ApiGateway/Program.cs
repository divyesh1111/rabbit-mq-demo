using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGateway API V1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();

app.MapControllers();

// simple root endpoint so / works
app.MapGet("/", () => "API Gateway is running. Use /swagger or /gateway/orders");

await app.UseOcelot();

app.Run();
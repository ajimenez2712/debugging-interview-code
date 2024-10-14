using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/touchpoints", async (ApplicationDbContext context) =>
{
var touchpoints = await context.Touchpoints.ToListAsync();
if (touchpoints == null)
{
return Results.NotFound("No touchpoints found.");
}
return Results.Ok(touchpoints);
})
.WithName("GetTouchpoints");

app.MapPost("/api/touchpoints", async (ApplicationDbContext context, Touchpoint touchpoint) =>
{
if (string.IsNullOrEmpty(touchpoint.Name))
{
return Results.BadRequest("Touchpoint name is required.");
}

context.Touchpoints.Add(touchpoint);
await context.SaveChangesAsync();

return Results.Ok(touchpoint);
})
.WithName("AddTouchpoint");

app.MapGet("/weatherforecast", () =>
{
  var summaries = new[]
  {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

  var forecast = Enumerable.Range(1, 5).Select(index =>
      new WeatherForecast
      (
          DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          Random.Shared.Next(-20, 55),
          summaries[Random.Shared.Next(summaries.Length)]
      ))
      .ToArray();

  return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

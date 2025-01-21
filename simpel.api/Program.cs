using simpel.api.Mappers;
using simpel.api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "react", policy =>
    {
        policy.WithOrigins("http://localhost:5173");
    });
});

builder.Services.AddHttpClient<ElectricityPriceService>();
builder.Services.AddSingleton<ElectricityPriceService>();

var app = builder.Build();

app.MapOpenApi("/api/doc");
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/api/doc", "v1");
    });
}
app.UseCors("react");
app.UseHttpsRedirection();


app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/api/pricedata/{area}", async (string area, ElectricityPriceService priceService) =>
{
    System.Console.WriteLine("request came");
    var priceData = await priceService.GetPriceDataAsync(area);
    if (priceData == null || priceData.Average == 0)
    {
        return Results.NotFound("No data found or invalid input.");
    }
    return Results.Ok(priceData.ToDto());
});

app.Run();

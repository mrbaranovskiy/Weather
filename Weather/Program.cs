using Microsoft.AspNetCore.Mvc;
using Weather.Actions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<EndpointCacheService>();
builder.Services.AddSingleton<EndpointCreationService>();
builder.Services.AddSingleton<EndpointStateService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
    .AllowCredentials()); // allow credentials

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/endpoint/new", EndpointActions.NewEndpoint);
app.MapGet("/endpoint/stop", EndpointActions.StopEndpoint);
app.MapGet("/endpoint/start", EndpointActions.RunEndpoint);
app.MapGet("/endpoint/list", EndpointActions.ListEndpoints);


app.Run();


using SpotifyClone.Domain.Models;
using SpotifyClone.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<SpotifyCloneContext>();
builder.Services.AddScoped<UserAuthentication>();
builder.Services.AddScoped<GetSuggestedPlayLists>();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using SpotifyClone.Domain.Models;
using SpotifyClone.Services.Services.PlaylistsServices;
using SpotifyClone.Services.Services.SearchServices;
using SpotifyClone.Services.Services.SongServices;
using SpotifyClone.Services.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<SpotifyCloneContext>();
builder.Services.AddScoped<UserAuthentication>();
builder.Services.AddScoped<GetSuggestedPlayLists>();
builder.Services.AddScoped<GetPlayLists>();
builder.Services.AddScoped<SongsService>();
builder.Services.AddScoped<UserProperties>();
builder.Services.AddScoped<FollowManager>();
builder.Services.AddScoped<PlaylistHandler>();
builder.Services.AddScoped<UserProfileActions>();
builder.Services.AddScoped<GetSearchResults>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

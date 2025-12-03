var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddSingleton<Random>(); // opcional
builder.Services.AddSingleton<Match3.Services.CreateBoards>();
builder.Services.AddSingleton<Match3.Services.GameService>();

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader()
              .AllowCredentials()
              .WithOrigins("http://localhost:5173"); // cambia al origin de tu Vite dev
    });
});

var app = builder.Build();

app.UseCors();
app.MapControllers();
app.MapHub<Match3.Hubs.GameHub>("/hubs/game");

app.Run();

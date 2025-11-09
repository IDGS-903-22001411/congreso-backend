using Microsoft.EntityFrameworkCore;
using CongresoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Configurar SQLite
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");
builder.Services.AddDbContext<BdCongresoContext>(options =>
    options.UseSqlite(connectionString));

// Política CORS (agrega aquí la URL de tu frontend cuando lo subas)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins(
            "http://localhost:5173",
            "https://localhost:5173",
            "https://congresotic.netlify.app"   // cambia por la URL real de tu frontend
        )
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

// Crear base de datos automáticamente (muy importante para Render)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BdCongresoContext>();
    db.Database.Migrate();
}

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseCors("AllowReact");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

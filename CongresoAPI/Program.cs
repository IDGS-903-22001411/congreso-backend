using Microsoft.EntityFrameworkCore;
using CongresoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar puerto dinámico para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

//  Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//  Detectar entorno y asignar cadena de conexión
string connectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = "Data Source=congreso.db"; // Local (Windows)
    builder.Logging.AddConsole();
    Console.WriteLine(" Entorno: Desarrollo (usando congreso.db local)");
}
else
{
    connectionString = "Data Source=/app/congreso.db"; // Render (Docker)
    Console.WriteLine(" Entorno: Producción (usando /app/congreso.db en Render)");
}

// Registrar DbContext con la conexión adecuada
builder.Services.AddDbContext<BdCongresoContext>(options =>
    options.UseSqlite(connectionString));

// Configurar CORS para React (local + Netlify)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins(
            "http://localhost:5173",            // desarrollo local
            "https://localhost:5173",           // desarrollo HTTPS
            "https://congresotic.netlify.app"   // producción Netlify
        )
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

// Aplicar migraciones automáticamente al iniciar
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

app.UseAuthorization();
app.MapControllers();

app.Run();

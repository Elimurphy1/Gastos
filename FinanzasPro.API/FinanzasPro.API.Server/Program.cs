using Microsoft.EntityFrameworkCore;
using ProyectoFinanzas;
// ACORDATE: Hacé Ctrl + . sobre ApplicationDbContext para importar su namespace si te tira error
// using ProyectoFinanzas; o el que corresponda a tu carpeta Data

var builder = WebApplication.CreateBuilder(args);

// 1. Configuraciones por defecto que te trajo el proyecto (Aspire)
builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

// 2. LO NUESTRO: Le decimos a la API que vamos a usar Controladores y tu Base de Datos
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();

// 3. Configuramos Swagger (para tener la interfaz gráfica verde que queremos ver)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();

// 4. Encendemos la interfaz gráfica de Swagger solo cuando estamos programando
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mantenemos los endpoints por defecto del sistema
app.MapDefaultEndpoints();

// 5. ENCENDEMOS LAS RUTAS: Esto conecta el /api/Ingresos con tu controlador
app.MapControllers();

app.Run();
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimalApi;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareaContext>(p => p.UseInMemoryDatabase("TareasDB"));

builder.Services.AddSqlServer<TareaContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareaContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria" + dbContext.Database.IsInMemory());
});


app.MapGet("/api/tareas", async([FromServices] TareaContext dbContext) =>
{
    //return Results.Ok(dbContext.Tareas.Where(p => p.PrioridadTarea == Prioridad.Baja));
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});





app.Run();

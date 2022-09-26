using Microsoft.EntityFrameworkCore;
using minimalApi.models;

namespace minimalApi
{
    public class TareaContext : DbContext
    {
        //debset set de datos del modelo creado previamente 
        //model en sigular, colecciones en plural
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        public TareaContext(DbContextOptions<TareaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            List<Categoria> categoriasInit = new List<Categoria>();
            categoriasInit.Add(new Categoria()
            {
                CategoriaId = Guid.Parse("44d89b42-77f7-4749-9673-143821266532"),
                Nombre = "Actividades Pendientes",
                Peso = 20
            });

            categoriasInit.Add(new Categoria()
            {
                CategoriaId = Guid.Parse("44d89b42-77f7-4749-9673-143821266502"),
                Nombre = "Actividades Personales",
                Peso = 50
            });


            modelBuilder.Entity<Categoria>(categoria =>
            {

                categoria.ToTable("Categorias");
                categoria.HasKey(p => p.CategoriaId);
                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
                categoria.Property(p => p.Descripcion).IsRequired(false);
                categoria.Property(p => p.Peso);
                categoria.HasData(categoriasInit);

            });


            List<Tarea> tareasInit = new List<Tarea>();

            tareasInit.Add(new Tarea()
            {
                TareaId = Guid.Parse("44d89b42-77f7-4749-9673-143821266510"),
                CategoriaId = Guid.Parse("44d89b42-77f7-4749-9673-143821266532"),
                PrioridadTarea = Prioridad.Media,
                Titulo = "Pago de servicios publicos",
                FechaCreacion = DateTime.Now

            });

            tareasInit.Add(new Tarea()
            {
                TareaId = Guid.Parse("44d89b42-77f7-4749-9673-143821266511"),
                CategoriaId = Guid.Parse("44d89b42-77f7-4749-9673-143821266502"),
                PrioridadTarea = Prioridad.Baja,
                Titulo = "Terminas de ver pelicula",
                FechaCreacion = DateTime.Now

            });

            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.ToTable("Tareas");
                tarea.HasKey(p => p.TareaId);
                tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(p => p.Descripcion).IsRequired(false);
                tarea.Property(p => p.PrioridadTarea);
                tarea.Property(p => p.FechaCreacion);
                tarea.Ignore(p => p.Resumen);
                tarea.HasData(tareasInit);


            });
        }
    }
}

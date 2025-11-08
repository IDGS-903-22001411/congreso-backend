using Microsoft.EntityFrameworkCore;

namespace CongresoAPI.Models
{
    public class BdCongresoContext : DbContext
    {
        public BdCongresoContext(DbContextOptions<BdCongresoContext> options)
            : base(options)
        {
        }

        public DbSet<Participante> Participantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Participante>().HasData(
    new Participante
    {
        Id = 1,
        Nombre = "Carlos",
        Apellidos = "Pérez",
        Email = "carlos.perez@example.com",
        Twitter = "@carlosp",
        Ocupacion = "Desarrollador",
        Avatar = "https://i.pravatar.cc/150?img=1",
        FechaRegistro = new DateTime(2024, 01, 01)
    },
    new Participante
    {
        Id = 2,
        Nombre = "Ana",
        Apellidos = "Gómez",
        Email = "ana.gomez@example.com",
        Twitter = "@anagomez",
        Ocupacion = "Diseñadora",
        Avatar = "https://i.pravatar.cc/150?img=2",
        FechaRegistro = new DateTime(2024, 01, 01)
    },
    new Participante
    {
        Id = 3,
        Nombre = "Luis",
        Apellidos = "Martínez",
        Email = "luis.martinez@example.com",
        Twitter = "@luism",
        Ocupacion = "Estudiante",
        Avatar = "https://i.pravatar.cc/150?img=3",
        FechaRegistro = new DateTime(2024, 01, 01)
    }
);

        }
    }
}

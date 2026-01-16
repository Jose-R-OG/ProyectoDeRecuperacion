using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoDeRecuperacion.Models;

namespace ProyectoDeRecuperacion.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Entrada> entradas { get; set; }
        public DbSet<EntradaDetalle> detalles { get; set; }
        public DbSet<Productos> productos { get; set; }
    }
}

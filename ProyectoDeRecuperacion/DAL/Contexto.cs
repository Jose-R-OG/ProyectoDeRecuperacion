using Microsoft.EntityFrameworkCore;
using ProyectoDeRecuperacion.Models;

namespace ProyectoDeRecuperacion.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options){}
    public DbSet<Entrada> entradas { get; set; }
    public DbSet<EntradaDetalle> detalles { get; set; }
    public DbSet<Productos> productos { get; set; }
}
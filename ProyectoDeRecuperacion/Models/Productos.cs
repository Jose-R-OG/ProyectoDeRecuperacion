using System.ComponentModel.DataAnnotations;

namespace ProyectoDeRecuperacion.Models;

public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public double Precio { get; set; }
    public double Costo { get; set; }
    public int Existencia { get; set; }
}


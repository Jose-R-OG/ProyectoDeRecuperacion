using System.ComponentModel.DataAnnotations;

namespace ProyectoDeRecuperacion.Models;

public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "La descripcion es un campo obligatorio")]
    public string Descripcion { get; set; } = string.Empty;
    [Range(0.1, double.MaxValue, ErrorMessage = "El precio tiene que ser mayor a 0")]
    public double Precio { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "El costo tiene que ser mayor a 0")]
    public double Costo { get; set; }
    public int Existencia { get; set; }
}


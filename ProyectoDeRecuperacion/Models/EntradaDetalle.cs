using System.ComponentModel.DataAnnotations;

namespace ProyectoDeRecuperacion.Models;

public class EntradaDetalle
{
    [Key]
    public int Id { get; set; }

    public int EntradaId { get; set; }

    public int ProductoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad no puede ser menor a 1")]
    public int Cantidad { get; set; }

    [Range(1, double.MaxValue, ErrorMessage = "El costo  no puede ser menor a 1")]
    public double Costo { get; set; }
}

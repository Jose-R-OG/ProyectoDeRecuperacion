using System.ComponentModel.DataAnnotations;

namespace ProyectoDeRecuperacion.Models;

public class Entrada
{
    [Key]
    public int EntradaId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "El Concepto es requerido")]
    public string Concepto { get; set; } = string.Empty;
    public double Total { get; set; }

    public ICollection<EntradaDetalle> Detalles { get; set; } = new List<EntradaDetalle>();
}

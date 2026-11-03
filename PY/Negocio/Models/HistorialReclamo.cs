using System;

namespace Negocio.Models
{
  public class HistorialReclamo
  {
    public int HistorialReclamoId { get; set; }

    public int ReclamoId { get; set; }

    public int EstadoAnteriorId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime Fecha { get; set; }

    public int? UsuarioModificador { get; set; }

    public DateTime? FechaModificacion { get; set; }

    // Estos son para mostrar en el DataGridView
    public string EstadoAnterior { get; set; }

    public string EstadoNuevo { get; set; }

    public string Usuario { get; set; }
  }
}


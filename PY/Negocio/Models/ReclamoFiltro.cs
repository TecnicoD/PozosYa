using System;

namespace Negocio.Models
{
  public class ReclamoFiltro
  {
    public string DNI { get; set; }

    public int? BarrioId { get; set; }

    public int? EstadoId { get; set; }

    public DateTime? FechaDesde { get; set; }

    public DateTime? FechaHasta { get; set; }
  }
}

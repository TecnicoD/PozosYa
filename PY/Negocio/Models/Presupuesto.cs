using System;

namespace Negocio.Models
{
  public class Presupuesto
  {
    public int PresupuestoId { get; set; }

    public int ReclamoId { get; set; }

    public int ProveedorId { get; set; }

    public decimal MontoEstimado { get; set; }

    public decimal? MontoFinal { get; set; }

    public string ArchivoPdf { get; set; }

    public DateTime FechaCarga { get; set; }

    public int UsuarioId { get; set; }

    public string DigitoVerificador { get; set; }

    public string NombreProveedor { get; set; }
  }
}

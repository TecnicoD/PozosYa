using System;

namespace Negocio.Models
{
  public class ReclamoListado
  {
    public int ReclamoId { get; set; }

    public string NumeroReclamo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Vecino { get; set; }

    public string Direccion { get; set; }

    public string Barrio { get; set; }

    public string Estado { get; set; }
  }
}

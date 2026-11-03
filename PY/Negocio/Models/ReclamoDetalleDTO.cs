using System;

namespace Negocio.Models
{
  public class ReclamoDetalleDTO
  {
    public int ReclamoId { get; set; }

    public string NumeroReclamo { get; set; }

    public string Vecino { get; set; }

    public string DNI { get; set; }

    public string Direccion { get; set; }

    public string Barrio { get; set; }

    public string Estado { get; set; }

    public string TipoDanio { get; set; }

    public string Severidad { get; set; }

    public string Descripcion { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Telefono { get; set; }

    public string Mail { get; set; }

    public string NroCatastral { get; set; }

    public DateTime FechaRegistroVecino { get; set; }


    public int EstadoId { get; set; }

    public int BarrioId { get; set; }

    public int TipoDanioId { get; set; }

    public int SeveridadId { get; set; }
  }
}

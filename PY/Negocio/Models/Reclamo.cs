using System;

public class Reclamo
{
  public int ReclamoId { get; set; }

  public string NumeroReclamo { get; set; }

  public int VecinoId { get; set; }

  public int TipoDanioId { get; set; }

  public int SeveridadId { get; set; }

  public int EstadoActualId { get; set; }

  public string Descripcion { get; set; }

  public string Direccion { get; set; }

  public decimal? Latitud { get; set; }

  public decimal? Longitud { get; set; }

  public int BarrioId { get; set; }

  public DateTime FechaCreacion { get; set; }

  public int UsuarioCreadorId { get; set; }

  public DateTime? FechaCierre { get; set; }

  public int? UsuarioModificadorId { get; set; }

  public DateTime? FechaModificacion { get; set; }

  public string DigitoVerificador { get; set; }
}

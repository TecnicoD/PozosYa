using System;

public class PartidaPresupuestaria
{
  public int PartidaPresupuestariaId { get; set; }

  public int Anio { get; set; }

  public int Trimestre { get; set; }

  public decimal MontoAsignado { get; set; }

  public decimal MontoComprometido { get; set; }

  public decimal MontoEjecutado { get; set; }

  public int UsuarioGerenteId { get; set; }

  public DateTime FechaAsignacion { get; set; }

  public string Observaciones { get; set; }

  public bool Activo { get; set; }

  public string TextoPartida
  {
    get
    {
      return Anio +
             " - Trimestre " +
             Trimestre;
    }
  }
}

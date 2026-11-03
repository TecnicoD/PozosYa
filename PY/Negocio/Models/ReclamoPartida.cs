using System;

public class ReclamoPartida
{
  public int ReclamosPartidaId { get; set; }

  public int ReclamoId { get; set; }

  public int PartidaId { get; set; }

  public decimal MontoAsignadoReclamo { get; set; }

  public DateTime FechaAsignacion { get; set; }

  public int UsuarioPriorizadorId { get; set; }

  public int NumeroPrioridad { get; set; }
}

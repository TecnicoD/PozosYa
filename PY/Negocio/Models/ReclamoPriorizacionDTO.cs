namespace Negocio.Models
{
  public class ReclamoPriorizacionDTO
  {
    public int ReclamoId { get; set; }

    public string NumeroReclamo { get; set; }

    public string Direccion { get; set; }

    public string Barrio { get; set; }

    public string Estado { get; set; }

    public decimal MontoPresupuesto { get; set; }

    // true si el reclamo ya está asociado a la partida
    // (solo se reordena, no se vuelve a insertar).
    public bool YaAsignado { get; set; }
  }
}

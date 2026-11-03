namespace Negocio.Models
{
  public class PermisoUsuario
{
    public int PermisoId { get; set; }
    public string Nombre { get; set; }
    public bool Heredado { get; set; }
    public bool Concedido { get; set; } // ¡Ahora sí WinForms lo detecta!
    public bool TienePermisoIndividual { get; set; }
}
}

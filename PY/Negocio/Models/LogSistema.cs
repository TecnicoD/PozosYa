using System;

namespace Negocio.Models
{
  public class LogSistema
  {
    public int LogsSistemaId { get; set; }

    public int UsuarioId { get; set; }

    public string Accion { get; set; }

    public string TablaAfectada { get; set; }

    public int RegistroId { get; set; }

    public DateTime Fecha { get; set; }

    public string Descripcion { get; set; }

    public string Usuario { get; set; }
  }
}

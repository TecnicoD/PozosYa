using System;

namespace Negocio.Models
{
  public class Usuario
  {
    public int UsuarioId { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public bool Activo { get; set; }

    public DateTime? UltimoLogin { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string DigitoVerificador { get; set; }

    public string NombreCompleto
    {
      get
      {
        return Nombre + " " + Apellido;
      }
    }
  }
}

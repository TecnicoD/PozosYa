using Datos.Repository;
using Negocio.Models;
using Negocio.Security;
using System.Security.Cryptography;
using System.Text;

namespace Negocio.Services
{
  public class SeguridadService
  {
    private readonly SeguridadRepository repository =
        new SeguridadRepository();

    public bool Login(string usuario, string password)
    {
      string hash = HashHelper.CalcularHash(password);

      Usuario user = repository.Login(usuario, hash);

      if (user == null)
        return false;

      SesionActual.Usuario = user;

      LogSistemaService logService = new LogSistemaService();

      logService.Registrar(
          user.UsuarioId,
          "LOGIN",
          "USUARIOS",
          user.UsuarioId,
          "Inicio de sesión del usuario " + user.Username
      );

      SesionActual.Permisos =
          repository.ObtenerPermisos(user.UsuarioId);

      return true;
    }

    
  }
}

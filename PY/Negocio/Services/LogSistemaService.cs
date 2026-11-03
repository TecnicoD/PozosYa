using Datos.Repository;
using Negocio.Models;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class LogSistemaService
  {
    private readonly LogSistemaRepository repository =
        new LogSistemaRepository();

    public void Registrar(
        int usuarioId,
        string accion,
        string tablaAfectada,
        int registroId,
        string descripcion)
    {
      LogSistema log = new LogSistema
      {
        UsuarioId = usuarioId,
        Accion = accion,
        TablaAfectada = tablaAfectada,
        RegistroId = registroId,
        Descripcion = descripcion
      };

      repository.Registrar(log);
    }

    public List<LogSistema> ObtenerTodos()
    {
      return repository.ObtenerTodos();
    }


  }
}

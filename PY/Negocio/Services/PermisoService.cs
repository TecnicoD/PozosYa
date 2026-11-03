using Datos.Repository;
using Negocio.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Negocio.Services
{
  public class PermisoService
  {
    private readonly PermisoRepository repository =
        new PermisoRepository();

    public List<Permiso> ObtenerTodos()
    {
      return repository.ObtenerTodos();
    }

    public Dictionary<int, bool> ObtenerPermisosUsuario(int usuarioId)
    {
      return repository.ObtenerPermisosUsuario(usuarioId);
    }


    public void ActualizarPermisosUsuario(int usuarioId, Dictionary<int, bool> permisos)
    {
      repository.ActualizarPermisosUsuario(usuarioId, permisos);
    }


    public List<int> ObtenerPermisosRoles(int usuarioId)
    {
      return repository.ObtenerPermisosRoles(usuarioId);
    }

    public List<PermisoUsuario> ObtenerVistaPermisosUsuario(int usuarioId)
    {
      var todos = repository.ObtenerTodos();
      var permisosRol = repository.ObtenerPermisosRoles(usuarioId);
      var permisosIndividuales = repository.ObtenerPermisosUsuario(usuarioId);

      List<PermisoUsuario> lista = new List<PermisoUsuario>();

      foreach (var permiso in todos)
      {
        bool heredado = permisosRol.Contains(permiso.PermisoId);

        bool concedido = heredado;

        if (permisosIndividuales.ContainsKey(permiso.PermisoId))
        {
          concedido = permisosIndividuales[permiso.PermisoId];
        }

        lista.Add(new PermisoUsuario
        {
          PermisoId = permiso.PermisoId,
          Nombre = permiso.Nombre,
          Heredado = heredado,
          Concedido = concedido,
          TienePermisoIndividual = permisosIndividuales.ContainsKey(permiso.PermisoId)
        });
      }

      return lista;
    }


    public void GuardarPermisosUsuario(int usuarioId, List<PermisoUsuario> permisos)
    {
      Dictionary<int, bool> overrides = new Dictionary<int, bool>();

      foreach (var permiso in permisos)
      {
        if (permiso.Concedido != permiso.Heredado)
        {
          overrides.Add(permiso.PermisoId, permiso.Concedido);
        }
      }

      repository.ActualizarPermisosUsuario(usuarioId, overrides);
    }



  }
}

using Datos.Repository;
using Negocio.Models;
using System;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class RolService
  {
    private readonly RolRepository repository = new RolRepository();

    public List<Rol> ObtenerRolesPorUsuario(int usuarioId)
    {
      return repository.ObtenerRolesPorUsuario(usuarioId);
    }

    public void ActualizarRolesUsuario(int usuarioId, List<int> roles)
    {
      repository.ActualizarRolesUsuario(usuarioId, roles);
    }

    public List<Rol> ObtenerTodos()
    {
      return repository.ObtenerTodos();
    }
  }
}

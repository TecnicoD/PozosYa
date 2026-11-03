using Datos.Repository;
using Negocio.Models;
using Negocio.Security;
using System.Collections.Generic;

public class UsuarioService
{
  private readonly UsuarioRepository repository = new UsuarioRepository();

  public List<Usuario> ObtenerTodos()
  {
    return repository.ObtenerTodos("", null, null);
  }

  public List<Usuario> ObtenerTodos(string texto, bool? activo, int? rolId)
  {
    return repository.ObtenerTodos(texto, activo, rolId);
  }

  public Usuario ObtenerPorId(int usuarioId)
  {
    return repository.ObtenerPorId(usuarioId);
  }

  public void Actualizar(Usuario usuario)
  {
    repository.Actualizar(usuario);
  }

  public void CambiarPassword(int usuarioId, string password)
  {
    string hash = HashHelper.CalcularHash(password);
    repository.CambiarPassword(usuarioId, hash);
  }

  public int Agregar(Usuario usuario)
  {
    return repository.Agregar(usuario);
  }

  public bool ExisteUsername(string username, int usuarioIdExcluir)
  {
    return repository.ExisteUsername(username, usuarioIdExcluir);
  }

  public bool ExisteEmail(string email, int usuarioIdExcluir)
  {
    return repository.ExisteEmail(email, usuarioIdExcluir);
  }

  public Usuario ObtenerPorEmail(string email)
  {
    return repository.ObtenerPorEmail(email);
  }
}

using Datos.Repository;
using System;

namespace Negocio.Services
{
  public class TokenRecuperacionService
  {
    private readonly TokenRecuperacionRepository repository =
        new TokenRecuperacionRepository();

    public void Crear(
        int usuarioId,
        string token,
        DateTime fechaGeneracion,
        DateTime fechaExpiracion)
    {
      repository.Agregar(
          usuarioId,
          token,
          fechaGeneracion,
          fechaExpiracion);
    }

    public bool Validar(
        int usuarioId,
        string token)
    {
      return repository.Validar(
          usuarioId,
          token);
    }

    public void MarcarUsado(
        int usuarioId,
        string token)
    {
      repository.MarcarUsado(
          usuarioId,
          token);
    }
  }
}

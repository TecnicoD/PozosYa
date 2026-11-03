using Datos.Repository;
using Negocio.Models;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class ReclamoPartidaService
  {
    private readonly ReclamoPartidaRepository repository =
        new ReclamoPartidaRepository();

    public void Agregar(
        int reclamoId,
        int partidaId,
        decimal monto,
        int usuarioPriorizadorId,
        int numeroPrioridad)
    {
      repository.Agregar(
          reclamoId,
          partidaId,
          monto,
          usuarioPriorizadorId,
          numeroPrioridad);
    }

    public List<ReclamoPriorizacionDTO> ObtenerPriorizadosPorPartida(
        int partidaId)
    {
      return repository.ObtenerPriorizadosPorPartida(
          partidaId);
    }

    public void ActualizarPrioridad(
        int reclamoId,
        int partidaId,
        int numeroPrioridad)
    {
      repository.ActualizarPrioridad(
          reclamoId,
          partidaId,
          numeroPrioridad);
    }
  }
}

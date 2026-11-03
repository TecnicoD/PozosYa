using Datos.Repository;
using Negocio.Models;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class PartidaPresupuestariaService
  {
    private readonly PartidaPresupuestariaRepository repository =
        new PartidaPresupuestariaRepository();

    public List<PartidaPresupuestaria> ObtenerTodos()
    {
      return repository.ObtenerTodos();
    }

    public PartidaPresupuestaria ObtenerPorId(
        int partidaPresupuestariaId)
    {
      return repository.ObtenerPorId(
          partidaPresupuestariaId);
    }

    public int Agregar(
        PartidaPresupuestaria partida)
    {
      return repository.Agregar(partida);
    }

    public void Actualizar(
        PartidaPresupuestaria partida)
    {
      repository.Actualizar(partida);
    }

    public List<PartidaPresupuestaria> ObtenerTodasDisponibles()
    {
      return repository.ObtenerTodasDisponibles();
    }

    public bool ExistePeriodo(
        int anio,
        int trimestre,
        int partidaIdExcluir)
    {
      return repository.ExistePeriodo(
          anio,
          trimestre,
          partidaIdExcluir);
    }

    public void AgregarComprometido(
    int partidaId,
    decimal monto)
    {
      repository.AgregarComprometido(
          partidaId,
          monto);
    }



  }
}

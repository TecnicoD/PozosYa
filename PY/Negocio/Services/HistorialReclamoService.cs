using Datos.Repository;
using Negocio.Models;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class HistorialReclamoService
  {
    private readonly HistorialReclamoRepository repository =
        new HistorialReclamoRepository();

    public List<HistorialReclamo> ObtenerPorReclamo(int reclamoId)
    {
      return repository.ObtenerPorReclamo(reclamoId);
    }
  }
}

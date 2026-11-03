using Datos.Repository;
using Negocio.Models;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class DetalleClaveValorService
  {
    private readonly DetalleClaveValorRepository repository =
        new DetalleClaveValorRepository();

    public List<DetalleClaveValor> ObtenerPorTabla(string tabla)
    {
      return repository.ObtenerPorTabla(tabla);
    }
  }
}

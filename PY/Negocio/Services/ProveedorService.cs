using Datos.Repository;
using Negocio.Models;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class ProveedorService
  {
    private readonly ProveedorRepository repository =
        new ProveedorRepository();

    public List<Proveedor> ObtenerTodos()
    {
      return repository.ObtenerTodos();
    }

    public Proveedor ObtenerPorId(int proveedorId)
    {
      return repository.ObtenerPorId(proveedorId);
    }

    public bool ExisteCuit(string cuit, int proveedorIdExcluir)
    {
      return repository.ExisteCuit(cuit, proveedorIdExcluir);
    }

    public int Agregar(Proveedor proveedor)
    {
      return repository.Agregar(proveedor);
    }

    public void Actualizar(Proveedor proveedor)
    {
      repository.Actualizar(proveedor);
    }
  }
}

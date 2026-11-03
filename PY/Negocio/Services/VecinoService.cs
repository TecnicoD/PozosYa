using Datos.Repository;

namespace Negocio.Services
{
  public class VecinoService
  {
    private readonly VecinoRepository repository = new VecinoRepository();

    public Vecino ObtenerPorDni(string dni)
    {
      return repository.ObtenerPorDni(dni);
    }


    public int Agregar(Vecino vecino)
    {
      return repository.Agregar(vecino);
    }
  }
}

using Datos.Repository;
using Negocio.Models;
using System;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class ConfiguracionService
  {
    private readonly ConfiguracionRepository repository =
        new ConfiguracionRepository();

    public List<Configuracion> ObtenerTodos()
    {
      return repository.ObtenerTodos();
    }

    public void Actualizar(
        string clave,
        string valor)
    {
      repository.Actualizar(
          clave,
          valor);
    }

    public string ObtenerValor(string clave)
    {
            Configuracion configuracion =
                ObtenerTodos().Find(
                    x => x.Clave.Equals(
                        clave,
                        StringComparison.OrdinalIgnoreCase));

            if (configuracion == null)
                return "";

            return configuracion.Valor;
    }
    }
}

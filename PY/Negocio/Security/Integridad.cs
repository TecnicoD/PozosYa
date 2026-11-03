using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PY.Negocio.Security
{
    public class Integridad
    {
            public static string ObtenerSemillaSistema()
            {
                // 1. Construir la ruta y cargar el archivo appsettings.json
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration config = builder.Build();

                // 2. Acceder al valor usando la clave combinada con ":"
                string semilla = config["Criptografia:SemillaSistema"];

                return semilla;
            }
        }
}

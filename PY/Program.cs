using Datos.Repository;
using Negocio.Models;
using Negocio.Services;
using PY.Presentacion.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PY
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
      static void Main()
      {
            // Si no se encuentra la base de datos, se ofrece crearla
            // o restaurarla desde un .bak antes de mostrar el login.
            if (!BaseDatosDisponible())
            {
                using (FormConfigurarBD configurar = new FormConfigurarBD())
                {
                    if (configurar.ShowDialog() != DialogResult.OK)
                        return;
                }
            }

            Application.Run(new FormLogin());
      }

        private static bool BaseDatosDisponible()
        {
            try
            {
                return new BaseDatosService().ExisteBaseDatos();
            }
            catch (Exception)
            {
                // No se pudo conectar al servidor: se muestra la configuración.
                return false;
            }
        }
    }
}

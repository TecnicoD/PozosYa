using Datos.Repository;
using PY.Datos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio.Services
{
    internal class AdministradorService
    {
        private readonly AdministradorRepository repository =  new AdministradorRepository();

        public int ejecutarRestoreBD(string nombreArchivo, string nombreBD) {
            return repository.EjecutarStoredProcedureRestaurar(nombreArchivo, nombreBD);

        }

        public int ejecutarBackupBD()
        {
            return repository.EjecutarStoredProcedureBackup();

        }
        public string ObtenerRutaBackupServidor()
        {
            return repository.ObtenerRutaBackupServidor();
        }

        public int ejecutarValidacionIntegridadBD()
        {
            return repository.EjecutarStoredProcedureValidacionIntegridadBD();

        }
    }
}

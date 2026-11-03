using Negocio.Models;
using System.Collections.Generic;

namespace Negocio.Security
{
  public static class SesionActual
  {
    public static Usuario Usuario { get; set; }

    public static HashSet<string> Permisos { get; set; }

    public static bool TienePermiso(string permiso)
    {
      return Permisos != null &&
             Permisos.Contains(permiso);
    }

    public static void CerrarSesion()
    {
      Usuario = null;

      if (Permisos != null)
        Permisos.Clear();
    }
  }

}

using System.Windows.Forms;

namespace Negocio.Security
{
  public static class ProteccionFormulario
  {
    public static bool ValidarAcceso(Form formulario, string permiso)
    {
      if (SesionActual.TienePermiso(permiso))
        return true;

      MessageBox.Show(
          "No tiene permisos para acceder a esta pantalla.",
          "Acceso denegado",
          MessageBoxButtons.OK,
          MessageBoxIcon.Warning);

      formulario.Close();

      return false;
    }
  }
}

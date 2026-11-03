using System.Data;
using System.Data.SqlClient;

namespace Datos.Connection
{
  public class Conexion
  {
    // El servidor se toma de la configuración (basedatos.json).
    private string connectionString
    {
      get
      {
        return ConfiguracionConexion.CadenaConexion(
            ConfiguracionConexion.NombreBaseDatos);
      }
    }


    public SqlConnection ObtenerConexion()
    {
      SqlConnection conexion =
          new SqlConnection(connectionString);

      conexion.Open();

      return conexion;
    }


    public void CerrarConexion(SqlConnection conexion)
    {
      if (conexion != null &&
          conexion.State == ConnectionState.Open)
      {
        conexion.Close();
      }
    }

    // Se conecta directo a master: sirve aunque la base PozosYa
    // todavía no exista (crear o restaurar la base).
    public SqlConnection ObtenerConexionAMaster()
    {
      SqlConnection conexion =
          new SqlConnection(
              ConfiguracionConexion.CadenaConexion("master"));

      conexion.Open();

      return conexion;
    }
  }
}

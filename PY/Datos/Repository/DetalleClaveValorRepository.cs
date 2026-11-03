using Datos.Connection;
using Negocio.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class DetalleClaveValorRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    public List<DetalleClaveValor> ObtenerPorTabla(string tabla)
    {
      List<DetalleClaveValor> lista = new List<DetalleClaveValor>();

      string query = @"
                SELECT
                    detalle_id,
                    tabla,
                    clave,
                    valor
                FROM detalleClaveValor
                WHERE tabla = @Tabla
                ORDER BY valor";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);
      comando.Parameters.AddWithValue("@Tabla", tabla);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new DetalleClaveValor
        {
          DetalleId = (int)reader["detalle_id"],
          Tabla = reader["tabla"].ToString(),
          Clave = reader["clave"].ToString(),
          Valor = reader["valor"].ToString()
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }
  }
}

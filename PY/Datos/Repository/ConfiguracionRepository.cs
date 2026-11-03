using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class ConfiguracionRepository
  {
    private readonly Conexion conexionBD =
        new Conexion();

    public List<Configuracion> ObtenerTodos()
    {
      List<Configuracion> lista =
          new List<Configuracion>();

      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
          SELECT
              configuracion_id,
              clave,
              valor,
              descripcion,
              tipo
          FROM configuraciones
          ORDER BY configuracion_id";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      SqlDataReader reader =
          comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new Configuracion
        {
          ConfiguracionId =
              Convert.ToInt32(
                  reader["configuracion_id"]),

          Clave =
              reader["clave"].ToString(),

          Valor =
              reader["valor"].ToString(),

          Descripcion =
              reader["descripcion"] == DBNull.Value
                  ? ""
                  : reader["descripcion"].ToString(),

          Tipo =
              reader["tipo"].ToString()
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }

    public void Actualizar(
        string clave,
        string valor)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
          UPDATE configuraciones
          SET valor = @Valor
          WHERE clave = @Clave";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@Valor",
          valor ?? "");

      comando.Parameters.AddWithValue(
          "@Clave",
          clave);

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }
  }
}

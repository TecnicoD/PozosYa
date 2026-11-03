using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class LogSistemaRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    public void Registrar(LogSistema log)
    {
      string query = @"
                INSERT INTO logs_sistema
                (
                    usuario_id,
                    accion,
                    tabla_afectada,
                    registro_id,
                    fecha,
                    descripcion
                )
                VALUES
                (
                    @UsuarioId,
                    @Accion,
                    @TablaAfectada,
                    @RegistroId,
                    GETDATE(),
                    @Descripcion
                )";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@UsuarioId", log.UsuarioId);
      comando.Parameters.AddWithValue("@Accion", log.Accion);
      comando.Parameters.AddWithValue("@TablaAfectada", log.TablaAfectada);
      comando.Parameters.AddWithValue("@RegistroId", log.RegistroId);

      comando.Parameters.AddWithValue(
          "@Descripcion",
          string.IsNullOrWhiteSpace(log.Descripcion)
              ? (object)DBNull.Value
              : log.Descripcion
      );

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }

    public List<LogSistema> ObtenerTodos()
    {
      List<LogSistema> lista = new List<LogSistema>();

      string query = @"
                SELECT
                    l.logs_sistema_id,
                    l.usuario_id,
                    u.nombre + ' ' + u.apellido AS Usuario,
                    l.accion,
                    l.tabla_afectada,
                    l.registro_id,
                    l.fecha,
                    l.descripcion
                FROM logs_sistema l
                INNER JOIN usuarios u
                    ON l.usuario_id = u.usuario_id
                ORDER BY l.fecha DESC";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new LogSistema
        {
          LogsSistemaId = Convert.ToInt32(reader["logs_sistema_id"]),
          UsuarioId = Convert.ToInt32(reader["usuario_id"]),
          Usuario = reader["Usuario"].ToString(),
          Accion = reader["accion"].ToString(),
          TablaAfectada = reader["tabla_afectada"].ToString(),
          RegistroId = Convert.ToInt32(reader["registro_id"]),
          Fecha = Convert.ToDateTime(reader["fecha"]),
          Descripcion = reader["descripcion"] == DBNull.Value
                ? ""
                : reader["descripcion"].ToString()
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }
  }
}

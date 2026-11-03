using Datos.Connection;
using System;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class TokenRecuperacionRepository
  {
    private readonly Conexion conexionBD =
        new Conexion();

    public void Agregar(
        int usuarioId,
        string token,
        DateTime fechaGeneracion,
        DateTime fechaExpiracion)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
        INSERT INTO tokens_recuperacion
        (
          usuario_id,
          token,
          fecha_generacion,
          fecha_expiracion,
          usado
        )
        VALUES
        (
          @UsuarioId,
          @Token,
          @FechaGeneracion,
          @FechaExpiracion,
          0
        )";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@UsuarioId", usuarioId);

      comando.Parameters.AddWithValue(
          "@Token", token);

      comando.Parameters.AddWithValue(
          "@FechaGeneracion", fechaGeneracion);

      comando.Parameters.AddWithValue(
          "@FechaExpiracion", fechaExpiracion);

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }

    public bool Validar(
        int usuarioId,
        string token)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
        SELECT COUNT(*)
        FROM tokens_recuperacion
        WHERE usuario_id = @UsuarioId
          AND token = @Token
          AND usado = 0
          AND fecha_expiracion > GETDATE()";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@UsuarioId", usuarioId);

      comando.Parameters.AddWithValue(
          "@Token", token);

      int cantidad =
          Convert.ToInt32(
              comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return cantidad > 0;
    }

    public void MarcarUsado(
        int usuarioId,
        string token)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
        UPDATE tokens_recuperacion
        SET usado = 1
        WHERE usuario_id = @UsuarioId
          AND token = @Token";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@UsuarioId", usuarioId);

      comando.Parameters.AddWithValue(
          "@Token", token);

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }
  }
}

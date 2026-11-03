using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class PermisoRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    public List<Permiso> ObtenerTodos()
    {
      List<Permiso> permisos = new List<Permiso>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = "SELECT * FROM permisos ORDER BY nombre";

      SqlCommand comando = new SqlCommand(query, conexion);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        Permiso permiso = new Permiso
        {
          PermisoId = Convert.ToInt32(reader["permiso_id"]),
          Nombre = reader["nombre"].ToString(),
          Descripcion = reader["descripcion"].ToString()
        };

        permisos.Add(permiso);
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return permisos;
    }

    public Dictionary<int, bool> ObtenerPermisosUsuario(int usuarioId)
    {
      Dictionary<int, bool> permisos =
          new Dictionary<int, bool>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        SELECT permiso_id, concedido
        FROM usuarios_permisos
        WHERE usuario_id = @usuarioId";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@usuarioId", usuarioId);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        permisos.Add(
            Convert.ToInt32(reader["permiso_id"]),
            Convert.ToBoolean(reader["concedido"]));
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return permisos;
    }

    public void ActualizarPermisosUsuario(int usuarioId, Dictionary<int, bool> permisos)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlTransaction transaccion = conexion.BeginTransaction();

      try
      {
        SqlCommand eliminar = new SqlCommand(
            "DELETE FROM usuarios_permisos WHERE usuario_id = @usuarioId",
            conexion,
            transaccion);

        eliminar.Parameters.AddWithValue("@usuarioId", usuarioId);

        eliminar.ExecuteNonQuery();

        foreach (var permiso in permisos)
        {
          SqlCommand insertar = new SqlCommand(@"
                INSERT INTO usuarios_permisos
                (usuario_id, permiso_id, concedido)
                VALUES
                (@usuarioId, @permisoId, @concedido)",
              conexion,
              transaccion);

          insertar.Parameters.AddWithValue("@usuarioId", usuarioId);
          insertar.Parameters.AddWithValue("@permisoId", permiso.Key);
          insertar.Parameters.AddWithValue("@concedido", permiso.Value);

          insertar.ExecuteNonQuery();
        }

        transaccion.Commit();
      }
      catch
      {
        transaccion.Rollback();
        throw;
      }
      finally
      {
        conexionBD.CerrarConexion(conexion);
      }
    }


    public List<int> ObtenerPermisosRoles(int usuarioId)
    {
      List<int> permisos = new List<int>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        SELECT DISTINCT rp.permiso_id
        FROM usuario_roles ur
        INNER JOIN roles_permisos rp
            ON ur.rol_id = rp.rol_id
        WHERE ur.usuario_id = @usuarioId";

      SqlCommand comando = new SqlCommand(query, conexion);
      comando.Parameters.AddWithValue("@usuarioId", usuarioId);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        permisos.Add(Convert.ToInt32(reader["permiso_id"]));
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return permisos;
    }
  }
}

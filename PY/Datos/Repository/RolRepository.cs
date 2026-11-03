using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class RolRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    public List<Rol> ObtenerRolesPorUsuario(int usuarioId)
    {
      List<Rol> roles = new List<Rol>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
                SELECT r.*
                FROM roles r
                INNER JOIN usuario_roles ur
                    ON r.rol_id = ur.rol_id
                WHERE ur.usuario_id = @usuarioId";

      SqlCommand comando = new SqlCommand(query, conexion);
      comando.Parameters.AddWithValue("@usuarioId", usuarioId);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        roles.Add(new Rol
        {
          RolId = Convert.ToInt32(reader["rol_id"]),
          Nombre = reader["nombre"].ToString(),
          Descripcion = reader["descripcion"].ToString(),
          Activo = Convert.ToBoolean(reader["activo"])
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return roles;
    }

    public void ActualizarRolesUsuario(int usuarioId, List<int> roles)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlTransaction transaccion = conexion.BeginTransaction();

      try
      {
        SqlCommand eliminar = new SqlCommand(
            "DELETE FROM usuario_roles WHERE usuario_id = @usuarioId",
            conexion,
            transaccion);

        eliminar.Parameters.AddWithValue("@usuarioId", usuarioId);
        eliminar.ExecuteNonQuery();

        foreach (int rolId in roles)
        {
          SqlCommand insertar = new SqlCommand(
              "INSERT INTO usuario_roles(usuario_id, rol_id) VALUES(@usuarioId,@rolId)",
              conexion,
              transaccion);

          insertar.Parameters.AddWithValue("@usuarioId", usuarioId);
          insertar.Parameters.AddWithValue("@rolId", rolId);

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

    public List<Rol> ObtenerTodos()
    {
      List<Rol> roles = new List<Rol>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        SELECT rol_id,
               nombre,
               descripcion,
               activo
        FROM roles
        ORDER BY nombre";

      SqlCommand comando = new SqlCommand(query, conexion);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        roles.Add(new Rol
        {
          RolId = Convert.ToInt32(reader["rol_id"]),
          Nombre = reader["nombre"].ToString(),
          Descripcion = reader["descripcion"].ToString(),
          Activo = Convert.ToBoolean(reader["activo"])
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return roles;
    }


  }
}

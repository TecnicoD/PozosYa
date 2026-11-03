using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class SeguridadRepository
  {
    private readonly Conexion conexionBD = new Conexion();


    private Usuario MapearUsuario(SqlDataReader reader)
    {
      return new Usuario
      {
        UsuarioId = Convert.ToInt32(reader["usuario_id"]),
        Nombre = reader["nombre"].ToString(),
        Apellido = reader["apellido"].ToString(),
        Username = reader["username"].ToString(),
        Email = reader["email"].ToString(),
        PasswordHash = reader["password_hash"].ToString(),
        Activo = Convert.ToBoolean(reader["activo"]),
        UltimoLogin = reader["ultimo_login"] == DBNull.Value
              ? (DateTime?)null
              : Convert.ToDateTime(reader["ultimo_login"]),
        FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]),
        DigitoVerificador = reader["digito_verificador"].ToString()
      };
    }


    public Usuario Login(string username, string passwordHash)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"SELECT * FROM usuarios
                             WHERE username=@username
                             AND password_hash=@password
                             AND activo=1";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@username", username);
      comando.Parameters.AddWithValue("@password", passwordHash);

      SqlDataReader reader = comando.ExecuteReader();

      Usuario usuario = null;

      if (reader.Read())
      {
        usuario = MapearUsuario(reader);
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return usuario;
    }



    public HashSet<string> ObtenerPermisos(int usuarioId)
    {
      HashSet<string> permisos = new HashSet<string>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      // 1) Permisos provenientes de los roles
      string queryRoles = @"
        SELECT DISTINCT p.nombre
        FROM usuario_roles ur
        INNER JOIN roles_permisos rp
            ON ur.rol_id = rp.rol_id
        INNER JOIN permisos p
            ON rp.permiso_id = p.permiso_id
        WHERE ur.usuario_id = @usuarioId";

      SqlCommand comandoRoles = new SqlCommand(queryRoles, conexion);
      comandoRoles.Parameters.AddWithValue("@usuarioId", usuarioId);

      SqlDataReader reader = comandoRoles.ExecuteReader();

      while (reader.Read())
      {
        permisos.Add(reader["nombre"].ToString());
      }

      reader.Close();

      // 2) Aplicar permisos individuales
      string queryUsuario = @"
        SELECT p.nombre, up.concedido
        FROM usuarios_permisos up
        INNER JOIN permisos p
            ON up.permiso_id = p.permiso_id
        WHERE up.usuario_id = @usuarioId";

      SqlCommand comandoUsuario = new SqlCommand(queryUsuario, conexion);
      comandoUsuario.Parameters.AddWithValue("@usuarioId", usuarioId);

      reader = comandoUsuario.ExecuteReader();

      while (reader.Read())
      {
        string permiso = reader["nombre"].ToString();
        bool concedido = Convert.ToBoolean(reader["concedido"]);

        if (concedido)
          permisos.Add(permiso);
        else
          permisos.Remove(permiso);
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return permisos;
    }


  }
}

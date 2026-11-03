using Datos.Connection;
using Negocio.Models;
using PY.Negocio.Security;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class UsuarioRepository
  {
    private readonly Conexion conexionBD = new Conexion();


    public List<Usuario> ObtenerTodos()
    {
      List<Usuario> usuarios = new List<Usuario>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = "SELECT * FROM usuarios";

      SqlCommand comando = new SqlCommand(query, conexion);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        Usuario usuario = new Usuario
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

        usuarios.Add(usuario);
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return usuarios;
    }

    public Usuario ObtenerPorId(int id)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = "SELECT * FROM usuarios WHERE usuario_id = @id";

      SqlCommand comando = new SqlCommand(query, conexion);
      comando.Parameters.AddWithValue("@id", id);

      SqlDataReader reader = comando.ExecuteReader();

      Usuario usuario = null;

      if (reader.Read())
      {
        usuario = new Usuario
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

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return usuario;
    }

    public int Agregar(Usuario usuario)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        INSERT INTO usuarios
        (
            nombre,
            apellido,
            username,
            email,
            password_hash,
            activo,
            ultimo_login,
            fecha_creacion,
            digito_verificador
        )
        VALUES
        (
            @nombre,
            @apellido,
            @username,
            @email,
            @password,
            @activo,
            @ultimoLogin,
            @fechaCreacion,
            @dv
        );

        SELECT SCOPE_IDENTITY();
    ";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
      comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
      comando.Parameters.AddWithValue("@username", usuario.Username);
      comando.Parameters.AddWithValue("@email", usuario.Email);
      comando.Parameters.AddWithValue("@password", usuario.PasswordHash);
      comando.Parameters.AddWithValue("@activo", usuario.Activo);
      comando.Parameters.AddWithValue("@ultimoLogin", (object)usuario.UltimoLogin ?? DBNull.Value);
      comando.Parameters.AddWithValue("@fechaCreacion", usuario.FechaCreacion);
      comando.Parameters.AddWithValue("@dv", Integridad.ObtenerSemillaSistema());

      int id = Convert.ToInt32(comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return id;
    }


    public bool Editar(Usuario usuario)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"UPDATE usuarios SET
                nombre=@nombre,
                apellido=@apellido,
                username=@username,
                email=@email,
                password_hash=@password,
                activo=@activo,
                ultimo_login=@ultimoLogin,
                digito_verificador=@dv
                WHERE usuario_id=@id";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@id", usuario.UsuarioId);
      comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
      comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
      comando.Parameters.AddWithValue("@username", usuario.Username);
      comando.Parameters.AddWithValue("@email", usuario.Email);
      comando.Parameters.AddWithValue("@password", usuario.PasswordHash);
      comando.Parameters.AddWithValue("@activo", usuario.Activo);
      comando.Parameters.AddWithValue("@ultimoLogin", (object)usuario.UltimoLogin ?? DBNull.Value);
      comando.Parameters.AddWithValue("@dv", Integridad.ObtenerSemillaSistema());

      int filas = comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);

      return filas > 0;
    }

    public bool Eliminar(int id)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = "DELETE FROM usuarios WHERE usuario_id=@id";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@id", id);

      int filas = comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);

      return filas > 0;
    }

    public bool ExisteUsuario(string username)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"SELECT COUNT(*) FROM usuarios
                             WHERE username=@username";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@username", username);

      int cantidad = Convert.ToInt32(comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return cantidad > 0;
    }

    public void Actualizar(Usuario usuario)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        UPDATE usuarios
        SET nombre = @nombre,
            apellido = @apellido,
            username = @username,
            email = @email,
            activo = @activo,
            digito_verificador = @dv
        WHERE usuario_id = @id";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@id", usuario.UsuarioId);
      comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
      comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
      comando.Parameters.AddWithValue("@username", usuario.Username);
      comando.Parameters.AddWithValue("@email", usuario.Email);
      comando.Parameters.AddWithValue("@activo", usuario.Activo);
      comando.Parameters.AddWithValue("@dv", Integridad.ObtenerSemillaSistema());

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }

    // Verifica si el username ya lo usa otro usuario
    // (usuarioIdExcluir = 0 para un alta).
    public bool ExisteUsername(string username, int usuarioIdExcluir)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        SELECT COUNT(*) FROM usuarios
        WHERE username = @username
          AND usuario_id <> @id";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@username", username);
      comando.Parameters.AddWithValue("@id", usuarioIdExcluir);

      int cantidad = Convert.ToInt32(comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return cantidad > 0;
    }

    // Verifica si el email ya lo usa otro usuario
    // (usuarioIdExcluir = 0 para un alta).
    public bool ExisteEmail(string email, int usuarioIdExcluir)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        SELECT COUNT(*) FROM usuarios
        WHERE email = @email
          AND usuario_id <> @id";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@email", email);
      comando.Parameters.AddWithValue("@id", usuarioIdExcluir);

      int cantidad = Convert.ToInt32(comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return cantidad > 0;
    }


    public void CambiarPassword(int usuarioId, string passwordHash)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        UPDATE usuarios
        SET password_hash = @password,
        digito_verificador = @dv
        WHERE usuario_id = @id";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@id", usuarioId);
      comando.Parameters.AddWithValue("@password", passwordHash);
      comando.Parameters.AddWithValue("@dv", Integridad.ObtenerSemillaSistema());

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }

    public List<Usuario> Buscar(string texto, bool? activo, int rolId)
    {
      List<Usuario> usuarios = new List<Usuario>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        SELECT DISTINCT u.*
        FROM usuarios u
        LEFT JOIN usuario_roles ur
            ON ur.usuario_id = u.usuario_id
        WHERE
        (@texto = ''
            OR u.nombre LIKE '%' + @texto + '%'
            OR u.apellido LIKE '%' + @texto + '%'
            OR u.username LIKE '%' + @texto + '%')

        AND
        (@activo IS NULL
            OR u.activo = @activo)

        AND
        (@rolId = 0
            OR ur.rol_id = @rolId)

        ORDER BY u.apellido, u.nombre";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@texto", texto);

      if (activo.HasValue)
        comando.Parameters.AddWithValue("@activo", activo.Value);
      else
        comando.Parameters.AddWithValue("@activo", DBNull.Value);

      comando.Parameters.AddWithValue("@rolId", rolId);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        Usuario usuario = new Usuario
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

        usuarios.Add(usuario);
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return usuarios;
    }

    public List<Usuario> ObtenerTodos(string texto, bool? activo, int? rolId)
    {
      List<Usuario> usuarios = new List<Usuario>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
        SELECT DISTINCT u.*
        FROM usuarios u
        LEFT JOIN usuario_roles ur
            ON u.usuario_id = ur.usuario_id
        WHERE
        (
            @texto = '' OR
            u.nombre LIKE '%' + @texto + '%' OR
            u.apellido LIKE '%' + @texto + '%' OR
            u.username LIKE '%' + @texto + '%'
        )
        AND
        (
            @activo IS NULL OR
            u.activo = @activo
        )
        AND
        (
            @rolId IS NULL OR
            ur.rol_id = @rolId
        )
        ORDER BY u.apellido, u.nombre";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@texto", texto);

      if (activo == null)
        comando.Parameters.AddWithValue("@activo", DBNull.Value);
      else
        comando.Parameters.AddWithValue("@activo", activo);

      if (rolId == null)
        comando.Parameters.AddWithValue("@rolId", DBNull.Value);
      else
        comando.Parameters.AddWithValue("@rolId", rolId);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        usuarios.Add(new Usuario
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
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return usuarios;
    }

    public Usuario ObtenerPorEmail(string email)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
      SELECT
          usuario_id,
          nombre,
          apellido,
          username,
          email,
          password_hash,
          activo,
          ultimo_login,
          fecha_creacion,
          digito_verificador
      FROM usuarios
      WHERE email = @Email";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@Email",
          email);

      SqlDataReader reader =
          comando.ExecuteReader();

      Usuario usuario = null;

      if (reader.Read())
      {
        usuario = new Usuario
        {
          UsuarioId =
              Convert.ToInt32(
                  reader["usuario_id"]),

          Nombre =
              reader["nombre"].ToString(),

          Apellido =
              reader["apellido"].ToString(),

          Username =
              reader["username"].ToString(),

          Email =
              reader["email"].ToString(),

          PasswordHash =
              reader["password_hash"].ToString(),

          Activo =
              Convert.ToBoolean(
                  reader["activo"]),

          UltimoLogin =
              reader["ultimo_login"] == DBNull.Value
                  ? (DateTime?)null
                  : Convert.ToDateTime(
                      reader["ultimo_login"]),

          FechaCreacion =
              Convert.ToDateTime(
                  reader["fecha_creacion"]),

          DigitoVerificador =
              reader["digito_verificador"] == DBNull.Value
                  ? ""
                  : reader["digito_verificador"].ToString()
        };
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return usuario;
    }


  }
}

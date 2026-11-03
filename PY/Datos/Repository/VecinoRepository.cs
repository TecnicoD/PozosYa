using Datos.Connection;
using PY.Negocio.Security;
using System;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class VecinoRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    public Vecino ObtenerPorDni(string dni)
    {
      Vecino vecino = null;

      string query = @"
                SELECT
                    vecino_id,
                    nombre,
                    apellido,
                    dni,
                    telefono,
                    mail,
                    direccion,
                    nro_catastral,
                    barrio_id,
                    fecha_registro,
                    activo,
                    digito_verificador
                FROM vecinos
                WHERE dni = @Dni
                  AND activo = 1";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);
      comando.Parameters.AddWithValue("@Dni", dni);

      SqlDataReader reader = comando.ExecuteReader();

      if (reader.Read())
      {
        vecino = new Vecino
        {
          VecinoId = Convert.ToInt32(reader["vecino_id"]),
          Nombre = reader["nombre"].ToString(),
          Apellido = reader["apellido"].ToString(),
          Dni = reader["dni"].ToString(),
          Telefono = reader["telefono"].ToString(),
          Mail = reader["mail"].ToString(),
          Direccion = reader["direccion"].ToString(),
          NroCatastral = reader["nro_catastral"].ToString(),
          BarrioId = Convert.ToInt32(reader["barrio_id"]),
          FechaRegistro = Convert.ToDateTime(reader["fecha_registro"]),
          Activo = Convert.ToBoolean(reader["activo"]),
          DigitoVerificador = reader["digito_verificador"].ToString()
        };
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return vecino;
    }

    public int Agregar(Vecino vecino)
    {
      string query = @"
        INSERT INTO vecinos
        (
            nombre,
            apellido,
            dni,
            telefono,
            mail,
            direccion,
            nro_catastral,
            barrio_id,
            fecha_registro,
            activo,
            digito_verificador
        )
        VALUES
        (
            @Nombre,
            @Apellido,
            @Dni,
            @Telefono,
            @Mail,
            @Direccion,
            @NroCatastral,
            @BarrioId,
            @FechaRegistro,
            @Activo,
            @DigitoVerificador
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@Nombre", vecino.Nombre);
      comando.Parameters.AddWithValue("@Apellido", vecino.Apellido);
      comando.Parameters.AddWithValue("@Dni", vecino.Dni);
      comando.Parameters.AddWithValue("@Telefono", vecino.Telefono);
      comando.Parameters.AddWithValue("@Mail", vecino.Mail);
      comando.Parameters.AddWithValue("@Direccion", vecino.Direccion);
      comando.Parameters.AddWithValue("@NroCatastral", vecino.NroCatastral);
      comando.Parameters.AddWithValue("@BarrioId", vecino.BarrioId);
      comando.Parameters.AddWithValue("@FechaRegistro", vecino.FechaRegistro);
      comando.Parameters.AddWithValue("@Activo", vecino.Activo);
      comando.Parameters.AddWithValue("@DigitoVerificador", Integridad.ObtenerSemillaSistema());

      int vecinoId = Convert.ToInt32(comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return vecinoId;
    }




  }
}

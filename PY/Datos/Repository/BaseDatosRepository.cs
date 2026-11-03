using Datos.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Datos.Repository
{
  // Creación y restauración de la base de datos PozosYa
  // (se usa cuando la aplicación arranca y no encuentra la base).
  public class BaseDatosRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    // Lanza SqlException si no se puede conectar al servidor.
    public bool ExisteBaseDatos()
    {
      using (SqlConnection conexion = conexionBD.ObtenerConexionAMaster())
      {
        SqlCommand comando = new SqlCommand(
            "SELECT DB_ID(@Nombre)",
            conexion);

        comando.Parameters.AddWithValue(
            "@Nombre",
            ConfiguracionConexion.NombreBaseDatos);

        return comando.ExecuteScalar() != DBNull.Value;
      }
    }

    // Ejecuta un script dividido en lotes por las líneas "GO" (igual que SSMS).
    public void EjecutarScript(string script)
    {
      string[] lotes = Regex.Split(
          script,
          @"^\s*GO\s*$",
          RegexOptions.Multiline | RegexOptions.IgnoreCase);

      using (SqlConnection conexion = conexionBD.ObtenerConexionAMaster())
      {
        foreach (string lote in lotes)
        {
          if (string.IsNullOrWhiteSpace(lote))
            continue;

          SqlCommand comando = new SqlCommand(lote, conexion);
          comando.CommandTimeout = 0;
          comando.ExecuteNonQuery();
        }
      }
    }

    // Borra la base (se usa si la creación quedó a medias).
    public void EliminarBaseDatos()
    {
      SqlConnection.ClearAllPools();

      using (SqlConnection conexion = conexionBD.ObtenerConexionAMaster())
      {
        SqlCommand comando = new SqlCommand(@"
            IF DB_ID(@Nombre) IS NOT NULL
            BEGIN
                DECLARE @Sql NVARCHAR(400) =
                    N'ALTER DATABASE ' + QUOTENAME(@Nombre) + N' SET SINGLE_USER WITH ROLLBACK IMMEDIATE; ' +
                    N'DROP DATABASE ' + QUOTENAME(@Nombre) + N';';
                EXEC sp_executesql @Sql;
            END",
            conexion);

        comando.Parameters.AddWithValue(
            "@Nombre",
            ConfiguracionConexion.NombreBaseDatos);

        comando.ExecuteNonQuery();
      }
    }

    // Restaura un .bak como base PozosYa, ubicando los archivos en las
    // carpetas de datos de este servidor (el .bak puede venir de otra PC).
    public void RestaurarBackup(string rutaBak)
    {
      using (SqlConnection conexion = conexionBD.ObtenerConexionAMaster())
      {
        // 1. Archivos que contiene el backup (datos y log)
        List<string[]> archivos = new List<string[]>();

        SqlCommand lista = new SqlCommand(
            "RESTORE FILELISTONLY FROM DISK = @Ruta",
            conexion);

        lista.Parameters.AddWithValue("@Ruta", rutaBak);
        lista.CommandTimeout = 0;

        using (SqlDataReader reader = lista.ExecuteReader())
        {
          while (reader.Read())
          {
            archivos.Add(new[]
            {
              reader["LogicalName"].ToString(),
              reader["Type"].ToString()
            });
          }
        }

        // 2. Carpetas de datos y log de este servidor
        string carpetaDatos =
            ObtenerCarpeta(conexion, "InstanceDefaultDataPath");

        string carpetaLog =
            ObtenerCarpeta(conexion, "InstanceDefaultLogPath");

        // 3. RESTORE ... WITH MOVE
        string nombre = ConfiguracionConexion.NombreBaseDatos;

        SqlCommand restaurar = new SqlCommand();
        restaurar.Connection = conexion;
        restaurar.CommandTimeout = 0;

        StringBuilder sql = new StringBuilder(
            "RESTORE DATABASE @Nombre FROM DISK = @Ruta WITH RECOVERY, REPLACE");

        int datos = 0;
        int logs = 0;

        for (int i = 0; i < archivos.Count; i++)
        {
          bool esLog = archivos[i][1] == "L";

          string archivoFisico = esLog
              ? Path.Combine(carpetaLog, nombre + "_log" + (logs++ == 0 ? "" : logs.ToString()) + ".ldf")
              : Path.Combine(carpetaDatos, nombre + (datos++ == 0 ? ".mdf" : "_" + datos + ".ndf"));

          sql.Append(", MOVE @Logico" + i + " TO @Fisico" + i);

          restaurar.Parameters.AddWithValue("@Logico" + i, archivos[i][0]);
          restaurar.Parameters.AddWithValue("@Fisico" + i, archivoFisico);
        }

        restaurar.CommandText = sql.ToString();
        restaurar.Parameters.AddWithValue("@Nombre", nombre);
        restaurar.Parameters.AddWithValue("@Ruta", rutaBak);

        restaurar.ExecuteNonQuery();
      }
    }

    private string ObtenerCarpeta(SqlConnection conexion, string propiedad)
    {
      SqlCommand comando = new SqlCommand(
          "SELECT CAST(SERVERPROPERTY('" + propiedad + "') AS NVARCHAR(500))",
          conexion);

      object resultado = comando.ExecuteScalar();

      if (resultado != null &&
          resultado != DBNull.Value &&
          !string.IsNullOrWhiteSpace(resultado.ToString()))
      {
        return resultado.ToString();
      }

      // Versiones viejas sin esa propiedad: se usa la carpeta de la base master.
      comando.CommandText =
          "SELECT physical_name FROM sys.master_files WHERE database_id = 1 AND file_id = 1";

      return Path.GetDirectoryName(comando.ExecuteScalar().ToString());
    }
  }
}

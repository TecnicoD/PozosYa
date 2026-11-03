using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class ArchivoRepository
  {
    private readonly Conexion conexionBD =
        new Conexion();

    public List<Archivo> ObtenerPorReclamo(int reclamoId)
    {
      List<Archivo> lista =
          new List<Archivo>();

      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                SELECT
                    archivo_id,
                    reclamo_id,
                    tipo_archivo,
                    ubicacion,
                    nombre_original,
                    tamanio,
                    fecha_carga,
                    usuario_id,
                    activo
                FROM archivos
                WHERE reclamo_id = @ReclamoId
                  AND activo = 1
                ORDER BY fecha_carga DESC";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@ReclamoId",
          reclamoId);

      SqlDataReader reader =
          comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new Archivo
        {
          ArchivoId =
                Convert.ToInt32(
                    reader["archivo_id"]),

          ReclamoId =
                Convert.ToInt32(
                    reader["reclamo_id"]),

          TipoArchivo =
                reader["tipo_archivo"].ToString(),

          Ubicacion =
                reader["ubicacion"].ToString(),

          NombreOriginal =
                reader["nombre_original"].ToString(),

          Tamanio =
                Convert.ToInt64(
                    reader["tamanio"]),

          FechaCarga =
                Convert.ToDateTime(
                    reader["fecha_carga"]),

          UsuarioId =
                Convert.ToInt32(
                    reader["usuario_id"]),

          Activo =
                Convert.ToBoolean(
                    reader["activo"])
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }

    public int Agregar(Archivo archivo)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                INSERT INTO archivos
                (
                    reclamo_id,
                    tipo_archivo,
                    ubicacion,
                    nombre_original,
                    tamanio,
                    usuario_id,
                    activo
                )
                VALUES
                (
                    @ReclamoId,
                    @TipoArchivo,
                    @Ubicacion,
                    @NombreOriginal,
                    @Tamanio,
                    @UsuarioId,
                    1
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@ReclamoId",
          archivo.ReclamoId);

      comando.Parameters.AddWithValue(
          "@TipoArchivo",
          archivo.TipoArchivo);

      comando.Parameters.AddWithValue(
          "@Ubicacion",
          archivo.Ubicacion);

      comando.Parameters.AddWithValue(
          "@NombreOriginal",
          archivo.NombreOriginal);

      comando.Parameters.AddWithValue(
          "@Tamanio",
          archivo.Tamanio);

      comando.Parameters.AddWithValue(
          "@UsuarioId",
          archivo.UsuarioId);

      int archivoId =
          Convert.ToInt32(
              comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return archivoId;
    }
  }
}

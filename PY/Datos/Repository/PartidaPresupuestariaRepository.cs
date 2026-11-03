using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class PartidaPresupuestariaRepository
  {
    private readonly Conexion conexionBD =
        new Conexion();

    public List<PartidaPresupuestaria> ObtenerTodos()
    {
      List<PartidaPresupuestaria> lista =
          new List<PartidaPresupuestaria>();

      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                SELECT
                    partida_presupuestaria_id,
                    anio,
                    trimestre,
                    monto_asignado,
                    monto_comprometido,
                    monto_ejecutado,
                    usuario_gerente_id,
                    fecha_asignacion,
                    observaciones,
                    activo
                FROM partidas_presupuestarias
                ORDER BY anio DESC, trimestre DESC";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      SqlDataReader reader =
          comando.ExecuteReader();

      while (reader.Read())
      {
        PartidaPresupuestaria partida =
            new PartidaPresupuestaria
            {
              PartidaPresupuestariaId =
                    Convert.ToInt32(
                        reader["partida_presupuestaria_id"]),

              Anio =
                    Convert.ToInt32(
                        reader["anio"]),

              Trimestre =
                    Convert.ToInt32(
                        reader["trimestre"]),

              MontoAsignado =
                    Convert.ToDecimal(
                        reader["monto_asignado"]),

              MontoComprometido =
                    Convert.ToDecimal(
                        reader["monto_comprometido"]),

              MontoEjecutado =
                    Convert.ToDecimal(
                        reader["monto_ejecutado"]),

              UsuarioGerenteId =
                    Convert.ToInt32(
                        reader["usuario_gerente_id"]),

              FechaAsignacion =
                    Convert.ToDateTime(
                        reader["fecha_asignacion"]),

              Observaciones =
                    reader["observaciones"] == DBNull.Value
                        ? ""
                        : reader["observaciones"].ToString(),

              Activo =
                    Convert.ToBoolean(
                        reader["activo"])
            };

        lista.Add(partida);
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }

    public PartidaPresupuestaria ObtenerPorId(
        int partidaPresupuestariaId)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                SELECT
                    partida_presupuestaria_id,
                    anio,
                    trimestre,
                    monto_asignado,
                    monto_comprometido,
                    monto_ejecutado,
                    usuario_gerente_id,
                    fecha_asignacion,
                    observaciones,
                    activo
                FROM partidas_presupuestarias
                WHERE partida_presupuestaria_id = @id";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@id",
          partidaPresupuestariaId);

      SqlDataReader reader =
          comando.ExecuteReader();

      if (!reader.Read())
      {
        reader.Close();
        conexionBD.CerrarConexion(conexion);
        return null;
      }

      PartidaPresupuestaria partida =
          new PartidaPresupuestaria
          {
            PartidaPresupuestariaId =
                  Convert.ToInt32(
                      reader["partida_presupuestaria_id"]),

            Anio =
                  Convert.ToInt32(
                      reader["anio"]),

            Trimestre =
                  Convert.ToInt32(
                      reader["trimestre"]),

            MontoAsignado =
                  Convert.ToDecimal(
                      reader["monto_asignado"]),

            MontoComprometido =
                  Convert.ToDecimal(
                      reader["monto_comprometido"]),

            MontoEjecutado =
                  Convert.ToDecimal(
                      reader["monto_ejecutado"]),

            UsuarioGerenteId =
                  Convert.ToInt32(
                      reader["usuario_gerente_id"]),

            FechaAsignacion =
                  Convert.ToDateTime(
                      reader["fecha_asignacion"]),

            Observaciones =
                  reader["observaciones"] == DBNull.Value
                      ? ""
                      : reader["observaciones"].ToString(),

            Activo =
                  Convert.ToBoolean(
                      reader["activo"])
          };

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return partida;
    }

    public int Agregar(
        PartidaPresupuestaria partida)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                INSERT INTO partidas_presupuestarias
                (
                    anio,
                    trimestre,
                    monto_asignado,
                    monto_comprometido,
                    monto_ejecutado,
                    usuario_gerente_id,
                    fecha_asignacion,
                    observaciones,
                    activo
                )
                VALUES
                (
                    @anio,
                    @trimestre,
                    @montoAsignado,
                    @montoComprometido,
                    @montoEjecutado,
                    @usuarioGerenteId,
                    @fechaAsignacion,
                    @observaciones,
                    @activo
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@anio", partida.Anio);

      comando.Parameters.AddWithValue(
          "@trimestre", partida.Trimestre);

      comando.Parameters.AddWithValue(
          "@montoAsignado",
          partida.MontoAsignado);

      comando.Parameters.AddWithValue(
          "@montoComprometido",
          partida.MontoComprometido);

      comando.Parameters.AddWithValue(
          "@montoEjecutado",
          partida.MontoEjecutado);

      comando.Parameters.AddWithValue(
          "@usuarioGerenteId",
          partida.UsuarioGerenteId);

      comando.Parameters.AddWithValue(
          "@fechaAsignacion",
          partida.FechaAsignacion);

      comando.Parameters.AddWithValue(
          "@observaciones",
          string.IsNullOrWhiteSpace(
              partida.Observaciones)
              ? (object)DBNull.Value
              : partida.Observaciones);

      comando.Parameters.AddWithValue(
          "@activo", partida.Activo);

      int id =
          Convert.ToInt32(
              comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return id;
    }

    public void Actualizar(
        PartidaPresupuestaria partida)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                UPDATE partidas_presupuestarias
                SET
                    anio = @anio,
                    trimestre = @trimestre,
                    monto_asignado = @montoAsignado,
                    monto_comprometido = @montoComprometido,
                    monto_ejecutado = @montoEjecutado,
                    usuario_gerente_id = @usuarioGerenteId,
                    observaciones = @observaciones,
                    activo = @activo
                WHERE partida_presupuestaria_id =
                    @id";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@id",
          partida.PartidaPresupuestariaId);

      comando.Parameters.AddWithValue(
          "@anio", partida.Anio);

      comando.Parameters.AddWithValue(
          "@trimestre", partida.Trimestre);

      comando.Parameters.AddWithValue(
          "@montoAsignado",
          partida.MontoAsignado);

      comando.Parameters.AddWithValue(
          "@montoComprometido",
          partida.MontoComprometido);

      comando.Parameters.AddWithValue(
          "@montoEjecutado",
          partida.MontoEjecutado);

      comando.Parameters.AddWithValue(
          "@usuarioGerenteId",
          partida.UsuarioGerenteId);

      comando.Parameters.AddWithValue(
          "@observaciones",
          string.IsNullOrWhiteSpace(
              partida.Observaciones)
              ? (object)DBNull.Value
              : partida.Observaciones);

      comando.Parameters.AddWithValue(
          "@activo", partida.Activo);

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }



    public List<PartidaPresupuestaria> ObtenerTodasDisponibles()
    {
      List<PartidaPresupuestaria> lista =
          new List<PartidaPresupuestaria>();

      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
      SELECT
          partida_presupuestaria_id,
          anio,
          trimestre,
          monto_asignado,
          monto_comprometido,
          monto_ejecutado,
          usuario_gerente_id,
          fecha_asignacion,
          observaciones,
          activo
      FROM partidas_presupuestarias
      WHERE activo = 1
      ORDER BY anio DESC, trimestre DESC";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      SqlDataReader reader =
          comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new PartidaPresupuestaria
        {
          PartidaPresupuestariaId =
              Convert.ToInt32(
                  reader["partida_presupuestaria_id"]),

          Anio =
              Convert.ToInt32(
                  reader["anio"]),

          Trimestre =
              Convert.ToInt32(
                  reader["trimestre"]),

          MontoAsignado =
              Convert.ToDecimal(
                  reader["monto_asignado"]),

          MontoComprometido =
              Convert.ToDecimal(
                  reader["monto_comprometido"]),

          MontoEjecutado =
              Convert.ToDecimal(
                  reader["monto_ejecutado"]),

          UsuarioGerenteId =
              Convert.ToInt32(
                  reader["usuario_gerente_id"]),

          FechaAsignacion =
              Convert.ToDateTime(
                  reader["fecha_asignacion"]),

          Observaciones =
              reader["observaciones"] == DBNull.Value
                  ? ""
                  : reader["observaciones"].ToString(),

          Activo =
              Convert.ToBoolean(
                  reader["activo"])
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }

    // Verifica si ya hay otra partida para el mismo año y trimestre
    // (partidaIdExcluir = 0 para un alta).
    public bool ExistePeriodo(
        int anio,
        int trimestre,
        int partidaIdExcluir)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
      SELECT COUNT(*)
      FROM partidas_presupuestarias
      WHERE anio = @Anio
        AND trimestre = @Trimestre
        AND partida_presupuestaria_id <> @PartidaId";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@Anio", anio);
      comando.Parameters.AddWithValue("@Trimestre", trimestre);
      comando.Parameters.AddWithValue("@PartidaId", partidaIdExcluir);

      int cantidad =
          Convert.ToInt32(
              comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return cantidad > 0;
    }

    public void AgregarComprometido(
    int partidaId,
    decimal monto)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
      UPDATE partidas_presupuestarias
      SET monto_comprometido =
          monto_comprometido + @Monto
      WHERE partida_presupuestaria_id = @PartidaId";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@Monto",
          monto);

      comando.Parameters.AddWithValue(
          "@PartidaId",
          partidaId);

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }




  }
}

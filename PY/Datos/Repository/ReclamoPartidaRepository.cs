using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class ReclamoPartidaRepository
  {
    private readonly Conexion conexionBD =
        new Conexion();

    public void Agregar(
        int reclamoId,
        int partidaId,
        decimal monto,
        int usuarioPriorizadorId,
        int numeroPrioridad)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
          INSERT INTO reclamos_partida
          (
              reclamo_id,
              partida_id,
              monto_asignado_reclamo,
              usuario_priorizador_id,
              numero_prioridad
          )
          VALUES
          (
              @ReclamoId,
              @PartidaId,
              @Monto,
              @UsuarioPriorizadorId,
              @NumeroPrioridad
          )";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@ReclamoId",
          reclamoId);

      comando.Parameters.AddWithValue(
          "@PartidaId",
          partidaId);

      comando.Parameters.AddWithValue(
          "@Monto",
          monto);

      comando.Parameters.AddWithValue(
          "@UsuarioPriorizadorId",
          usuarioPriorizadorId);

      comando.Parameters.AddWithValue(
          "@NumeroPrioridad",
          numeroPrioridad);

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }

    public List<ReclamoPriorizacionDTO> ObtenerPriorizadosPorPartida(
        int partidaId)
    {
      List<ReclamoPriorizacionDTO> lista =
          new List<ReclamoPriorizacionDTO>();

      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      // Solo los reclamos que siguen en estado PRIORIZADO:
      // los que ya están en ejecución o finalizados no se reordenan.
      string query = @"
          SELECT
              r.reclamo_id,
              r.numero_reclamo,
              r.direccion,
              barrio.valor AS barrio,
              estado.valor AS estado,
              rp.monto_asignado_reclamo
          FROM reclamos_partida rp

          INNER JOIN reclamos r
              ON rp.reclamo_id = r.reclamo_id

          INNER JOIN detalleClaveValor barrio
              ON r.barrio_id = barrio.detalle_id

          INNER JOIN detalleClaveValor estado
              ON r.estado_actual_id = estado.detalle_id

          WHERE rp.partida_id = @PartidaId
            AND estado.clave = 'PRIORIZADO'

          ORDER BY rp.numero_prioridad ASC";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@PartidaId",
          partidaId);

      SqlDataReader reader =
          comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new ReclamoPriorizacionDTO
        {
          ReclamoId =
              Convert.ToInt32(
                  reader["reclamo_id"]),

          NumeroReclamo =
              reader["numero_reclamo"].ToString(),

          Direccion =
              reader["direccion"].ToString(),

          Barrio =
              reader["barrio"].ToString(),

          Estado =
              reader["estado"].ToString(),

          MontoPresupuesto =
              Convert.ToDecimal(
                  reader["monto_asignado_reclamo"]),

          YaAsignado = true
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }

    public void ActualizarPrioridad(
        int reclamoId,
        int partidaId,
        int numeroPrioridad)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
          UPDATE reclamos_partida
          SET numero_prioridad = @NumeroPrioridad
          WHERE reclamo_id = @ReclamoId
            AND partida_id = @PartidaId";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@NumeroPrioridad",
          numeroPrioridad);

      comando.Parameters.AddWithValue(
          "@ReclamoId",
          reclamoId);

      comando.Parameters.AddWithValue(
          "@PartidaId",
          partidaId);

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }
  }
}

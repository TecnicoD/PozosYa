using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class HistorialReclamoRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    public List<HistorialReclamo> ObtenerPorReclamo(int reclamoId)
    {
      List<HistorialReclamo> lista = new List<HistorialReclamo>();

      string query = @"
        SELECT
            h.historial_reclamo_id,
            h.reclamo_id,
            h.estado_anterior_id,
            h.usuario_id,
            h.fecha,
            h.usuario_modificador,
            h.fecha_modificacion,

            e.valor AS estado_anterior,
            n.valor AS estado_nuevo,

            ISNULL(u.nombre + ' ' + u.apellido, 'Usuario desconocido') AS usuario

        FROM
        (
            -- El estado nuevo de cada cambio es el estado anterior del
            -- cambio siguiente; para el último, el estado actual del reclamo.
            SELECT
                hr.*,
                ISNULL(
                    LEAD(hr.estado_anterior_id) OVER (
                        ORDER BY hr.fecha, hr.historial_reclamo_id),
                    r.estado_actual_id) AS estado_nuevo_id
            FROM historial_reclamo hr
            INNER JOIN reclamos r
                ON hr.reclamo_id = r.reclamo_id
            WHERE hr.reclamo_id = @ReclamoId
        ) h

        LEFT JOIN detalleClaveValor e
            ON h.estado_anterior_id = e.detalle_id

        LEFT JOIN detalleClaveValor n
            ON h.estado_nuevo_id = n.detalle_id

        LEFT JOIN usuarios u
            ON h.usuario_id = u.usuario_id

        ORDER BY h.fecha DESC, h.historial_reclamo_id DESC";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@ReclamoId", reclamoId);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new HistorialReclamo
        {
          HistorialReclamoId =
                Convert.ToInt32(reader["historial_reclamo_id"]),

          ReclamoId =
                Convert.ToInt32(reader["reclamo_id"]),

          EstadoAnteriorId =
                Convert.ToInt32(reader["estado_anterior_id"]),

          UsuarioId =
                Convert.ToInt32(reader["usuario_id"]),

          Fecha =
                Convert.ToDateTime(reader["fecha"]),

          UsuarioModificador =
                reader["usuario_modificador"] == DBNull.Value
                    ? (int?)null
                    : Convert.ToInt32(reader["usuario_modificador"]),

          FechaModificacion =
                reader["fecha_modificacion"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(reader["fecha_modificacion"]),

          EstadoAnterior =
                reader["estado_anterior"] == DBNull.Value
                    ? "Sin estado"
                    : reader["estado_anterior"].ToString(),

          EstadoNuevo =
                reader["estado_nuevo"] == DBNull.Value
                    ? ""
                    : reader["estado_nuevo"].ToString(),

          Usuario =
                reader["usuario"].ToString()
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }



  }
}

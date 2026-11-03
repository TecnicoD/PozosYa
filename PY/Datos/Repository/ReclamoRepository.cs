using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Negocio.Security;
using PY.Negocio.Security;

namespace Datos.Repository
{
  public class ReclamoRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    public List<ReclamoListado> ObtenerListado()
    {
      List<ReclamoListado> lista = new List<ReclamoListado>();

      string query = @"
                SELECT
                    r.reclamo_id,
                    r.numero_reclamo,
                    r.fecha_creacion,
                    CONCAT(v.nombre, ' ', v.apellido) AS vecino,
                    r.direccion,
                    b.valor AS barrio,
                    e.valor AS estado
                FROM reclamos r
                INNER JOIN vecinos v
                    ON r.vecino_id = v.vecino_id
                INNER JOIN detalleClaveValor b
                    ON r.barrio_id = b.detalle_id
                INNER JOIN detalleClaveValor e
                    ON r.estado_actual_id = e.detalle_id
                ORDER BY r.fecha_creacion DESC";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new ReclamoListado
        {
          ReclamoId = Convert.ToInt32(reader["reclamo_id"]),
          NumeroReclamo = reader["numero_reclamo"].ToString(),
          FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]),
          Vecino = reader["vecino"].ToString(),
          Direccion = reader["direccion"].ToString(),
          Barrio = reader["barrio"].ToString(),
          Estado = reader["estado"].ToString(),

        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }

    public ReclamoDetalleDTO ObtenerPorId(int reclamoId)
    {
      ReclamoDetalleDTO reclamo = null;

      string query = @"
                SELECT
        r.reclamo_id,
        r.numero_reclamo,
        v.nombre + ' ' + v.apellido AS Vecino,
        v.dni,
        v.telefono,
        v.mail,
        v.nro_catastral,
        v.fecha_registro,
        r.direccion,

        r.barrio_id,
        r.estado_actual_id,
        r.tipo_danio_id,
        r.severidad_id,
        b.valor AS Barrio,
        e.valor AS Estado,
        td.valor AS TipoDanio,
        s.valor AS Severidad,


        r.descripcion,
        r.latitud,
        r.longitud,
        r.fecha_creacion
        FROM reclamos r

        INNER JOIN vecinos v
        ON r.vecino_id = v.vecino_id

        INNER JOIN detalleClaveValor b
        ON r.barrio_id = b.detalle_id

        INNER JOIN detalleClaveValor e
        ON r.estado_actual_id = e.detalle_id

        INNER JOIN detalleClaveValor td
        ON r.tipo_danio_id = td.detalle_id

        INNER JOIN detalleClaveValor s
        ON r.severidad_id = s.detalle_id

        WHERE r.reclamo_id = @ReclamoId;";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);
      comando.Parameters.AddWithValue("@ReclamoId", reclamoId);

      SqlDataReader reader = comando.ExecuteReader();

      if (reader.Read())
      {
        reclamo = new ReclamoDetalleDTO
        {
          ReclamoId = Convert.ToInt32(reader["reclamo_id"]),
          NumeroReclamo = reader["numero_reclamo"].ToString(),
          Vecino = reader["Vecino"].ToString(),
          DNI = reader["dni"].ToString(),
          Direccion = reader["direccion"].ToString(),
          Barrio = reader["Barrio"].ToString(),
          Estado = reader["Estado"].ToString(),
          TipoDanio = reader["TipoDanio"].ToString(),
          Severidad = reader["Severidad"].ToString(),
          Descripcion = reader["descripcion"].ToString(),
          Latitud = reader["latitud"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["latitud"]),
          Longitud = reader["longitud"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["longitud"]),
          FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]),
          Telefono = reader["telefono"].ToString(),
          Mail = reader["mail"].ToString(),
          NroCatastral = reader["nro_catastral"].ToString(),
          FechaRegistroVecino = Convert.ToDateTime(reader["fecha_registro"]),
          BarrioId = Convert.ToInt32(reader["barrio_id"]),
          EstadoId = Convert.ToInt32(reader["estado_actual_id"]),
          TipoDanioId = Convert.ToInt32(reader["tipo_danio_id"]),
          SeveridadId = Convert.ToInt32(reader["severidad_id"]),
        };
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return reclamo;
    }

    public void Actualizar(ReclamoDetalleDTO reclamo)
    {
      string query = @"
        DECLARE @EstadoAnteriorId INT;

        SELECT @EstadoAnteriorId = estado_actual_id
        FROM reclamos
        WHERE reclamo_id = @ReclamoId;

        UPDATE reclamos
        SET
            direccion = @Direccion,
            descripcion = @Descripcion,
            barrio_id = @BarrioId,
            estado_actual_id = @EstadoId,
            tipo_danio_id = @TipoDanioId,
            severidad_id = @SeveridadId,
            fecha_modificacion = GETDATE(),
            usuario_modificador_id = @UsuarioId,
            digito_verificador = @DigitoVerificador
        WHERE reclamo_id = @ReclamoId;

        IF @EstadoAnteriorId <> @EstadoId
        BEGIN
            INSERT INTO historial_reclamo
            (
                reclamo_id,
                estado_anterior_id,
                usuario_id,
                fecha,
                usuario_modificador,
                fecha_modificacion
            )
            VALUES
            (
                @ReclamoId,
                @EstadoAnteriorId,
                @UsuarioId,
                GETDATE(),
                @UsuarioId,
                GETDATE()
            );
        END";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@Direccion", reclamo.Direccion);
      comando.Parameters.AddWithValue("@Descripcion", reclamo.Descripcion);
      comando.Parameters.AddWithValue("@BarrioId", reclamo.BarrioId);
      comando.Parameters.AddWithValue("@EstadoId", reclamo.EstadoId);
      comando.Parameters.AddWithValue("@TipoDanioId", reclamo.TipoDanioId);
      comando.Parameters.AddWithValue("@SeveridadId", reclamo.SeveridadId);
      comando.Parameters.AddWithValue("@ReclamoId", reclamo.ReclamoId);
      comando.Parameters.AddWithValue("@UsuarioId", SesionActual.Usuario.UsuarioId);
      comando.Parameters.AddWithValue("@DigitoVerificador", Integridad.ObtenerSemillaSistema());
      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }


    public int Agregar(Reclamo reclamo)
    {
      string query = @"
        INSERT INTO reclamos
        (
            numero_reclamo,
            vecino_id,
            tipo_danio_id,
            severidad_id,
            estado_actual_id,
            descripcion,
            direccion,
            latitud,
            longitud,
            barrio_id,
            fecha_creacion,
            usuario_creador_id,
            digito_verificador
        )
        VALUES
        (
            @NumeroReclamo,
            @VecinoId,
            @TipoDanioId,
            @SeveridadId,
            @EstadoActualId,
            @Descripcion,
            @Direccion,
            @Latitud,
            @Longitud,
            @BarrioId,
            @FechaCreacion,
            @UsuarioCreadorId,
            @DigitoVerificador
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@NumeroReclamo", reclamo.NumeroReclamo);
      comando.Parameters.AddWithValue("@VecinoId", reclamo.VecinoId);
      comando.Parameters.AddWithValue("@TipoDanioId", reclamo.TipoDanioId);
      comando.Parameters.AddWithValue("@SeveridadId", reclamo.SeveridadId);
      comando.Parameters.AddWithValue("@EstadoActualId", reclamo.EstadoActualId);
      comando.Parameters.AddWithValue("@Descripcion", reclamo.Descripcion);
      comando.Parameters.AddWithValue("@Direccion", reclamo.Direccion);

      comando.Parameters.AddWithValue(
          "@Latitud",
          reclamo.Latitud.HasValue ? (object)reclamo.Latitud.Value : DBNull.Value);

      comando.Parameters.AddWithValue(
          "@Longitud",
          reclamo.Longitud.HasValue ? (object)reclamo.Longitud.Value : DBNull.Value);

      comando.Parameters.AddWithValue("@BarrioId", reclamo.BarrioId);
      comando.Parameters.AddWithValue("@FechaCreacion", reclamo.FechaCreacion);
      comando.Parameters.AddWithValue("@UsuarioCreadorId", reclamo.UsuarioCreadorId);
      //comando.Parameters.AddWithValue("@DigitoVerificador", reclamo.DigitoVerificador);
      comando.Parameters.AddWithValue("@DigitoVerificador", Integridad.ObtenerSemillaSistema());



      int reclamoId = Convert.ToInt32(comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return reclamoId;
    }

    public List<ReclamoListado> Buscar(ReclamoFiltro filtro)
    {
      List<ReclamoListado> lista = new List<ReclamoListado>();

      string query = @"
        SELECT
            r.reclamo_id,
            r.numero_reclamo,
            r.fecha_creacion,
            CONCAT(v.nombre, ' ', v.apellido) AS vecino,
            r.direccion,
            b.valor AS barrio,
            e.valor AS estado

        FROM reclamos r

        INNER JOIN vecinos v
            ON r.vecino_id = v.vecino_id

        INNER JOIN detalleClaveValor b
            ON r.barrio_id = b.detalle_id

        INNER JOIN detalleClaveValor e
            ON r.estado_actual_id = e.detalle_id

        WHERE
            (@DNI = '' OR v.dni LIKE '%' + @DNI + '%')

            AND (@BarrioId IS NULL OR r.barrio_id = @BarrioId)

            AND (@EstadoId IS NULL OR r.estado_actual_id = @EstadoId)

            AND (@FechaDesde IS NULL OR r.fecha_creacion >= @FechaDesde)

            AND (@FechaHasta IS NULL OR r.fecha_creacion < DATEADD(DAY, 1, @FechaHasta))

        ORDER BY r.fecha_creacion DESC";

      SqlConnection conexion = conexionBD.ObtenerConexion();

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@DNI",
          string.IsNullOrWhiteSpace(filtro.DNI)
              ? ""
              : filtro.DNI.Trim());

      comando.Parameters.AddWithValue(
          "@BarrioId",
          filtro.BarrioId.HasValue
              ? (object)filtro.BarrioId.Value
              : DBNull.Value);

      comando.Parameters.AddWithValue(
          "@EstadoId",
          filtro.EstadoId.HasValue
              ? (object)filtro.EstadoId.Value
              : DBNull.Value);

      comando.Parameters.AddWithValue(
          "@FechaDesde",
          filtro.FechaDesde.HasValue
              ? (object)filtro.FechaDesde.Value.Date
              : DBNull.Value);

      comando.Parameters.AddWithValue(
          "@FechaHasta",
          filtro.FechaHasta.HasValue
              ? (object)filtro.FechaHasta.Value.Date
              : DBNull.Value);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        lista.Add(new ReclamoListado
        {
          ReclamoId = Convert.ToInt32(reader["reclamo_id"]),
          NumeroReclamo = reader["numero_reclamo"].ToString(),
          FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]),
          Vecino = reader["vecino"].ToString(),
          Direccion = reader["direccion"].ToString(),
          Barrio = reader["barrio"].ToString(),
          Estado = reader["estado"].ToString()
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }

    public List<ReclamoPriorizacionDTO> ObtenerParaPriorizacion()
    {
      List<ReclamoPriorizacionDTO> lista =
          new List<ReclamoPriorizacionDTO>();

      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
      SELECT
          r.reclamo_id,
          r.numero_reclamo,
          r.direccion,
          barrio.valor AS barrio,
          estado.valor AS estado,
          p.monto_estimado AS monto_presupuesto
      FROM reclamos r

      INNER JOIN detalleClaveValor barrio
          ON r.barrio_id = barrio.detalle_id

      INNER JOIN detalleClaveValor estado
          ON r.estado_actual_id = estado.detalle_id

      INNER JOIN presupuestos p
          ON r.reclamo_id = p.reclamo_id

      WHERE estado.clave = 'PRESUPUESTADO'

      ORDER BY r.fecha_creacion ASC";

      SqlCommand comando =
          new SqlCommand(query, conexion);

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
                  reader["monto_presupuesto"])
        });
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return lista;
    }


  }
}

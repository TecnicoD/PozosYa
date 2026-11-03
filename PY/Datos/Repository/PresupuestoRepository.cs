using Datos.Connection;
using Negocio.Models;
using PY.Negocio.Security;
using System;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class PresupuestoRepository
  {
    private readonly Conexion conexionBD =
        new Conexion();

    public Presupuesto ObtenerPorReclamo(int reclamoId)
    {
      Presupuesto presupuesto = null;

      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                SELECT
                    p.presupuesto_id,
                    p.reclamo_id,
                    p.proveedor_id,
                    p.monto_estimado,
                    p.monto_final,
                    p.archivo_pdf,
                    p.fecha_carga,
                    p.usuario_id,
                    p.digito_verificador,
                    pr.nombre AS nombre_proveedor
                FROM presupuestos p
                INNER JOIN proveedores pr
                    ON p.proveedor_id = pr.proveedor_id
                WHERE p.reclamo_id = @ReclamoId";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@ReclamoId",
          reclamoId);

      SqlDataReader reader =
          comando.ExecuteReader();

      if (reader.Read())
      {
        presupuesto = new Presupuesto
        {
          PresupuestoId =
                Convert.ToInt32(reader["presupuesto_id"]),

          ReclamoId =
                Convert.ToInt32(reader["reclamo_id"]),

          ProveedorId =
                Convert.ToInt32(reader["proveedor_id"]),

          MontoEstimado =
                Convert.ToDecimal(reader["monto_estimado"]),

          MontoFinal =
                reader["monto_final"] == DBNull.Value
                    ? (decimal?)null
                    : Convert.ToDecimal(reader["monto_final"]),

          ArchivoPdf =
                reader["archivo_pdf"] == DBNull.Value
                    ? null
                    : reader["archivo_pdf"].ToString(),

          FechaCarga =
                Convert.ToDateTime(reader["fecha_carga"]),

          UsuarioId =
                Convert.ToInt32(reader["usuario_id"]),

          DigitoVerificador =
                reader["digito_verificador"] == DBNull.Value
                    ? ""
                    : reader["digito_verificador"].ToString(),

          NombreProveedor =
                reader["nombre_proveedor"].ToString()
        };
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return presupuesto;
    }

    public int Agregar(Presupuesto presupuesto)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                INSERT INTO presupuestos
                (
                    reclamo_id,
                    proveedor_id,
                    monto_estimado,
                    monto_final,
                    archivo_pdf,
                    usuario_id,
                    digito_verificador
                )
                VALUES
                (
                    @ReclamoId,
                    @ProveedorId,
                    @MontoEstimado,
                    @MontoFinal,
                    @ArchivoPdf,
                    @UsuarioId,
                    @DigitoVerificador
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@ReclamoId",
          presupuesto.ReclamoId);

      comando.Parameters.AddWithValue(
          "@ProveedorId",
          presupuesto.ProveedorId);

      comando.Parameters.AddWithValue(
          "@MontoEstimado",
          presupuesto.MontoEstimado);

      comando.Parameters.AddWithValue(
          "@MontoFinal",
          presupuesto.MontoFinal.HasValue
              ? (object)presupuesto.MontoFinal.Value
              : DBNull.Value);

      comando.Parameters.AddWithValue(
          "@ArchivoPdf",
          string.IsNullOrWhiteSpace(
              presupuesto.ArchivoPdf)
              ? (object)DBNull.Value
              : presupuesto.ArchivoPdf);

      comando.Parameters.AddWithValue(
          "@UsuarioId",
          presupuesto.UsuarioId);

      comando.Parameters.AddWithValue(
          "@DigitoVerificador",
          Integridad.ObtenerSemillaSistema());

      int id =
          Convert.ToInt32(
              comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return id;
    }

    public void Actualizar(Presupuesto presupuesto)
    {
      SqlConnection conexion =
          conexionBD.ObtenerConexion();

      string query = @"
                UPDATE presupuestos
                SET
                    proveedor_id = @ProveedorId,
                    monto_estimado = @MontoEstimado,
                    monto_final = @MontoFinal,
                    archivo_pdf = @ArchivoPdf,
                    digito_verificador = @DigitoVerificador
                WHERE presupuesto_id = @PresupuestoId";

      SqlCommand comando =
          new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@ProveedorId",
          presupuesto.ProveedorId);

      comando.Parameters.AddWithValue(
          "@MontoEstimado",
          presupuesto.MontoEstimado);

      comando.Parameters.AddWithValue(
          "@MontoFinal",
          presupuesto.MontoFinal.HasValue
              ? (object)presupuesto.MontoFinal.Value
              : DBNull.Value);

      comando.Parameters.AddWithValue(
          "@ArchivoPdf",
          string.IsNullOrWhiteSpace(
              presupuesto.ArchivoPdf)
              ? (object)DBNull.Value
              : presupuesto.ArchivoPdf);

      comando.Parameters.AddWithValue(
          "@PresupuestoId",
          presupuesto.PresupuestoId);

      comando.Parameters.AddWithValue(
           "@DigitoVerificador",
           Integridad.ObtenerSemillaSistema());

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }
  }
}

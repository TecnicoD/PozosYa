using Datos.Connection;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos.Repository
{
  public class ProveedorRepository
  {
    private readonly Conexion conexionBD = new Conexion();

    public List<Proveedor> ObtenerTodos()
    {
      List<Proveedor> proveedores = new List<Proveedor>();

      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
                SELECT
                    proveedor_id,
                    nombre,
                    cuit,
                    telefono,
                    mail,
                    direccion,
                    activo
                FROM proveedores
                ORDER BY nombre";

      SqlCommand comando = new SqlCommand(query, conexion);

      SqlDataReader reader = comando.ExecuteReader();

      while (reader.Read())
      {
        Proveedor proveedor = new Proveedor
        {
          ProveedorId = Convert.ToInt32(reader["proveedor_id"]),
          Nombre = reader["nombre"].ToString(),
          Cuit = reader["cuit"].ToString(),
          Telefono = reader["telefono"].ToString(),
          Mail = reader["mail"].ToString(),
          Direccion = reader["direccion"].ToString(),
          Activo = Convert.ToBoolean(reader["activo"])
        };

        proveedores.Add(proveedor);
      }

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return proveedores;
    }

    public Proveedor ObtenerPorId(int proveedorId)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
                SELECT
                    proveedor_id,
                    nombre,
                    cuit,
                    telefono,
                    mail,
                    direccion,
                    activo
                FROM proveedores
                WHERE proveedor_id = @ProveedorId";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue(
          "@ProveedorId",
          proveedorId);

      SqlDataReader reader = comando.ExecuteReader();

      if (!reader.Read())
      {
        reader.Close();
        conexionBD.CerrarConexion(conexion);
        return null;
      }

      Proveedor proveedor = new Proveedor
      {
        ProveedorId = Convert.ToInt32(reader["proveedor_id"]),
        Nombre = reader["nombre"].ToString(),
        Cuit = reader["cuit"].ToString(),
        Telefono = reader["telefono"].ToString(),
        Mail = reader["mail"].ToString(),
        Direccion = reader["direccion"].ToString(),
        Activo = Convert.ToBoolean(reader["activo"])
      };

      reader.Close();
      conexionBD.CerrarConexion(conexion);

      return proveedor;
    }

    // Verifica si el CUIT ya lo usa otro proveedor
    // (proveedorIdExcluir = 0 para un alta).
    public bool ExisteCuit(string cuit, int proveedorIdExcluir)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
                SELECT COUNT(*) FROM proveedores
                WHERE cuit = @Cuit
                  AND proveedor_id <> @ProveedorId";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@Cuit", cuit);
      comando.Parameters.AddWithValue("@ProveedorId", proveedorIdExcluir);

      int cantidad = Convert.ToInt32(comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return cantidad > 0;
    }

    public int Agregar(Proveedor proveedor)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
                INSERT INTO proveedores
                (
                    nombre,
                    cuit,
                    telefono,
                    mail,
                    direccion,
                    activo
                )
                VALUES
                (
                    @Nombre,
                    @Cuit,
                    @Telefono,
                    @Mail,
                    @Direccion,
                    @Activo
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
      comando.Parameters.AddWithValue("@Cuit", proveedor.Cuit);
      comando.Parameters.AddWithValue("@Telefono",
          (object)proveedor.Telefono ?? DBNull.Value);
      comando.Parameters.AddWithValue("@Mail",
          (object)proveedor.Mail ?? DBNull.Value);
      comando.Parameters.AddWithValue("@Direccion",
          (object)proveedor.Direccion ?? DBNull.Value);
      comando.Parameters.AddWithValue("@Activo", proveedor.Activo);

      int proveedorId =
          Convert.ToInt32(comando.ExecuteScalar());

      conexionBD.CerrarConexion(conexion);

      return proveedorId;
    }

    public void Actualizar(Proveedor proveedor)
    {
      SqlConnection conexion = conexionBD.ObtenerConexion();

      string query = @"
                UPDATE proveedores
                SET
                    nombre = @Nombre,
                    cuit = @Cuit,
                    telefono = @Telefono,
                    mail = @Mail,
                    direccion = @Direccion,
                    activo = @Activo
                WHERE proveedor_id = @ProveedorId";

      SqlCommand comando = new SqlCommand(query, conexion);

      comando.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
      comando.Parameters.AddWithValue("@Cuit", proveedor.Cuit);
      comando.Parameters.AddWithValue("@Telefono",
          (object)proveedor.Telefono ?? DBNull.Value);
      comando.Parameters.AddWithValue("@Mail",
          (object)proveedor.Mail ?? DBNull.Value);
      comando.Parameters.AddWithValue("@Direccion",
          (object)proveedor.Direccion ?? DBNull.Value);
      comando.Parameters.AddWithValue("@Activo", proveedor.Activo);
      comando.Parameters.AddWithValue("@ProveedorId",
          proveedor.ProveedorId);

      comando.ExecuteNonQuery();

      conexionBD.CerrarConexion(conexion);
    }
  }
}

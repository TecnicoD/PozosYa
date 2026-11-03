using Datos.Connection;
using PY.Negocio.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PY.Datos.Repository
{
    internal class AdministradorRepository
    {

        private readonly Conexion conexionBD = new Conexion();

        public int EjecutarStoredProcedureRestaurar(string nombreArchivo, string nombrebd)
        {
            // Cambiar el cursor a modo espera (reloj de arena) porque la restauración puede tardar
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexionAMaster())
                {
                    using (SqlCommand comando = new SqlCommand("SP_RestaurarBaseDatos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@RutaCompletaBAK", nombreArchivo);
                        comando.Parameters.AddWithValue("@NombreBD", nombrebd);
                        comando.CommandTimeout = 0;
                        comando.ExecuteNonQuery();
                        conexionBD.CerrarConexion(conexion);
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Error", MessageBoxButtons.OK);
                return -1;
            }
            finally
            {
                // Regresar el cursor a su estado normal
                Cursor.Current = Cursors.Default;
            }
        }

        public int EjecutarStoredProcedureBackup()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand("sp_BackupDatabaseAutoconfigurado", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.CommandTimeout = 0;
                        comando.ExecuteNonQuery();
                        conexionBD.CerrarConexion(conexion);
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                // Regresar el cursor a su estado normal
                Cursor.Current = Cursors.Default;
            }
        }
        // Carpeta de backups por defecto de la instancia de SQL Server.
        public string ObtenerRutaBackupServidor()
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand(
                    "SELECT CAST(SERVERPROPERTY('InstanceDefaultBackupPath') AS NVARCHAR(500))",
                    conexion);

                object resultado = comando.ExecuteScalar();

                return resultado == null || resultado == DBNull.Value
                    ? null
                    : resultado.ToString();
            }
        }

        public int EjecutarStoredProcedureValidacionIntegridadBD()
        {
            // Cambiar el cursor a modo espera (reloj de arena) porque la restauración puede tardar
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexionAMaster())
                {
                    using (SqlCommand comando = new SqlCommand("SP_ValidacionIntegridadBD", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Semilla", Integridad.ObtenerSemillaSistema());
                        comando.CommandTimeout = 0;
                        comando.ExecuteNonQuery();
                        conexionBD.CerrarConexion(conexion);
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Error", MessageBoxButtons.OK);
                return -1;
            }
            finally
            {
                // Regresar el cursor a su estado normal
                Cursor.Current = Cursors.Default;
            }
        }
    }
    }

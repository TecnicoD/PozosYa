using Datos.Connection;
using Datos.Repository;
using System;
using System.IO;

namespace Negocio.Services
{
  public class BaseDatosService
  {
    private readonly BaseDatosRepository repository =
        new BaseDatosRepository();

    // Script con la base completa y datos de ejemplo (se copia junto al .exe).
    private readonly string rutaScriptDemo =
        Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Datos",
            "DataBase",
            "Scripts",
            "PozosYa_Demo.sql");

    // SQL Server no puede leer archivos del Escritorio o Descargas del
    // usuario: el .bak se copia a Documentos públicos antes de restaurarlo.
    private readonly string carpetaIntercambio =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
            "PozosYa");

    public string Servidor
    {
      get { return ConfiguracionConexion.Servidor; }
      set { ConfiguracionConexion.Servidor = value; }
    }

    public void GuardarServidor()
    {
      ConfiguracionConexion.Guardar();
    }

    // Lanza SqlException si no se puede conectar al servidor.
    public bool ExisteBaseDatos()
    {
      return repository.ExisteBaseDatos();
    }

    public void CrearBaseDemo()
    {
      if (!File.Exists(rutaScriptDemo))
        throw new FileNotFoundException(
            "No se encontró el script de la base de demo.");

      try
      {
        repository.EjecutarScript(
            File.ReadAllText(rutaScriptDemo));
      }
      catch
      {
        // Si falló a mitad de camino, se borra la base para no dejarla incompleta.
        try { repository.EliminarBaseDatos(); }
        catch (Exception) { }

        throw;
      }
    }

    public void RestaurarBackup(string rutaOrigen)
    {
      if (!File.Exists(rutaOrigen))
        throw new FileNotFoundException(
            "No se encontró el archivo seleccionado.");

      Directory.CreateDirectory(carpetaIntercambio);

      string copia =
          Path.Combine(
              carpetaIntercambio,
              "restaurar_" + Path.GetFileName(rutaOrigen));

      File.Copy(rutaOrigen, copia, true);

      try
      {
        repository.RestaurarBackup(copia);
      }
      finally
      {
        try { File.Delete(copia); }
        catch (IOException) { }
      }
    }
  }
}

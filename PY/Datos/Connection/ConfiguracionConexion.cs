using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Datos.Connection
{
  // Servidor SQL elegido en la pantalla "Configurar base de datos".
  // Se guarda en basedatos.json junto al ejecutable (por defecto: localhost),
  // así cada compañero puede usar su propia instancia (ej: .\SQLEXPRESS).
  public static class ConfiguracionConexion
  {
    public const string NombreBaseDatos = "PozosYa";

    private const string ServidorPorDefecto = "localhost";

    private static readonly string archivo =
        Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "basedatos.json");

    private static string servidor;

    public static string Servidor
    {
      get
      {
        if (servidor == null)
          servidor = Leer();

        return servidor;
      }
      set
      {
        servidor = string.IsNullOrWhiteSpace(value)
            ? ServidorPorDefecto
            : value.Trim();
      }
    }

    public static void Guardar()
    {
      JObject json = new JObject();
      json["Servidor"] = Servidor;

      File.WriteAllText(archivo, json.ToString());
    }

    public static string CadenaConexion(string baseDatos)
    {
      return "Server=" + Servidor +
             ";Database=" + baseDatos +
             ";Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=8;";
    }

    private static string Leer()
    {
      try
      {
        if (File.Exists(archivo))
        {
          string valor =
              (string)JObject.Parse(File.ReadAllText(archivo))["Servidor"];

          if (!string.IsNullOrWhiteSpace(valor))
            return valor;
        }
      }
      catch (Exception)
      {
        // Archivo dañado: se usa el servidor por defecto.
      }

      return ServidorPorDefecto;
    }
  }
}

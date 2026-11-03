using Datos.Repository;
using Negocio.Models;
using System;
using System.IO;

namespace Negocio.Services
{
  public class PresupuestoService
  {
    private readonly PresupuestoRepository repository =
        new PresupuestoRepository();

    private readonly string carpetaPresupuestos =
        Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Presupuestos");

    public Presupuesto ObtenerPorReclamo(int reclamoId)
    {
      return repository.ObtenerPorReclamo(reclamoId);
    }

    public int Agregar(Presupuesto presupuesto)
    {
      presupuesto.ArchivoPdf =
          CopiarPdf(presupuesto);

      return repository.Agregar(presupuesto);
    }

    public void Actualizar(Presupuesto presupuesto)
    {
      presupuesto.ArchivoPdf =
          CopiarPdf(presupuesto);

      repository.Actualizar(presupuesto);
    }

    // Ruta completa del PDF guardado (para abrirlo).
    public string ObtenerRutaCompleta(string archivoPdf)
    {
      if (string.IsNullOrWhiteSpace(archivoPdf))
        return null;

      if (Path.IsPathRooted(archivoPdf))
        return archivoPdf;

      return Path.Combine(
          AppDomain.CurrentDomain.BaseDirectory,
          archivoPdf);
    }

    // Si el PDF viene de una ruta de la PC del usuario, se copia a la
    // carpeta Presupuestos de la aplicación (igual que las evidencias)
    // y se guarda la ruta relativa. Si ya es una ruta relativa
    // (PDF ya copiado), se deja como está.
    private string CopiarPdf(Presupuesto presupuesto)
    {
      string rutaOrigen =
          presupuesto.ArchivoPdf;

      if (string.IsNullOrWhiteSpace(rutaOrigen) ||
          !Path.IsPathRooted(rutaOrigen))
      {
        return rutaOrigen;
      }

      if (!File.Exists(rutaOrigen))
        throw new FileNotFoundException(
            "No se encontró el archivo seleccionado.");

      if (!Path.GetExtension(rutaOrigen)
              .Equals(".pdf", StringComparison.OrdinalIgnoreCase))
      {
        throw new InvalidOperationException(
            "Solo se permiten archivos PDF.");
      }

      string carpetaReclamo =
          Path.Combine(
              carpetaPresupuestos,
              "Reclamo_" + presupuesto.ReclamoId);

      Directory.CreateDirectory(carpetaReclamo);

      // Se conserva el nombre original para mostrarlo en pantalla.
      string nombreArchivo =
          Guid.NewGuid().ToString("N").Substring(0, 8) +
          "_" +
          Path.GetFileName(rutaOrigen);

      File.Copy(
          rutaOrigen,
          Path.Combine(carpetaReclamo, nombreArchivo),
          false);

      return Path.Combine(
          "Presupuestos",
          "Reclamo_" + presupuesto.ReclamoId,
          nombreArchivo);
    }
  }
}

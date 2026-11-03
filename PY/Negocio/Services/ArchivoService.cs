using Datos.Repository;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Negocio.Services
{
  public class ArchivoService
  {
    private readonly ArchivoRepository repository =
        new ArchivoRepository();

    private readonly string carpetaEvidencias =
        Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Evidencias");

    public List<Archivo> ObtenerPorReclamo(int reclamoId)
    {
      return repository.ObtenerPorReclamo(reclamoId);
    }

    public int AgregarEvidencia(
        int reclamoId,
        string rutaOrigen,
        int usuarioId)
    {
      if (!File.Exists(rutaOrigen))
        throw new FileNotFoundException(
            "No se encontró el archivo seleccionado.");

      string extension =
          Path.GetExtension(rutaOrigen)
              .ToLowerInvariant();

      string[] extensionesPermitidas =
      {
                ".jpg",
                ".jpeg",
                ".png"
            };

      if (Array.IndexOf(
          extensionesPermitidas,
          extension) < 0)
      {
        throw new InvalidOperationException(
            "Solo se permiten archivos JPG, JPEG o PNG.");
      }

      Directory.CreateDirectory(
          carpetaEvidencias);

      string carpetaReclamo =
          Path.Combine(
              carpetaEvidencias,
              "Reclamo_" + reclamoId);

      Directory.CreateDirectory(
          carpetaReclamo);

      string nombreInterno =
          Guid.NewGuid().ToString("N") +
          extension;

      string rutaDestino =
          Path.Combine(
              carpetaReclamo,
              nombreInterno);

      File.Copy(
          rutaOrigen,
          rutaDestino,
          false);

      string rutaRelativa =
          Path.Combine(
              "Evidencias",
              "Reclamo_" + reclamoId,
              nombreInterno);

      Archivo archivo = new Archivo
      {
        ReclamoId = reclamoId,
        TipoArchivo = extension,
        Ubicacion = rutaRelativa,
        NombreOriginal =
              Path.GetFileName(rutaOrigen),
        Tamanio =
              new FileInfo(rutaOrigen).Length,
        UsuarioId = usuarioId,
        Activo = true
      };

      return repository.Agregar(archivo);
    }
  }
}

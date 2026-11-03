using Datos.Repository;
using Negocio.Models;
using System.Collections.Generic;

namespace Negocio.Services
{
  public class ReclamoService
  {
    private readonly ReclamoRepository repository = new ReclamoRepository();

    private readonly EmailService emailService =
    new EmailService();

    public List<ReclamoListado> ObtenerListado()
    {
      return repository.ObtenerListado();
    }

    public ReclamoDetalleDTO ObtenerPorId(int reclamoId)
    {
      return repository.ObtenerPorId(reclamoId);
    }

    public void Actualizar(ReclamoDetalleDTO reclamo)
    {
      // =========================================================
      // OBTENER ESTADO ANTERIOR
      // =========================================================

      ReclamoDetalleDTO reclamoAnterior =
          repository.ObtenerPorId(
              reclamo.ReclamoId);

      if (reclamoAnterior == null)
      {
        throw new System.InvalidOperationException(
            "No se encontró el reclamo.");
      }

      bool cambioEstado =
          reclamoAnterior.EstadoId != reclamo.EstadoId;


      // =========================================================
      // ACTUALIZAR
      // =========================================================

      repository.Actualizar(reclamo);


      // =========================================================
      // NOTIFICAR CAMBIO DE ESTADO
      // =========================================================

      if (!cambioEstado)
        return;

      try
      {
        // Volvemos a consultar para obtener
        // el nuevo estado y todos los datos
        // actualizados del reclamo.
        ReclamoDetalleDTO reclamoActualizado =
            repository.ObtenerPorId(
                reclamo.ReclamoId);

        if (reclamoActualizado == null)
          return;

        if (string.IsNullOrWhiteSpace(
            reclamoActualizado.Mail))
        {
          return;
        }

        string asunto =
            "Actualización de su reclamo - Pozos Ya";

        string cuerpo =
            "Hola " +
            reclamoActualizado.Vecino +
            ",\n\n" +

            "Le informamos que su reclamo " +
            reclamoActualizado.NumeroReclamo +
            " ha cambiado de estado.\n\n" +

            "Estado anterior: " +
            reclamoAnterior.Estado +
            "\n" +

            "Nuevo estado: " +
            reclamoActualizado.Estado +
            "\n\n" +

            "Dirección: " +
            reclamoActualizado.Direccion +
            "\n\n" +

            "Pozos Ya\n" +
            "Municipalidad de Lomas de Zamora";

        emailService.Enviar(
            reclamoActualizado.Mail,
            asunto,
            cuerpo);
      }
      catch (System.Exception)
      {
        // El cambio de estado ya fue realizado.
        // Si falla el correo, no se revierte el reclamo.
      }
    }

    public int Agregar(Reclamo reclamo)
    {
      return repository.Agregar(reclamo);
    }

    public List<ReclamoListado> Buscar(ReclamoFiltro filtro)
    {
      return repository.Buscar(filtro);
    }

    public List<ReclamoPriorizacionDTO> ObtenerParaPriorizacion()
    {
      return repository.ObtenerParaPriorizacion();
    }
  }
}

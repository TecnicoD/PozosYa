using System;
using System.Net;
using System.Net.Mail;

namespace Negocio.Services
{
  public class EmailService
  {
    private const string CorreoRemitente =
        "lucashood.dev@gmail.com";

    // REEMPLAZÁ ESTE VALOR POR TU CONTRASEÑA
    // DE APLICACIÓN DE GOOGLE.
    private const string ContrasenaAplicacion =
        "fatc unsp stcy agqs";


    public void Enviar(
        string destinatario,
        string asunto,
        string cuerpo)
    {
      if (string.IsNullOrWhiteSpace(destinatario))
        throw new ArgumentException(
            "El destinatario es obligatorio.");

      if (string.IsNullOrWhiteSpace(asunto))
        throw new ArgumentException(
            "El asunto es obligatorio.");

      if (string.IsNullOrWhiteSpace(cuerpo))
        throw new ArgumentException(
            "El cuerpo del correo es obligatorio.");


      MailMessage mensaje =
          new MailMessage();

      mensaje.From =
          new MailAddress(
              CorreoRemitente,
              "Pozos Ya");

      mensaje.To.Add(
          destinatario);

      mensaje.Subject =
          asunto;

      mensaje.Body =
          cuerpo;

      mensaje.IsBodyHtml = false;


      SmtpClient smtp =
          new SmtpClient(
              "smtp.gmail.com",
              587);

      smtp.EnableSsl = true;

      smtp.Credentials =
          new NetworkCredential(
              CorreoRemitente,
              ContrasenaAplicacion);

      smtp.Send(mensaje);

      mensaje.Dispose();
      smtp.Dispose();
    }
  }
}

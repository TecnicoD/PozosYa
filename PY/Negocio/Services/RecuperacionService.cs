using Negocio.Models;
using System;

namespace Negocio.Services
{
  public class RecuperacionService
  {
    private readonly UsuarioService usuarioService =
        new UsuarioService();

    private readonly TokenRecuperacionService tokenService =
        new TokenRecuperacionService();

    private readonly EmailService emailService =
        new EmailService();

    public bool SolicitarRecuperacion(
        string email)
    {
      Usuario usuario =
          usuarioService.ObtenerPorEmail(email);

      if (usuario == null ||
          !usuario.Activo)
      {
        return false;
      }

      string codigo =
          GenerarCodigo();

      DateTime ahora =
          DateTime.Now;

      DateTime expiracion =
          ahora.AddMinutes(10);

      tokenService.Crear(
          usuario.UsuarioId,
          codigo,
          ahora,
          expiracion);

      string cuerpo =
          "Hola " +
          usuario.Nombre +
          ",\n\n" +
          "Tu código de recuperación de Pozos Ya es:\n\n" +
          codigo +
          "\n\n" +
          "El código vence en 10 minutos.\n\n" +
          "Si no solicitaste este cambio, ignorá este correo.";

      emailService.Enviar(
          usuario.Email,
          "Recuperación de contraseña - Pozos Ya",
          cuerpo);

      return true;
    }

    private string GenerarCodigo()
    {
      Random random =
          new Random();

      return random
          .Next(100000, 1000000)
          .ToString();
    }
  }
}

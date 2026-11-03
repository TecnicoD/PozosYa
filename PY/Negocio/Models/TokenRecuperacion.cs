using System;

public class TokenRecuperacion
{
  public int TokenRecuperacionId { get; set; }

  public int UsuarioId { get; set; }

  public string Token { get; set; }

  public DateTime FechaGeneracion { get; set; }

  public bool Usado { get; set; }

  public DateTime FechaExpiracion { get; set; }
}

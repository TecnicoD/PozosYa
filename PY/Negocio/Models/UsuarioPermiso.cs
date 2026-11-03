using System;

public class UsuarioPermiso
{
  public int usuario_permiso_id { get; set; }

  public int UsuarioId { get; set; }

  public int PermisoId { get; set; }

  public bool Concedido { get; set; }
}

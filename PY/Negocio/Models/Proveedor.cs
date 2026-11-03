using System;

namespace Negocio.Models
{
  public class Proveedor
  {
    public int ProveedorId { get; set; }

    public string Nombre { get; set; }

    public string Cuit { get; set; }

    public string Telefono { get; set; }

    public string Mail { get; set; }

    public string Direccion { get; set; }

    public bool Activo { get; set; }
  }
}

using System;

namespace Negocio.Models
{
  public class Archivo
  {
    public int ArchivoId { get; set; }

    public int ReclamoId { get; set; }

    public string TipoArchivo { get; set; }

    public string Ubicacion { get; set; }

    public string NombreOriginal { get; set; }

    public long Tamanio { get; set; }

    public DateTime FechaCarga { get; set; }

    public int UsuarioId { get; set; }

    public bool Activo { get; set; }
  }
}

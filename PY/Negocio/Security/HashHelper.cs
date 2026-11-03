using System.Security.Cryptography;
using System.Text;

namespace Negocio.Security
{
  public static class HashHelper
  {
    public static string CalcularHash(string texto)
    {
      using (SHA256 sha = SHA256.Create())
      {
        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));

        StringBuilder sb = new StringBuilder();

        foreach (byte b in bytes)
          sb.Append(b.ToString("x2"));

        return sb.ToString();
      }
    }
  }
}

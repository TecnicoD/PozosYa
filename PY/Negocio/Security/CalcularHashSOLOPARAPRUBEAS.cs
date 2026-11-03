using System;
using System.Security.Cryptography;
using System.Text;

namespace CalculadoraHash
{
  class Program
  {
    static void calcularonline(string[] args)
    {
      Console.Write("Contraseña: ");
      string password = Console.ReadLine();

      Console.WriteLine();
      Console.WriteLine("SHA-256:");
      Console.WriteLine(CalcularHash(password));

      Console.WriteLine();
      Console.WriteLine("Presione una tecla para salir...");
      Console.ReadKey();
    }

    static string CalcularHash(string texto)
    {
      using (SHA256 sha = SHA256.Create())
      {
        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));

        StringBuilder sb = new StringBuilder();

        foreach (byte b in bytes)
        {
          sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
      }
    }
  }
}

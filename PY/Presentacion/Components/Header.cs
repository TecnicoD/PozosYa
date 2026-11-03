using Negocio.Security;
using System;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Components
{
  public partial class Header : UserControl
  {
    public Header()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      CargarUsuario();
    }

    private void CargarUsuario()
    {
      if (SesionActual.Usuario == null)
      {
        label2.Text = Traductor.T("Bienvenido");
        return;
      }

      label2.Text =
          Traductor.T("Bienvenido, ") +
          SesionActual.Usuario.Username;
    }

    private void Header_Load(
        object sender,
        EventArgs e)
    {
      CargarUsuario();
    }

    private void label1_Click(
        object sender,
        EventArgs e)
    {
    }
  }
}

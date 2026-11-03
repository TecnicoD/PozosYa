using Negocio.Services;
using PY.Presentacion.Idiomas;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PY.Presentacion.Forms
{
  // Se muestra al iniciar si no se encuentra la base de datos:
  // permite crearla con datos de demo o restaurarla desde un .bak,
  // sin tener que entrar a SQL Server Management Studio.
  public partial class FormConfigurarBD : Form
  {
    // =========================
    // VARIABLES
    // =========================

    private readonly BaseDatosService baseDatosService =
        new BaseDatosService();

    // Error de SQL Server cuando el .bak es de una versión más nueva.
    private const int ErrorVersionIncompatible = 3169;


    // =========================
    // CONSTRUCTOR
    // =========================

    public FormConfigurarBD()
    {
      InitializeComponent();
      Traductor.Traducir(this);
    }

    private void FormConfigurarBD_Load(object sender, EventArgs e)
    {
      txtServidor.Text = baseDatosService.Servidor;

      MostrarDiagnostico();
    }


    // =========================
    // DIAGNÓSTICO
    // =========================

    private void MostrarDiagnostico()
    {
      try
      {
        lblEstado.Text = baseDatosService.ExisteBaseDatos()
            ? string.Format(
                Traductor.T("La base de datos PozosYa ya existe en el servidor '{0}'. Se va a usar esa."),
                baseDatosService.Servidor)
            : string.Format(
                Traductor.T("No se encontró la base de datos PozosYa en el servidor '{0}'. Creala con datos de demo o restaurá un backup."),
                baseDatosService.Servidor);
      }
      catch (SqlException)
      {
        lblEstado.Text = string.Format(
            Traductor.T("No se pudo conectar al servidor '{0}'. Revisá el nombre del servidor."),
            baseDatosService.Servidor);
      }
    }


    // =========================
    // ACCIONES
    // =========================

    private void btnCrearDemo_Click(object sender, EventArgs e)
    {
      if (!PrepararServidor())
        return;

      Ejecutar(
          () => baseDatosService.CrearBaseDemo(),
          Traductor.T("La base de datos de demo quedó lista.") +
          "\n\n" +
          Traductor.T("Usuario: admin") +
          "\n" +
          Traductor.T("Contraseña: admin123"));
    }

    private void btnRestaurar_Click(object sender, EventArgs e)
    {
      if (!PrepararServidor())
        return;

      using (OpenFileDialog dialogo = new OpenFileDialog())
      {
        dialogo.Title = Traductor.T("Seleccionar backup de la base de datos");
        dialogo.Filter = Traductor.T("Backup de SQL Server|*.bak");
        dialogo.Multiselect = false;

        if (dialogo.ShowDialog() != DialogResult.OK)
          return;

        string ruta = dialogo.FileName;

        Ejecutar(
            () => baseDatosService.RestaurarBackup(ruta),
            Traductor.T("La base de datos se restauró correctamente."));
      }
    }

    private void btnSalir_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }


    // =========================
    // AUXILIARES
    // =========================

    // Toma el servidor escrito y verifica la conexión.
    // Si en ese servidor ya existe la base, se usa directamente.
    private bool PrepararServidor()
    {
      baseDatosService.Servidor = txtServidor.Text;
      txtServidor.Text = baseDatosService.Servidor;

      try
      {
        if (!baseDatosService.ExisteBaseDatos())
          return true;

        baseDatosService.GuardarServidor();

        MessageBox.Show(
            string.Format(
                Traductor.T("La base de datos PozosYa ya existe en el servidor '{0}'. Se va a usar esa."),
                baseDatosService.Servidor),
            Traductor.T("Base de datos"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        DialogResult = DialogResult.OK;
        Close();

        return false;
      }
      catch (SqlException ex)
      {
        MessageBox.Show(
            string.Format(
                Traductor.T("No se pudo conectar al servidor '{0}'. Revisá el nombre del servidor."),
                baseDatosService.Servidor) +
            "\n\n" +
            ex.Message,
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        return false;
      }
    }

    private void Ejecutar(Action accion, string mensajeExito)
    {
      Cursor = Cursors.WaitCursor;
      HabilitarBotones(false);

      lblEstado.Text = Traductor.T("Procesando, puede tardar unos segundos...");
      lblEstado.Refresh();

      try
      {
        accion();

        baseDatosService.GuardarServidor();

        MessageBox.Show(
            mensajeExito,
            Traductor.T("Base de datos"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        DialogResult = DialogResult.OK;
        Close();
      }
      catch (SqlException ex) when (ex.Number == ErrorVersionIncompatible)
      {
        MessageBox.Show(
            Traductor.T("El backup fue hecho con una versión más nueva de SQL Server y no se puede restaurar en este servidor. Usá \"Crear base de demo\"."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        MostrarDiagnostico();
      }
      catch (Exception ex)
      {
        MessageBox.Show(
            Traductor.T("No se pudo preparar la base de datos.") +
            "\n\n" +
            Traductor.T(ex.Message),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        MostrarDiagnostico();
      }
      finally
      {
        Cursor = Cursors.Default;
        HabilitarBotones(true);
      }
    }

    private void HabilitarBotones(bool habilitar)
    {
      btnCrearDemo.Enabled = habilitar;
      btnRestaurar.Enabled = habilitar;
      btnSalir.Enabled = habilitar;
      txtServidor.Enabled = habilitar;
    }
  }
}

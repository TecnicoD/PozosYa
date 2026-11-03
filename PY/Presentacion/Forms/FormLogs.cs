using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Negocio.Models;
using Negocio.Services;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormLogs : Form
  {
    // =========================
    // CONSTRUCTOR
    // =========================

    public FormLogs()
    {
      InitializeComponent();
      Traductor.Traducir(this);
    }


    // =========================
    // INICIALIZACIÓN
    // =========================

    private void FormLogs_Load(
        object sender,
        EventArgs e)
    {
      CargarLogs();
    }


    // =========================
    // LOGS
    // =========================

    private void CargarLogs()
    {
      LogSistemaService service =
          new LogSistemaService();

      List<LogSistema> lista =
          service.ObtenerTodos();

      dgvLogs.AutoGenerateColumns = false;
      dgvLogs.DataSource = lista;
    }
  }
}

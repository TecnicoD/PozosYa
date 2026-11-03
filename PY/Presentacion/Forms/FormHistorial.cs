using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Negocio.Models;
using Negocio.Services;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormHistorial : Form
  {
    // =========================
    // VARIABLES
    // =========================

    private readonly int reclamoId;


    // =========================
    // CONSTRUCTOR
    // =========================

    public FormHistorial(int reclamoId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      this.reclamoId = reclamoId;

      CargarHistorial();
    }


    // =========================
    // HISTORIAL
    // =========================

    private void CargarHistorial()
    {
      HistorialReclamoService service =
          new HistorialReclamoService();

      List<HistorialReclamo> historial =
          service.ObtenerPorReclamo(reclamoId);

      dgvHistorial.AutoGenerateColumns = false;
      dgvHistorial.Rows.Clear();

      foreach (HistorialReclamo item in historial)
      {
        dgvHistorial.Rows.Add(
            item.Fecha.ToString("dd/MM/yyyy HH:mm"),
            item.EstadoAnterior,
            item.EstadoNuevo,
            item.Usuario,
            item.FechaModificacion.HasValue
                ? item.FechaModificacion.Value.ToString(
                    "dd/MM/yyyy HH:mm")
                : ""
        );
      }
    }


    // =========================
    // EVENTOS
    // =========================

    private void btnCerrar_Click(
        object sender,
        EventArgs e)
    {
      Close();
    }
  }
}

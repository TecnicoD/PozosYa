using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormDetallePartida : Form
  {
    // =========================================================
    // VARIABLES
    // =========================================================

    private readonly PartidaPresupuestariaService partidaService =
        new PartidaPresupuestariaService();

    private readonly UsuarioService usuarioService =
        new UsuarioService();

    private readonly bool esNuevo;

    private readonly int partidaId;

    private PartidaPresupuestaria partida;


    // =========================================================
    // NUEVA PARTIDA
    // =========================================================

    public FormDetallePartida()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      esNuevo = true;

      Load += FormDetallePartida_Load;
    }


    // =========================================================
    // EDITAR PARTIDA
    // =========================================================

    public FormDetallePartida(int partidaId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      esNuevo = false;

      this.partidaId = partidaId;

      Load += FormDetallePartida_Load;
    }


    // =========================================================
    // LOAD
    // =========================================================

    // La validación va en Load: en el constructor Close() no cierra el formulario.
    private void FormDetallePartida_Load(object sender, EventArgs e)
    {
      CargarTrimestres();

      if (esNuevo)
      {
        // Seguridad: crear
        if (!SesionActual.TienePermiso(
            Permisos.PARTIDAS_PRESUPUESTARIAS_CREAR))
        {
          MessageBox.Show(
              Traductor.T("No tiene permisos para crear partidas presupuestarias."),
              Traductor.T("Acceso denegado"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);

          Close();
          return;
        }

        partida = new PartidaPresupuestaria();

        CargarGerentes();
        ConfigurarNuevo();

        CambiarModoEdicion(true);
        return;
      }

      // Seguridad: ver
      if (!SesionActual.TienePermiso(
          Permisos.PARTIDAS_PRESUPUESTARIAS_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder a esta partida presupuestaria."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        Close();
        return;
      }

      partida =
          partidaService.ObtenerPorId(partidaId);

      if (partida == null)
      {
        MessageBox.Show(
            Traductor.T("No se encontró la partida presupuestaria."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        Close();
        return;
      }

      CargarGerentes();
      CargarDatos();

      CambiarModoEdicion(false);
    }

    // Se cargan en los dos modos (antes solo al crear, y al editar
    // el combo quedaba vacío y no se podía guardar).
    private void CargarTrimestres()
    {
      cmbTrimestre.Items.Clear();

      cmbTrimestre.Items.Add("1");
      cmbTrimestre.Items.Add("2");
      cmbTrimestre.Items.Add("3");
      cmbTrimestre.Items.Add("4");
    }


    // =========================================================
    // CONFIGURACIÓN NUEVO
    // =========================================================

    private void ConfigurarNuevo()
    {
      Text = Traductor.T("Nueva Partida Presupuestaria");

      txtID.Text = Traductor.T("Nuevo");

      cmbTrimestre.SelectedIndex = 0;

      txtMontoComprometido.Text = "0";
      txtMontoEjecutado.Text = "0";

      chkActivo.Checked = true;

      txtFechaAsignacion.Text =
          Traductor.T("Se asignará automáticamente");
    }


    // =========================================================
    // GERENTES
    // =========================================================

    private void CargarGerentes()
    {
      // Solo usuarios activos con rol Gerente.
      Rol rolGerente =
          new RolService().ObtenerTodos()
              .Find(r => r.Nombre == "Gerente");

      List<Usuario> usuarios =
          usuarioService.ObtenerTodos(
              "",
              true,
              rolGerente?.RolId);

      // Si la partida ya tiene un gerente que hoy no tiene el rol,
      // se lo agrega para no perder el dato al editarla.
      if (partida != null &&
          partida.UsuarioGerenteId > 0 &&
          !usuarios.Exists(u => u.UsuarioId == partida.UsuarioGerenteId))
      {
        Usuario gerenteActual =
            usuarioService.ObtenerPorId(partida.UsuarioGerenteId);

        if (gerenteActual != null)
          usuarios.Add(gerenteActual);
      }

      cmbGerente.DataSource = usuarios;

      cmbGerente.DisplayMember = "Username";
      cmbGerente.ValueMember = "UsuarioId";
    }


    // =========================================================
    // CARGAR DATOS
    // =========================================================

    private void CargarDatos()
    {
      txtID.Text =
          partida.PartidaPresupuestariaId.ToString();

      txtAnio.Text =
          partida.Anio.ToString();

      cmbTrimestre.SelectedItem =
          partida.Trimestre.ToString();

      txtMontoAsignado.Text =
          partida.MontoAsignado.ToString("N2");

      txtMontoComprometido.Text =
          partida.MontoComprometido.ToString("N2");

      txtMontoEjecutado.Text =
          partida.MontoEjecutado.ToString("N2");

      cmbGerente.SelectedValue =
          partida.UsuarioGerenteId;

      txtFechaAsignacion.Text =
          partida.FechaAsignacion
              .ToString("dd/MM/yyyy HH:mm");

      txtObservaciones.Text =
          partida.Observaciones;

      chkActivo.Checked =
          partida.Activo;
    }


    // =========================================================
    // MODO EDICIÓN
    // =========================================================

    private void CambiarModoEdicion(bool editar)
    {
      txtAnio.ReadOnly = !editar;
      txtMontoAsignado.ReadOnly = !editar;
      txtMontoComprometido.ReadOnly = !editar;
      txtMontoEjecutado.ReadOnly = !editar;
      txtObservaciones.ReadOnly = !editar;

      txtID.ReadOnly = true;
      txtFechaAsignacion.ReadOnly = true;

      cmbTrimestre.Enabled = editar;
      cmbGerente.Enabled = editar;
      chkActivo.Enabled = editar;

      btnGuardar.Visible = editar;
      btnCancelar.Visible = editar;

      // El botón modificar solamente aparece
      // si tiene permiso para guardar/modificar.
      btnModificar.Visible =
          !editar &&
          SesionActual.TienePermiso(
              Permisos.PARTIDAS_PRESUPUESTARIAS_GUARDAR);
    }


    // =========================================================
    // MODIFICAR
    // =========================================================

    private void btnModificar_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PARTIDAS_PRESUPUESTARIAS_GUARDAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para modificar partidas presupuestarias."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      CambiarModoEdicion(true);
    }


    // =========================================================
    // CANCELAR
    // =========================================================

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      if (!esNuevo)
      {
        CargarDatos();
        CambiarModoEdicion(false);
        return;
      }

      Close();
    }


    // =========================================================
    // GUARDAR
    // =========================================================

    private void btnGuardar_Click(object sender, EventArgs e)
    {
      // Para guardar una nueva partida:
      if (esNuevo)
      {
        if (!SesionActual.TienePermiso(
            Permisos.PARTIDAS_PRESUPUESTARIAS_CREAR))
        {
          MessageBox.Show(
              Traductor.T("No tiene permisos para crear partidas presupuestarias."),
              Traductor.T("Acceso denegado"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);

          return;
        }
      }
      // Para modificar una existente:
      else
      {
        if (!SesionActual.TienePermiso(
            Permisos.PARTIDAS_PRESUPUESTARIAS_GUARDAR))
        {
          MessageBox.Show(
              Traductor.T("No tiene permisos para guardar cambios."),
              Traductor.T("Acceso denegado"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);

          return;
        }
      }

      if (!ValidarDatos())
        return;

      partida.Anio =
          Convert.ToInt32(txtAnio.Text.Trim());

      partida.Trimestre =
          Convert.ToInt32(
              cmbTrimestre.SelectedItem);

      partida.MontoAsignado =
          Convert.ToDecimal(
              txtMontoAsignado.Text.Trim());

      partida.MontoComprometido =
          Convert.ToDecimal(
              txtMontoComprometido.Text.Trim());

      partida.MontoEjecutado =
          Convert.ToDecimal(
              txtMontoEjecutado.Text.Trim());

      partida.UsuarioGerenteId =
          Convert.ToInt32(
              cmbGerente.SelectedValue);

      partida.Observaciones =
          txtObservaciones.Text.Trim();

      partida.Activo =
          chkActivo.Checked;


      // =======================================================
      // CREAR
      // =======================================================

      if (esNuevo)
      {
        partida.FechaAsignacion =
            DateTime.Now;

        int id =
            partidaService.Agregar(partida);

        partida.PartidaPresupuestariaId = id;

        RegistrarLog(
            "CREAR",
            id,
            "Se creó una partida presupuestaria.");
      }

      // =======================================================
      // MODIFICAR
      // =======================================================

      else
      {
        partidaService.Actualizar(partida);

        RegistrarLog(
            "MODIFICAR",
            partida.PartidaPresupuestariaId,
            "Se modificó una partida presupuestaria.");
      }


      MessageBox.Show(
          Traductor.T("La partida se guardó correctamente."),
          Traductor.T("Partida presupuestaria"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);

      DialogResult = DialogResult.OK;
      Close();
    }


    // =========================================================
    // VALIDACIÓN
    // =========================================================

    private bool ValidarDatos()
    {
      int anio;

      if (!int.TryParse(
          txtAnio.Text.Trim(),
          out anio))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar un año válido."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtAnio.Focus();
        return false;
      }

      if (anio < 2000 || anio > 2100)
      {
        MessageBox.Show(
            Traductor.T("El año ingresado no es válido."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtAnio.Focus();
        return false;
      }


      if (cmbTrimestre.SelectedIndex < 0)
      {
        MessageBox.Show(
            Traductor.T("Debe seleccionar un trimestre."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        cmbTrimestre.Focus();
        return false;
      }

      if (partidaService.ExistePeriodo(
          anio,
          Convert.ToInt32(cmbTrimestre.SelectedItem),
          esNuevo ? 0 : partida.PartidaPresupuestariaId))
      {
        MessageBox.Show(
            Traductor.T("Ya existe una partida presupuestaria para ese año y trimestre."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        cmbTrimestre.Focus();
        return false;
      }


      decimal montoAsignado;

      if (!decimal.TryParse(
          txtMontoAsignado.Text.Trim(),
          out montoAsignado))
      {
        MessageBox.Show(
            Traductor.T("El monto asignado no es válido."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtMontoAsignado.Focus();
        return false;
      }

      if (montoAsignado < 0)
      {
        MessageBox.Show(
            Traductor.T("El monto asignado no puede ser negativo."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtMontoAsignado.Focus();
        return false;
      }


      decimal comprometido;

      if (!decimal.TryParse(
          txtMontoComprometido.Text.Trim(),
          out comprometido) ||
          comprometido < 0)
      {
        MessageBox.Show(
            Traductor.T("El monto comprometido no es válido."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtMontoComprometido.Focus();
        return false;
      }


      decimal ejecutado;

      if (!decimal.TryParse(
          txtMontoEjecutado.Text.Trim(),
          out ejecutado) ||
          ejecutado < 0)
      {
        MessageBox.Show(
            Traductor.T("El monto ejecutado no es válido."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtMontoEjecutado.Focus();
        return false;
      }


      if (ejecutado > comprometido)
      {
        MessageBox.Show(
            Traductor.T("El monto ejecutado no puede ser mayor al comprometido."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtMontoEjecutado.Focus();
        return false;
      }


      if (comprometido > montoAsignado)
      {
        MessageBox.Show(
            Traductor.T("El monto comprometido no puede ser mayor al monto asignado."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtMontoComprometido.Focus();
        return false;
      }


      if (cmbGerente.SelectedValue == null)
      {
        MessageBox.Show(
            Traductor.T("Debe seleccionar un gerente."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        cmbGerente.Focus();
        return false;
      }


      return true;
    }


    // =========================================================
    // LOG
    // =========================================================

    private void RegistrarLog(
        string accion,
        int partidaId,
        string descripcion)
    {
      LogSistemaService logService =
          new LogSistemaService();

      logService.Registrar(
          SesionActual.Usuario.UsuarioId,
          accion,
          "PARTIDAS_PRESUPUESTARIAS",
          partidaId,
          descripcion);
    }
  }
}

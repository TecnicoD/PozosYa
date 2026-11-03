using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using Newtonsoft.Json.Linq;
using System.IO;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormDetalleReclamo : Form
  {
    // =========================
    // VARIABLES
    // =========================

    private readonly int reclamoId;

    private JToken poligonoLomas;

    private bool perimetroCargado = false;
    private GMapOverlay overlayMarcador;


    // =========================
    // CONSTRUCTOR
    // =========================

    public FormDetalleReclamo(int reclamoId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      this.reclamoId = reclamoId;
    }


    // =========================
    // INICIALIZACIÓN
    // =========================

    private void FormDetalleReclamo_Load(object sender, EventArgs e)
    {
      CargarCombos();

      ConfigurarMapa();

      mapa.MouseClick +=new MouseEventHandler(mapa_MouseClick);
      CargarPerimetroLomas();

      CargarReclamo();

      HabilitarEdicion(false);
      ConfigurarPermisos();
    }

    // =========================
    // PERMISOS
    // =========================

    private void ConfigurarPermisos()
    {
      btnVerificacion.Visible =
          SesionActual.TienePermiso(
              Permisos.VERIFICACION_VER);

      btnPresupuesto.Visible =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_VER);
    }



    // =========================
    // RECLAMO
    // =========================

    private void CargarReclamo()
    {
      ReclamoService service =
          new ReclamoService();

      ReclamoDetalleDTO reclamo =
          service.ObtenerPorId(reclamoId);

      if (reclamo == null)
      {
        MessageBox.Show(
            Traductor.T("No se encontró el reclamo."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );

        Close();
        return;
      }

      // Datos generales
      txtNumeroReclamo.Text =
          reclamo.NumeroReclamo;

      txtDescripcion.Text =
          reclamo.Descripcion;

      // Ubicación
      txtDireccion.Text =
          reclamo.Direccion;

      txtLatitud.Text =
          reclamo.Latitud.HasValue
              ? reclamo.Latitud.Value.ToString()
              : "";

      txtLongitud.Text =
          reclamo.Longitud.HasValue
              ? reclamo.Longitud.Value.ToString()
              : "";

      if (reclamo.BarrioId > 0)
        cmbBarrio.SelectedValue =
            reclamo.BarrioId;

      // Estado y clasificación
      if (reclamo.EstadoId > 0)
        cmbEstado.SelectedValue =
            reclamo.EstadoId;

      if (reclamo.TipoDanioId > 0)
        cmbTipoDanio.SelectedValue =
            reclamo.TipoDanioId;

      if (reclamo.SeveridadId > 0)
        cmbSeveridad.SelectedValue =
            reclamo.SeveridadId;

      // Vecino
      txtVecino.Text =
          reclamo.Vecino;

      txtDni.Text =
          reclamo.DNI;

      txtTelefono.Text =
          reclamo.Telefono;

      txtMail.Text =
          reclamo.Mail;

      // Mapa
      CargarMapa(reclamo);
    }


    // =========================
    // COMBOS
    // =========================

    private void CargarCombos()
    {
      DetalleClaveValorService service =
          new DetalleClaveValorService();

      CargarCombo(
          cmbBarrio,
          service.ObtenerPorTabla("BARRIO")
      );

      CargarCombo(
          cmbEstado,
          service.ObtenerPorTabla("ESTADO")
      );

      CargarCombo(
          cmbTipoDanio,
          service.ObtenerPorTabla("TIPO_DANIO")
      );

      CargarCombo(
          cmbSeveridad,
          service.ObtenerPorTabla("SEVERIDAD")
      );
    }

    private void CargarCombo(
        ComboBox combo,
        List<DetalleClaveValor> lista)
    {
      combo.DataSource = lista;
      combo.DisplayMember = "Valor";
      combo.ValueMember = "DetalleId";
    }


    // =========================
    // MAPA
    // =========================

    private void ConfigurarMapa()
    {
      GMaps.Instance.Mode =
          AccessMode.ServerAndCache;

      GMapProvider.UserAgent =
          "PozosYa/1.0";

      mapa.MapProvider =
          GMapProviders.GoogleMap;

      mapa.MinZoom = 5;
      mapa.MaxZoom = 20;
      mapa.Zoom = 15;

      mapa.ShowCenter = false;
      mapa.DragButton = MouseButtons.Left;

      overlayMarcador =
          new GMapOverlay("marcador");

      mapa.Overlays.Add(
          overlayMarcador);
    }

    private void CargarPerimetroLomas()
    {
      try
      {
        string ruta =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources",
                "Geo",
                "lomas_de_zamora.geojson");

        if (!File.Exists(ruta))
        {
          MessageBox.Show(
              Traductor.T("No se encontró el archivo del perímetro de Lomas de Zamora."),
              Traductor.T("Mapa"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);

          perimetroCargado = false;
          return;
        }

        string json =
            File.ReadAllText(ruta);

        JObject geoJson =
            JObject.Parse(json);

        JArray features =
            geoJson["features"] as JArray;

        if (features == null ||
            features.Count == 0)
        {
          MessageBox.Show(
              Traductor.T("El archivo del perímetro no contiene geometrías."),
              Traductor.T("Mapa"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);

          perimetroCargado = false;
          return;
        }

        JToken feature =
            features[0];

        poligonoLomas =
            feature["geometry"];

        if (poligonoLomas == null)
        {
          MessageBox.Show(
              Traductor.T("No se encontró la geometría del perímetro."),
              Traductor.T("Mapa"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);

          perimetroCargado = false;
          return;
        }

        DibujarPerimetroLomas();

        perimetroCargado = true;
        }
      catch (Exception ex)
      {
        perimetroCargado = false;

        MessageBox.Show(
            Traductor.T("No se pudo cargar el perímetro de Lomas de Zamora.\n\n") +
            Traductor.T(ex.Message),
            Traductor.T("Mapa"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
    }

    private void DibujarPerimetroLomas()
    {
      if (poligonoLomas == null)
        return;

      string tipo =
          poligonoLomas["type"]?.ToString();

      JArray coordenadas =
          poligonoLomas["coordinates"] as JArray;

      if (coordenadas == null)
        return;

      GMapOverlay overlay =
          new GMapOverlay("perimetro_lomas");

      if (tipo == "MultiPolygon")
      {
        foreach (JToken polygon in coordenadas)
        {
          JArray anilloExterno =
              polygon[0] as JArray;

          if (anilloExterno == null)
            continue;

          List<PointLatLng> puntos =
              ConvertirCoordenadas(
                  anilloExterno);

          if (puntos.Count < 3)
            continue;

          GMapPolygon poligono =
              new GMapPolygon(
                  puntos,
                  "Lomas de Zamora");

          poligono.Fill =
            new System.Drawing.SolidBrush(
                System.Drawing.Color.FromArgb(
                    50,
                    System.Drawing.Color.Green));

          poligono.Stroke =
              new System.Drawing.Pen(
                  System.Drawing.Color.Green,
                  2);

          overlay.Polygons.Add(
              poligono);
        }
      }

      mapa.Overlays.Add(overlay);

      mapa.Position =
          new PointLatLng(
              -34.76,
              -58.40);

      mapa.Zoom = 13;
    }


    private List<PointLatLng> ConvertirCoordenadas(JArray coordenadas)
    {
      List<PointLatLng> puntos =
          new List<PointLatLng>();

      foreach (JToken coordenada in coordenadas)
      {
        if (coordenada is JArray par &&
            par.Count >= 2)
        {
          double longitud =
              par[0].Value<double>();

          double latitud =
              par[1].Value<double>();

          puntos.Add(
              new PointLatLng(
                  latitud,
                  longitud));
        }
      }

      return puntos;
    }

    private bool PuntoDentroDeLomas(double latitud, double longitud)
    {
      if (!perimetroCargado ||
          poligonoLomas == null)
      {
        return false;
      }

      string tipo =
          poligonoLomas["type"]?.ToString();

      JArray coordenadas =
          poligonoLomas["coordinates"] as JArray;

      if (coordenadas == null)
        return false;

      PointLatLng punto =
          new PointLatLng(
              latitud,
              longitud);

      if (tipo == "MultiPolygon")
      {
        foreach (JToken polygon in coordenadas)
        {
          JArray anilloExterno =
              polygon[0] as JArray;

          if (anilloExterno == null)
            continue;

          List<PointLatLng> puntos =
              ConvertirCoordenadas(
                  anilloExterno);

          if (PuntoDentroPoligono(
              punto,
              puntos))
          {
            return true;
          }
        }
      }

      return false;
    }


    private bool PuntoDentroPoligono(PointLatLng punto, List<PointLatLng> poligono)
    {
      bool dentro = false;

      int j =
          poligono.Count - 1;

      for (int i = 0;
           i < poligono.Count;
           i++)
      {
        double xi =
            poligono[i].Lng;

        double yi =
            poligono[i].Lat;

        double xj =
            poligono[j].Lng;

        double yj =
            poligono[j].Lat;

        bool intersecta =
            ((yi > punto.Lat) != (yj > punto.Lat)) &&
            (punto.Lng <
                (xj - xi) *
                (punto.Lat - yi) /
                (yj - yi) +
                xi);

        if (intersecta)
          dentro = !dentro;

        j = i;
      }

      return dentro;
    }


    private void CargarMapa(ReclamoDetalleDTO reclamo)
    {
      if (!reclamo.Latitud.HasValue ||
          !reclamo.Longitud.HasValue)
      {
        return;
      }

      double latitud =
          Convert.ToDouble(
              reclamo.Latitud.Value);

      double longitud =
          Convert.ToDouble(
              reclamo.Longitud.Value);

      mapa.Position =
          new PointLatLng(
              latitud,
              longitud);

      mapa.Zoom = 13;

      // Limpiar solamente el marcador
      overlayMarcador.Markers.Clear();

      GMarkerGoogle marcador =
          new GMarkerGoogle(
              new PointLatLng(
                  latitud,
                  longitud),
              GMarkerGoogleType.red_dot);

      overlayMarcador.Markers.Add(
          marcador);
    }


    // =========================
    // MODO EDICIÓN
    // =========================

    private void HabilitarEdicion(bool editar)
    {
      // Campos editables
      txtDireccion.ReadOnly = !editar;
      txtDescripcion.ReadOnly = !editar;

      cmbBarrio.Enabled = editar;
      // El estado no se edita a mano: solo cambia con el circuito
      // (verificación, presupuesto, priorización, obra).
      cmbEstado.Enabled = false;
      cmbTipoDanio.Enabled = editar;
      cmbSeveridad.Enabled = editar;

      // Campos de solo lectura
      txtNumeroReclamo.ReadOnly = true;
      txtVecino.ReadOnly = true;
      txtDni.ReadOnly = true;
      txtTelefono.ReadOnly = true;
      txtMail.ReadOnly = true;
      txtLatitud.ReadOnly = true;
      txtLongitud.ReadOnly = true;

      // Botones
      btnModificar.Visible =
          !editar &&
          SesionActual.TienePermiso(Permisos.RECLAMOS_EDITAR);
      btnGuardar.Visible = editar;
      btnCancelar.Visible = editar;
    }


    // =========================
    // DATOS DEL FORMULARIO
    // =========================

    private ReclamoDetalleDTO ObtenerDatosFormulario()
    {
      return new ReclamoDetalleDTO
      {
        ReclamoId = reclamoId,

        Direccion =
              txtDireccion.Text.Trim(),

        Descripcion =
              txtDescripcion.Text.Trim(),

        BarrioId =
              Convert.ToInt32(
                  cmbBarrio.SelectedValue
              ),

        EstadoId =
              Convert.ToInt32(
                  cmbEstado.SelectedValue
              ),

        TipoDanioId =
              Convert.ToInt32(
                  cmbTipoDanio.SelectedValue
              ),

        SeveridadId =
              Convert.ToInt32(
                  cmbSeveridad.SelectedValue
              )
      };
    }


    // =========================
    // EDICIÓN
    // =========================

    private void btnModificar_Click(object sender, EventArgs e)
    {
      HabilitarEdicion(true);
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      CargarReclamo();
      HabilitarEdicion(false);
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(
          txtDireccion.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar una dirección."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        );

        return;
      }

      DialogResult respuesta =
          MessageBox.Show(
              Traductor.T("¿Desea guardar los cambios?"),
              Traductor.T("Confirmación"),
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question
          );

      if (respuesta != DialogResult.Yes)
        return;

      ReclamoService service =
          new ReclamoService();

      ReclamoDetalleDTO reclamo =
          ObtenerDatosFormulario();

      service.Actualizar(reclamo);

      RegistrarLogModificacion(
          reclamo.ReclamoId
      );

      MessageBox.Show(
          Traductor.T("Los cambios se guardaron correctamente."),
          Traductor.T("Reclamo"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information
      );

      CargarReclamo();
      HabilitarEdicion(false);
    }

    private void RegistrarLogModificacion(int reclamoId)
    {
      LogSistemaService logService =
          new LogSistemaService();

      logService.Registrar(
          SesionActual.Usuario.UsuarioId,
          "MODIFICAR",
          "RECLAMOS",
          reclamoId,
          "Se modificó el reclamo."
      );
    }


    // =========================
    // HISTORIAL
    // =========================

    private void btnVerHistorial_Click(object sender, EventArgs e)
    {
      FormHistorial form =
          new FormHistorial(reclamoId);

      form.ShowDialog();
    }

    private void btnVerificacion_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.VERIFICACION_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder a la verificación."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      FormVerificacion form =
          new FormVerificacion(reclamoId);

      form.ShowDialog();
    }

    private void btnPresupuesto_Click(object sender,EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRESUPUESTO_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder al presupuesto."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      FormPresupuesto formulario =
          new FormPresupuesto(reclamoId);

      formulario.ShowDialog();
    }

    private void mapa_MouseClick(object sender,MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;

      if (!perimetroCargado)
      {
        MessageBox.Show(
            Traductor.T("No se pudo cargar el perímetro de Lomas de Zamora.\n") +
            Traductor.T("No es posible seleccionar una ubicación."),
            Traductor.T("Mapa"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      PointLatLng punto =
          mapa.FromLocalToLatLng(
              e.X,
              e.Y);

      double latitud =
          punto.Lat;

      double longitud =
          punto.Lng;


      if (!PuntoDentroDeLomas(
          latitud,
          longitud))
      {
        MessageBox.Show(
            Traductor.T("La ubicación seleccionada se encuentra fuera ") +
            Traductor.T("del perímetro del Partido de Lomas de Zamora."),
            Traductor.T("Ubicación no válida"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }


      // =========================================================
      // UBICACIÓN VÁLIDA
      // =========================================================

      txtLatitud.Text =
          latitud.ToString();

      txtLongitud.Text =
          longitud.ToString();


      mapa.Position =
          punto;


      overlayMarcador.Markers.Clear();

      GMarkerGoogle marcador =
          new GMarkerGoogle(
              punto,
              GMarkerGoogleType.red_dot);

      overlayMarcador.Markers.Add(
          marcador);
    }


  }
}

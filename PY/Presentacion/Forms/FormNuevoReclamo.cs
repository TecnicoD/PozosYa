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
  public partial class FormNuevoReclamo : Form
  {
    // =========================
    // VARIABLES
    // =========================

    private readonly VecinoService vecinoService =
        new VecinoService();

    private Vecino vecinoEncontrado;

    private JToken poligonoLomas;

    private bool perimetroCargado = false;

    private GMapOverlay overlayMarcador;

    // =========================
    // CONSTRUCTOR
    // =========================

    public FormNuevoReclamo()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      CargarCombos();
      ConfigurarMapa();

      mapa.MouseClick += mapa_MouseClick;

      CargarPerimetroLomas();
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

      mapa.Position =
          new PointLatLng(
              -34.761,
              -58.402);

      mapa.MinZoom = 5;
      mapa.MaxZoom = 20;
      mapa.Zoom = 13;

      mapa.ShowCenter = false;
      mapa.CanDragMap = true;
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
          perimetroCargado = false;

          MessageBox.Show(
              Traductor.T("No se encontró el archivo del perímetro de Lomas de Zamora."),
              Traductor.T("Mapa"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);

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
          perimetroCargado = false;

          MessageBox.Show(
              Traductor.T("El archivo del perímetro no contiene geometrías."),
              Traductor.T("Mapa"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);

          return;
        }

        // Buscamos específicamente el Partido de Lomas de Zamora.
        JToken featureEncontrado = null;

        foreach (JToken feature in features)
        {
          string nombre =
              feature["properties"]?["fna"]?.ToString();

          if (!string.IsNullOrWhiteSpace(nombre) &&
              nombre.Equals(
                  "Partido de Lomas de Zamora",
                  StringComparison.OrdinalIgnoreCase))
          {
            featureEncontrado = feature;
            break;
          }
        }

        if (featureEncontrado == null)
        {
          MessageBox.Show(
              Traductor.T("No se encontró el Partido de Lomas de Zamora en el GeoJSON."),
              Traductor.T("Mapa"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);

          perimetroCargado = false;
          return;
        }

        poligonoLomas =
            featureEncontrado["geometry"];

        if (poligonoLomas == null)
        {
          MessageBox.Show(
              Traductor.T("No se encontró la geometría de Lomas de Zamora."),
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
                  3);

          overlay.Polygons.Add(
              poligono);
        }
      }

      mapa.Overlays.Add(
          overlay);

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

    private bool PuntoDentroPoligono(PointLatLng punto,List<PointLatLng> poligono)
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

      // Ubicación válida
      txtLatitud.Text =
          latitud.ToString("F8");

      txtLongitud.Text =
          longitud.ToString("F8");

      overlayMarcador.Markers.Clear();

      GMarkerGoogle marcador =
          new GMarkerGoogle(
              punto,
              GMarkerGoogleType.red_dot);

      overlayMarcador.Markers.Add(
          marcador);
    }


    // =========================
    // VECINO
    // =========================

    private void btnConsultar_Click(object sender, EventArgs e)
    {
      string dni = txtDNI.Text.Trim();

      if (string.IsNullOrWhiteSpace(dni))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar un DNI."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        );

        txtDNI.Focus();
        return;
      }

      vecinoEncontrado =
          vecinoService.ObtenerPorDni(dni);

      if (vecinoEncontrado == null)
      {
        MessageBox.Show(
            Traductor.T("No se encontró un vecino con ese DNI.\n") +
            Traductor.T("Puede completar sus datos para registrarlo."),
            Traductor.T("Vecino no encontrado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );

        LimpiarDatosVecino();
        return;
      }

      CargarDatosVecino();
    }

    private void CargarDatosVecino()
    {
      txtNombre.Text = vecinoEncontrado.Nombre;
      txtApellido.Text = vecinoEncontrado.Apellido;
      txtTelefono.Text = vecinoEncontrado.Telefono;
      txtMail.Text = vecinoEncontrado.Mail;
      txtNroCatastral.Text =
          vecinoEncontrado.NroCatastral;

      txtNombre.ReadOnly = true;
      txtApellido.ReadOnly = true;
      txtTelefono.ReadOnly = true;
      txtMail.ReadOnly = true;
      txtNroCatastral.ReadOnly = true;
    }

    private void LimpiarDatosVecino()
    {
      vecinoEncontrado = null;

      txtNombre.Clear();
      txtApellido.Clear();
      txtTelefono.Clear();
      txtMail.Clear();
      txtNroCatastral.Clear();

      txtNombre.ReadOnly = false;
      txtApellido.ReadOnly = false;
      txtTelefono.ReadOnly = false;
      txtMail.ReadOnly = false;
      txtNroCatastral.ReadOnly = false;
    }

    private Vecino ObtenerVecinoFormulario()
    {
      return new Vecino
      {
        Nombre = txtNombre.Text.Trim(),
        Apellido = txtApellido.Text.Trim(),
        Dni = txtDNI.Text.Trim(),
        Telefono = txtTelefono.Text.Trim(),
        Mail = txtMail.Text.Trim(),
        Direccion = txtDireccion.Text.Trim(),
        NroCatastral =
              txtNroCatastral.Text.Trim(),

        Activo = true,
        FechaRegistro = DateTime.Now,
        BarrioId =
              Convert.ToInt32(cmbBarrio.SelectedValue),

        DigitoVerificador = ""
      };
    }


    // =========================
    // RECLAMO
    // =========================

    private Reclamo ObtenerReclamoFormulario()
    {
      return new Reclamo
      {
        VecinoId =
              vecinoEncontrado.VecinoId,

        NumeroReclamo =
              "REC-" +
              DateTime.Now.ToString(
                  "yyyyMMddHHmmssfff"
              ),

        TipoDanioId =
              Convert.ToInt32(
                  cmbTipoDanio.SelectedValue
              ),

        SeveridadId =
              Convert.ToInt32(
                  cmbSeveridad.SelectedValue
              ),

        Descripcion =
              txtDescripcion.Text.Trim(),

        Direccion =
              txtDireccion.Text.Trim(),

        BarrioId =
              Convert.ToInt32(
                  cmbBarrio.SelectedValue
              ),

        FechaCreacion = DateTime.Now,

        UsuarioCreadorId =
              SesionActual.Usuario.UsuarioId,

        DigitoVerificador = "",

        Latitud =
              Convert.ToDecimal(
                  txtLatitud.Text
              ),

        Longitud =
              Convert.ToDecimal(
                  txtLongitud.Text
              ),

        EstadoActualId = 1
      };
    }


    // =========================
    // GUARDAR
    // =========================

    private void btnGuardar_Click(object sender, EventArgs e)
    {
      // Se valida todo antes de guardar, para no dejar
      // un vecino nuevo guardado sin su reclamo.
      if (!ValidarDatosVecino())
        return;

      if (!ValidarDatosReclamo())
        return;

      if (vecinoEncontrado == null)
      {
        Vecino nuevoVecino =
            ObtenerVecinoFormulario();

        int vecinoId =
            vecinoService.Agregar(nuevoVecino);

        vecinoEncontrado = nuevoVecino;
        vecinoEncontrado.VecinoId = vecinoId;
      }

      ReclamoService reclamoService =
          new ReclamoService();

      Reclamo nuevoReclamo =
          ObtenerReclamoFormulario();

      int reclamoId =
          reclamoService.Agregar(nuevoReclamo);

      RegistrarLogCreacion(
          reclamoId,
          nuevoReclamo.NumeroReclamo
      );

      MessageBox.Show(
          Traductor.T("El reclamo se registró correctamente."),
          Traductor.T("Reclamo registrado"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information
      );

      DialogResult = DialogResult.OK;
      Close();
    }

    private void RegistrarLogCreacion(int reclamoId, string numeroReclamo)
    {
      LogSistemaService logService =
          new LogSistemaService();

      logService.Registrar(
          SesionActual.Usuario.UsuarioId,
          "CREAR",
          "RECLAMOS",
          reclamoId,
          "Se creó el reclamo " +
          numeroReclamo
      );
    }


    private void btnCancelar_Click(object sender, EventArgs e)
    {
      Close();
    }

    // =========================
    // COMBOS
    // =========================

    private void CargarCombos()
    {
      DetalleClaveValorService detalleService =
          new DetalleClaveValorService();

      CargarCombo(
          cmbBarrio,
          detalleService.ObtenerPorTabla("BARRIO")
      );

      CargarCombo(
          cmbTipoDanio,
          detalleService.ObtenerPorTabla("TIPO_DANIO")
      );

      CargarCombo(
          cmbSeveridad,
          detalleService.ObtenerPorTabla("SEVERIDAD")
      );
    }

    private void CargarCombo(ComboBox combo, List<DetalleClaveValor> lista)
    {
      combo.DataSource = lista;
      combo.DisplayMember = "Valor";
      combo.ValueMember = "DetalleId";
    }


    // =========================
    // VALIDACIONES
    // =========================

    private bool ValidarDatosVecino()
    {
      if (string.IsNullOrWhiteSpace(txtNombre.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el nombre del vecino.")
        );

        txtNombre.Focus();
        return false;
      }

      if (string.IsNullOrWhiteSpace(txtApellido.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el apellido del vecino.")
        );

        txtApellido.Focus();
        return false;
      }

      if (string.IsNullOrWhiteSpace(txtDNI.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el DNI.")
        );

        txtDNI.Focus();
        return false;
      }

      if (cmbBarrio.SelectedValue == null)
      {
        MessageBox.Show(
            Traductor.T("Debe seleccionar un barrio.")
        );

        cmbBarrio.Focus();
        return false;
      }

      return true;
    }

    private bool ValidarDatosReclamo()
    {
      if (string.IsNullOrWhiteSpace(txtDireccion.Text))
      {
        MostrarValidacion(
            "Debe ingresar una dirección.");

        txtDireccion.Focus();
        return false;
      }

      if (cmbTipoDanio.SelectedValue == null)
      {
        MostrarValidacion(
            "Debe seleccionar un tipo de daño.");

        cmbTipoDanio.Focus();
        return false;
      }

      if (cmbSeveridad.SelectedValue == null)
      {
        MostrarValidacion(
            "Debe seleccionar una severidad.");

        cmbSeveridad.Focus();
        return false;
      }

      if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
      {
        MostrarValidacion(
            "Debe ingresar una descripción del reclamo.");

        txtDescripcion.Focus();
        return false;
      }

      // La ubicación solo se carga haciendo clic en el mapa.
      decimal latitud;
      decimal longitud;

      if (!decimal.TryParse(txtLatitud.Text, out latitud) ||
          !decimal.TryParse(txtLongitud.Text, out longitud))
      {
        MostrarValidacion(
            "Debe marcar la ubicación del reclamo en el mapa.");

        return false;
      }

      return true;
    }

    private void MostrarValidacion(string mensaje)
    {
      MessageBox.Show(
          Traductor.T(mensaje),
          Traductor.T("Validación"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
    }
  }
}

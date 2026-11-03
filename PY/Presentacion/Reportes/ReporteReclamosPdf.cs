using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using PY.Presentacion.Idiomas;
using System;
using System.Collections.Generic;

namespace PY.Presentacion.Reportes
{
  // Genera un PDF tipo planilla (Excel) con el listado de reclamos
  // que se está mostrando en pantalla, con los filtros aplicados.
  public static class ReporteReclamosPdf
  {
    // =========================================================
    // COLORES (estilo planilla)
    // =========================================================

    private static readonly Color ColorEncabezado =
        new Color(31, 111, 67);

    private static readonly Color ColorFilaAlterna =
        new Color(234, 241, 236);

    private static readonly Color ColorBorde =
        new Color(191, 191, 191);


    // =========================================================
    // GENERAR
    // =========================================================

    public static void Generar(
        string rutaDestino,
        List<ReclamoListado> reclamos,
        string filtrosAplicados)
    {
      Document documento =
          new Document();

      documento.Info.Title =
          Traductor.T("Reporte de reclamos");

      ConfigurarEstilos(documento);

      Section seccion =
          documento.AddSection();

      ConfigurarPagina(seccion);

      AgregarEncabezado(
          seccion,
          filtrosAplicados);

      AgregarTabla(
          seccion,
          reclamos);

      Paragraph total =
          seccion.AddParagraph(
              Traductor.T("Total de reclamos: ") +
              reclamos.Count);

      total.Format.SpaceBefore = "0.3cm";
      total.Format.Font.Bold = true;

      // -------------------------------------------------------
      // RENDERIZAR Y GUARDAR
      // -------------------------------------------------------

      PdfDocumentRenderer renderer =
          new PdfDocumentRenderer(true);

      renderer.Document = documento;
      renderer.RenderDocument();
      renderer.PdfDocument.Save(rutaDestino);
    }


    // =========================================================
    // PÁGINA Y ESTILOS
    // =========================================================

    private static void ConfigurarEstilos(Document documento)
    {
      Style normal =
          documento.Styles["Normal"];

      normal.Font.Name = "Arial";
      normal.Font.Size = 9;
    }

    private static void ConfigurarPagina(Section seccion)
    {
      seccion.PageSetup.PageFormat = PageFormat.A4;
      seccion.PageSetup.Orientation = Orientation.Landscape;

      seccion.PageSetup.TopMargin = "1.5cm";
      seccion.PageSetup.BottomMargin = "1.5cm";
      seccion.PageSetup.LeftMargin = "1.5cm";
      seccion.PageSetup.RightMargin = "1.5cm";

      // Pie: "Página X de Y"
      Paragraph pie =
          seccion.Footers.Primary.AddParagraph();

      pie.Format.Alignment = ParagraphAlignment.Right;
      pie.Format.Font.Size = 8;

      pie.AddText(Traductor.T("Página") + " ");
      pie.AddPageField();
      pie.AddText(" " + Traductor.T("de") + " ");
      pie.AddNumPagesField();
    }


    // =========================================================
    // ENCABEZADO
    // =========================================================

    private static void AgregarEncabezado(
        Section seccion,
        string filtrosAplicados)
    {
      string municipalidad =
          new ConfiguracionService()
              .ObtenerValor("MUNICIPALIDAD_NOMBRE");

      Paragraph titulo =
          seccion.AddParagraph(municipalidad);

      titulo.Format.Font.Size = 14;
      titulo.Format.Font.Bold = true;

      Paragraph subtitulo =
          seccion.AddParagraph(Traductor.T("Reporte de reclamos"));

      subtitulo.Format.Font.Size = 12;
      subtitulo.Format.SpaceAfter = "0.2cm";

      string usuario =
          SesionActual.Usuario == null
              ? ""
              : " - " + SesionActual.Usuario.NombreCompleto;

      seccion.AddParagraph(
          Traductor.T("Generado: ") +
          DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
          usuario);

      Paragraph filtros =
          seccion.AddParagraph(
              Traductor.T("Filtros: ") +
              filtrosAplicados);

      filtros.Format.SpaceAfter = "0.4cm";
    }


    // =========================================================
    // TABLA
    // =========================================================

    private static void AgregarTabla(
        Section seccion,
        List<ReclamoListado> reclamos)
    {
      Table tabla =
          seccion.AddTable();

      tabla.Borders.Width = 0.5;
      tabla.Borders.Color = ColorBorde;
      tabla.Rows.LeftIndent = 0;

      // Ancho útil en A4 horizontal: 26,7 cm
      tabla.AddColumn("5cm");    // N° reclamo
      tabla.AddColumn("3cm");    // Fecha
      tabla.AddColumn("5cm");    // Vecino
      tabla.AddColumn("6.7cm");  // Dirección
      tabla.AddColumn("4cm");    // Barrio
      tabla.AddColumn("3cm");    // Estado

      // -------------------------------------------------------
      // ENCABEZADO (se repite en cada página)
      // -------------------------------------------------------

      Row encabezado =
          tabla.AddRow();

      encabezado.HeadingFormat = true;
      encabezado.Shading.Color = ColorEncabezado;
      encabezado.Format.Font.Bold = true;
      encabezado.Format.Font.Color = Colors.White;

      string[] columnas =
      {
          Traductor.T("N° Reclamo"),
          Traductor.T("Fecha"),
          Traductor.T("Vecino"),
          Traductor.T("Dirección"),
          Traductor.T("Barrio"),
          Traductor.T("Estado")
      };

      for (int i = 0; i < columnas.Length; i++)
      {
        encabezado.Cells[i].AddParagraph(columnas[i]);
      }

      // -------------------------------------------------------
      // FILAS
      // -------------------------------------------------------

      for (int i = 0; i < reclamos.Count; i++)
      {
        ReclamoListado reclamo =
            reclamos[i];

        Row fila =
            tabla.AddRow();

        if (i % 2 == 1)
          fila.Shading.Color = ColorFilaAlterna;

        fila.Cells[0].AddParagraph(reclamo.NumeroReclamo);
        fila.Cells[1].AddParagraph(
            reclamo.FechaCreacion.ToString("dd/MM/yyyy HH:mm"));
        fila.Cells[2].AddParagraph(reclamo.Vecino);
        fila.Cells[3].AddParagraph(reclamo.Direccion);
        fila.Cells[4].AddParagraph(reclamo.Barrio);
        fila.Cells[5].AddParagraph(reclamo.Estado);
      }

      // Margen interno de las celdas
      tabla.TopPadding = 2;
      tabla.BottomPadding = 2;
    }
  }
}

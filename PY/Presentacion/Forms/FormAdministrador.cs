using Negocio.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
    public partial class FormAdministrador : Form
    {
        private readonly ConfiguracionService configuracionService = new ConfiguracionService();
        private readonly AdministradorService administradorService = new AdministradorService();
        public FormAdministrador()
        {
            InitializeComponent();
            Traductor.Traducir(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarArchivosBackup();
        }

        private void actualizarListadoBkps(string rutaCarpetaBackups)
        {
            lstBackups.Items.Clear();

            // Obtiene todos los archivos .bak (puedes cambiar la extensión si usas otra)
            string[] archivos;

            try
            {
                archivos = Directory.GetFiles(rutaCarpetaBackups, "*.bak");
            }
            catch (UnauthorizedAccessException)
            {
                // SQL Server puede escribir en su carpeta de backups aunque Windows
                // no le dé permiso de lectura al usuario que usa la aplicación.
                MessageBox.Show(Traductor.T("Windows no permite leer la carpeta de backups con este usuario. Los backups se generan igual, pero no se pueden listar desde acá."), Traductor.T("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (string archivo in archivos)
            {
                // Agrega solo el nombre del archivo (ej: backup_2026.bak) y no la ruta completa
                lstBackups.Items.Add(Path.GetFileName(archivo));
            }

        }
        /// <summary>
        /// Lee la carpeta y llena el ListBox solo con los nombres de los archivos
        /// </summary>
        private void CargarArchivosBackup()
        {
            txbCarpetaBkp.Text  = configuracionService.ObtenerValor("ruta_backup");

            try
            {
                // Si la carpeta configurada no existe en esta PC (por ejemplo otra
                // versión de SQL Server), se usa la carpeta de backups por defecto
                // de la instancia y se guarda en la configuración.
                if (!Directory.Exists(txbCarpetaBkp.Text))
                {
                    string rutaServidor = administradorService.ObtenerRutaBackupServidor();

                    if (!string.IsNullOrWhiteSpace(rutaServidor) && Directory.Exists(rutaServidor))
                    {
                        configuracionService.Actualizar("ruta_backup", rutaServidor);
                        txbCarpetaBkp.Text = rutaServidor;
                    }
                }

                if (Directory.Exists(txbCarpetaBkp.Text))
                {
                    actualizarListadoBkps(txbCarpetaBkp.Text);
                }
                else
                {
                    MessageBox.Show(Traductor.T("La carpeta de copias de seguridad no existe."), Traductor.T("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Traductor.T("Error al cargar archivos: ") + Traductor.T(ex.Message), Traductor.T("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento del botón para restaurar el archivo seleccionado
        /// </summary>
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            // 1. Validar que el usuario haya seleccionado un archivo
            if (lstBackups.SelectedItem == null)
            {
                MessageBox.Show(Traductor.T("Por favor, seleccione un archivo de la lista."), Traductor.T("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreArchivo = lstBackups.SelectedItem.ToString();

            // 2. Confirmación del usuario
            DialogResult resultado = MessageBox.Show(string.Format(Traductor.T("¿Está seguro de que desea restaurar el archivo '{0}'?\nSe sobrescribirán los datos actuales."), nombreArchivo),
                Traductor.T("Confirmar Restauración"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                string nombreBD = configuracionService.ObtenerValor("nombre_db");
                int resultado_bkp = administradorService.ejecutarRestoreBD(txbCarpetaBkp.Text+'\\'+nombreArchivo, nombreBD);

                if (resultado_bkp == 0)
                    MessageBox.Show(Traductor.T("La base de datos se ha restaurado correctamente."), Traductor.T("Éxito"), MessageBoxButtons.OK);
                else
                    MessageBox.Show(Traductor.T("Ocurrió un error al restaurar la base de datos"), Traductor.T("Error en Servidor"), MessageBoxButtons.OK);

            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(Traductor.T("¿Está seguro de que desea generar archivo de backup?"),
                Traductor.T("Confirmar Backup"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                int resultado_bkp = administradorService.ejecutarBackupBD();

                if (resultado_bkp == 0) {
                    MessageBox.Show(Traductor.T("Se ha generado el archivo exitosamente."), Traductor.T("OK"), MessageBoxButtons.OK);
                    actualizarListadoBkps(txbCarpetaBkp.Text);
                } else
                    MessageBox.Show(Traductor.T("Ocurrió un error al generar el backup de la base de datos."), Traductor.T("ERROR"), MessageBoxButtons.OK);

            }

        }

        private void btnVerificarIntegridad_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(Traductor.T("¿Está seguro de que desea verificar la integridad de datos?"),
            Traductor.T("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                int resultado_bkp = administradorService.ejecutarValidacionIntegridadBD();

                if (resultado_bkp == 0)
                {
                    MessageBox.Show(Traductor.T("Se ha validado la integridad de datos exitosamente."), Traductor.T("OK"), MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show(Traductor.T("Ocurrió un error en la integridad de datos."), Traductor.T("ERROR"), MessageBoxButtons.OK);

            }
        }
    }

}

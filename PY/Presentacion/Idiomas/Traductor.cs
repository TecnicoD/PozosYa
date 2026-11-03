using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace PY.Presentacion.Idiomas
{
  public enum Idioma
  {
    Espanol,
    Ingles,
    Portugues
  }

  // Traducción de la interfaz.
  // Los textos del sistema están escritos en español y se usan como
  // clave del diccionario: si un texto no está cargado, se muestra
  // en español. Los datos de la BD (estados, barrios, etc.) no se traducen.
  public static class Traductor
  {
    // =========================================================
    // IDIOMA ACTUAL
    // =========================================================

    public static Idioma IdiomaActual { get; set; } = Idioma.Espanol;

    // Texto original (en español) de cada control ya traducido,
    // para poder volver a traducirlo si se cambia el idioma.
    private static readonly ConditionalWeakTable<object, string> originales =
        new ConditionalWeakTable<object, string>();


    // =========================================================
    // TRADUCIR UN TEXTO
    // =========================================================

    public static string T(string texto)
    {
      if (IdiomaActual == Idioma.Espanol ||
          string.IsNullOrWhiteSpace(texto))
      {
        return texto;
      }

      // Se conservan los espacios y saltos de línea de los extremos
      // (ej: "El reclamo " o "\nFondos disponibles: $").
      string nucleo = texto.Trim();
      int inicio = texto.IndexOf(nucleo, StringComparison.Ordinal);
      string prefijo = texto.Substring(0, inicio);
      string sufijo = texto.Substring(inicio + nucleo.Length);

      string traducido;

      if (!Buscar(nucleo, out traducido))
      {
        // "Severidad:" usa la misma traducción que "Severidad".
        if (nucleo.EndsWith(":") &&
            Buscar(nucleo.TrimEnd(':').TrimEnd(), out traducido))
        {
          traducido += ":";
        }
        else
        {
          return texto;
        }
      }

      return prefijo + traducido + sufijo;
    }

    private static bool Buscar(string clave, out string traducido)
    {
      traducido = null;

      string[] valores;

      if (!textos.TryGetValue(clave, out valores))
        return false;

      traducido =
          IdiomaActual == Idioma.Ingles
              ? valores[0]
              : valores[1];

      return true;
    }


    // =========================================================
    // TRADUCIR UN FORMULARIO
    // =========================================================

    // Traduce los controles del formulario (labels, botones, títulos,
    // columnas de grillas). Se llama después de InitializeComponent().
    public static void Traducir(Control control)
    {
      Aplicar(control, false);
    }

    // Vuelve a traducir desde el español (cuando se cambia el idioma
    // con el formulario abierto, ej: el login).
    public static void Retraducir(Control control)
    {
      Aplicar(control, true);
    }

    private static void Aplicar(Control control, bool forzar)
    {
      if (DebeTraducirTexto(control))
      {
        TraducirPropiedad(
            control,
            control.Text,
            t => control.Text = t,
            forzar);
      }

      DataGridView grilla = control as DataGridView;

      if (grilla != null)
      {
        foreach (DataGridViewColumn columna in grilla.Columns)
        {
          DataGridViewColumn c = columna;

          TraducirPropiedad(
              c,
              c.HeaderText,
              t => c.HeaderText = t,
              forzar);
        }
      }

      foreach (Control hijo in control.Controls)
      {
        Aplicar(hijo, forzar);
      }
    }

    private static void TraducirPropiedad(
        object objeto,
        string textoActual,
        Action<string> asignar,
        bool forzar)
    {
      string original;

      if (originales.TryGetValue(objeto, out original))
      {
        // Ya fue traducido antes: solo se repite si se cambió el idioma.
        if (!forzar)
          return;
      }
      else
      {
        original = textoActual;
        originales.Add(objeto, original);
      }

      asignar(T(original));
    }

    // Los campos donde escribe el usuario o que muestran datos
    // (TextBox, ComboBox, ListBox, etc.) no se traducen.
    private static bool DebeTraducirTexto(Control control)
    {
      return control is Form ||
             control is Label ||
             control is ButtonBase ||
             control is GroupBox ||
             control is TabPage;
    }


    // =========================================================
    // DICCIONARIO: español -> { inglés, portugués }
    // =========================================================

    private static readonly Dictionary<string, string[]> textos =
        new Dictionary<string, string[]>
    {
      // ---------------------------------------------------------
      // GENERALES
      // ---------------------------------------------------------
      { "Idiomas", new[] { "Languages", "Idiomas" } },
      { "Acceso denegado", new[] { "Access denied", "Acesso negado" } },
      { "Aviso", new[] { "Notice", "Aviso" } },
      { "Buscar", new[] { "Search", "Buscar" } },
      { "Cancelar", new[] { "Cancel", "Cancelar" } },
      { "Cantidad", new[] { "Count", "Quantidade" } },
      { "Cerrar", new[] { "Close", "Fechar" } },
      { "Confirmación", new[] { "Confirmation", "Confirmação" } },
      { "Confirmar", new[] { "Confirm", "Confirmar" } },
      { "ERROR", new[] { "ERROR", "ERRO" } },
      { "Error", new[] { "Error", "Erro" } },
      { "Éxito", new[] { "Success", "Sucesso" } },
      { "Filtros", new[] { "Filters", "Filtros" } },
      { "Guardar", new[] { "Save", "Salvar" } },
      { "Guardar cambios", new[] { "Save changes", "Salvar alterações" } },
      { "Información", new[] { "Information", "Informação" } },
      { "Limpiar", new[] { "Clear", "Limpar" } },
      { "Modificar", new[] { "Edit", "Editar" } },
      { "Nuevo", new[] { "New", "Novo" } },
      { "Principal", new[] { "Main", "Principal" } },
      { "Seleccionar", new[] { "Select", "Selecionar" } },
      { "Todos", new[] { "All", "Todos" } },
      { "Validación", new[] { "Validation", "Validação" } },
      { "Activa", new[] { "Active", "Ativa" } },
      { "Activas", new[] { "Active", "Ativas" } },
      { "Activo", new[] { "Active", "Ativo" } },
      { "Activos", new[] { "Active", "Ativos" } },
      { "Inactiva", new[] { "Inactive", "Inativa" } },
      { "Inactivas", new[] { "Inactive", "Inativas" } },
      { "Inactivo", new[] { "Inactive", "Inativo" } },
      { "Inactivos", new[] { "Inactive", "Inativos" } },
      { "Desde", new[] { "From", "De" } },
      { "Hasta", new[] { "To", "Até" } },
      { "Sin filtros", new[] { "No filters", "Sem filtros" } },
      { "No tiene permisos para acceder a esta pantalla.", new[] { "You do not have permission to access this screen.", "Você não tem permissão para acessar esta tela." } },
      { "No tiene permisos para guardar cambios.", new[] { "You do not have permission to save changes.", "Você não tem permissão para salvar alterações." } },
      { "No tiene permisos para realizar esta acción.", new[] { "You do not have permission to perform this action.", "Você não tem permissão para realizar esta ação." } },
      { "Los cambios se guardaron correctamente.", new[] { "Changes were saved successfully.", "As alterações foram salvas com sucesso." } },
      { "¿Desea guardar los cambios?", new[] { "Do you want to save the changes?", "Deseja salvar as alterações?" } },

      // ---------------------------------------------------------
      // CAMPOS COMUNES
      // ---------------------------------------------------------
      { "Apellido", new[] { "Last name", "Sobrenome" } },
      { "Barrio", new[] { "Neighborhood", "Bairro" } },
      { "Código", new[] { "Code", "Código" } },
      { "Contaseña", new[] { "Password", "Senha" } },
      { "Contraseña", new[] { "Password", "Senha" } },
      { "CUIT", new[] { "Tax ID (CUIT)", "CUIT" } },
      { "Descripcion", new[] { "Description", "Descrição" } },
      { "Descripción", new[] { "Description", "Descrição" } },
      { "Direccion", new[] { "Address", "Endereço" } },
      { "Dirección", new[] { "Address", "Endereço" } },
      { "DNI", new[] { "ID number", "DNI" } },
      { "Email", new[] { "Email", "E-mail" } },
      { "Estado", new[] { "Status", "Status" } },
      { "Estado Anterior", new[] { "Previous status", "Status anterior" } },
      { "Fecha", new[] { "Date", "Data" } },
      { "Fecha de alta", new[] { "Created on", "Data de cadastro" } },
      { "Fecha de Modificación", new[] { "Modified on", "Data de modificação" } },
      { "ID", new[] { "ID", "ID" } },
      { "Latitud", new[] { "Latitude", "Latitude" } },
      { "Localidad", new[] { "Locality", "Localidade" } },
      { "Longitud", new[] { "Longitude", "Longitude" } },
      { "Mail", new[] { "Email", "E-mail" } },
      { "Nombre", new[] { "Name", "Nome" } },
      { "Número", new[] { "Number", "Número" } },
      { "N° Catastral", new[] { "Cadastral No.", "Nº cadastral" } },
      { "Observaciones", new[] { "Notes", "Observações" } },
      { "Registro", new[] { "Record", "Registro" } },
      { "Rol", new[] { "Role", "Perfil" } },
      { "Tabla", new[] { "Table", "Tabela" } },
      { "Teléfono", new[] { "Phone", "Telefone" } },
      { "Ubicación", new[] { "Location", "Localização" } },
      { "Usuario", new[] { "User", "Usuário" } },
      { "Username", new[] { "Username", "Nome de usuário" } },
      { "Vecino", new[] { "Resident", "Morador" } },
      { "Último Login", new[] { "Last login", "Último acesso" } },
      { "Acción", new[] { "Action", "Ação" } },

      // ---------------------------------------------------------
      // LOGIN / MENÚ / ENCABEZADO
      // ---------------------------------------------------------
      { "Login", new[] { "Login", "Login" } },
      { "Ingresar", new[] { "Sign in", "Entrar" } },
      { "¿Olvidaste tu contraseña?", new[] { "Forgot your password?", "Esqueceu sua senha?" } },
      { "Usuario o contraseña incorrectos.", new[] { "Incorrect username or password.", "Usuário ou senha incorretos." } },
      { "Portal Interno de Gestión Vial", new[] { "Internal Road Management Portal", "Portal Interno de Gestão Viária" } },
      { "MUNICIPIO DE LOMAS DE ZAMORA", new[] { "LOMAS DE ZAMORA MUNICIPALITY", "MUNICÍPIO DE LOMAS DE ZAMORA" } },
      { "Dirección de Sistemas | Lomas de Zamora | PP3 - ISFT N° 172", new[] { "Systems Department | Lomas de Zamora | PP3 - ISFT No. 172", "Diretoria de Sistemas | Lomas de Zamora | PP3 - ISFT Nº 172" } },
      { "Bienvenido", new[] { "Welcome", "Bem-vindo" } },
      { "Bienvenido,", new[] { "Welcome,", "Bem-vindo," } },
      { "Cerrar sesión", new[] { "Sign out", "Sair" } },
      { "¿Desea cerrar la sesión?", new[] { "Do you want to sign out?", "Deseja encerrar a sessão?" } },
      { "Reclamos", new[] { "Claims", "Reclamações" } },
      { "Priorización", new[] { "Prioritization", "Priorização" } },
      { "Partidas Presupuestarias", new[] { "Budget allocations", "Dotações orçamentárias" } },
      { "Partidas Prespuestarias", new[] { "Budget allocations", "Dotações orçamentárias" } },
      { "Proveedores", new[] { "Suppliers", "Fornecedores" } },
      { "Configuración", new[] { "Settings", "Configurações" } },
      { "Seguridad", new[] { "Security", "Segurança" } },
      { "Logs", new[] { "Logs", "Logs" } },
      { "Logs del Sistema", new[] { "System logs", "Logs do sistema" } },
      { "Administración de Sistema", new[] { "System administration", "Administração do sistema" } },
      { "Administrador del Sistemas", new[] { "System administrator", "Administrador do sistema" } },
      { "Pozos Ya", new[] { "Pozos Ya", "Pozos Ya" } },

      // ---------------------------------------------------------
      // RECUPERACIÓN DE CONTRASEÑA
      // ---------------------------------------------------------
      { "Recuperar Contraseña", new[] { "Recover password", "Recuperar senha" } },
      { "Recuperar contraseña", new[] { "Recover password", "Recuperar senha" } },
      { "Recuperación", new[] { "Recovery", "Recuperação" } },
      { "Ingrese tu correo electrónico", new[] { "Enter your email", "Digite seu e-mail" } },
      { "Enviar Código", new[] { "Send code", "Enviar código" } },
      { "Validar Código", new[] { "Validate code", "Validar código" } },
      { "Restablecer", new[] { "Reset", "Redefinir" } },
      { "Restablecer Contraseña", new[] { "Reset password", "Redefinir senha" } },
      { "Nueva Contraseña", new[] { "New password", "Nova senha" } },
      { "Nueva contraseña", new[] { "New password", "Nova senha" } },
      { "Confirmar Contraseña", new[] { "Confirm password", "Confirmar senha" } },
      { "Confirmar contraseña", new[] { "Confirm password", "Confirmar senha" } },
      { "Debe ingresar su correo electrónico.", new[] { "You must enter your email.", "Você deve informar seu e-mail." } },
      { "No existe un usuario activo asociado a ese correo.", new[] { "There is no active user associated with that email.", "Não existe um usuário ativo associado a esse e-mail." } },
      { "Se envió un código de recuperación a su correo.", new[] { "A recovery code was sent to your email.", "Um código de recuperação foi enviado para seu e-mail." } },
      { "El código tiene una vigencia de 10 minutos.", new[] { "The code is valid for 10 minutes.", "O código é válido por 10 minutos." } },
      { "No se pudo enviar el código.", new[] { "The code could not be sent.", "Não foi possível enviar o código." } },
      { "Debe ingresar el código de recuperación.", new[] { "You must enter the recovery code.", "Você deve informar o código de recuperação." } },
      { "Debe ingresar una nueva contraseña.", new[] { "You must enter a new password.", "Você deve informar uma nova senha." } },
      { "Debe confirmar la nueva contraseña.", new[] { "You must confirm the new password.", "Você deve confirmar a nova senha." } },
      { "Las contraseñas no coinciden.", new[] { "Passwords do not match.", "As senhas não coincidem." } },
      { "No se encontró un usuario activo asociado al correo.", new[] { "No active user associated with the email was found.", "Nenhum usuário ativo associado ao e-mail foi encontrado." } },
      { "El código es incorrecto, ya fue utilizado o está vencido.", new[] { "The code is incorrect, already used or expired.", "O código está incorreto, já foi utilizado ou expirou." } },
      { "La contraseña se restableció correctamente.", new[] { "The password was reset successfully.", "A senha foi redefinida com sucesso." } },
      { "Debe ingresar una contraseña.", new[] { "You must enter a password.", "Você deve informar uma senha." } },
      { "Contraseña actualizada correctamente.", new[] { "Password updated successfully.", "Senha atualizada com sucesso." } },
      { "Cambiar Contraseña", new[] { "Change password", "Alterar senha" } },

      // ---------------------------------------------------------
      // RECLAMOS
      // ---------------------------------------------------------
      { "Reclamo", new[] { "Claim", "Reclamação" } },
      { "Nuevo Reclamo", new[] { "New claim", "Nova reclamação" } },
      { "Exportar PDF", new[] { "Export PDF", "Exportar PDF" } },
      { "Numero Reclamo", new[] { "Claim number", "Número da reclamação" } },
      { "N° Reclamo", new[] { "Claim No.", "Nº reclamação" } },
      { "Mostrando X reclamos", new[] { "Showing X claims", "Mostrando X reclamações" } },
      { "Mostrando {0} reclamos.", new[] { "Showing {0} claims.", "Mostrando {0} reclamações." } },
      { "Detalle del Reclamo", new[] { "Claim details", "Detalhes da reclamação" } },
      { "Detalle del reclamo", new[] { "Claim details", "Detalhes da reclamação" } },
      { "Detalle del reclamo #", new[] { "Claim details #", "Detalhes da reclamação #" } },
      { "Detalle del reclamo #...", new[] { "Claim details #...", "Detalhes da reclamação #..." } },
      { "Datos del reclamo", new[] { "Claim data", "Dados da reclamação" } },
      { "Detalles Generales", new[] { "General details", "Detalhes gerais" } },
      { "Estado actual", new[] { "Current status", "Status atual" } },
      { "Estado actual: ...", new[] { "Current status: ...", "Status atual: ..." } },
      { "Tipo de Daño", new[] { "Damage type", "Tipo de dano" } },
      { "Tipo de daño", new[] { "Damage type", "Tipo de dano" } },
      { "Severidad", new[] { "Severity", "Gravidade" } },
      { "Consultar DNI", new[] { "Look up ID", "Consultar DNI" } },
      { "Mapa", new[] { "Map", "Mapa" } },
      { "Ver Historial", new[] { "View history", "Ver histórico" } },
      { "Historial", new[] { "History", "Histórico" } },
      { "Historial del Reclamo", new[] { "Claim history", "Histórico da reclamação" } },
      { "Panel de acciones", new[] { "Actions panel", "Painel de ações" } },
      { "Operador de Contact Center.", new[] { "Contact Center operator.", "Operador de Contact Center." } },
      { "Reclamo registrado", new[] { "Claim registered", "Reclamação registrada" } },
      { "El reclamo se registró correctamente.", new[] { "The claim was registered successfully.", "A reclamação foi registrada com sucesso." } },
      { "Debe ingresar un DNI.", new[] { "You must enter an ID number.", "Você deve informar um DNI." } },
      { "Debe ingresar el DNI.", new[] { "You must enter the ID number.", "Você deve informar o DNI." } },
      { "Debe ingresar el nombre del vecino.", new[] { "You must enter the resident's first name.", "Você deve informar o nome do morador." } },
      { "Debe ingresar el apellido del vecino.", new[] { "You must enter the resident's last name.", "Você deve informar o sobrenome do morador." } },
      { "Debe seleccionar un barrio.", new[] { "You must select a neighborhood.", "Você deve selecionar um bairro." } },
      { "Debe ingresar una dirección.", new[] { "You must enter an address.", "Você deve informar um endereço." } },
      { "Vecino no encontrado", new[] { "Resident not found", "Morador não encontrado" } },
      { "No se encontró un vecino con ese DNI.", new[] { "No resident was found with that ID number.", "Nenhum morador foi encontrado com esse DNI." } },
      { "Puede completar sus datos para registrarlo.", new[] { "You can fill in their details to register them.", "Você pode preencher os dados para cadastrá-lo." } },
      { "No se encontró el reclamo.", new[] { "The claim was not found.", "A reclamação não foi encontrada." } },
      { "No se encontró el estado '", new[] { "Status not found: '", "Status não encontrado: '" } },
      { "No hay reclamos para exportar.", new[] { "There are no claims to export.", "Não há reclamações para exportar." } },
      { "No se pudo generar el reporte.", new[] { "The report could not be generated.", "Não foi possível gerar o relatório." } },
      { "Guardar reporte de reclamos", new[] { "Save claims report", "Salvar relatório de reclamações" } },
      { "Reporte", new[] { "Report", "Relatório" } },
      { "Archivo PDF|*.pdf", new[] { "PDF file|*.pdf", "Arquivo PDF|*.pdf" } },

      // Mapa
      { "No se encontró el archivo del perímetro de Lomas de Zamora.", new[] { "The Lomas de Zamora boundary file was not found.", "O arquivo do perímetro de Lomas de Zamora não foi encontrado." } },
      { "El archivo del perímetro no contiene geometrías.", new[] { "The boundary file contains no geometries.", "O arquivo do perímetro não contém geometrias." } },
      { "No se encontró el Partido de Lomas de Zamora en el GeoJSON.", new[] { "The Lomas de Zamora district was not found in the GeoJSON.", "O município de Lomas de Zamora não foi encontrado no GeoJSON." } },
      { "No se encontró la geometría de Lomas de Zamora.", new[] { "The Lomas de Zamora geometry was not found.", "A geometria de Lomas de Zamora não foi encontrada." } },
      { "No se encontró la geometría del perímetro.", new[] { "The boundary geometry was not found.", "A geometria do perímetro não foi encontrada." } },
      { "No se pudo cargar el perímetro de Lomas de Zamora.", new[] { "The Lomas de Zamora boundary could not be loaded.", "Não foi possível carregar o perímetro de Lomas de Zamora." } },
      { "No es posible seleccionar una ubicación.", new[] { "A location cannot be selected.", "Não é possível selecionar uma localização." } },
      { "La ubicación seleccionada se encuentra fuera", new[] { "The selected location is outside", "A localização selecionada está fora" } },
      { "del perímetro del Partido de Lomas de Zamora.", new[] { "the boundary of the Lomas de Zamora district.", "do perímetro do município de Lomas de Zamora." } },
      { "Ubicación no válida", new[] { "Invalid location", "Localização inválida" } },

      // ---------------------------------------------------------
      // VERIFICACIÓN
      // ---------------------------------------------------------
      { "VERIFICACIÓN", new[] { "VERIFICATION", "VERIFICAÇÃO" } },
      { "Verificación", new[] { "Verification", "Verificação" } },
      { "Verificar", new[] { "Verify", "Verificar" } },
      { "Evidencia", new[] { "Evidence", "Evidência" } },
      { "Abrir Evidencia", new[] { "Open evidence", "Abrir evidência" } },
      { "Seleccionar Archivo", new[] { "Select file", "Selecionar arquivo" } },
      { "Seleccionar Archivo...", new[] { "Select file...", "Selecionar arquivo..." } },
      { "Archivo Seleccionado", new[] { "Selected file", "Arquivo selecionado" } },
      { "Ningún archivo seleccionado", new[] { "No file selected", "Nenhum arquivo selecionado" } },
      { "Ningún archivo cargado", new[] { "No file uploaded", "Nenhum arquivo carregado" } },
      { "Registrar inicio de obra", new[] { "Register work start", "Registrar início da obra" } },
      { "Finalizar Obra", new[] { "Finish work", "Finalizar obra" } },
      { "Cancelar Reclamo", new[] { "Cancel claim", "Cancelar reclamação" } },
      { "Cancelar reclamo", new[] { "Cancel claim", "Cancelar reclamação" } },
      { "Seleccionar evidencia fotográfica", new[] { "Select photographic evidence", "Selecionar evidência fotográfica" } },
      { "Imágenes|*.jpg;*.jpeg;*.png", new[] { "Images|*.jpg;*.jpeg;*.png", "Imagens|*.jpg;*.jpeg;*.png" } },
      { "No tiene permisos para acceder a la verificación.", new[] { "You do not have permission to access verification.", "Você não tem permissão para acessar a verificação." } },
      { "No tiene permisos para subir evidencia.", new[] { "You do not have permission to upload evidence.", "Você não tem permissão para enviar evidências." } },
      { "El reclamo fue cancelado.", new[] { "The claim was cancelled.", "A reclamação foi cancelada." } },
      { "El reclamo fue verificado correctamente.", new[] { "The claim was verified successfully.", "A reclamação foi verificada com sucesso." } },
      { "El reclamo ya fue presupuestado.", new[] { "The claim has already been budgeted.", "A reclamação já foi orçada." } },
      { "La obra ya fue finalizada.", new[] { "The work has already been finished.", "A obra já foi finalizada." } },
      { "No hay acciones disponibles para el estado actual.", new[] { "No actions are available for the current status.", "Não há ações disponíveis para o status atual." } },
      { "No hay evidencias cargadas.", new[] { "No evidence uploaded.", "Nenhuma evidência carregada." } },
      { "La evidencia se cargó correctamente.", new[] { "The evidence was uploaded successfully.", "A evidência foi carregada com sucesso." } },
      { "Error al cargar evidencia", new[] { "Error uploading evidence", "Erro ao carregar evidência" } },
      { "Debe seleccionar una evidencia.", new[] { "You must select an evidence file.", "Você deve selecionar uma evidência." } },
      { "No se encontró el archivo de evidencia.", new[] { "The evidence file was not found.", "O arquivo de evidência não foi encontrado." } },
      { "No se pudo abrir la imagen.", new[] { "The image could not be opened.", "Não foi possível abrir a imagem." } },
      { "¿Está seguro de que desea verificar este reclamo?", new[] { "Are you sure you want to verify this claim?", "Tem certeza de que deseja verificar esta reclamação?" } },
      { "¿Está seguro de que desea registrar el inicio de la obra?", new[] { "Are you sure you want to register the start of the work?", "Tem certeza de que deseja registrar o início da obra?" } },
      { "¿Está seguro de que desea finalizar la obra?", new[] { "Are you sure you want to finish the work?", "Tem certeza de que deseja finalizar a obra?" } },
      { "¿Está seguro de que desea cancelar el reclamo?", new[] { "Are you sure you want to cancel the claim?", "Tem certeza de que deseja cancelar a reclamação?" } },
      { "No se encontró el archivo seleccionado.", new[] { "The selected file was not found.", "O arquivo selecionado não foi encontrado." } },
      { "Solo se permiten archivos JPG, JPEG o PNG.", new[] { "Only JPG, JPEG or PNG files are allowed.", "Somente arquivos JPG, JPEG ou PNG são permitidos." } },

      // ---------------------------------------------------------
      // PRESUPUESTO
      // ---------------------------------------------------------
      { "PRESUPUESTO", new[] { "BUDGET", "ORÇAMENTO" } },
      { "Presupuesto", new[] { "Budget", "Orçamento" } },
      { "Presupuesto PDF", new[] { "Budget PDF", "Orçamento PDF" } },
      { "Presupuestado", new[] { "Budgeted", "Orçado" } },
      { "Proveedor", new[] { "Supplier", "Fornecedor" } },
      { "Monto Estimado", new[] { "Estimated amount", "Valor estimado" } },
      { "Monto Final", new[] { "Final amount", "Valor final" } },
      { "Confirmar presupuesto", new[] { "Confirm budget", "Confirmar orçamento" } },
      { "Seleccionar presupuesto PDF", new[] { "Select budget PDF", "Selecionar orçamento PDF" } },
      { "No tiene permisos para acceder al presupuesto.", new[] { "You do not have permission to access the budget.", "Você não tem permissão para acessar o orçamento." } },
      { "No tiene permisos para modificar el presupuesto.", new[] { "You do not have permission to edit the budget.", "Você não tem permissão para editar o orçamento." } },
      { "No tiene permisos para confirmar el presupuesto.", new[] { "You do not have permission to confirm the budget.", "Você não tem permissão para confirmar o orçamento." } },
      { "No tiene permisos para cancelar el reclamo.", new[] { "You do not have permission to cancel the claim.", "Você não tem permissão para cancelar a reclamação." } },
      { "No tiene permisos para adjuntar el PDF.", new[] { "You do not have permission to attach the PDF.", "Você não tem permissão para anexar o PDF." } },
      { "No tiene permisos para cargar el monto.", new[] { "You do not have permission to enter the amount.", "Você não tem permissão para informar o valor." } },
      { "Solo se puede crear y confirmar un presupuesto", new[] { "A budget can only be created and confirmed", "Um orçamento só pode ser criado e confirmado" } },
      { "cuando el reclamo está en estado VERIFICADO.", new[] { "when the claim is in VERIFIED status.", "quando a reclamação está no status VERIFICADO." } },
      { "¿Está seguro de que desea confirmar el presupuesto?", new[] { "Are you sure you want to confirm the budget?", "Tem certeza de que deseja confirmar o orçamento?" } },
      { "El reclamo pasará al estado PRESUPUESTADO.", new[] { "The claim will move to BUDGETED status.", "A reclamação passará para o status ORÇADO." } },
      { "El presupuesto fue confirmado correctamente.", new[] { "The budget was confirmed successfully.", "O orçamento foi confirmado com sucesso." } },
      { "El reclamo ya está cancelado.", new[] { "The claim is already cancelled.", "A reclamação já está cancelada." } },
      { "No se puede cancelar un reclamo finalizado.", new[] { "A finished claim cannot be cancelled.", "Não é possível cancelar uma reclamação finalizada." } },
      { "Debe seleccionar un proveedor.", new[] { "You must select a supplier.", "Você deve selecionar um fornecedor." } },
      { "El monto estimado no es válido.", new[] { "The estimated amount is not valid.", "O valor estimado não é válido." } },
      { "El monto estimado debe ser mayor a cero.", new[] { "The estimated amount must be greater than zero.", "O valor estimado deve ser maior que zero." } },
      { "Debe adjuntar el presupuesto en PDF.", new[] { "You must attach the budget as a PDF.", "Você deve anexar o orçamento em PDF." } },
      { "El monto final no es válido.", new[] { "The final amount is not valid.", "O valor final não é válido." } },
      { "El monto final no puede ser negativo.", new[] { "The final amount cannot be negative.", "O valor final não pode ser negativo." } },

      // ---------------------------------------------------------
      // PRIORIZACIÓN
      // ---------------------------------------------------------
      { "Partida", new[] { "Allocation", "Dotação" } },
      { "Partida -", new[] { "Allocation -", "Dotação -" } },
      { "Partida: -", new[] { "Allocation: -", "Dotação: -" } },
      { "Partida Seleccionada", new[] { "Selected allocation", "Dotação selecionada" } },
      { "Partida presupuestaria", new[] { "Budget allocation", "Dotação orçamentária" } },
      { "- Trimestre", new[] { "- Quarter", "- Trimestre" } },
      { "Fondos", new[] { "Funds", "Fundos" } },
      { "Fondos Disponibles", new[] { "Available funds", "Fundos disponíveis" } },
      { "Fondos disponibles: $", new[] { "Available funds: $", "Fundos disponíveis: $" } },
      { "Asignado: $", new[] { "Allocated: $", "Alocado: $" } },
      { "Asignado: $0,00", new[] { "Allocated: $0.00", "Alocado: $0,00" } },
      { "Comprometido", new[] { "Committed", "Comprometido" } },
      { "Comprometido: $", new[] { "Committed: $", "Comprometido: $" } },
      { "Comprometido: $0,00", new[] { "Committed: $0.00", "Comprometido: $0,00" } },
      { "Ejecutado", new[] { "Executed", "Executado" } },
      { "Ejecutado: $", new[] { "Executed: $", "Executado: $" } },
      { "Ejecutado: $0,00", new[] { "Executed: $0.00", "Executado: $0,00" } },
      { "Disponible: $", new[] { "Available: $", "Disponível: $" } },
      { "Disponible: $0,00", new[] { "Available: $0.00", "Disponível: $0,00" } },
      { "Costo", new[] { "Cost", "Custo" } },
      { "Orden de priorización", new[] { "Priority order", "Ordem de priorização" } },
      { "↑ Subir", new[] { "↑ Move up", "↑ Subir" } },
      { "↓ Bajar", new[] { "↓ Move down", "↓ Descer" } },
      { "Procesar priorización", new[] { "Process prioritization", "Processar priorização" } },
      { "Total seleccionado: $", new[] { "Selected total: $", "Total selecionado: $" } },
      { "Total seleccionado: $0", new[] { "Selected total: $0", "Total selecionado: $0" } },
      { "(nuevo)", new[] { "(new)", "(novo)" } },
      { "No tiene permisos para acceder a la priorización.", new[] { "You do not have permission to access prioritization.", "Você não tem permissão para acessar a priorização." } },
      { "No tiene permisos para buscar reclamos.", new[] { "You do not have permission to search claims.", "Você não tem permissão para buscar reclamações." } },
      { "No tiene permisos para seleccionar reclamos.", new[] { "You do not have permission to select claims.", "Você não tem permissão para selecionar reclamações." } },
      { "No tiene permisos para ordenar la priorización.", new[] { "You do not have permission to reorder the prioritization.", "Você não tem permissão para ordenar a priorização." } },
      { "No tiene permisos para procesar la priorización.", new[] { "You do not have permission to process the prioritization.", "Você não tem permissão para processar a priorização." } },
      { "Debe seleccionar una partida presupuestaria.", new[] { "You must select a budget allocation.", "Você deve selecionar uma dotação orçamentária." } },
      { "Debe seleccionar al menos un reclamo.", new[] { "You must select at least one claim.", "Você deve selecionar pelo menos uma reclamação." } },
      { "No se encontró uno de los reclamos seleccionados.", new[] { "One of the selected claims was not found.", "Uma das reclamações selecionadas não foi encontrada." } },
      { "El reclamo", new[] { "Claim", "A reclamação" } },
      { "ya no está en estado PRESUPUESTADO.", new[] { "is no longer in BUDGETED status.", "não está mais no status ORÇADO." } },
      { "Los fondos disponibles no son suficientes.", new[] { "Available funds are not sufficient.", "Os fundos disponíveis não são suficientes." } },
      { "Saldo insuficiente", new[] { "Insufficient balance", "Saldo insuficiente" } },
      { "¿Está seguro de procesar la priorización?", new[] { "Are you sure you want to process the prioritization?", "Tem certeza de que deseja processar a priorização?" } },
      { "Reclamos nuevos:", new[] { "New claims:", "Novas reclamações:" } },
      { "Reclamos ya asignados (se actualiza su orden):", new[] { "Already assigned claims (order will be updated):", "Reclamações já atribuídas (a ordem será atualizada):" } },
      { "Monto total nuevos: $", new[] { "Total new amount: $", "Valor total novas: $" } },
      { "Confirmar priorización", new[] { "Confirm prioritization", "Confirmar priorização" } },
      { "La priorización fue procesada correctamente.", new[] { "The prioritization was processed successfully.", "A priorização foi processada com sucesso." } },
      { "Ocurrió un error al procesar la priorización.", new[] { "An error occurred while processing the prioritization.", "Ocorreu um erro ao processar a priorização." } },

      // ---------------------------------------------------------
      // PARTIDAS PRESUPUESTARIAS
      // ---------------------------------------------------------
      { "Detalle Partida", new[] { "Allocation details", "Detalhes da dotação" } },
      { "Nueva Partida Presupuestaria", new[] { "New budget allocation", "Nova dotação orçamentária" } },
      { "Año", new[] { "Year", "Ano" } },
      { "Trimestre", new[] { "Quarter", "Trimestre" } },
      { "Monto Asignado", new[] { "Allocated amount", "Valor alocado" } },
      { "Monto Comprometido", new[] { "Committed amount", "Valor comprometido" } },
      { "Monto Ejecutado", new[] { "Executed amount", "Valor executado" } },
      { "Fecha de Asignación", new[] { "Allocation date", "Data de alocação" } },
      { "Gerente", new[] { "Manager", "Gerente" } },
      { "Se asignará automáticamente", new[] { "Will be assigned automatically", "Será atribuída automaticamente" } },
      { "Mostrando {0} partidas.", new[] { "Showing {0} allocations.", "Mostrando {0} dotações." } },
      { "No tiene permisos para crear partidas presupuestarias.", new[] { "You do not have permission to create budget allocations.", "Você não tem permissão para criar dotações orçamentárias." } },
      { "No tiene permisos para acceder a esta partida presupuestaria.", new[] { "You do not have permission to access this budget allocation.", "Você não tem permissão para acessar esta dotação orçamentária." } },
      { "No tiene permisos para modificar partidas presupuestarias.", new[] { "You do not have permission to edit budget allocations.", "Você não tem permissão para editar dotações orçamentárias." } },
      { "No se encontró la partida presupuestaria.", new[] { "The budget allocation was not found.", "A dotação orçamentária não foi encontrada." } },
      { "Debe ingresar un año válido.", new[] { "You must enter a valid year.", "Você deve informar um ano válido." } },
      { "El año ingresado no es válido.", new[] { "The year entered is not valid.", "O ano informado não é válido." } },
      { "Debe seleccionar un trimestre.", new[] { "You must select a quarter.", "Você deve selecionar um trimestre." } },
      { "El monto asignado no es válido.", new[] { "The allocated amount is not valid.", "O valor alocado não é válido." } },
      { "El monto asignado no puede ser negativo.", new[] { "The allocated amount cannot be negative.", "O valor alocado não pode ser negativo." } },
      { "El monto comprometido no es válido.", new[] { "The committed amount is not valid.", "O valor comprometido não é válido." } },
      { "El monto ejecutado no es válido.", new[] { "The executed amount is not valid.", "O valor executado não é válido." } },
      { "El monto ejecutado no puede ser mayor al comprometido.", new[] { "The executed amount cannot exceed the committed amount.", "O valor executado não pode ser maior que o comprometido." } },
      { "El monto comprometido no puede ser mayor al monto asignado.", new[] { "The committed amount cannot exceed the allocated amount.", "O valor comprometido não pode ser maior que o valor alocado." } },
      { "Debe seleccionar un gerente.", new[] { "You must select a manager.", "Você deve selecionar um gerente." } },
      { "La partida se guardó correctamente.", new[] { "The allocation was saved successfully.", "A dotação foi salva com sucesso." } },

      // ---------------------------------------------------------
      // PROVEEDORES
      // ---------------------------------------------------------
      { "Detalle Proveedor", new[] { "Supplier details", "Detalhes do fornecedor" } },
      { "Detalle del Proveedor", new[] { "Supplier details", "Detalhes do fornecedor" } },
      { "Nuevo Proveedor", new[] { "New supplier", "Novo fornecedor" } },
      { "Mostrando {0} proveedores.", new[] { "Showing {0} suppliers.", "Mostrando {0} fornecedores." } },
      { "No tiene permisos para acceder a proveedores.", new[] { "You do not have permission to access suppliers.", "Você não tem permissão para acessar fornecedores." } },
      { "No posee permisos para crear proveedores.", new[] { "You do not have permission to create suppliers.", "Você não tem permissão para criar fornecedores." } },
      { "No posee permisos para editar proveedores.", new[] { "You do not have permission to edit suppliers.", "Você não tem permissão para editar fornecedores." } },
      { "No posee permisos para guardar proveedores.", new[] { "You do not have permission to save suppliers.", "Você não tem permissão para salvar fornecedores." } },
      { "No se encontró el proveedor.", new[] { "The supplier was not found.", "O fornecedor não foi encontrado." } },
      { "Debe ingresar el nombre del proveedor.", new[] { "You must enter the supplier name.", "Você deve informar o nome do fornecedor." } },
      { "Debe ingresar el CUIT.", new[] { "You must enter the tax ID (CUIT).", "Você deve informar o CUIT." } },
      { "El proveedor se guardó correctamente.", new[] { "The supplier was saved successfully.", "O fornecedor foi salvo com sucesso." } },

      // ---------------------------------------------------------
      // CONFIGURACIÓN
      // ---------------------------------------------------------
      { "No tiene permisos para acceder a configuración.", new[] { "You do not have permission to access settings.", "Você não tem permissão para acessar as configurações." } },
      { "No tiene permisos para modificar la configuración.", new[] { "You do not have permission to edit settings.", "Você não tem permissão para editar as configurações." } },
      { "No tiene permisos para guardar la configuración.", new[] { "You do not have permission to save settings.", "Você não tem permissão para salvar as configurações." } },
      { "Debe ingresar el nombre de la municipalidad.", new[] { "You must enter the municipality name.", "Você deve informar o nome do município." } },
      { "Debe ingresar la dirección.", new[] { "You must enter the address.", "Você deve informar o endereço." } },
      { "Debe ingresar el teléfono.", new[] { "You must enter the phone number.", "Você deve informar o telefone." } },
      { "Debe ingresar el correo electrónico.", new[] { "You must enter the email.", "Você deve informar o e-mail." } },
      { "La configuración se guardó correctamente.", new[] { "Settings were saved successfully.", "As configurações foram salvas com sucesso." } },

      // ---------------------------------------------------------
      // USUARIOS Y PERMISOS
      // ---------------------------------------------------------
      { "Gestión de Usuarios", new[] { "User management", "Gestão de usuários" } },
      { "Gestón de Usuarios", new[] { "User management", "Gestão de usuários" } },
      { "Administra los usurios del sistemas, sus roles y permisos individuales.", new[] { "Manage system users, their roles and individual permissions.", "Gerencia os usuários do sistema, seus perfis e permissões individuais." } },
      { "Detalle de Usuario", new[] { "User details", "Detalhes do usuário" } },
      { "Detalle del Usuario", new[] { "User details", "Detalhes do usuário" } },
      { "Nuevo Usuario", new[] { "New user", "Novo usuário" } },
      { "Nuevo usuario", new[] { "New user", "Novo usuário" } },
      { "Roles Asignados", new[] { "Assigned roles", "Perfis atribuídos" } },
      { "Administrar Permisos Individuales", new[] { "Manage individual permissions", "Gerenciar permissões individuais" } },
      { "Permisos por Usuario", new[] { "Permissions by user", "Permissões por usuário" } },
      { "Estimador", new[] { "Estimator", "Orçamentista" } },
      { "Priorizador", new[] { "Prioritizer", "Priorizador" } },
      { "Verificador", new[] { "Verifier", "Verificador" } },
      { "No posee permisos para crear usuarios.", new[] { "You do not have permission to create users.", "Você não tem permissão para criar usuários." } },
      { "No posee permisos para editar usuarios.", new[] { "You do not have permission to edit users.", "Você não tem permissão para editar usuários." } },
      { "No se encontró el usuario.", new[] { "The user was not found.", "O usuário não foi encontrado." } },
      { "No tiene permisos para cambiar contraseñas.", new[] { "You do not have permission to change passwords.", "Você não tem permissão para alterar senhas." } },
      { "No tiene permisos para administrar permisos individuales.", new[] { "You do not have permission to manage individual permissions.", "Você não tem permissão para gerenciar permissões individuais." } },
      { "No posee permisos para administrar permisos individuales.", new[] { "You do not have permission to manage individual permissions.", "Você não tem permissão para gerenciar permissões individuais." } },
      { "Permisos guardados correctamente.", new[] { "Permissions saved successfully.", "Permissões salvas com sucesso." } },

      // ---------------------------------------------------------
      // ADMINISTRACIÓN (BACKUP)
      // ---------------------------------------------------------
      { "FormAdministrador", new[] { "System administration", "Administração do sistema" } },
      { "Administración de Respaldo e Integridad", new[] { "Backup and integrity management", "Administração de backup e integridade" } },
      { "Carpeta de BackUp", new[] { "Backup folder", "Pasta de backup" } },
      { "Realizar Backup", new[] { "Create backup", "Fazer backup" } },
      { "Restaurar", new[] { "Restore", "Restaurar" } },
      { "Verificar Integridad", new[] { "Check integrity", "Verificar integridade" } },
      { "Confirmar Backup", new[] { "Confirm backup", "Confirmar backup" } },
      { "Confirmar Restauración", new[] { "Confirm restore", "Confirmar restauração" } },
      { "Error en Servidor", new[] { "Server error", "Erro no servidor" } },
      { "Error al cargar archivos:", new[] { "Error loading files:", "Erro ao carregar arquivos:" } },
      { "La carpeta de copias de seguridad no existe.", new[] { "The backup folder does not exist.", "A pasta de backups não existe." } },
      { "Por favor, seleccione un archivo de la lista.", new[] { "Please select a file from the list.", "Por favor, selecione um arquivo da lista." } },
      { "¿Está seguro de que desea restaurar el archivo '{0}'?\nSe sobrescribirán los datos actuales.", new[] { "Are you sure you want to restore the file '{0}'?\nCurrent data will be overwritten.", "Tem certeza de que deseja restaurar o arquivo '{0}'?\nOs dados atuais serão substituídos." } },
      { "La base de datos se ha restaurado correctamente.", new[] { "The database was restored successfully.", "O banco de dados foi restaurado com sucesso." } },
      { "Ocurrió un error al restaurar la base de datos", new[] { "An error occurred while restoring the database", "Ocorreu um erro ao restaurar o banco de dados" } },
      { "¿Está seguro de que desea generar archivo de backup?", new[] { "Are you sure you want to create a backup file?", "Tem certeza de que deseja gerar um arquivo de backup?" } },
      { "Se ha generado el archivo exitosamente.", new[] { "The file was generated successfully.", "O arquivo foi gerado com sucesso." } },
      { "Ocurrió un error al generar el backup de la base de datos.", new[] { "An error occurred while creating the database backup.", "Ocorreu um erro ao gerar o backup do banco de dados." } },
      { "¿Está seguro de que desea verificar la integridad de datos?", new[] { "Are you sure you want to check data integrity?", "Tem certeza de que deseja verificar a integridade dos dados?" } },
      { "Se ha validado la integridad de datos exitosamente.", new[] { "Data integrity was validated successfully.", "A integridade dos dados foi validada com sucesso." } },
      { "Ocurrió un error en la integridad de datos.", new[] { "A data integrity error occurred.", "Ocorreu um erro na integridade dos dados." } },

      // ---------------------------------------------------------
      // VALIDACIONES AGREGADAS
      // ---------------------------------------------------------
      { "Estado Nuevo", new[] { "New status", "Novo status" } },
      { "Debe seleccionar un tipo de daño.", new[] { "You must select a damage type.", "Você deve selecionar um tipo de dano." } },
      { "Debe seleccionar una severidad.", new[] { "You must select a severity.", "Você deve selecionar uma gravidade." } },
      { "Debe ingresar una descripción del reclamo.", new[] { "You must enter a description of the claim.", "Você deve informar uma descrição da reclamação." } },
      { "Debe marcar la ubicación del reclamo en el mapa.", new[] { "You must mark the claim location on the map.", "Você deve marcar a localização da reclamação no mapa." } },
      { "Ya existe una partida presupuestaria para ese año y trimestre.", new[] { "A budget allocation already exists for that year and quarter.", "Já existe uma dotação orçamentária para esse ano e trimestre." } },
      { "Ya existe un proveedor con ese CUIT.", new[] { "A supplier with that tax ID (CUIT) already exists.", "Já existe um fornecedor com esse CUIT." } },
      { "Debe ingresar el nombre.", new[] { "You must enter the first name.", "Você deve informar o nome." } },
      { "Debe ingresar el apellido.", new[] { "You must enter the last name.", "Você deve informar o sobrenome." } },
      { "Debe ingresar el nombre de usuario.", new[] { "You must enter the username.", "Você deve informar o nome de usuário." } },
      { "Ya existe un usuario con ese nombre de usuario.", new[] { "A user with that username already exists.", "Já existe um usuário com esse nome de usuário." } },
      { "Ya existe un usuario con ese correo electrónico.", new[] { "A user with that email already exists.", "Já existe um usuário com esse e-mail." } },
      { "No se pudo guardar el presupuesto.", new[] { "The budget could not be saved.", "Não foi possível salvar o orçamento." } },
      { "Solo se permiten archivos PDF.", new[] { "Only PDF files are allowed.", "Somente arquivos PDF são permitidos." } },
      { "Windows no permite leer la carpeta de backups con este usuario. Los backups se generan igual, pero no se pueden listar desde acá.", new[] { "Windows does not allow this user to read the backup folder. Backups are still created, but they cannot be listed here.", "O Windows não permite que este usuário leia a pasta de backups. Os backups são gerados mesmo assim, mas não podem ser listados aqui." } },

      // ---------------------------------------------------------
      // CONFIGURAR BASE DE DATOS
      // ---------------------------------------------------------
      { "Configurar base de datos", new[] { "Database setup", "Configurar banco de dados" } },
      { "Pozos Ya - Base de datos", new[] { "Pozos Ya - Database", "Pozos Ya - Banco de dados" } },
      { "Base de datos", new[] { "Database", "Banco de dados" } },
      { "No se encontró la base de datos.", new[] { "The database was not found.", "O banco de dados não foi encontrado." } },
      { "Servidor SQL", new[] { "SQL Server", "Servidor SQL" } },
      { "Ejemplos: localhost, .\\SQLEXPRESS", new[] { "Examples: localhost, .\\SQLEXPRESS", "Exemplos: localhost, .\\SQLEXPRESS" } },
      { "Crear base de demo", new[] { "Create demo database", "Criar banco de demonstração" } },
      { "Crea la base con datos de ejemplo. Funciona en cualquier versión de SQL Server (2016 o superior).", new[] { "Creates the database with sample data. Works with any SQL Server version (2016 or later).", "Cria o banco com dados de exemplo. Funciona em qualquer versão do SQL Server (2016 ou superior)." } },
      { "Restaurar desde .bak", new[] { "Restore from .bak", "Restaurar de .bak" } },
      { "Solo backups hechos en la misma versión de SQL Server o en una anterior.", new[] { "Only backups made with the same or an older SQL Server version.", "Somente backups feitos na mesma versão do SQL Server ou em uma anterior." } },
      { "Salir", new[] { "Exit", "Sair" } },
      { "La base de datos PozosYa ya existe en el servidor '{0}'. Se va a usar esa.", new[] { "The PozosYa database already exists on server '{0}'. It will be used.", "O banco PozosYa já existe no servidor '{0}'. Ele será usado." } },
      { "No se encontró la base de datos PozosYa en el servidor '{0}'. Creala con datos de demo o restaurá un backup.", new[] { "The PozosYa database was not found on server '{0}'. Create it with demo data or restore a backup.", "O banco PozosYa não foi encontrado no servidor '{0}'. Crie-o com dados de demonstração ou restaure um backup." } },
      { "No se pudo conectar al servidor '{0}'. Revisá el nombre del servidor.", new[] { "Could not connect to server '{0}'. Check the server name.", "Não foi possível conectar ao servidor '{0}'. Verifique o nome do servidor." } },
      { "La base de datos de demo quedó lista.", new[] { "The demo database is ready.", "O banco de demonstração está pronto." } },
      { "Usuario: admin", new[] { "User: admin", "Usuário: admin" } },
      { "Contraseña: admin123", new[] { "Password: admin123", "Senha: admin123" } },
      { "Seleccionar backup de la base de datos", new[] { "Select database backup", "Selecionar backup do banco de dados" } },
      { "Backup de SQL Server|*.bak", new[] { "SQL Server backup|*.bak", "Backup do SQL Server|*.bak" } },
      { "La base de datos se restauró correctamente.", new[] { "The database was restored successfully.", "O banco de dados foi restaurado com sucesso." } },
      { "Procesando, puede tardar unos segundos...", new[] { "Processing, this may take a few seconds...", "Processando, pode levar alguns segundos..." } },
      { "El backup fue hecho con una versión más nueva de SQL Server y no se puede restaurar en este servidor. Usá \"Crear base de demo\".", new[] { "The backup was made with a newer SQL Server version and cannot be restored on this server. Use \"Create demo database\".", "O backup foi feito com uma versão mais nova do SQL Server e não pode ser restaurado neste servidor. Use \"Criar banco de demonstração\"." } },
      { "No se pudo preparar la base de datos.", new[] { "The database could not be prepared.", "Não foi possível preparar o banco de dados." } },
      { "No se encontró el script de la base de demo.", new[] { "The demo database script was not found.", "O script do banco de demonstração não foi encontrado." } },

      // ---------------------------------------------------------
      // REPORTE PDF
      // ---------------------------------------------------------
      { "Reporte de reclamos", new[] { "Claims report", "Relatório de reclamações" } },
      { "Generado", new[] { "Generated", "Gerado" } },
      { "Total de reclamos", new[] { "Total claims", "Total de reclamações" } },
      { "Página", new[] { "Page", "Página" } },
      { "de", new[] { "of", "de" } }
    };
  }
}

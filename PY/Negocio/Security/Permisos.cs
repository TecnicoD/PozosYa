using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Security
{
  public static class Permisos
  {
    // Seguridad
    public const string SEGURIDAD_LOGIN = "SEGURIDAD_LOGIN";
    public const string SEGURIDAD_RECUPERAR_ACCESO = "SEGURIDAD_RECUPERAR_ACCESO";
    public const string SEGURIDAD_CERRAR_SESION = "SEGURIDAD_CERRAR_SESION";
    public const string SEGURIDAD_VER = "SEGURIDAD_VER";

    // Reclamos
    public const string RECLAMOS_VER = "RECLAMOS_VER";
    public const string RECLAMOS_CREAR = "RECLAMOS_CREAR";
    public const string RECLAMOS_BUSCAR = "RECLAMOS_BUSCAR";
    public const string RECLAMOS_VER_DETALLE = "RECLAMOS_VER_DETALLE";
    public const string RECLAMOS_EDITAR = "RECLAMOS_EDITAR";
    public const string RECLAMOS_GUARDAR = "RECLAMOS_GUARDAR";
    public const string RECLAMOS_CANCELAR = "RECLAMOS_CANCELAR";
    public const string RECLAMOS_VER_HISTORIAL = "RECLAMOS_VER_HISTORIAL";

    // Nuevos Reclamos
    public const string NUEVOS_RECLAMOS_GUARDAR = "NUEVOS_RECLAMOS_GUARDAR";
    public const string NUEVOS_RECLAMOS_CANCELAR = "NUEVOS_RECLAMOS_CANCELAR";

    // Priorización
    public const string PRIORIZACION_VER = "PRIORIZACION_VER";
    public const string PRIORIZACION_BUSCAR = "PRIORIZACION_BUSCAR";
    public const string PRIORIZACION_SELECCIONAR_TRIMESTRE = "PRIORIZACION_SELECCIONAR_TRIMESTRE";
    public const string PRIORIZACION_SELECCIONAR_RECLAMOS = "PRIORIZACION_SELECCIONAR_RECLAMOS";
    public const string PRIORIZACION_ORDENAR = "PRIORIZACION_ORDENAR";
    public const string PRIORIZACION_PROCESAR = "PRIORIZACION_PROCESAR";

    // Partidas Presupuestarias
    public const string PARTIDAS_PRESUPUESTARIAS_VER = "PARTIDAS_PRESUPUESTARIAS_VER";
    public const string PARTIDAS_PRESUPUESTARIAS_CREAR = "PARTIDAS_PRESUPUESTARIAS_CREAR";
    public const string PARTIDAS_PRESUPUESTARIAS_GUARDAR = "PARTIDAS_PRESUPUESTARIAS_GUARDAR";

    // Proveedores
    public const string PROVEEDORES_VER = "PROVEEDORES_VER";
    public const string PROVEEDORES_BUSCAR = "PROVEEDORES_BUSCAR";
    public const string PROVEEDORES_CREAR = "PROVEEDORES_CREAR";
    public const string PROVEEDORES_EDITAR = "PROVEEDORES_EDITAR";
    public const string PROVEEDORES_GUARDAR = "PROVEEDORES_GUARDAR";

    // Configuración
    public const string CONFIGURACION_VER = "CONFIGURACION_VER";
    public const string CONFIGURACION_BUSCAR = "CONFIGURACION_BUSCAR";
    public const string CONFIGURACION_CREAR = "CONFIGURACION_CREAR";
    public const string CONFIGURACION_EDITAR = "CONFIGURACION_EDITAR";
    public const string CONFIGURACION_GUARDAR = "CONFIGURACION_GUARDAR";

    // Usuarios
    public const string USUARIOS_VER = "USUARIOS_VER";
    public const string USUARIOS_BUSCAR = "USUARIOS_BUSCAR";
    public const string USUARIOS_CREAR = "USUARIOS_CREAR";
    public const string USUARIOS_EDITAR = "USUARIOS_EDITAR";
    public const string USUARIOS_GUARDAR = "USUARIOS_GUARDAR";
    public const string USUARIOS_CAMBIAR_CONTRASENA = "USUARIOS_CAMBIAR_CONTRASENA";
    public const string USUARIOS_ASIGNAR_ROLES = "USUARIOS_ASIGNAR_ROLES";
    public const string USUARIOS_QUITAR_ROLES = "USUARIOS_QUITAR_ROLES";
    public const string USUARIOS_ADMINISTRAR_PERMISOS = "USUARIOS_ADMINISTRAR_PERMISOS";
    public const string USUARIOS_OTORGAR_PERMISO = "USUARIOS_OTORGAR_PERMISO";
    public const string USUARIOS_REVOCAR_PERMISO = "USUARIOS_REVOCAR_PERMISO";

    // Verificación
    public const string VERIFICACION_VER = "VERIFICACION_VER";
    public const string VERIFICACION_SUBIR_EVIDENCIA = "VERIFICACION_SUBIR_EVIDENCIA";
    public const string VERIFICACION_MARCAR_INICIADO = "VERIFICACION_MARCAR_INICIADO";
    public const string VERIFICACION_MARCAR_EN_EJECUCION = "VERIFICACION_MARCAR_EN_EJECUCION";
    public const string VERIFICACION_MARCAR_FINALIZADO = "VERIFICACION_MARCAR_FINALIZADO";
    public const string VERIFICACION_CANCELAR_RECLAMO = "VERIFICACION_CANCELAR_RECLAMO";

    // Presupuesto
    public const string PRESUPUESTO_VER = "PRESUPUESTO_VER";
    public const string PRESUPUESTO_MODIFICAR = "PRESUPUESTO_MODIFICAR";
    public const string PRESUPUESTO_GUARDAR = "PRESUPUESTO_GUARDAR";
    public const string PRESUPUESTO_SELECCIONAR_PROVEEDOR = "PRESUPUESTO_SELECCIONAR_PROVEEDOR";
    public const string PRESUPUESTO_CARGAR_MONTO = "PRESUPUESTO_CARGAR_MONTO";
    public const string PRESUPUESTO_ADJUNTAR_ARCHIVO = "PRESUPUESTO_ADJUNTAR_ARCHIVO";
    public const string PRESUPUESTO_CONFIRMAR = "PRESUPUESTO_CONFIRMAR";
    public const string PRESUPUESTO_CANCELAR_RECLAMO = "PRESUPUESTO_CANCELAR_RECLAMO";

    // Logs
    public const string LOGS_VER = "LOGS_VER";
    public const string LOGS_BUSCAR = "LOGS_BUSCAR";
    public const string LOGS_VER_HISTORIAL = "LOGS_VER_HISTORIAL";
  }
}

/*
=========================================================
                POZOS YA - BASE DE DEMOSTRACIÓN
=========================================================
Crea la base PozosYa con datos de ejemplo.
Compatible con SQL Server 2016 o superior.

La aplicación ejecuta este script automáticamente cuando
no encuentra la base ("Crear base de demo"). También se
puede ejecutar a mano desde SQL Server Management Studio.

Usuarios (contraseña de todos: admin123):
  admin, operador, verificador, estimador, priorizador, gerente
*/

IF DB_ID('PozosYa') IS NULL
    CREATE DATABASE PozosYa;
GO

USE PozosYa;
GO

-- CONFIGURACIONES

CREATE TABLE configuraciones(
	configuracion_id INT IDENTITY(1,1) PRIMARY KEY,
	clave VARCHAR(50) NOT NULL,
	valor VARCHAR(100) NOT NULL,
	descripcion VARCHAR(255),
	tipo VARCHAR(50) NOT NULL
);



-- PERMISOS

CREATE TABLE permisos(
	permiso_id INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR(100) NOT NULL,
	descripcion VARCHAR(255)
);




-- ROLES

CREATE TABLE roles(
	rol_id INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR(100) NOT NULL,
	descripcion VARCHAR(255),
	activo BIT NOT NULL DEFAULT(1)
);



-- USUARIOS

CREATE TABLE usuarios(
	usuario_id INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR(100) NOT NULL,
	apellido VARCHAR(100) NOT NULL,
	username VARCHAR(50) NOT NULL UNIQUE,
	email VARCHAR(150) NOT NULL UNIQUE,
	password_hash VARCHAR(255) NOT NULL,
	activo BIT NOT NULL DEFAULT(1),
	ultimo_login DATETIME2,
	fecha_creacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	digito_verificador VARCHAR(100)
);



-- VECINOS

CREATE TABLE vecinos(
	vecino_id INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR(100) NOT NULL,
	apellido VARCHAR(100) NOT NULL,
	dni VARCHAR(20) NOT NULL UNIQUE,
	telefono VARCHAR(30),
	mail VARCHAR(150),
	direccion VARCHAR(255),
	nro_catastral VARCHAR(100),
	barrio_id INT NOT NULL,
	fecha_registro DATETIME2 NOT NULL DEFAULT(GETDATE()),
	activo BIT NOT NULL DEFAULT(1),
	digito_verificador VARCHAR(100)
);



-- PROVEEDORES

CREATE TABLE proveedores(
	proveedor_id INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR(150) NOT NULL,
	cuit VARCHAR(20) NOT NULL UNIQUE,
	telefono VARCHAR(30),
	mail VARCHAR(150),
	direccion VARCHAR(255),
	activo BIT NOT NULL DEFAULT(1)
);



-- DETALLE CLAVE VALOR

CREATE TABLE detalleClaveValor(
	detalle_id INT IDENTITY(1,1) PRIMARY KEY,
	tabla VARCHAR(50) NOT NULL,
	clave VARCHAR(100) NOT NULL,
	valor VARCHAR(100) NOT NULL
);


-- ROLES PERMISOS

CREATE TABLE roles_permisos(
	rol_permiso_id INT IDENTITY(1,1) PRIMARY KEY,
	rol_id INT NOT NULL,
	permiso_id INT NOT NULL,

	FOREIGN KEY (rol_id)
	REFERENCES roles(rol_id),

	FOREIGN KEY (permiso_id)
	REFERENCES permisos(permiso_id)
);



-- USUARIOS ROLES

CREATE TABLE usuario_roles(
	usuario_rol_id INT IDENTITY(1,1) PRIMARY KEY,
	usuario_id INT NOT NULL,
	rol_id INT NOT NULL,

	FOREIGN KEY (usuario_id)
	REFERENCES usuarios(usuario_id),

	FOREIGN KEY (rol_id)
	REFERENCES roles(rol_id)
);



-- USUARIOS PERMISOS

CREATE TABLE usuarios_permisos(
	usuario_permiso_id INT IDENTITY(1,1) PRIMARY KEY,
	usuario_id INT NOT NULL,
	permiso_id INT NOT NULL,
	concedido BIT NOT NULL,

	FOREIGN KEY (usuario_id)
	REFERENCES usuarios(usuario_id),

	FOREIGN KEY (permiso_id)
	REFERENCES permisos(permiso_id)
);



-- TOKENS RECUPARECION

CREATE TABLE tokens_recuperacion(
	token_recuperacion_id INT IDENTITY(1,1) PRIMARY KEY,
	usuario_id INT NOT NULL,
	token VARCHAR(255) NOT NULL,
	fecha_generacion DATETIME2 NOT NULL,
	usado BIT NOT NULL DEFAULT(0),
	fecha_expiracion DATETIME2 NOT NULL,

	FOREIGN KEY (usuario_id)
	REFERENCES usuarios(usuario_id)
);



-- LOGS SISTEMA

CREATE TABLE logs_sistema(
	logs_sistema_id INT IDENTITY(1,1) PRIMARY KEY,
	usuario_id INT NOT NULL,
	accion VARCHAR(100) NOT NULL,
	tabla_afectada VARCHAR(100) NOT NULL,
	registro_id INT NOT NULL,
	fecha DATETIME2 NOT NULL DEFAULT(GETDATE()),
	descripcion VARCHAR(255),

	FOREIGN KEY (usuario_id)
	REFERENCES usuarios(usuario_id)
);



--PARTIDAS PRESUPUESTARIAS

CREATE TABLE partidas_presupuestarias(
	partida_presupuestaria_id INT IDENTITY(1,1) PRIMARY KEY,
	anio INT NOT NULL,
	trimestre INT NOT NULL,
	monto_asignado DECIMAL(18,2) NOT NULL,
	monto_comprometido DECIMAL(18,2) NOT NULL DEFAULT(0),
	monto_ejecutado DECIMAL(18,2) NOT NULL DEFAULT(0),
	usuario_gerente_id INT NOT NULL,
	fecha_asignacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	observaciones VARCHAR(255),
	activo BIT NOT NULL DEFAULT(1),

	FOREIGN KEY (usuario_gerente_id)
	REFERENCES usuarios(usuario_id)
);



-- RECLAMOS

CREATE TABLE reclamos(
	reclamo_id INT IDENTITY(1,1) PRIMARY KEY,
	numero_reclamo VARCHAR(30) NOT NULL UNIQUE,
	vecino_id INT NOT NULL,
	tipo_danio_id INT NOT NULL,
	severidad_id INT NOT NULL,
	estado_actual_id INT NOT NULL,
	descripcion VARCHAR(500) NOT NULL,
	direccion VARCHAR(255) NOT NULL,
	latitud DECIMAL(10,8),
	longitud DECIMAL(11,8),
	barrio_id INT NOT NULL,
	fecha_creacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	usuario_creador_id INT NOT NULL,
	fecha_cierre DATETIME2,
	usuario_modificador_id INT,
	fecha_modificacion DATETIME2,
	digito_verificador VARCHAR(100),

	FOREIGN KEY (vecino_id)
	REFERENCES vecinos(vecino_id),

	FOREIGN KEY (tipo_danio_id)
	REFERENCES detalleClaveValor(detalle_id),

	FOREIGN KEY (severidad_id)
	REFERENCES detalleClaveValor(detalle_id),

	FOREIGN KEY (estado_actual_id)
	REFERENCES detalleClaveValor(detalle_id),

	FOREIGN KEY (barrio_id)
	REFERENCES detalleClaveValor(detalle_id),

	FOREIGN KEY (usuario_creador_id)
	REFERENCES usuarios(usuario_id),

	FOREIGN KEY (usuario_modificador_id)
	REFERENCES usuarios(usuario_id)
);



-- HISTORIAL RECLAMOS

CREATE TABLE historial_reclamo(
	historial_reclamo_id INT IDENTITY(1,1) PRIMARY KEY,
	reclamo_id INT NOT NULL,
	estado_anterior_id INT NOT NULL,
	usuario_id INT NOT NULL,
	fecha DATETIME2 NOT NULL DEFAULT(GETDATE()),
	usuario_modificador INT,
	fecha_modificacion DATETIME2,

	FOREIGN KEY (reclamo_id)
	REFERENCES reclamos(reclamo_id),

	FOREIGN KEY (estado_anterior_id)
	REFERENCES detalleClaveValor(detalle_id),

	FOREIGN KEY (usuario_id)
	REFERENCES usuarios(usuario_id),

	FOREIGN KEY (usuario_modificador)
	REFERENCES usuarios(usuario_id)
);



-- ARCHIVOS

CREATE TABLE archivos(
	archivo_id INT IDENTITY(1,1) PRIMARY KEY,
	reclamo_id INT NOT NULL,
	tipo_archivo VARCHAR(50) NOT NULL,
	ubicacion VARCHAR(255) NOT NULL,
	nombre_original VARCHAR(255) NOT NULL,
	tamanio BIGINT NOT NULL,
	fecha_carga DATETIME2 NOT NULL DEFAULT(GETDATE()),
	usuario_id INT NOT NULL,
	activo BIT NOT NULL DEFAULT(1),

	FOREIGN KEY (reclamo_id)
	REFERENCES reclamos(reclamo_id),

	FOREIGN KEY (usuario_id)
	REFERENCES usuarios(usuario_id)
);



-- PRESUPUESTOS

CREATE TABLE presupuestos(
	presupuesto_id INT IDENTITY(1,1) PRIMARY KEY,
	reclamo_id INT NOT NULL,
	proveedor_id INT NOT NULL,
	monto_estimado DECIMAL(18,2) NOT NULL,
	monto_final DECIMAL(18,2),
	archivo_pdf VARCHAR(255),
	fecha_carga DATETIME2 NOT NULL DEFAULT(GETDATE()),
	usuario_id INT NOT NULL,
	digito_verificador VARCHAR(100),

	FOREIGN KEY (reclamo_id)
	REFERENCES reclamos(reclamo_id),

	FOREIGN KEY (proveedor_id)
	REFERENCES proveedores(proveedor_id),

	FOREIGN KEY (usuario_id)
	REFERENCES usuarios(usuario_id)
);



-- RECLAMOS_PARTIDA

CREATE TABLE reclamos_partida(
	reclamos_partida_id INT IDENTITY(1,1) PRIMARY KEY,
	reclamo_id INT NOT NULL,
	partida_id INT NOT NULL,
	monto_asignado_reclamo DECIMAL(18,2) NOT NULL,
	fecha_asignacion DATETIME2 NOT NULL DEFAULT(GETDATE()),
	usuario_priorizador_id INT NOT NULL,
	numero_prioridad INT NOT NULL,

	FOREIGN KEY (reclamo_id)
	REFERENCES reclamos(reclamo_id),

	FOREIGN KEY (partida_id)
	REFERENCES partidas_presupuestarias(partida_presupuestaria_id),

	FOREIGN KEY (usuario_priorizador_id)
	REFERENCES usuarios(usuario_id)
);
GO

-- =====================================================
-- PROCEDIMIENTOS Y TRIGGER
-- =====================================================

CREATE PROCEDURE [dbo].[sp_GenerarDigitoVerificadorUniversal]
    @NombreTabla VARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CampoPK VARCHAR(128);
    DECLARE @ListaCamposConcatenar VARCHAR(MAX);
    DECLARE @SqlDinamico NVARCHAR(MAX);
    DECLARE @SemillaGuardada VARCHAR(100);

    -- 1. Obtener la semilla del sistema desde la tabla central de configuraciones
    SELECT @SemillaGuardada = [valor] 
    FROM [dbo].[configuraciones] 
    WHERE [clave] = 'semilla_hash';

    IF @SemillaGuardada IS NULL
    BEGIN
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        RAISERROR('Error Crítico: No se encontró la ''semilla_hash'' en la tabla de configuraciones.', 16, 1);
        RETURN;
    END

    -- 2. Descubrir automáticamente el nombre de la Clave Primaria (PK) de la tabla
    SELECT TOP 1 @CampoPK = c.name
    FROM sys.indexes i
    INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.is_primary_key = 1 
      AND i.object_id = OBJECT_ID(@NombreTabla);

    IF @CampoPK IS NULL
    BEGIN
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @MsgErrorPK VARCHAR(300) = CONCAT('Error: La tabla ', @NombreTabla, ' no existe o no tiene una Clave Primaria definida.');
        RAISERROR(@MsgErrorPK, 16, 1);
        RETURN;
    END

    -- 3. CAMBIADO: Concatenar todos los campos ORDENADOS ALFABÉTICAMENTE por nombre (excepto digito_verificador)
    SELECT @ListaCamposConcatenar = COALESCE(@ListaCamposConcatenar + ' + ''|'' + ', '') + 'ISNULL(CAST([' + name + '] AS VARCHAR(MAX)), '''')'
    FROM sys.columns
    WHERE object_id = OBJECT_ID(@NombreTabla)
      AND name <> 'digito_verificador'
    ORDER BY name; -- <--- CAMBIO CLAVE AQUÍ: Orden alfabético por nombre

    -- 4. Construir el bloque de procesamiento dinámico con cortocircuito inmediato y separadores
    SET @SqlDinamico = N'
        DECLARE @IdRegistro INT, @SemillaEntrante VARCHAR(100), @CamposConcatenados VARCHAR(MAX);
        DECLARE @HashSemillaEntrante VARCHAR(100), @NuevoHash VARCHAR(100);
        DECLARE @SemillaSistema VARCHAR(100) = @ParamSemillaSistema;
        
        DECLARE cur_dinamico CURSOR LOCAL FAST_FORWARD FOR
        SELECT t.' + QUOTENAME(@CampoPK) + ', t.[digito_verificador], ' + @ListaCamposConcatenar + '
        FROM ' + @NombreTabla + ' t
        INNER JOIN #RegistrosModificados tmp ON t.' + QUOTENAME(@CampoPK) + ' = tmp.id_modificado;

        OPEN cur_dinamico;
        FETCH NEXT FROM cur_dinamico INTO @IdRegistro, @SemillaEntrante, @CamposConcatenados;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @NuevoHash = NULL;

            -- Lógica Criptográfica con LOWER() para blindar contra Case Sensitivity
            SET @HashSemillaEntrante = LOWER(CONVERT(VARCHAR(100), HASHBYTES(''SHA2_256'', @SemillaEntrante), 2));

            IF @HashSemillaEntrante = LOWER(@SemillaSistema)
            BEGIN
                SET @NuevoHash = CONVERT(VARCHAR(100), HASHBYTES(''SHA2_256'', CONCAT(@SemillaEntrante, @CamposConcatenados)), 2);
            END

            -- Validación estricta con error y freno de mano inmediato
            IF @NuevoHash IS NULL OR LTRIM(RTRIM(@NuevoHash)) = ''''
            BEGIN
                CLOSE cur_dinamico;
                DEALLOCATE cur_dinamico; 
                
                RAISERROR(''Inconsistencia de datos: La semilla provista es inválida o el hash no pudo ser generado. Operación cancelada.'', 16, 1);
                RETURN;
            END
            ELSE
            BEGIN
                UPDATE ' + @NombreTabla + '
                SET [digito_verificador] = @NuevoHash
                WHERE ' + QUOTENAME(@CampoPK) + ' = @IdRegistro;
            END

            FETCH NEXT FROM cur_dinamico INTO @IdRegistro, @SemillaEntrante, @CamposConcatenados;
        END;

        CLOSE cur_dinamico;
        DEALLOCATE cur_dinamico;
    ';

    BEGIN TRY
        EXEC sp_executesql @SqlDinamico, 
                          N'@ParamSemillaSistema VARCHAR(100)', 
                          @ParamSemillaSistema = @SemillaGuardada;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN;
    END CATCH
END;
GO

CREATE PROCEDURE dbo.sp_BackupDatabaseAutoconfigurado
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NombreBaseDatos NVARCHAR(128);
    DECLARE @RutaCarpeta NVARCHAR(500);
    DECLARE @ArchivoBackup NVARCHAR(700);
    DECLARE @Fecha NVARCHAR(50);
    DECLARE @SQL NVARCHAR(MAX);

    -- 1. Obtener los valores desde tu tabla de configuración
    SELECT @RutaCarpeta = valor FROM dbo.configuraciones WHERE clave = 'ruta_backup';
    SELECT @NombreBaseDatos = valor FROM dbo.configuraciones WHERE clave = 'nombre_db';

    -- 2. Validar que los datos existan en la tabla antes de continuar
    IF @NombreBaseDatos IS NULL OR @RutaCarpeta IS NULL
    BEGIN
        RAISERROR('Error: No se encontró la ruta de backup o el nombre de la base de datos en la tabla dbo.configuracion.', 16, 1);
        RETURN;
    END;

    -- 3. Quitar la barra invertida (\) final de la ruta si el usuario la incluyó, para evitar dobles barras
    IF RIGHT(@RutaCarpeta, 1) = '\'
    BEGIN
        SET @RutaCarpeta = SUBSTRING(@RutaCarpeta, 1, LEN(@RutaCarpeta) - 1);
    END;

    -- 4. Generar una marca de tiempo única (AñoMesDía_HoraMinutoSegundo)
    SET @Fecha = CONVERT(NVARCHAR(20), GETDATE(), 112) + '_' + 
                 REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 108), ':', '');

    -- 5. Construir la ruta completa del archivo .bak
    SET @ArchivoBackup = @RutaCarpeta + N'\' + @NombreBaseDatos + N'_' + @Fecha + N'.bak';

    -- 6. Construir y ejecutar el comando BACKUP dinámico
    SET @SQL = N'BACKUP DATABASE ' + QUOTENAME(@NombreBaseDatos) + 
               N' TO DISK = N''' + @ArchivoBackup + N'''' + 
               N' WITH FORMAT, INIT, NAME = N''' + @NombreBaseDatos + N'-Backup completo'', SKIP, NOREWIND, STATS = 10;';

    EXEC sp_executesql @SQL;
END;
GO

CREATE TRIGGER [dbo].[TR_reclamos_GenerarDigitoVerificador]
ON [dbo].[reclamos]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF TRIGGER_NESTLEVEL() > 1 RETURN; 

    CREATE TABLE #RegistrosModificados (id_modificado INT);

    -- Buscamos la clave primaria usando el nombre de texto limpio
    DECLARE @CampoPK VARCHAR(128);
    SELECT TOP 1 @CampoPK = c.name FROM sys.indexes i 
    INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.is_primary_key = 1 AND i.object_id = OBJECT_ID('dbo.reclamos');

    INSERT INTO #RegistrosModificados (id_modificado)
    SELECT [reclamo_id] FROM inserted;

    -- Enviamos el nombre sin los corchetes adicionales
    EXEC [dbo].[sp_GenerarDigitoVerificadorUniversal] @NombreTabla = 'dbo.reclamos';
END;
GO

-- =====================================================
-- DATOS DE REFERENCIA
-- =====================================================

-- CONFIGURACIONES
INSERT INTO configuraciones (clave, valor, descripcion, tipo) VALUES
('MUNICIPALIDAD_NOMBRE', 'Municipalidad de Lomas de Zamora', 'Nombre de la municipalidad', 'TEXTO'),
('MUNICIPALIDAD_DIRECCION', 'Manuel Castro 220, Lomas de Zamora', 'Dirección de la municipalidad', 'TEXTO'),
('MUNICIPALIDAD_TELEFONO', '(011) 4239-1000', 'Teléfono de la municipalidad', 'TEXTO'),
('MUNICIPALIDAD_EMAIL', 'atencionvecinal@pozosya.com.ar', 'Correo electrónico de la municipalidad', 'EMAIL'),
('semilla_hash', '58bf37829dc123fb57421451004c6e2607e28c9af7a8a0f064efa333b1c0b309', 'Hash de la semilla para validar la integridad de datos', 'seguridad'),
('ruta_backup', '', 'Carpeta de backups (se completa sola al abrir Administración)', 'seguridad'),
('nombre_db', 'PozosYa', 'Nombre de la base de datos', 'seguridad');
GO

-- ROLES
SET IDENTITY_INSERT roles ON;
INSERT INTO roles (rol_id, nombre, descripcion, activo) VALUES
(1, 'SysAdmin', 'Administrador del sistema', 1),
(2, 'Operador', 'Operador del Contact Center', 1),
(3, 'Verificador', 'Verificador de reclamos', 1),
(4, 'Estimador', 'Responsable de presupuestos', 1),
(5, 'Priorizador', 'Responsable de priorización', 1),
(6, 'Gerente', 'Gerente de Área', 1);
SET IDENTITY_INSERT roles OFF;
GO

-- PERMISOS
SET IDENTITY_INSERT permisos ON;
INSERT INTO permisos (permiso_id, nombre, descripcion) VALUES
(1, 'SEGURIDAD_LOGIN', 'Acceder al sistema'),
(2, 'SEGURIDAD_RECUPERAR_ACCESO', 'Recuperar contraseña'),
(3, 'SEGURIDAD_CERRAR_SESION', 'Cerrar sesión'),
(4, 'SEGURIDAD_VER', 'Acceder al módulo Seguridad'),
(5, 'RECLAMOS_VER', 'Ver reclamos'),
(6, 'RECLAMOS_CREAR', 'Crear reclamos'),
(7, 'RECLAMOS_BUSCAR', 'Buscar reclamos'),
(8, 'RECLAMOS_VER_DETALLE', 'Ver detalle de reclamo'),
(9, 'RECLAMOS_EDITAR', 'Editar reclamo'),
(10, 'RECLAMOS_GUARDAR', 'Guardar reclamo'),
(11, 'RECLAMOS_CANCELAR', 'Cancelar edición'),
(12, 'RECLAMOS_VER_HISTORIAL', 'Ver historial'),
(13, 'NUEVOS_RECLAMOS_GUARDAR', 'Guardar nuevo reclamo'),
(14, 'NUEVOS_RECLAMOS_CANCELAR', 'Cancelar alta'),
(15, 'PRIORIZACION_VER', 'Ver priorización'),
(16, 'PRIORIZACION_BUSCAR', 'Buscar reclamos'),
(17, 'PRIORIZACION_SELECCIONAR_TRIMESTRE', 'Seleccionar trimestre'),
(18, 'PRIORIZACION_SELECCIONAR_RECLAMOS', 'Seleccionar reclamos'),
(19, 'PRIORIZACION_ORDENAR', 'Ordenar priorización'),
(20, 'PRIORIZACION_PROCESAR', 'Procesar priorización'),
(21, 'VERIFICACION_VER', 'Ver verificaciones'),
(22, 'VERIFICACION_SUBIR_EVIDENCIA', 'Subir evidencia'),
(23, 'VERIFICACION_MARCAR_INICIADO', 'Marcar iniciado'),
(24, 'VERIFICACION_MARCAR_EN_EJECUCION', 'Marcar en ejecución'),
(25, 'VERIFICACION_MARCAR_FINALIZADO', 'Marcar finalizado'),
(26, 'VERIFICACION_CANCELAR_RECLAMO', 'Cancelar reclamo'),
(27, 'PRESUPUESTO_VER', 'Ver presupuesto'),
(28, 'PRESUPUESTO_MODIFICAR', 'Modificar presupuesto'),
(29, 'PRESUPUESTO_GUARDAR', 'Guardar presupuesto'),
(30, 'PRESUPUESTO_SELECCIONAR_PROVEEDOR', 'Seleccionar proveedor'),
(31, 'PRESUPUESTO_CARGAR_MONTO', 'Cargar monto'),
(32, 'PRESUPUESTO_ADJUNTAR_ARCHIVO', 'Adjuntar archivo'),
(33, 'PRESUPUESTO_CONFIRMAR', 'Confirmar presupuesto'),
(34, 'PRESUPUESTO_CANCELAR_RECLAMO', 'Cancelar reclamo'),
(35, 'PARTIDAS_PRESUPUESTARIAS_VER', 'Ver partidas'),
(36, 'PARTIDAS_PRESUPUESTARIAS_CREAR', 'Crear partida'),
(37, 'PARTIDAS_PRESUPUESTARIAS_GUARDAR', 'Guardar partida'),
(38, 'PROVEEDORES_VER', 'Ver proveedores'),
(39, 'PROVEEDORES_BUSCAR', 'Buscar proveedores'),
(40, 'PROVEEDORES_CREAR', 'Crear proveedor'),
(41, 'PROVEEDORES_EDITAR', 'Editar proveedor'),
(42, 'PROVEEDORES_GUARDAR', 'Guardar proveedor'),
(43, 'CONFIGURACION_VER', 'Ver configuración'),
(44, 'CONFIGURACION_BUSCAR', 'Buscar configuración'),
(45, 'CONFIGURACION_CREAR', 'Crear configuración'),
(46, 'CONFIGURACION_EDITAR', 'Editar configuración'),
(47, 'CONFIGURACION_GUARDAR', 'Guardar configuración'),
(48, 'USUARIOS_VER', 'Ver usuarios'),
(49, 'USUARIOS_BUSCAR', 'Buscar usuarios'),
(50, 'USUARIOS_EDITAR', 'Editar usuario'),
(51, 'USUARIOS_GUARDAR', 'Guardar usuario'),
(52, 'USUARIOS_CAMBIAR_CONTRASENA', 'Cambiar contraseña'),
(53, 'USUARIOS_ASIGNAR_ROLES', 'Asignar roles'),
(54, 'USUARIOS_QUITAR_ROLES', 'Quitar roles'),
(55, 'USUARIOS_ADMINISTRAR_PERMISOS', 'Administrar permisos'),
(56, 'USUARIOS_OTORGAR_PERMISO', 'Otorgar permiso'),
(57, 'USUARIOS_REVOCAR_PERMISO', 'Revocar permiso'),
(58, 'LOGS_VER', 'Ver logs'),
(59, 'LOGS_BUSCAR', 'Buscar logs'),
(60, 'LOGS_VER_HISTORIAL', 'Ver historial'),
(61, 'USUARIOS_CREAR', 'Crear nuevos usuarios'),
(62, 'ADMINISTRADOR_VER', 'Ver Administración de sistema');
SET IDENTITY_INSERT permisos OFF;
GO

-- ROLES_PERMISOS
INSERT INTO roles_permisos (rol_id, permiso_id) VALUES
(1,1),
(1,2),
(1,3),
(1,4),
(1,43),
(1,44),
(1,45),
(1,46),
(1,47),
(1,48),
(1,49),
(1,50),
(1,51),
(1,52),
(1,53),
(1,54),
(1,55),
(1,56),
(1,57),
(1,58),
(1,59),
(1,60),
(1,61),
(1,62),
(2,1),
(2,2),
(2,3),
(2,5),
(2,6),
(2,7),
(2,9),
(2,10),
(2,11),
(2,13),
(2,14),
(3,1),
(3,2),
(3,3),
(3,5),
(3,7),
(3,8),
(3,12),
(3,21),
(3,22),
(3,23),
(3,24),
(3,25),
(3,26),
(4,1),
(4,2),
(4,3),
(4,5),
(4,7),
(4,8),
(4,12),
(4,27),
(4,28),
(4,29),
(4,30),
(4,31),
(4,32),
(4,33),
(4,34),
(5,1),
(5,2),
(5,3),
(5,5),
(5,7),
(5,8),
(5,12),
(5,15),
(5,16),
(5,17),
(5,18),
(5,19),
(5,20),
(6,1),
(6,2),
(6,3),
(6,5),
(6,7),
(6,8),
(6,12),
(6,35),
(6,36),
(6,37),
(6,38),
(6,39),
(6,40),
(6,41),
(6,42),
(6,58),
(6,59),
(6,60);
GO

-- DETALLECLAVEVALOR
SET IDENTITY_INSERT detalleClaveValor ON;
INSERT INTO detalleClaveValor (detalle_id, tabla, clave, valor) VALUES
(1, 'ESTADO', 'INICIADO', 'Iniciado'),
(2, 'ESTADO', 'VERIFICADO', 'Verificado'),
(3, 'ESTADO', 'PRESUPUESTADO', 'Presupuestado'),
(4, 'ESTADO', 'PRIORIZADO', 'Priorizado'),
(5, 'ESTADO', 'EN_EJECUCION', 'En Ejecución'),
(6, 'ESTADO', 'FINALIZADO', 'Finalizado'),
(7, 'SEVERIDAD', 'BAJA', 'Baja'),
(8, 'SEVERIDAD', 'MEDIA', 'Media'),
(9, 'SEVERIDAD', 'ALTA', 'Alta'),
(10, 'TIPO_DANIO', 'BACHE', 'Bache'),
(11, 'TIPO_DANIO', 'LUMINARIA', 'Luminaria'),
(12, 'TIPO_DANIO', 'ARBOL', 'Árbol caído'),
(13, 'TIPO_DANIO', 'CORDON', 'Cordón cuneta'),
(14, 'TIPO_DANIO', 'SUMIDERO', 'Sumidero obstruido'),
(15, 'BARRIO', 'BANFIELD', 'Banfield'),
(16, 'BARRIO', 'TEMPERLEY', 'Temperley'),
(17, 'BARRIO', 'LOMAS', 'Lomas de Zamora'),
(18, 'BARRIO', 'TURDERA', 'Turdera'),
(19, 'BARRIO', 'LLAVALLOL', 'Llavallol'),
(20, 'ESTADO', 'CANCELADO', 'Cancelado'),
(21, 'BARRIO', 'SAN JOSE', 'San José'),
(22, 'BARRIO', 'VILLA FIORITO', 'Villa Fiorito'),
(23, 'BARRIO', 'VILLA CENTENARIO', 'Villa Centenario'),
(24, 'BARRIO', 'INGENIERO BUDGE', 'Ingeniero Budge'),
(25, 'BARRIO', 'VILLA ALBERTINA', 'Villa Albertina'),
(26, 'BARRIO', 'SANTA CATALINA', 'Santa Catalina'),
(27, 'BARRIO', 'PARQUE BARON', 'Parque Barón'),
(28, 'BARRIO', 'VILLA LAMADRID', 'Villa Lamadrid'),
(29, 'BARRIO', 'SANTA MARTA', 'Santa Marta');
SET IDENTITY_INSERT detalleClaveValor OFF;
GO

-- =====================================================
-- DATOS DE DEMOSTRACIÓN
-- =====================================================

-- USUARIOS
SET IDENTITY_INSERT usuarios ON;
INSERT INTO usuarios (usuario_id, nombre, apellido, username, email, password_hash, activo, ultimo_login, fecha_creacion) VALUES
(1, 'Administrador', 'del Sistema', 'admin', 'lucashood2005+admin@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1, '2026-06-15T08:58:00', '2026-06-01T08:00:00'),
(2, 'Laura', 'Benítez', 'operador', 'lucashood2005+operador@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1, '2026-10-25T12:28:00', '2026-06-15T09:10:00'),
(3, 'Martín', 'Sosa', 'verificador', 'lucashood2005+verificador@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1, '2026-10-17T10:08:00', '2026-06-15T09:15:00'),
(4, 'Carolina', 'Ríos', 'estimador', 'lucashood2005+estimador@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1, '2026-10-10T11:28:00', '2026-06-15T09:20:00'),
(5, 'Diego', 'Acosta', 'priorizador', 'lucashood2005+priorizador@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1, '2026-09-25T09:48:00', '2026-06-15T09:25:00'),
(6, 'Silvia', 'Romero', 'gerente', 'lucashood2005+gerente@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1, '2026-10-20T11:18:00', '2026-06-15T09:30:00');
SET IDENTITY_INSERT usuarios OFF;
GO

-- USUARIO_ROLES
INSERT INTO usuario_roles (usuario_id, rol_id) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5),
(1, 6),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6);
GO

-- PROVEEDORES
SET IDENTITY_INSERT proveedores ON;
INSERT INTO proveedores (proveedor_id, nombre, cuit, telefono, mail, direccion, activo) VALUES
(1, 'Pavimentos del Sur S.A.', '30-71458236-4', '(011) 4292-3150', 'contacto@pavimentosdelsur.com.ar', 'Av. Hipólito Yrigoyen 9850, Lomas de Zamora', 1),
(2, 'Iluminación Urbana S.R.L.', '30-70918452-7', '(011) 4243-7781', 'obras@iluminacionurbana.com.ar', 'Av. Meeks 1120, Temperley', 1),
(3, 'Forestal Conurbano S.R.L.', '30-71233098-1', '(011) 4231-0945', 'info@forestalconurbano.com.ar', 'Av. Antártida Argentina 2210, Llavallol', 1),
(4, 'Hidráulica Sur S.A.', '30-71102774-9', '(011) 4202-5563', 'presupuestos@hidraulicasur.com.ar', 'Av. Alsina 2650, Banfield', 1),
(5, 'Obras Viales Lomas S.A.', '30-70655319-2', '(011) 4244-8820', 'administracion@obrasvialeslomas.com.ar', 'Laprida 1530, Lomas de Zamora', 1);
SET IDENTITY_INSERT proveedores OFF;
GO

-- VECINOS
SET IDENTITY_INSERT vecinos ON;
INSERT INTO vecinos (vecino_id, nombre, apellido, dni, telefono, mail, direccion, nro_catastral, barrio_id, fecha_registro, activo) VALUES
(1, 'María José', 'Fernández', '27845123', '11-5823-4471', 'lucashood2005@gmail.com', 'Maipú 1250', 'LZ-15-1037', 15, '2026-07-06T09:14:00', 1),
(2, 'Jorge', 'Ramírez', '20456789', '11-6234-9087', 'lucashood2005@gmail.com', 'Av. Meeks 640', 'LZ-16-1074', 16, '2026-07-09T18:40:00', 1),
(3, 'Ana Laura', 'Gómez', '31258963', '11-4789-3321', 'lucashood2005@gmail.com', 'Laprida 980', 'LZ-17-1111', 17, '2026-07-14T11:05:00', 1),
(4, 'Ricardo', 'Pereyra', '17852364', '11-3345-7712', 'lucashood2005@gmail.com', 'Av. Hipólito Yrigoyen 11450', 'LZ-18-1148', 18, '2026-07-21T07:52:00', 1),
(5, 'Valeria', 'Suárez', '33654128', '11-6612-0458', 'lucashood2005@gmail.com', 'Larroque 2100', 'LZ-15-1185', 15, '2026-08-03T10:30:00', 1),
(6, 'Héctor', 'Domínguez', '14235987', '11-4452-8896', 'lucashood2005@gmail.com', 'Av. Antártida Argentina 1650', 'LZ-19-1222', 19, '2026-08-11T16:20:00', 1),
(7, 'Lucía', 'Martínez', '38963254', '11-5571-2234', 'lucashood2005@gmail.com', 'Gorriti 450', 'LZ-17-1259', 17, '2026-08-20T14:00:00', 1),
(8, 'Pablo', 'Giménez', '29741852', '11-6789-4410', 'lucashood2005@gmail.com', '9 de Julio 320', 'LZ-16-1296', 16, '2026-09-05T19:10:00', 1),
(9, 'Graciela', 'Molina', '16547893', '11-4123-6658', 'lucashood2005@gmail.com', 'Las Lilas 1430', 'LZ-21-1333', 21, '2026-09-10T12:00:00', 1),
(10, 'Sergio', 'Herrera', '25896314', '11-5034-7789', 'lucashood2005@gmail.com', 'Av. Juan XXIII 2350', 'LZ-23-1370', 23, '2026-09-16T15:25:00', 1),
(11, 'Florencia', 'Castro', '36521478', '11-6658-1123', 'lucashood2005@gmail.com', 'Vieytes 1430', 'LZ-15-1407', 15, '2026-09-29T10:15:00', 1),
(12, 'Néstor', 'Álvarez', '12458796', '11-4987-5564', 'lucashood2005@gmail.com', 'Pasco 2250', 'LZ-29-1444', 29, '2026-10-02T08:40:00', 1),
(13, 'Daniela', 'Torres', '30147852', '11-5214-8890', 'lucashood2005@gmail.com', 'Oliden 1980', 'LZ-27-1481', 27, '2026-10-05T21:05:00', 1),
(14, 'Miguel Ángel', 'Romero', '22369874', '11-6321-4457', 'lucashood2005@gmail.com', 'Olimpo 1250', 'LZ-24-1518', 24, '2026-10-14T13:50:00', 1),
(15, 'Silvina', 'Medina', '34785126', '11-4578-2236', 'lucashood2005@gmail.com', 'Cayetano Beliera 1150', 'LZ-22-1555', 22, '2026-10-22T17:35:00', 1),
(16, 'Carlos', 'Benítez', '18965472', '11-5897-3364', 'lucashood2005@gmail.com', 'Gral. Paz 1250', 'LZ-19-1592', 19, '2026-10-25T12:40:00', 1);
SET IDENTITY_INSERT vecinos OFF;
GO

-- PARTIDAS_PRESUPUESTARIAS
SET IDENTITY_INSERT partidas_presupuestarias ON;
INSERT INTO partidas_presupuestarias (partida_presupuestaria_id, anio, trimestre, monto_asignado, monto_comprometido, monto_ejecutado, usuario_gerente_id, fecha_asignacion, observaciones, activo) VALUES
(1, 2026, 3, 15000000.00, 10360000.00, 4705000.00, 6, '2026-06-26T10:00:00', 'Partida trimestral para bacheo, alumbrado y desagües. Trimestre cerrado.', 0),
(2, 2026, 4, 18000000.00, 5070000.00, 0.00, 6, '2026-09-15T10:00:00', 'Partida trimestral vigente.', 1),
(3, 2027, 1, 20000000.00, 0.00, 0.00, 6, '2026-10-20T11:30:00', 'Partida aprobada para el primer trimestre de 2027.', 1);
SET IDENTITY_INSERT partidas_presupuestarias OFF;
GO

-- Los reclamos se insertan con la semilla en digito_verificador:
-- el trigger TR_reclamos_GenerarDigitoVerificador la reemplaza por el hash.
-- RECLAMOS
SET IDENTITY_INSERT reclamos ON;
INSERT INTO reclamos (reclamo_id, numero_reclamo, vecino_id, tipo_danio_id, severidad_id, estado_actual_id, descripcion, direccion, latitud, longitud, barrio_id, fecha_creacion, usuario_creador_id, fecha_cierre, usuario_modificador_id, fecha_modificacion, digito_verificador) VALUES
(1, 'REC-20260706091400037', 1, 10, 9, 6, 'Bache de gran tamaño frente a la escuela; ocupa medio carril y ya provocó daños en vehículos.', 'Maipú 1250', -34.74660000, -58.39720000, 15, '2026-07-06T09:14:00', 2, '2026-08-19T16:00:00', 3, '2026-08-19T16:00:00', 'Maxi1234'),
(2, 'REC-20260709184000074', 2, 11, 8, 6, 'Tres luminarias apagadas sobre la avenida; la cuadra queda totalmente a oscuras de noche.', 'Av. Meeks 640', -34.77660000, -58.39820000, 16, '2026-07-09T18:40:00', 2, '2026-08-06T15:30:00', 3, '2026-08-06T15:30:00', 'Maxi1234'),
(3, 'REC-20260714110500111', 3, 14, 9, 6, 'Sumidero tapado con hojas y basura; cuando llueve se inunda la esquina y el agua entra a las veredas.', 'Laprida 980', -34.76260000, -58.40620000, 17, '2026-07-14T11:05:00', 2, '2026-08-12T14:00:00', 3, '2026-08-12T14:00:00', 'Maxi1234'),
(4, 'REC-20260721075200148', 4, 12, 9, 6, 'Árbol caído por la tormenta bloqueando la vereda y parte de la calzada, con ramas apoyadas sobre los cables.', 'Av. Hipólito Yrigoyen 11450', -34.79260000, -58.41020000, 18, '2026-07-21T07:52:00', 2, '2026-08-02T13:00:00', 3, '2026-08-02T13:00:00', 'Maxi1234'),
(5, 'REC-20260803103000185', 5, 13, 8, 5, 'Cordón cuneta roto en unos 15 metros; el agua se estanca y se forman charcos permanentes.', 'Larroque 2100', -34.74450000, -58.39550000, 15, '2026-08-03T10:30:00', 2, NULL, 3, '2026-09-01T08:00:00', 'Maxi1234'),
(6, 'REC-20260811162000222', 6, 10, 9, 5, 'Varios baches seguidos sobre la mano hacia la estación, muy peligrosos para motos y bicicletas.', 'Av. Antártida Argentina 1650', -34.79260000, -58.43220000, 19, '2026-08-11T16:20:00', 2, NULL, 3, '2026-08-31T08:30:00', 'Maxi1234'),
(7, 'REC-20260902094500259', 7, 10, 9, 4, 'Bache profundo en la esquina con Boedo; cuando llueve se llena de agua y no se ve.', 'Gorriti 450', -34.76050000, -58.40450000, 17, '2026-09-02T09:45:00', 2, NULL, 5, '2026-09-25T10:00:00', 'Maxi1234'),
(8, 'REC-20260905191000296', 8, 11, 9, 4, 'Columna de alumbrado sin luz en la parada de colectivo; los vecinos esperan a oscuras.', '9 de Julio 320', -34.77450000, -58.39650000, 16, '2026-09-05T19:10:00', 2, NULL, 5, '2026-09-25T10:00:00', 'Maxi1234'),
(9, 'REC-20260910120000333', 9, 14, 8, 4, 'Sumidero obstruido con tierra; el agua corre por la calle hasta la esquina.', 'Las Lilas 1430', -34.77260000, -58.42720000, 21, '2026-09-10T12:00:00', 2, NULL, 5, '2026-09-25T10:00:00', 'Maxi1234'),
(10, 'REC-20260916152500370', 10, 13, 8, 4, 'Cordón cuneta hundido frente al club; se acumula agua y barro en la entrada.', 'Av. Juan XXIII 2350', -34.73760000, -58.42720000, 23, '2026-09-16T15:25:00', 2, NULL, 5, '2026-09-25T10:00:00', 'Maxi1234'),
(11, 'REC-20260929101500407', 11, 10, 8, 3, 'Hundimiento del asfalto alrededor de una tapa de servicio; el desnivel crece cada semana.', 'Vieytes 1430', -34.74240000, -58.39380000, 15, '2026-09-29T10:15:00', 2, NULL, 4, '2026-10-04T11:40:00', 'Maxi1234'),
(12, 'REC-20261002084000444', 12, 12, 8, 3, 'Árbol con ramas secas que caen sobre la vereda; riesgo para los peatones.', 'Pasco 2250', -34.78160000, -58.44720000, 29, '2026-10-02T08:40:00', 2, NULL, 4, '2026-10-05T11:40:00', 'Maxi1234'),
(13, 'REC-20261005210500481', 13, 11, 7, 3, 'Luminaria que se prende y se apaga toda la noche.', 'Oliden 1980', -34.77260000, -58.38220000, 27, '2026-10-05T21:05:00', 2, NULL, 4, '2026-10-10T11:40:00', 'Maxi1234'),
(14, 'REC-20261012083000518', 3, 10, 9, 2, 'Bache en la avenida a la altura de la plaza; los autos lo esquivan invadiendo el otro carril.', 'Av. Hipólito Yrigoyen 8650', -34.75840000, -58.40280000, 17, '2026-10-12T08:30:00', 2, NULL, 3, '2026-10-13T11:20:00', 'Maxi1234'),
(15, 'REC-20261014135000555', 14, 14, 9, 2, 'Sumidero desbordado; el agua estancada genera mal olor y mosquitos.', 'Olimpo 1250', -34.72460000, -58.46220000, 24, '2026-10-14T13:50:00', 2, NULL, 3, '2026-10-16T09:20:00', 'Maxi1234'),
(16, 'REC-20261016112000592', 4, 13, 7, 2, 'Cordón cuneta con roturas menores frente al jardín de infantes.', 'Pasco 650', -34.79050000, -58.40850000, 18, '2026-10-16T11:20:00', 2, NULL, 3, '2026-10-17T10:20:00', 'Maxi1234'),
(17, 'REC-20261021090500629', 8, 11, 8, 1, 'Luminaria colgando de la columna; parece que se va a caer.', 'Av. Almirante Brown 4200', -34.77240000, -58.39480000, 16, '2026-10-21T09:05:00', 2, NULL, NULL, NULL, 'Maxi1234'),
(18, 'REC-20261022173500666', 15, 10, 9, 1, 'Pozo grande en la calle; los autos no pueden pasar y tienen que dar la vuelta.', 'Cayetano Beliera 1150', -34.71060000, -58.45020000, 22, '2026-10-22T17:35:00', 2, NULL, NULL, NULL, 'Maxi1234'),
(19, 'REC-20261023101000703', 1, 12, 7, 1, 'Las raíces del árbol levantaron la vereda y rompieron parte del cordón.', 'Alsina 820', -34.74030000, -58.39210000, 15, '2026-10-23T10:10:00', 2, NULL, NULL, NULL, 'Maxi1234'),
(20, 'REC-20261025124000740', 16, 14, 8, 1, 'Rejilla del sumidero rota y hundida; peligro para los peatones.', 'Gral. Paz 1250', -34.79050000, -58.43050000, 19, '2026-10-25T12:40:00', 2, NULL, NULL, NULL, 'Maxi1234'),
(21, 'REC-20260820140000777', 7, 10, 7, 20, 'Bache chico junto al cordón. (Reclamo duplicado: la empresa de agua lo reparó antes de la verificación.)', 'Boedo 330', -34.75630000, -58.40110000, 17, '2026-08-20T14:00:00', 2, '2026-08-22T10:15:00', 3, '2026-08-22T10:15:00', 'Maxi1234');
SET IDENTITY_INSERT reclamos OFF;
GO

-- HISTORIAL_RECLAMO
INSERT INTO historial_reclamo (reclamo_id, estado_anterior_id, usuario_id, fecha, usuario_modificador, fecha_modificacion) VALUES
(1, 1, 3, '2026-07-08T10:20:00', 3, '2026-07-08T10:20:00'),
(1, 2, 4, '2026-07-11T11:40:00', 4, '2026-07-11T11:40:00'),
(1, 3, 5, '2026-07-30T11:00:00', 5, '2026-07-30T11:00:00'),
(1, 4, 3, '2026-08-04T08:30:00', 3, '2026-08-04T08:30:00'),
(1, 5, 3, '2026-08-19T16:00:00', 3, '2026-08-19T16:00:00'),
(2, 1, 3, '2026-07-10T11:20:00', 3, '2026-07-10T11:20:00'),
(2, 2, 4, '2026-07-12T11:40:00', 4, '2026-07-12T11:40:00'),
(2, 3, 5, '2026-07-30T11:00:00', 5, '2026-07-30T11:00:00'),
(2, 4, 3, '2026-08-03T09:00:00', 3, '2026-08-03T09:00:00'),
(2, 5, 3, '2026-08-06T15:30:00', 3, '2026-08-06T15:30:00'),
(3, 1, 3, '2026-07-16T09:20:00', 3, '2026-07-16T09:20:00'),
(3, 2, 4, '2026-07-19T11:40:00', 4, '2026-07-19T11:40:00'),
(3, 3, 5, '2026-07-30T11:00:00', 5, '2026-07-30T11:00:00'),
(3, 4, 3, '2026-08-05T08:00:00', 3, '2026-08-05T08:00:00'),
(3, 5, 3, '2026-08-12T14:00:00', 3, '2026-08-12T14:00:00'),
(4, 1, 3, '2026-07-22T10:20:00', 3, '2026-07-22T10:20:00'),
(4, 2, 4, '2026-07-24T11:40:00', 4, '2026-07-24T11:40:00'),
(4, 3, 5, '2026-07-30T11:00:00', 5, '2026-07-30T11:00:00'),
(4, 4, 3, '2026-08-01T07:30:00', 3, '2026-08-01T07:30:00'),
(4, 5, 3, '2026-08-02T13:00:00', 3, '2026-08-02T13:00:00'),
(5, 1, 3, '2026-08-05T11:20:00', 3, '2026-08-05T11:20:00'),
(5, 2, 4, '2026-08-08T11:40:00', 4, '2026-08-08T11:40:00'),
(5, 3, 5, '2026-08-20T10:30:00', 5, '2026-08-20T10:30:00'),
(5, 4, 3, '2026-09-01T08:00:00', 3, '2026-09-01T08:00:00'),
(6, 1, 3, '2026-08-12T09:20:00', 3, '2026-08-12T09:20:00'),
(6, 2, 4, '2026-08-14T11:40:00', 4, '2026-08-14T11:40:00'),
(6, 3, 5, '2026-08-20T10:30:00', 5, '2026-08-20T10:30:00'),
(6, 4, 3, '2026-08-31T08:30:00', 3, '2026-08-31T08:30:00'),
(7, 1, 3, '2026-09-04T10:20:00', 3, '2026-09-04T10:20:00'),
(7, 2, 4, '2026-09-07T11:40:00', 4, '2026-09-07T11:40:00'),
(7, 3, 5, '2026-09-25T10:00:00', 5, '2026-09-25T10:00:00'),
(8, 1, 3, '2026-09-06T11:20:00', 3, '2026-09-06T11:20:00'),
(8, 2, 4, '2026-09-08T11:40:00', 4, '2026-09-08T11:40:00'),
(8, 3, 5, '2026-09-25T10:00:00', 5, '2026-09-25T10:00:00'),
(9, 1, 3, '2026-09-12T09:20:00', 3, '2026-09-12T09:20:00'),
(9, 2, 4, '2026-09-15T11:40:00', 4, '2026-09-15T11:40:00'),
(9, 3, 5, '2026-09-25T10:00:00', 5, '2026-09-25T10:00:00'),
(10, 1, 3, '2026-09-17T10:20:00', 3, '2026-09-17T10:20:00'),
(10, 2, 4, '2026-09-19T11:40:00', 4, '2026-09-19T11:40:00'),
(10, 3, 5, '2026-09-25T10:00:00', 5, '2026-09-25T10:00:00'),
(11, 1, 3, '2026-10-01T11:20:00', 3, '2026-10-01T11:20:00'),
(11, 2, 4, '2026-10-04T11:40:00', 4, '2026-10-04T11:40:00'),
(12, 1, 3, '2026-10-03T09:20:00', 3, '2026-10-03T09:20:00'),
(12, 2, 4, '2026-10-05T11:40:00', 4, '2026-10-05T11:40:00'),
(13, 1, 3, '2026-10-07T10:20:00', 3, '2026-10-07T10:20:00'),
(13, 2, 4, '2026-10-10T11:40:00', 4, '2026-10-10T11:40:00'),
(14, 1, 3, '2026-10-13T11:20:00', 3, '2026-10-13T11:20:00'),
(15, 1, 3, '2026-10-16T09:20:00', 3, '2026-10-16T09:20:00'),
(16, 1, 3, '2026-10-17T10:20:00', 3, '2026-10-17T10:20:00'),
(21, 1, 3, '2026-08-22T10:15:00', 3, '2026-08-22T10:15:00');
GO

-- PRESUPUESTOS
INSERT INTO presupuestos (reclamo_id, proveedor_id, monto_estimado, monto_final, archivo_pdf, fecha_carga, usuario_id) VALUES
(1, 1, 2350000.00, 2480000.00, NULL, '2026-07-11T11:40:00', 4),
(2, 2, 420000.00, 415000.00, NULL, '2026-07-12T11:40:00', 4),
(3, 4, 780000.00, 820000.00, NULL, '2026-07-19T11:40:00', 4),
(4, 3, 960000.00, 990000.00, NULL, '2026-07-24T11:40:00', 4),
(5, 5, 3100000.00, NULL, NULL, '2026-08-08T11:40:00', 4),
(6, 1, 2750000.00, NULL, NULL, '2026-08-14T11:40:00', 4),
(7, 1, 1850000.00, NULL, NULL, '2026-09-07T11:40:00', 4),
(8, 2, 380000.00, NULL, NULL, '2026-09-08T11:40:00', 4),
(9, 4, 640000.00, NULL, NULL, '2026-09-15T11:40:00', 4),
(10, 5, 2200000.00, NULL, NULL, '2026-09-19T11:40:00', 4),
(11, 1, 1450000.00, NULL, NULL, '2026-10-04T11:40:00', 4),
(12, 3, 720000.00, NULL, NULL, '2026-10-05T11:40:00', 4),
(13, 2, 260000.00, NULL, NULL, '2026-10-10T11:40:00', 4);
GO

-- RECLAMOS_PARTIDA
INSERT INTO reclamos_partida (reclamo_id, partida_id, monto_asignado_reclamo, fecha_asignacion, usuario_priorizador_id, numero_prioridad) VALUES
(1, 1, 2350000.00, '2026-07-30T11:00:00', 5, 1),
(2, 1, 420000.00, '2026-07-30T11:00:00', 5, 4),
(3, 1, 780000.00, '2026-07-30T11:00:00', 5, 3),
(4, 1, 960000.00, '2026-07-30T11:00:00', 5, 2),
(5, 1, 3100000.00, '2026-08-20T10:30:00', 5, 6),
(6, 1, 2750000.00, '2026-08-20T10:30:00', 5, 5),
(7, 2, 1850000.00, '2026-09-25T10:00:00', 5, 1),
(8, 2, 380000.00, '2026-09-25T10:00:00', 5, 2),
(9, 2, 640000.00, '2026-09-25T10:00:00', 5, 4),
(10, 2, 2200000.00, '2026-09-25T10:00:00', 5, 3);
GO

-- LOGS_SISTEMA
INSERT INTO logs_sistema (usuario_id, accion, tabla_afectada, registro_id, fecha, descripcion) VALUES
(1, 'LOGIN', 'USUARIOS', 1, '2026-06-15T08:58:00', 'Inicio de sesión del usuario admin'),
(1, 'CREAR', 'USUARIOS', 2, '2026-06-15T09:10:00', 'Se creó el usuario operador.'),
(1, 'CREAR', 'USUARIOS', 3, '2026-06-15T09:15:00', 'Se creó el usuario verificador.'),
(1, 'CREAR', 'USUARIOS', 4, '2026-06-15T09:20:00', 'Se creó el usuario estimador.'),
(1, 'CREAR', 'USUARIOS', 5, '2026-06-15T09:25:00', 'Se creó el usuario priorizador.'),
(1, 'CREAR', 'USUARIOS', 6, '2026-06-15T09:30:00', 'Se creó el usuario gerente.'),
(6, 'LOGIN', 'USUARIOS', 6, '2026-06-20T09:55:00', 'Inicio de sesión del usuario gerente'),
(6, 'CREAR', 'PROVEEDORES', 1, '2026-06-20T10:07:00', 'Se creó el proveedor Pavimentos del Sur S.A.'),
(6, 'CREAR', 'PROVEEDORES', 2, '2026-06-20T10:14:00', 'Se creó el proveedor Iluminación Urbana S.R.L.'),
(6, 'CREAR', 'PROVEEDORES', 3, '2026-06-20T10:21:00', 'Se creó el proveedor Forestal Conurbano S.R.L.'),
(6, 'CREAR', 'PROVEEDORES', 4, '2026-06-20T10:28:00', 'Se creó el proveedor Hidráulica Sur S.A.'),
(6, 'CREAR', 'PROVEEDORES', 5, '2026-06-20T10:35:00', 'Se creó el proveedor Obras Viales Lomas S.A.'),
(6, 'LOGIN', 'USUARIOS', 6, '2026-06-26T09:48:00', 'Inicio de sesión del usuario gerente'),
(6, 'CREAR', 'PARTIDAS_PRESUPUESTARIAS', 1, '2026-06-26T10:00:00', 'Se creó una partida presupuestaria.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-07-06T09:02:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 1, '2026-07-06T09:14:00', 'Se creó el reclamo REC-20260706091400037'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-07-08T10:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 1, '2026-07-08T10:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-07-09T18:28:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 2, '2026-07-09T18:40:00', 'Se creó el reclamo REC-20260709184000074'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-07-10T11:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 2, '2026-07-10T11:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-07-11T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 1, '2026-07-11T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-07-12T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 2, '2026-07-12T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-07-14T10:53:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 3, '2026-07-14T11:05:00', 'Se creó el reclamo REC-20260714110500111'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-07-16T09:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 3, '2026-07-16T09:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-07-19T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 3, '2026-07-19T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-07-21T07:40:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 4, '2026-07-21T07:52:00', 'Se creó el reclamo REC-20260721075200148'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-07-22T10:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 4, '2026-07-22T10:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-07-24T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 4, '2026-07-24T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(5, 'LOGIN', 'USUARIOS', 5, '2026-07-30T10:48:00', 'Inicio de sesión del usuario priorizador'),
(5, 'PRIORIZAR', 'RECLAMOS', 1, '2026-07-30T11:00:00', 'El reclamo REC-20260706091400037 fue priorizado. Partida: 2026 - Trimestre 3. Prioridad: 1'),
(5, 'PRIORIZAR', 'RECLAMOS', 2, '2026-07-30T11:00:00', 'El reclamo REC-20260709184000074 fue priorizado. Partida: 2026 - Trimestre 3. Prioridad: 4'),
(5, 'PRIORIZAR', 'RECLAMOS', 3, '2026-07-30T11:00:00', 'El reclamo REC-20260714110500111 fue priorizado. Partida: 2026 - Trimestre 3. Prioridad: 3'),
(5, 'PRIORIZAR', 'RECLAMOS', 4, '2026-07-30T11:00:00', 'El reclamo REC-20260721075200148 fue priorizado. Partida: 2026 - Trimestre 3. Prioridad: 2'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-01T07:18:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 4, '2026-08-01T07:30:00', 'El reclamo pasó al estado ''En Ejecución''.'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-02T12:48:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 4, '2026-08-02T13:00:00', 'El reclamo pasó al estado ''Finalizado''.'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-03T08:48:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 2, '2026-08-03T09:00:00', 'El reclamo pasó al estado ''En Ejecución''.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-08-03T10:18:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 5, '2026-08-03T10:30:00', 'Se creó el reclamo REC-20260803103000185'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-04T08:18:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 1, '2026-08-04T08:30:00', 'El reclamo pasó al estado ''En Ejecución''.'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-05T07:48:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 3, '2026-08-05T08:00:00', 'El reclamo pasó al estado ''En Ejecución''.'),
(3, 'MODIFICAR', 'VERIFICACION', 5, '2026-08-05T11:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-06T15:18:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 2, '2026-08-06T15:30:00', 'El reclamo pasó al estado ''Finalizado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-08-08T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 5, '2026-08-08T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-08-11T16:08:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 6, '2026-08-11T16:20:00', 'Se creó el reclamo REC-20260811162000222'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-12T09:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 6, '2026-08-12T09:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(3, 'MODIFICAR', 'VERIFICACION', 3, '2026-08-12T14:00:00', 'El reclamo pasó al estado ''Finalizado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-08-14T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 6, '2026-08-14T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-19T15:48:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 1, '2026-08-19T16:00:00', 'El reclamo pasó al estado ''Finalizado''.'),
(5, 'LOGIN', 'USUARIOS', 5, '2026-08-20T10:18:00', 'Inicio de sesión del usuario priorizador'),
(5, 'PRIORIZAR', 'RECLAMOS', 5, '2026-08-20T10:30:00', 'El reclamo REC-20260803103000185 fue priorizado. Partida: 2026 - Trimestre 3. Prioridad: 6'),
(5, 'PRIORIZAR', 'RECLAMOS', 6, '2026-08-20T10:30:00', 'El reclamo REC-20260811162000222 fue priorizado. Partida: 2026 - Trimestre 3. Prioridad: 5'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-08-20T13:48:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 21, '2026-08-20T14:00:00', 'Se creó el reclamo REC-20260820140000777'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-22T10:03:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 21, '2026-08-22T10:15:00', 'El reclamo pasó al estado ''Cancelado''.'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-08-31T08:18:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 6, '2026-08-31T08:30:00', 'El reclamo pasó al estado ''En Ejecución''.'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-09-01T07:48:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 5, '2026-09-01T08:00:00', 'El reclamo pasó al estado ''En Ejecución''.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-09-02T09:33:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 7, '2026-09-02T09:45:00', 'Se creó el reclamo REC-20260902094500259'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-09-04T10:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 7, '2026-09-04T10:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-09-05T18:58:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 8, '2026-09-05T19:10:00', 'Se creó el reclamo REC-20260905191000296'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-09-06T11:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 8, '2026-09-06T11:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-09-07T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 7, '2026-09-07T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-09-08T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 8, '2026-09-08T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-09-10T11:48:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 9, '2026-09-10T12:00:00', 'Se creó el reclamo REC-20260910120000333'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-09-12T09:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 9, '2026-09-12T09:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(6, 'LOGIN', 'USUARIOS', 6, '2026-09-15T09:48:00', 'Inicio de sesión del usuario gerente'),
(6, 'CREAR', 'PARTIDAS_PRESUPUESTARIAS', 2, '2026-09-15T10:00:00', 'Se creó una partida presupuestaria.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-09-15T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 9, '2026-09-15T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-09-16T15:13:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 10, '2026-09-16T15:25:00', 'Se creó el reclamo REC-20260916152500370'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-09-17T10:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 10, '2026-09-17T10:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-09-19T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 10, '2026-09-19T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(5, 'LOGIN', 'USUARIOS', 5, '2026-09-25T09:48:00', 'Inicio de sesión del usuario priorizador'),
(5, 'PRIORIZAR', 'RECLAMOS', 7, '2026-09-25T10:00:00', 'El reclamo REC-20260902094500259 fue priorizado. Partida: 2026 - Trimestre 4. Prioridad: 1'),
(5, 'PRIORIZAR', 'RECLAMOS', 8, '2026-09-25T10:00:00', 'El reclamo REC-20260905191000296 fue priorizado. Partida: 2026 - Trimestre 4. Prioridad: 2'),
(5, 'PRIORIZAR', 'RECLAMOS', 9, '2026-09-25T10:00:00', 'El reclamo REC-20260910120000333 fue priorizado. Partida: 2026 - Trimestre 4. Prioridad: 4'),
(5, 'PRIORIZAR', 'RECLAMOS', 10, '2026-09-25T10:00:00', 'El reclamo REC-20260916152500370 fue priorizado. Partida: 2026 - Trimestre 4. Prioridad: 3'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-09-29T10:03:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 11, '2026-09-29T10:15:00', 'Se creó el reclamo REC-20260929101500407'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-10-01T11:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 11, '2026-10-01T11:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-02T08:28:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 12, '2026-10-02T08:40:00', 'Se creó el reclamo REC-20261002084000444'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-10-03T09:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 12, '2026-10-03T09:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-10-04T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 11, '2026-10-04T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-10-05T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 12, '2026-10-05T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-05T20:53:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 13, '2026-10-05T21:05:00', 'Se creó el reclamo REC-20261005210500481'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-10-07T10:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 13, '2026-10-07T10:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(4, 'LOGIN', 'USUARIOS', 4, '2026-10-10T11:28:00', 'Inicio de sesión del usuario estimador'),
(4, 'CONFIRMAR', 'PRESUPUESTOS', 13, '2026-10-10T11:40:00', 'Se creó y confirmó el presupuesto. El reclamo pasó a PRESUPUESTADO.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-12T08:18:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 14, '2026-10-12T08:30:00', 'Se creó el reclamo REC-20261012083000518'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-10-13T11:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 14, '2026-10-13T11:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-14T13:38:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 15, '2026-10-14T13:50:00', 'Se creó el reclamo REC-20261014135000555'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-10-16T09:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 15, '2026-10-16T09:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-16T11:08:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 16, '2026-10-16T11:20:00', 'Se creó el reclamo REC-20261016112000592'),
(3, 'LOGIN', 'USUARIOS', 3, '2026-10-17T10:08:00', 'Inicio de sesión del usuario verificador'),
(3, 'MODIFICAR', 'VERIFICACION', 16, '2026-10-17T10:20:00', 'El reclamo pasó al estado ''Verificado''.'),
(6, 'LOGIN', 'USUARIOS', 6, '2026-10-20T11:18:00', 'Inicio de sesión del usuario gerente'),
(6, 'CREAR', 'PARTIDAS_PRESUPUESTARIAS', 3, '2026-10-20T11:30:00', 'Se creó una partida presupuestaria.'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-21T08:53:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 17, '2026-10-21T09:05:00', 'Se creó el reclamo REC-20261021090500629'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-22T17:23:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 18, '2026-10-22T17:35:00', 'Se creó el reclamo REC-20261022173500666'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-23T09:58:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 19, '2026-10-23T10:10:00', 'Se creó el reclamo REC-20261023101000703'),
(2, 'LOGIN', 'USUARIOS', 2, '2026-10-25T12:28:00', 'Inicio de sesión del usuario operador'),
(2, 'CREAR', 'RECLAMOS', 20, '2026-10-25T12:40:00', 'Se creó el reclamo REC-20261025124000740');
GO

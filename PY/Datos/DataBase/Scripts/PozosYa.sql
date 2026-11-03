/*

=========================================================
                    POZOS YA
=========================================================


1 - CREACIÓN DE LA BASE DE DATOS
*/
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


-------- INSERT --------

-- ROLES
INSERT INTO roles(nombre,descripcion,activo)
VALUES
('SysAdmin','Administrador del sistema',1),

('Operador',
'Operador del Contact Center',
1),

('Verificador',
'Verificador de reclamos',
1),

('Estimador',
'Responsable de presupuestos',
1),

('Priorizador',
'Responsable de priorización',
1),

('Gerente',
'Gerente de Área',
1);



--------------------------------------------------
-- ESTADOS
--------------------------------------------------

INSERT INTO detalleClaveValor(tabla,clave,valor)
VALUES

('ESTADO',
'INICIADO',
'Iniciado'),

('ESTADO',
'VERIFICADO',
'Verificado'),

('ESTADO',
'PRESUPUESTADO',
'Presupuestado'),

('ESTADO',
'PRIORIZADO',
'Priorizado'),

('ESTADO',
'EN_EJECUCION',
'En Ejecución'),

('ESTADO',
'FINALIZADO',
'Finalizado'),

('ESTADO',
'CANCELADO',
'Cancelado');

--------------------------------------------------
-- SEVERIDADES
--------------------------------------------------

INSERT INTO detalleClaveValor(tabla,clave,valor)
VALUES

('SEVERIDAD',
'BAJA',
'Baja'),

('SEVERIDAD',
'MEDIA',
'Media'),

('SEVERIDAD',
'ALTA',
'Alta');


--------------------------------------------------
-- TIPOS DE DAÑO
--------------------------------------------------

INSERT INTO detalleClaveValor(tabla,clave,valor)
VALUES

('TIPO_DANIO',
'BACHE',
'Bache'),

('TIPO_DANIO',
'LUMINARIA',
'Luminaria'),

('TIPO_DANIO',
'ARBOL',
'Árbol caído'),

('TIPO_DANIO',
'CORDON',
'Cordón cuneta'),

('TIPO_DANIO',
'SUMIDERO',
'Sumidero obstruido');


--------------------------------------------------
-- BARRIOS
--------------------------------------------------

INSERT INTO detalleClaveValor(tabla,clave,valor)
VALUES

('BARRIO','BANFIELD','Banfield'),
('BARRIO','TEMPERLEY','Temperley'),
('BARRIO','LOMAS','Lomas de Zamora'),
('BARRIO','TURDERA','Turdera'),
('BARRIO','LLAVALLOL','Llavallol'),
('BARRIO','SAN JOSE','San Jose'),
('BARRIO','VILLA FIORITO','Villa Fiorito'),
('BARRIO','VILLA CENTENARIO','Villa Centenario'),
('BARRIO','INGENIERO BUDGE','Ingeniero Budge'),
('BARRIO','VILLA ALBERTINA','Villa Albertina'),
('BARRIO','SANTA CATALINA','Santa Catalina'),
('BARRIO','PARQUE BARON','Parque Baron'),
('BARRIO','VIILLA LAMADRID','Villa Lamadrid'),
('BARRIO','SANTA MARTA','Santa Marta');

--------------------------------------------------
-- USUARIO SYSADMIN
--------------------------------------------------
-- MIRAR LA CONTRASEÑAAAAAAAAAAAAAAAAAAAAAA
INSERT INTO usuarios(
nombre,
apellido,
username,
email,
password_hash,
activo,
fecha_creacion
)

VALUES(

'Sistema',
'Administrador',
'admin',
'admin@pozosya.com',
'admin123',
1,
GETDATE()

);



--------------------------------------------------
-- VECINOS
--------------------------------------------------

INSERT INTO vecinos
(
    nombre,
    apellido,
    dni,
    telefono,
    mail,
    direccion,
    nro_catastral,
    barrio_id,
    fecha_registro,
    activo
)
VALUES
(
    'Juan',
    'Pérez',
    '30111222',
    '1122334455',
    'juan@gmail.com',
    'Av. Hipólito Yrigoyen 1234',
    'CAT-001',
    15,
    GETDATE(),
    1
),
(
    'María',
    'Gómez',
    '28999888',
    '1166778899',
    'maria@gmail.com',
    'Las Heras 450',
    'CAT-002',
    16,
    GETDATE(),
    1
),
(
    'Carlos',
    'Rodríguez',
    '32444555',
    '1145678912',
    'carlos@gmail.com',
    'Belgrano 890',
    'CAT-003',
    17,
    GETDATE(),
    1
),
(
    'Ana',
    'Fernández',
    '35123456',
    '1134567890',
    'ana@gmail.com',
    'España 220',
    'CAT-004',
    18,
    GETDATE(),
    1
),
(
    'Lucía',
    'Martínez',
    '37888999',
    '1199988877',
    'lucia@gmail.com',
    'French 1050',
    'CAT-005',
    19,
    GETDATE(),
    1
);

--------------------------------------------------
-- RECLAMOS
--------------------------------------------------

INSERT INTO reclamos
(
    numero_reclamo,
    vecino_id,
    tipo_danio_id,
    severidad_id,
    estado_actual_id,
    descripcion,
    direccion,
    latitud,
    longitud,
    barrio_id,
    usuario_creador_id,
    fecha_creacion
)
VALUES
(
    'REC-000001',
    1,
    10,
    9,
    1,
    'Bache de gran tamaño en la calzada.',
    'Av. Hipólito Yrigoyen 1234',
    -34.761200,
    -58.401500,
    15,
    1,
    GETDATE()
),
(
    'REC-000002',
    2,
    11,
    8,
    2,
    'Luminaria apagada desde hace varios días.',
    'Las Heras 450',
    -34.763100,
    -58.403000,
    16,
    1,
    GETDATE()
),
(
    'REC-000003',
    3,
    12,
    7,
    3,
    'Árbol caído obstruyendo parcialmente la calle.',
    'Belgrano 890',
    -34.759800,
    -58.399600,
    17,
    1,
    GETDATE()
),
(
    'REC-000004',
    4,
    13,
    8,
    4,
    'Cordón cuneta deteriorado.',
    'España 220',
    -34.758700,
    -58.404100,
    18,
    1,
    GETDATE()
),
(
    'REC-000005',
    5,
    14,
    9,
    5,
    'Sumidero completamente obstruido.',
    'French 1050',
    -34.760900,
    -58.400800,
    19,
    1,
    GETDATE()
);


--
-- Permisos
---
-- SEGURIDAD


INSERT INTO permisos(nombre, descripcion) VALUES
('SEGURIDAD_LOGIN','Acceder al sistema'),
('SEGURIDAD_RECUPERAR_ACCESO','Recuperar contraseña'),
('SEGURIDAD_CERRAR_SESION','Cerrar sesión'),
('SEGURIDAD_VER','Acceder al módulo Seguridad');

-- RECLAMOS
INSERT INTO permisos(nombre, descripcion) VALUES
('RECLAMOS_VER','Ver reclamos'),
('RECLAMOS_CREAR','Crear reclamos'),
('RECLAMOS_BUSCAR','Buscar reclamos'),
('RECLAMOS_VER_DETALLE','Ver detalle de reclamo'),
('RECLAMOS_EDITAR','Editar reclamo'),
('RECLAMOS_GUARDAR','Guardar reclamo'),
('RECLAMOS_CANCELAR','Cancelar edición'),
('RECLAMOS_VER_HISTORIAL','Ver historial');

-- NUEVOS RECLAMOS
INSERT INTO permisos(nombre, descripcion) VALUES
('NUEVOS_RECLAMOS_GUARDAR','Guardar nuevo reclamo'),
('NUEVOS_RECLAMOS_CANCELAR','Cancelar alta');

-- PRIORIZACIÓN
INSERT INTO permisos(nombre, descripcion) VALUES
('PRIORIZACION_VER','Ver priorización'),
('PRIORIZACION_BUSCAR','Buscar reclamos'),
('PRIORIZACION_SELECCIONAR_TRIMESTRE','Seleccionar trimestre'),
('PRIORIZACION_SELECCIONAR_RECLAMOS','Seleccionar reclamos'),
('PRIORIZACION_ORDENAR','Ordenar priorización'),
('PRIORIZACION_PROCESAR','Procesar priorización');

-- VERIFICACIÓN
INSERT INTO permisos(nombre, descripcion) VALUES
('VERIFICACION_VER','Ver verificaciones'),
('VERIFICACION_SUBIR_EVIDENCIA','Subir evidencia'),
('VERIFICACION_MARCAR_INICIADO','Marcar iniciado'),
('VERIFICACION_MARCAR_EN_EJECUCION','Marcar en ejecución'),
('VERIFICACION_MARCAR_FINALIZADO','Marcar finalizado'),
('VERIFICACION_CANCELAR_RECLAMO','Cancelar reclamo');

-- PRESUPUESTO
INSERT INTO permisos(nombre, descripcion) VALUES
('PRESUPUESTO_VER','Ver presupuesto'),
('PRESUPUESTO_MODIFICAR','Modificar presupuesto'),
('PRESUPUESTO_GUARDAR','Guardar presupuesto'),
('PRESUPUESTO_SELECCIONAR_PROVEEDOR','Seleccionar proveedor'),
('PRESUPUESTO_CARGAR_MONTO','Cargar monto'),
('PRESUPUESTO_ADJUNTAR_ARCHIVO','Adjuntar archivo'),
('PRESUPUESTO_CONFIRMAR','Confirmar presupuesto'),
('PRESUPUESTO_CANCELAR_RECLAMO','Cancelar reclamo');

-- PARTIDAS PRESUPUESTARIAS
INSERT INTO permisos(nombre, descripcion) VALUES
('PARTIDAS_PRESUPUESTARIAS_VER','Ver partidas'),
('PARTIDAS_PRESUPUESTARIAS_CREAR','Crear partida'),
('PARTIDAS_PRESUPUESTARIAS_GUARDAR','Guardar partida');

-- PROVEEDORES
INSERT INTO permisos(nombre, descripcion) VALUES
('PROVEEDORES_VER','Ver proveedores'),
('PROVEEDORES_BUSCAR','Buscar proveedores'),
('PROVEEDORES_CREAR','Crear proveedor'),
('PROVEEDORES_EDITAR','Editar proveedor'),
('PROVEEDORES_GUARDAR','Guardar proveedor');

-- CONFIGURACIÓN
INSERT INTO permisos(nombre, descripcion) VALUES
('CONFIGURACION_VER','Ver configuración'),
('CONFIGURACION_BUSCAR','Buscar configuración'),
('CONFIGURACION_CREAR','Crear configuración'),
('CONFIGURACION_EDITAR','Editar configuración'),
('CONFIGURACION_GUARDAR','Guardar configuración');

-- USUARIOS
INSERT INTO permisos(nombre, descripcion) VALUES
('USUARIOS_VER','Ver usuarios'),
('USUARIOS_BUSCAR','Buscar usuarios'),
('USUARIOS_EDITAR','Editar usuario'),
('USUARIOS_GUARDAR','Guardar usuario'),
('USUARIOS_CAMBIAR_CONTRASENA','Cambiar contraseña'),
('USUARIOS_ASIGNAR_ROLES','Asignar roles'),
('USUARIOS_QUITAR_ROLES','Quitar roles'),
('USUARIOS_ADMINISTRAR_PERMISOS','Administrar permisos'),
('USUARIOS_OTORGAR_PERMISO','Otorgar permiso'),
('USUARIOS_REVOCAR_PERMISO','Revocar permiso');

-- LOGS
INSERT INTO permisos(nombre, descripcion) VALUES
('LOGS_VER','Ver logs'),
('LOGS_BUSCAR','Buscar logs'),
('LOGS_VER_HISTORIAL','Ver historial');

-- ADMINISTRACION SISTEMA
INSERT INTO permisos(nombre, descripcion) VALUES
('ADMINISTRADOR_VER','Ver Administración de sistema'); -- Número 62

-- Lo agregué después, me lo había perdido
INSERT INTO permisos (nombre, descripcion)
VALUES
('USUARIOS_CREAR', 'Crear nuevos usuarios');

INSERT INTO roles_permisos (rol_id, permiso_id)
SELECT 1, permiso_id
FROM permisos
WHERE nombre = 'USUARIOS_CREAR';



INSERT INTO roles_permisos (rol_id, permiso_id)
VALUES

-- ==========================================
-- OPERADOR (rol_id = 2)
-- ==========================================
(2,1),   -- LOGIN
(2,2),   -- RECUPERAR
(2,3),   -- CERRAR SESION

(2,5),   -- RECLAMOS_VER
(2,6),   -- RECLAMOS_CREAR
(2,7),   -- RECLAMOS_BUSCAR
(2,9),   -- RECLAMOS_EDITAR
(2,10),  -- RECLAMOS_GUARDAR
(2,11),  -- RECLAMOS_CANCELAR

(2,13),  -- NUEVOS_RECLAMOS_GUARDAR
(2,14),  -- NUEVOS_RECLAMOS_CANCELAR


-- ==========================================
-- VERIFICADOR (rol_id = 3)
-- ==========================================
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


-- ==========================================
-- ESTIMADOR (rol_id = 4)
-- ==========================================
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


-- ==========================================
-- PRIORIZADOR (rol_id = 5)
-- ==========================================
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


-- ==========================================
-- GERENTE (rol_id = 6)
-- ==========================================
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
(6,60),


-- ==========================================
-- SYSADMIN (rol_id = 1)
-- ==========================================
(1,1),
(1,2),
(1,3),

(1,4),

(1,48),
(1,49),
(1,61),
(1,50),
(1,51),
(1,52),
(1,53),
(1,54),
(1,55),
(1,56),
(1,57),

(1,43),
(1,44),
(1,45),
(1,46),
(1,47),

(1,58),
(1,59),
(1,60),
(1,62);



INSERT INTO configuraciones
(clave, valor, descripcion, tipo)
VALUES
('MUNICIPALIDAD_NOMBRE', 'Municipalidad de Lomas de Zamora', 'Nombre de la municipalidad', 'TEXTO'),
('MUNICIPALIDAD_DIRECCION', 'Alsina 220', 'Dirección de la municipalidad', 'TEXTO'),
('MUNICIPALIDAD_TELEFONO', '11-XXXX-XXXX', 'Teléfono de la municipalidad', 'TEXTO'),
('MUNICIPALIDAD_EMAIL', 'municipalidad@gmail.com', 'Correo electrónico de la municipalidad', 'EMAIL');



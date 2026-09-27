IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ElectricSolutionDB')
BEGIN
    CREATE DATABASE ElectricSolutionDB;
END
GO

USE ElectricSolutionDB;
GO

IF OBJECT_ID('Notificacion', 'U') IS NOT NULL DROP TABLE Notificacion;
IF OBJECT_ID('CorteProgramado', 'U') IS NOT NULL DROP TABLE CorteProgramado;
IF OBJECT_ID('Reporte', 'U') IS NOT NULL DROP TABLE Reporte;
IF OBJECT_ID('Pago', 'U') IS NOT NULL DROP TABLE Pago;
IF OBJECT_ID('Factura', 'U') IS NOT NULL DROP TABLE Factura;
IF OBJECT_ID('Cuenta', 'U') IS NOT NULL DROP TABLE Cuenta;
IF OBJECT_ID('Cliente', 'U') IS NOT NULL DROP TABLE Cliente;
GO

CREATE TABLE Cliente (
    idCliente       INT IDENTITY(1,1) PRIMARY KEY,
    NombreCliente   NVARCHAR(100) NOT NULL,
    Cedula          NVARCHAR(20)  NOT NULL UNIQUE,
    NumeroNiss      NVARCHAR(20)  NOT NULL UNIQUE,
    Correo          NVARCHAR(100) NOT NULL UNIQUE,
    Telefono        NVARCHAR(20)  NULL,
    Direccion       NVARCHAR(200) NULL,
    Contrasena      NVARCHAR(100) NOT NULL,
    Estado          NVARCHAR(20)  NOT NULL DEFAULT 'Activo'
);
GO

CREATE TABLE Cuenta (
    idCuenta         INT IDENTITY(1,1) PRIMARY KEY,
    NumeroCuenta     NVARCHAR(30)  NOT NULL UNIQUE,
    Saldo            DECIMAL(10,2) NOT NULL DEFAULT 0,
    FechaVencimiento DATE          NULL,
    idCliente        INT           NOT NULL,
    FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente)
);
GO

CREATE TABLE Factura (
    idFactura     INT IDENTITY(1,1) PRIMARY KEY,
    Periodo       NVARCHAR(20)  NOT NULL,
    Monto         DECIMAL(10,2) NOT NULL,
    Estado        NVARCHAR(20)  NOT NULL DEFAULT 'Pendiente',
    FechaEmision  DATE          NOT NULL,
    idCuenta      INT           NOT NULL,
    FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
);
GO

CREATE TABLE Pago (
    idPago       INT IDENTITY(1,1) PRIMARY KEY,
    FechaPago    DATETIME      NOT NULL DEFAULT GETDATE(),
    Monto        DECIMAL(10,2) NOT NULL,
    MetodoPago   NVARCHAR(30)  NOT NULL,
    Referencia   NVARCHAR(50)  NULL,
    idFactura    INT           NOT NULL,
    FOREIGN KEY (idFactura) REFERENCES Factura(idFactura)
);
GO

CREATE TABLE Reporte (
    idReporte       INT IDENTITY(1,1) PRIMARY KEY,
    TipoReporte     NVARCHAR(50)  NOT NULL,
    Descripcion     NVARCHAR(300) NOT NULL,
    FechaReporte    DATETIME      NOT NULL DEFAULT GETDATE(),
    Estado          NVARCHAR(30)  NOT NULL DEFAULT 'Recibido',
    idCliente       INT           NOT NULL,
    FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente)
);
GO

CREATE TABLE CorteProgramado (
    idCorte      INT IDENTITY(1,1) PRIMARY KEY,
    Fecha        DATE         NOT NULL,
    HoraInicio   NVARCHAR(10) NOT NULL,
    HoraFin      NVARCHAR(10) NOT NULL,
    Zona         NVARCHAR(100) NOT NULL,
    Descripcion  NVARCHAR(200) NULL
);
GO

CREATE TABLE Notificacion (
    idNotificacion  INT IDENTITY(1,1) PRIMARY KEY,
    Mensaje         NVARCHAR(300) NOT NULL,
    FechaEnvio      DATETIME      NOT NULL DEFAULT GETDATE(),
    Leida           BIT           NOT NULL DEFAULT 0,
    idCliente       INT           NOT NULL,
    FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente)
);
GO


INSERT INTO Cliente (NombreCliente, Cedula, NumeroNiss, Correo, Telefono, Direccion, Contrasena, Estado)
VALUES
('Andy Bermúdez', '001-010100-0001A', 'NISS001', 'andy@test.com', '8888-1111', 'Managua, Bello Horizonte', '1234', 'Activo'),
('Jonathan Josse', '001-020200-0002B', 'NISS002', 'jonathan@test.com', '8888-2222', 'Managua, Altamira', '1234', 'Activo'),
('Alejandro Betanco', '001-030300-0003C', 'NISS003', 'alejandro@test.com', '8888-3333', 'Managua, Las Colinas', '1234', 'Activo');
GO

INSERT INTO Cuenta (NumeroCuenta, Saldo, FechaVencimiento, idCliente)
VALUES
('CTA-0001', 1250.50, '2026-10-15', 1),
('CTA-0002', 0.00,    '2026-10-15', 2),
('CTA-0003', 780.25,  '2026-10-15', 3);
GO

INSERT INTO Factura (Periodo, Monto, Estado, FechaEmision, idCuenta)
VALUES
('Septiembre 2026', 1250.50, 'Pendiente', '2026-09-01', 1),
('Agosto 2026',      1100.00, 'Pagada',    '2026-08-01', 1),
('Septiembre 2026',     0.00, 'Pagada',    '2026-09-01', 2),
('Septiembre 2026',   780.25, 'Pendiente', '2026-09-01', 3);
GO

INSERT INTO Pago (Monto, MetodoPago, Referencia, idFactura)
VALUES
(1100.00, 'Tarjeta',  'REF-0001', 2),
(0.00,    'Efectivo', 'REF-0002', 3);
GO

INSERT INTO Reporte (TipoReporte, Descripcion, Estado, idCliente)
VALUES
('Avería', 'Corte de energía en el sector', 'Recibido',   1),
('Avería', 'Poste caído en la cuadra',      'En proceso', 3);
GO

INSERT INTO Notificacion (Mensaje, Leida, idCliente)
VALUES
('Su factura de Septiembre está disponible.', 0, 1),
('Corte programado en su zona el 20/09/2026.', 0, 1),
('Su pago fue registrado correctamente.',      1, 2);
GO

INSERT INTO CorteProgramado (Fecha, HoraInicio, HoraFin, Zona, Descripcion)
VALUES
('2026-09-20', '08:00', '12:00', 'Bello Horizonte', 'Mantenimiento de red'),
('2026-09-22', '13:00', '17:00', 'Altamira',        'Cambio de transformador');
GO

PRINT 'Base de datos ElectricSolutionDB creada correctamente con datos de prueba.';

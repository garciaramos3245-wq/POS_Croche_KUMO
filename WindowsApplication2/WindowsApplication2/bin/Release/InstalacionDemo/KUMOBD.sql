-- Prepara la base de datos KUMO en una instancia local de SQL Server Express.
-- Es seguro volver a ejecutar este archivo: conserva los datos existentes.

IF DB_ID(N'KUMOBD') IS NULL
BEGIN
    EXEC(N'CREATE DATABASE [KUMOBD]');
END;
GO

USE [KUMOBD];
GO

IF OBJECT_ID(N'dbo.CATEGORÍA', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[CATEGORÍA] (
        Id_Categoria INT IDENTITY(1,1) PRIMARY KEY,
        NombreCat VARCHAR(50) NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.PRODUCTO', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PRODUCTO (
        Id_Producto INT IDENTITY(1,1) PRIMARY KEY,
        NombrePr VARCHAR(30) NOT NULL,
        Precio DECIMAL(10,2) NOT NULL,
        Id_Categoria INT NULL,
        CONSTRAINT FK_CATEGORIA_PRODUCTO FOREIGN KEY (Id_Categoria)
            REFERENCES dbo.[CATEGORÍA](Id_Categoria)
    );
END;
GO

IF OBJECT_ID(N'dbo.INVENTARIO', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.INVENTARIO (
        Id_Inv INT IDENTITY(1,1) PRIMARY KEY,
        cant_disp INT NULL,
        Id_Producto INT NULL,
        CONSTRAINT FK_INVENTARIO_PRODUCTO FOREIGN KEY (Id_Producto)
            REFERENCES dbo.PRODUCTO(Id_Producto)
    );
END;
GO

IF OBJECT_ID(N'dbo.CLIENTES', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.CLIENTES (
        ID_CLIENTE INT IDENTITY(1,1) PRIMARY KEY,
        Nombres_cl VARCHAR(50) NULL,
        Apellidos VARCHAR(50) NULL,
        Telefono VARCHAR(15) NULL,
        Fecha_Reg DATETIME NULL CONSTRAINT DF_CLIENTES_FECHA DEFAULT GETDATE()
    );
END;
GO

IF OBJECT_ID(N'dbo.PEDIDOS', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PEDIDOS (
        Id_Pedido INT IDENTITY(1,1) PRIMARY KEY,
        ID_CLIENTE INT NOT NULL,
        Fecha DATETIME NULL CONSTRAINT DF_PEDIDOS_FECHA DEFAULT GETDATE(),
        Total DECIMAL(10,2) NULL,
        MetodoPago VARCHAR(30) NULL,
        Subtotal DECIMAL(10,2) NULL,
        Descuento DECIMAL(10,2) NULL,
        BaseGravable DECIMAL(10,2) NULL,
        IVA DECIMAL(10,2) NULL,
        TasaIVA DECIMAL(5,2) NULL,
        PagoCon DECIMAL(10,2) NULL,
        Cambio DECIMAL(10,2) NULL,
        DescripcionPedido NVARCHAR(MAX) NULL,
        Colores NVARCHAR(MAX) NULL,
        Medidas NVARCHAR(MAX) NULL,
        Notas NVARCHAR(MAX) NULL,
        Anticipo DECIMAL(10,2) NULL CONSTRAINT DF_PEDIDOS_ANTICIPO DEFAULT 0,
        Saldo DECIMAL(10,2) NULL CONSTRAINT DF_PEDIDOS_SALDO DEFAULT 0,
        Cancelada BIT NULL CONSTRAINT DF_PEDIDOS_CANCELADA DEFAULT 0,
        FechaCancelacion DATETIME NULL,
        MotivoCancelacion NVARCHAR(200) NULL,
        CONSTRAINT FK_PEDIDOS_CLIENTES FOREIGN KEY (ID_CLIENTE)
            REFERENCES dbo.CLIENTES(ID_CLIENTE)
    );
END;
GO

IF COL_LENGTH('dbo.PEDIDOS', 'Subtotal') IS NULL ALTER TABLE dbo.PEDIDOS ADD Subtotal DECIMAL(10,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'Descuento') IS NULL ALTER TABLE dbo.PEDIDOS ADD Descuento DECIMAL(10,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'BaseGravable') IS NULL ALTER TABLE dbo.PEDIDOS ADD BaseGravable DECIMAL(10,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'IVA') IS NULL ALTER TABLE dbo.PEDIDOS ADD IVA DECIMAL(10,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'TasaIVA') IS NULL ALTER TABLE dbo.PEDIDOS ADD TasaIVA DECIMAL(5,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'MetodoPago') IS NULL ALTER TABLE dbo.PEDIDOS ADD MetodoPago VARCHAR(30) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'PagoCon') IS NULL ALTER TABLE dbo.PEDIDOS ADD PagoCon DECIMAL(10,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'Cambio') IS NULL ALTER TABLE dbo.PEDIDOS ADD Cambio DECIMAL(10,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'DescripcionPedido') IS NULL ALTER TABLE dbo.PEDIDOS ADD DescripcionPedido NVARCHAR(MAX) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'Colores') IS NULL ALTER TABLE dbo.PEDIDOS ADD Colores NVARCHAR(MAX) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'Medidas') IS NULL ALTER TABLE dbo.PEDIDOS ADD Medidas NVARCHAR(MAX) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'Notas') IS NULL ALTER TABLE dbo.PEDIDOS ADD Notas NVARCHAR(MAX) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'Anticipo') IS NULL ALTER TABLE dbo.PEDIDOS ADD Anticipo DECIMAL(10,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'Saldo') IS NULL ALTER TABLE dbo.PEDIDOS ADD Saldo DECIMAL(10,2) NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'Cancelada') IS NULL ALTER TABLE dbo.PEDIDOS ADD Cancelada BIT NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'FechaCancelacion') IS NULL ALTER TABLE dbo.PEDIDOS ADD FechaCancelacion DATETIME NULL;
IF COL_LENGTH('dbo.PEDIDOS', 'MotivoCancelacion') IS NULL ALTER TABLE dbo.PEDIDOS ADD MotivoCancelacion NVARCHAR(200) NULL;
GO

IF COL_LENGTH('dbo.PEDIDOS', 'MetodoPago') IS NOT NULL ALTER TABLE dbo.PEDIDOS ALTER COLUMN MetodoPago VARCHAR(30) NULL;
GO

UPDATE dbo.PEDIDOS SET Subtotal = ISNULL(Subtotal, Total) WHERE Subtotal IS NULL;
UPDATE dbo.PEDIDOS SET Descuento = 0 WHERE Descuento IS NULL;
UPDATE dbo.PEDIDOS SET IVA = 0 WHERE IVA IS NULL;
UPDATE dbo.PEDIDOS SET BaseGravable = ISNULL(Total, 0) - ISNULL(IVA, 0) WHERE BaseGravable IS NULL;
UPDATE dbo.PEDIDOS SET TasaIVA = 0 WHERE TasaIVA IS NULL;
UPDATE dbo.PEDIDOS SET MetodoPago = 'Efectivo' WHERE MetodoPago IS NULL OR MetodoPago = '';
UPDATE dbo.PEDIDOS SET PagoCon = ISNULL(Total, 0) WHERE PagoCon IS NULL;
UPDATE dbo.PEDIDOS SET Cambio = 0 WHERE Cambio IS NULL;
UPDATE dbo.PEDIDOS SET Anticipo = 0 WHERE Anticipo IS NULL;
UPDATE dbo.PEDIDOS SET Saldo = ISNULL(Total, 0) - ISNULL(Anticipo, 0) WHERE Saldo IS NULL;
UPDATE dbo.PEDIDOS SET Cancelada = 0 WHERE Cancelada IS NULL;
GO

IF OBJECT_ID(N'dbo.DET_PEDIDOS', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DET_PEDIDOS (
        Id_Detalle INT IDENTITY(1,1) PRIMARY KEY,
        Id_Pedido INT NOT NULL,
        Id_Producto INT NOT NULL,
        Cantidad INT NOT NULL,
        PrecioVentaMomento DECIMAL(10,2) NOT NULL,
        CONSTRAINT FK_DETALLE_PEDIDO FOREIGN KEY (Id_Pedido)
            REFERENCES dbo.PEDIDOS(Id_Pedido),
        CONSTRAINT FK_DETALLE_PRODUCTO FOREIGN KEY (Id_Producto)
            REFERENCES dbo.PRODUCTO(Id_Producto)
    );
END;
GO

IF OBJECT_ID(N'dbo.VENTAS', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.VENTAS (
        Id_Venta INT IDENTITY(1,1) PRIMARY KEY,
        Fecha DATETIME NULL CONSTRAINT DF_VENTAS_FECHA DEFAULT GETDATE(),
        LugarBazar VARCHAR(100) NULL,
        Id_Producto INT NOT NULL,
        Id_Cliente INT NULL CONSTRAINT DF_VENTAS_CLIENTE DEFAULT 1,
        Cantidad INT NOT NULL,
        PrecioVenta DECIMAL(10,2) NULL,
        CONSTRAINT FK_VENTA_PRODUCTO FOREIGN KEY (Id_Producto)
            REFERENCES dbo.PRODUCTO(Id_Producto),
        CONSTRAINT FK_VENTA_CLIENTE FOREIGN KEY (Id_Cliente)
            REFERENCES dbo.CLIENTES(ID_CLIENTE)
    );
END;
GO

INSERT INTO dbo.[CATEGORÍA] (NombreCat)
SELECT datos.NombreCat
FROM (VALUES ('Amigurumis'), ('Accesorios'), ('Decoracion'), ('Hilos')) AS datos(NombreCat)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.[CATEGORÍA] c WHERE c.NombreCat = datos.NombreCat
);

IF NOT EXISTS (
    SELECT 1 FROM dbo.CLIENTES WHERE Nombres_cl = 'Publico' AND ISNULL(Apellidos, '') = 'General'
)
BEGIN
    INSERT INTO dbo.CLIENTES (Nombres_cl, Apellidos, Telefono)
    VALUES ('Publico', 'General', '');
END;

DECLARE @Productos TABLE (
    NombrePr VARCHAR(30),
    Precio DECIMAL(10,2),
    Categoria VARCHAR(50),
    Existencia INT
);

INSERT INTO @Productos (NombrePr, Precio, Categoria, Existencia)
VALUES
    ('Oso mini', 120.00, 'Amigurumis', 36),
    ('Bufanda', 60.00, 'Accesorios', 47),
    ('Portavasos', 65.00, 'Decoracion', 34),
    ('Ramo', 1000.00, 'Decoracion', 71),
    ('Hilo color amarillo', 15.00, 'Hilos', 10),
    ('Hilo color blanco', 15.00, 'Hilos', 20);

INSERT INTO dbo.PRODUCTO (NombrePr, Precio, Id_Categoria)
SELECT datos.NombrePr, datos.Precio, categoria.Id_Categoria
FROM @Productos datos
INNER JOIN dbo.[CATEGORÍA] categoria ON categoria.NombreCat = datos.Categoria
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.PRODUCTO producto WHERE producto.NombrePr = datos.NombrePr
);

INSERT INTO dbo.INVENTARIO (cant_disp, Id_Producto)
SELECT datos.Existencia, producto.Id_Producto
FROM @Productos datos
INNER JOIN dbo.PRODUCTO producto ON producto.NombrePr = datos.NombrePr
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.INVENTARIO inventario WHERE inventario.Id_Producto = producto.Id_Producto
);
GO

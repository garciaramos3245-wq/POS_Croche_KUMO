-- Esquema SQLite portable de KUMO POS.
-- Este archivo se ejecuta al primer inicio y conserva los registros existentes.

PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS [CATEGORÍA] (
    Id_Categoria INTEGER PRIMARY KEY AUTOINCREMENT,
    NombreCat TEXT NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS PRODUCTO (
    Id_Producto INTEGER PRIMARY KEY AUTOINCREMENT,
    NombrePr TEXT NOT NULL,
    Precio NUMERIC NOT NULL,
    Id_Categoria INTEGER NULL,
    FOREIGN KEY (Id_Categoria) REFERENCES [CATEGORÍA](Id_Categoria)
);

CREATE TABLE IF NOT EXISTS INVENTARIO (
    Id_Inv INTEGER PRIMARY KEY AUTOINCREMENT,
    cant_disp INTEGER NULL,
    Id_Producto INTEGER NULL,
    FOREIGN KEY (Id_Producto) REFERENCES PRODUCTO(Id_Producto)
);

CREATE TABLE IF NOT EXISTS CLIENTES (
    ID_CLIENTE INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombres_cl TEXT NULL,
    Apellidos TEXT NULL,
    Telefono TEXT NULL,
    Fecha_Reg TEXT DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS PEDIDOS (
    Id_Pedido INTEGER PRIMARY KEY AUTOINCREMENT,
    ID_CLIENTE INTEGER NOT NULL,
    Fecha TEXT DEFAULT CURRENT_TIMESTAMP,
    Total NUMERIC NULL,
    MetodoPago TEXT NULL,
    Subtotal NUMERIC NULL,
    Descuento NUMERIC NULL,
    BaseGravable NUMERIC NULL,
    IVA NUMERIC NULL,
    TasaIVA NUMERIC NULL,
    PagoCon NUMERIC NULL,
    Cambio NUMERIC NULL,
    DescripcionPedido TEXT NULL,
    Colores TEXT NULL,
    Medidas TEXT NULL,
    Notas TEXT NULL,
    Anticipo NUMERIC DEFAULT 0,
    Saldo NUMERIC DEFAULT 0,
    Cancelada INTEGER DEFAULT 0,
    FechaCancelacion TEXT NULL,
    MotivoCancelacion TEXT NULL,
    FOREIGN KEY (ID_CLIENTE) REFERENCES CLIENTES(ID_CLIENTE)
);

CREATE TABLE IF NOT EXISTS DET_PEDIDOS (
    Id_Detalle INTEGER PRIMARY KEY AUTOINCREMENT,
    Id_Pedido INTEGER NOT NULL,
    Id_Producto INTEGER NOT NULL,
    Cantidad INTEGER NOT NULL,
    PrecioVentaMomento NUMERIC NOT NULL,
    FOREIGN KEY (Id_Pedido) REFERENCES PEDIDOS(Id_Pedido),
    FOREIGN KEY (Id_Producto) REFERENCES PRODUCTO(Id_Producto)
);

CREATE TABLE IF NOT EXISTS VENTAS (
    Id_Venta INTEGER PRIMARY KEY AUTOINCREMENT,
    Fecha TEXT DEFAULT CURRENT_TIMESTAMP,
    LugarBazar TEXT NULL,
    Id_Producto INTEGER NOT NULL,
    Id_Cliente INTEGER DEFAULT 1,
    Cantidad INTEGER NOT NULL,
    PrecioVenta NUMERIC NULL,
    FOREIGN KEY (Id_Producto) REFERENCES PRODUCTO(Id_Producto),
    FOREIGN KEY (Id_Cliente) REFERENCES CLIENTES(ID_CLIENTE)
);

INSERT INTO [CATEGORÍA] (NombreCat)
SELECT 'Amigurumis'
WHERE NOT EXISTS (SELECT 1 FROM [CATEGORÍA] WHERE NombreCat = 'Amigurumis');
INSERT INTO [CATEGORÍA] (NombreCat)
SELECT 'Accesorios'
WHERE NOT EXISTS (SELECT 1 FROM [CATEGORÍA] WHERE NombreCat = 'Accesorios');
INSERT INTO [CATEGORÍA] (NombreCat)
SELECT 'Decoracion'
WHERE NOT EXISTS (SELECT 1 FROM [CATEGORÍA] WHERE NombreCat = 'Decoracion');
INSERT INTO [CATEGORÍA] (NombreCat)
SELECT 'Hilos'
WHERE NOT EXISTS (SELECT 1 FROM [CATEGORÍA] WHERE NombreCat = 'Hilos');

INSERT INTO CLIENTES (Nombres_cl, Apellidos, Telefono)
SELECT 'Publico', 'General', ''
WHERE NOT EXISTS (
    SELECT 1 FROM CLIENTES WHERE Nombres_cl = 'Publico' AND IFNULL(Apellidos, '') = 'General'
);

INSERT INTO PRODUCTO (NombrePr, Precio, Id_Categoria)
SELECT 'Oso mini', 120.00, Id_Categoria FROM [CATEGORÍA] WHERE NombreCat = 'Amigurumis'
AND NOT EXISTS (SELECT 1 FROM PRODUCTO WHERE NombrePr = 'Oso mini');
INSERT INTO PRODUCTO (NombrePr, Precio, Id_Categoria)
SELECT 'Bufanda', 60.00, Id_Categoria FROM [CATEGORÍA] WHERE NombreCat = 'Accesorios'
AND NOT EXISTS (SELECT 1 FROM PRODUCTO WHERE NombrePr = 'Bufanda');
INSERT INTO PRODUCTO (NombrePr, Precio, Id_Categoria)
SELECT 'Portavasos', 65.00, Id_Categoria FROM [CATEGORÍA] WHERE NombreCat = 'Decoracion'
AND NOT EXISTS (SELECT 1 FROM PRODUCTO WHERE NombrePr = 'Portavasos');
INSERT INTO PRODUCTO (NombrePr, Precio, Id_Categoria)
SELECT 'Ramo', 1000.00, Id_Categoria FROM [CATEGORÍA] WHERE NombreCat = 'Decoracion'
AND NOT EXISTS (SELECT 1 FROM PRODUCTO WHERE NombrePr = 'Ramo');
INSERT INTO PRODUCTO (NombrePr, Precio, Id_Categoria)
SELECT 'Hilo color amarillo', 15.00, Id_Categoria FROM [CATEGORÍA] WHERE NombreCat = 'Hilos'
AND NOT EXISTS (SELECT 1 FROM PRODUCTO WHERE NombrePr = 'Hilo color amarillo');
INSERT INTO PRODUCTO (NombrePr, Precio, Id_Categoria)
SELECT 'Hilo color blanco', 15.00, Id_Categoria FROM [CATEGORÍA] WHERE NombreCat = 'Hilos'
AND NOT EXISTS (SELECT 1 FROM PRODUCTO WHERE NombrePr = 'Hilo color blanco');

INSERT INTO INVENTARIO (cant_disp, Id_Producto)
SELECT 36, Id_Producto FROM PRODUCTO WHERE NombrePr = 'Oso mini'
AND NOT EXISTS (SELECT 1 FROM INVENTARIO WHERE Id_Producto = PRODUCTO.Id_Producto);
INSERT INTO INVENTARIO (cant_disp, Id_Producto)
SELECT 47, Id_Producto FROM PRODUCTO WHERE NombrePr = 'Bufanda'
AND NOT EXISTS (SELECT 1 FROM INVENTARIO WHERE Id_Producto = PRODUCTO.Id_Producto);
INSERT INTO INVENTARIO (cant_disp, Id_Producto)
SELECT 34, Id_Producto FROM PRODUCTO WHERE NombrePr = 'Portavasos'
AND NOT EXISTS (SELECT 1 FROM INVENTARIO WHERE Id_Producto = PRODUCTO.Id_Producto);
INSERT INTO INVENTARIO (cant_disp, Id_Producto)
SELECT 71, Id_Producto FROM PRODUCTO WHERE NombrePr = 'Ramo'
AND NOT EXISTS (SELECT 1 FROM INVENTARIO WHERE Id_Producto = PRODUCTO.Id_Producto);
INSERT INTO INVENTARIO (cant_disp, Id_Producto)
SELECT 10, Id_Producto FROM PRODUCTO WHERE NombrePr = 'Hilo color amarillo'
AND NOT EXISTS (SELECT 1 FROM INVENTARIO WHERE Id_Producto = PRODUCTO.Id_Producto);
INSERT INTO INVENTARIO (cant_disp, Id_Producto)
SELECT 20, Id_Producto FROM PRODUCTO WHERE NombrePr = 'Hilo color blanco'
AND NOT EXISTS (SELECT 1 FROM INVENTARIO WHERE Id_Producto = PRODUCTO.Id_Producto);

-- Crea la base de datos principal del sistema KUMO.
CREATE DATABASE KUMOBD

-- Selecciona la base para crear todas las tablas siguientes.
USE KUMOBD 

-- Guarda las categorias comerciales de los productos.
CREATE TABLE CATEGORÍA(
Id_Categoria int identity (1,1) PRIMARY KEY,
NombreCat varchar(50) not null)

-- Inserta las categorias iniciales sin duplicarlas si ya existen.
INSERT INTO CATEGORÍA (NombreCat)
SELECT v.NombreCat
FROM (VALUES
('Amigurumis'),
('Accesorios'),
('Decoracion'),
('Hilos')
) AS v(NombreCat)
WHERE NOT EXISTS (
    SELECT 1
    FROM CATEGORÍA c
    WHERE c.NombreCat = v.NombreCat
)

-- Guarda el catalogo de productos que se venden en caja.
CREATE TABLE PRODUCTO (
Id_Producto INT IDENTITY(1,1) PRIMARY KEY,
NombrePr varchar(30) NOT NULL,
Precio decimal(10,2) not null,
Id_Categoria int,
-- Relaciona cada producto con una categoria existente.
constraint FK_CATEGORÍA
FOREIGN KEY (Id_Categoria) REFERENCES CATEGORÍA(Id_Categoria))

-- Guarda las existencias disponibles por producto.
CREATE TABLE INVENTARIO(
Id_Inv int identity (1,1) PRIMARY KEY,
cant_disp int,
Id_Producto int,
-- Relaciona cada registro de inventario con un producto.
constraint FK_PRODUCTO
FOREIGN KEY (Id_Producto) REFERENCES PRODUCTO(Id_Producto))

-- Guarda los datos basicos de clientes usados en pedidos y ventas.
CREATE TABLE CLIENTES(
ID_CLIENTE INT IDENTITY(1,1) PRIMARY KEY,
Nombres_cl varchar (50),
Apellidos varchar (50),
Telefono varchar (15),
Fecha_Reg DATETIME DEFAULT GETDATE())

-- Guarda el encabezado de pedidos o ventas con cliente, fecha, total y metodo de pago.
CREATE TABLE PEDIDOS(
Id_Pedido INT IDENTITY(1,1) PRIMARY KEY,
    ID_CLIENTE INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2),
    MetodoPago VARCHAR(20),
    DescripcionPedido NVARCHAR(MAX),
    Colores NVARCHAR(MAX),
    Medidas NVARCHAR(MAX),
    Notas NVARCHAR(MAX),
    Anticipo DECIMAL(10,2) DEFAULT 0,
    Saldo DECIMAL(10,2) DEFAULT 0,
    Cancelada BIT DEFAULT 0,
    FechaCancelacion DATETIME NULL,
    MotivoCancelacion NVARCHAR(200),
    -- Relaciona el pedido con el cliente que lo genera.
    CONSTRAINT FK_PEDIDOS_CLIENTES FOREIGN KEY (ID_CLIENTE) 
    REFERENCES CLIENTES(ID_CLIENTE))

-- Guarda el detalle de productos incluidos en cada pedido o venta.
CREATE TABLE DET_PEDIDOS(
Id_Detalle INT IDENTITY(1,1) PRIMARY KEY,
    Id_Pedido INT NOT NULL,
    Id_Producto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioVentaMomento DECIMAL(10,2) NOT NULL, 
    -- Relaciona cada linea con su pedido.
    CONSTRAINT FK_Detalle_Pedido FOREIGN KEY (Id_Pedido) 
    REFERENCES PEDIDOS(Id_Pedido),
    -- Relaciona cada linea con el producto vendido.
    CONSTRAINT FK_Detalle_Producto FOREIGN KEY (Id_Producto) 
    REFERENCES PRODUCTO(Id_Producto))

-- Guarda ventas simples por producto y cliente cuando se usa el flujo de VENTAS.
CREATE TABLE VENTAS(
Id_Venta INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME DEFAULT GETDATE(),
    LugarBazar VARCHAR(100),
    Id_Producto INT NOT NULL,
    Id_Cliente INT DEFAULT 1,
    Cantidad INT NOT NULL,
    PrecioVenta DECIMAL(10,2),  
    -- Relaciona la venta con el producto vendido.
    CONSTRAINT FK_Venta_Producto FOREIGN KEY (Id_Producto) REFERENCES PRODUCTO(Id_Producto),
    -- Relaciona la venta con el cliente registrado.
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (Id_Cliente) REFERENCES CLIENTES(Id_Cliente))

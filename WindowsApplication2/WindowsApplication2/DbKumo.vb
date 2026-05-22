' Centraliza la conexion a SQL Server, las consultas reutilizables y los apoyos para clientes y pagos.

Imports System.Data
Imports System.Data.SqlClient

Module DbKumo

    ' Funciones compartidas para abrir conexiones y ejecutar consultas contra SQL Server.

    ' Crea una conexion nueva usando la cadena configurada en My.Settings.
    Public Function ObtenerConexion() As SqlConnection
        Return New SqlConnection(My.Settings.Con_Croche)
    End Function

    ' Ejecuta una consulta SELECT parametrizada y devuelve los resultados en un DataTable.
    Public Function ObtenerTabla(sql As String, ParamArray parametros() As SqlParameter) As DataTable
        Dim tabla As New DataTable()
        Using cn = ObtenerConexion()
            Using da As New SqlDataAdapter(sql, cn)
                If parametros IsNot Nothing AndAlso parametros.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(parametros)
                End If
                da.Fill(tabla)
            End Using
        End Using
        Return tabla
    End Function

    ' Ejecuta una consulta que devuelve un solo valor, como un conteo o un folio.
    Public Function ObtenerEscalar(sql As String, ParamArray parametros() As SqlParameter) As Object
        Using cn = ObtenerConexion()
            cn.Open()
            Using cmd As New SqlCommand(sql, cn)
                If parametros IsNot Nothing AndAlso parametros.Length > 0 Then
                    cmd.Parameters.AddRange(parametros)
                End If
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    ' Garantiza que PEDIDOS tenga las columnas de subtotal, descuento, IVA y pago que usa la caja.
    Public Sub AsegurarColumnasPagoPedido()
        Using cn = ObtenerConexion()
            cn.Open()
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Subtotal') IS NULL ALTER TABLE PEDIDOS ADD Subtotal DECIMAL(10,2) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Descuento') IS NULL ALTER TABLE PEDIDOS ADD Descuento DECIMAL(10,2) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'BaseGravable') IS NULL ALTER TABLE PEDIDOS ADD BaseGravable DECIMAL(10,2) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'IVA') IS NULL ALTER TABLE PEDIDOS ADD IVA DECIMAL(10,2) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'TasaIVA') IS NULL ALTER TABLE PEDIDOS ADD TasaIVA DECIMAL(5,2) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'MetodoPago') IS NULL ALTER TABLE PEDIDOS ADD MetodoPago VARCHAR(30) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'PagoCon') IS NULL ALTER TABLE PEDIDOS ADD PagoCon DECIMAL(10,2) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Cambio') IS NULL ALTER TABLE PEDIDOS ADD Cambio DECIMAL(10,2) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Cancelada') IS NULL ALTER TABLE PEDIDOS ADD Cancelada BIT NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'FechaCancelacion') IS NULL ALTER TABLE PEDIDOS ADD FechaCancelacion DATETIME NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'MotivoCancelacion') IS NULL ALTER TABLE PEDIDOS ADD MotivoCancelacion NVARCHAR(200) NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET Subtotal = ISNULL(Subtotal, Total) WHERE Subtotal IS NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET Descuento = ISNULL(Descuento, 0) WHERE Descuento IS NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET IVA = ISNULL(IVA, 0) WHERE IVA IS NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET BaseGravable = ISNULL(BaseGravable, Total - ISNULL(IVA, 0)) WHERE BaseGravable IS NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET TasaIVA = ISNULL(TasaIVA, 0) WHERE TasaIVA IS NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET MetodoPago = ISNULL(NULLIF(MetodoPago, ''), 'Efectivo') WHERE MetodoPago IS NULL OR MetodoPago = ''")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET PagoCon = ISNULL(PagoCon, Total) WHERE PagoCon IS NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET Cambio = ISNULL(Cambio, 0) WHERE Cambio IS NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET Cancelada = ISNULL(Cancelada, 0) WHERE Cancelada IS NULL")
        End Using
    End Sub

    ' Garantiza que PEDIDOS tenga los campos completos de la ficha de pedido especial.
    Public Sub AsegurarColumnasDetallePedido()
        Using cn = ObtenerConexion()
            cn.Open()
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'DescripcionPedido') IS NULL ALTER TABLE PEDIDOS ADD DescripcionPedido NVARCHAR(MAX) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Colores') IS NULL ALTER TABLE PEDIDOS ADD Colores NVARCHAR(MAX) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Medidas') IS NULL ALTER TABLE PEDIDOS ADD Medidas NVARCHAR(MAX) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Notas') IS NULL ALTER TABLE PEDIDOS ADD Notas NVARCHAR(MAX) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Anticipo') IS NULL ALTER TABLE PEDIDOS ADD Anticipo DECIMAL(10,2) NULL")
            EjecutarSqlPagoPedido(cn, "IF COL_LENGTH('PEDIDOS', 'Saldo') IS NULL ALTER TABLE PEDIDOS ADD Saldo DECIMAL(10,2) NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET Anticipo = ISNULL(Anticipo, 0) WHERE Anticipo IS NULL")
            EjecutarSqlPagoPedido(cn, "UPDATE PEDIDOS SET Saldo = ISNULL(Saldo, ISNULL(Total, 0) - ISNULL(Anticipo, 0)) WHERE Saldo IS NULL")
        End Using
    End Sub

    ' Ejecuta una instruccion SQL administrativa usando una conexion ya abierta.
    Private Sub EjecutarSqlPagoPedido(cn As SqlConnection, sql As String)
        Using cmd As New SqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' Busca un cliente por nombre y telefono; si no existe, lo crea dentro de la transaccion actual.
    Public Function ObtenerIdCliente(nombreCompleto As String, telefono As String, trans As SqlTransaction) As Integer
        Dim nombres As String = nombreCompleto.Trim()
        Dim apellidos As String = ""

        If nombres = "" Then nombres = "Cliente"

        If nombres.Length > 50 Then
            nombres = nombres.Substring(0, 50)
        End If

        Using cmdBuscar As New SqlCommand(
            "SELECT TOP 1 ID_CLIENTE FROM CLIENTES " &
            "WHERE Nombres_cl = @nombres AND ISNULL(Apellidos,'') = @apellidos AND ISNULL(Telefono,'') = @telefono",
            trans.Connection,
            trans)
            cmdBuscar.Parameters.AddWithValue("@nombres", nombres)
            cmdBuscar.Parameters.AddWithValue("@apellidos", apellidos)
            cmdBuscar.Parameters.AddWithValue("@telefono", If(telefono, ""))

            Dim encontrado = cmdBuscar.ExecuteScalar()
            If encontrado IsNot Nothing Then
                Return CInt(encontrado)
            End If
        End Using

        Using cmdInsertar As New SqlCommand(
            "INSERT INTO CLIENTES (Nombres_cl, Apellidos, Telefono) VALUES (@nombres, @apellidos, @telefono); " &
            "SELECT CAST(SCOPE_IDENTITY() AS INT);",
            trans.Connection,
            trans)
            cmdInsertar.Parameters.AddWithValue("@nombres", nombres)
            cmdInsertar.Parameters.AddWithValue("@apellidos", apellidos)
            cmdInsertar.Parameters.AddWithValue("@telefono", If(telefono, ""))
            Return CInt(cmdInsertar.ExecuteScalar())
        End Using
    End Function

    ' Obtiene o crea el cliente generico Publico General para ventas de mostrador.
    Public Function ObtenerIdClienteGeneral(trans As SqlTransaction) As Integer
        Using cmdBuscar As New SqlCommand(
            "SELECT TOP 1 ID_CLIENTE FROM CLIENTES " &
            "WHERE Nombres_cl = 'Publico' AND ISNULL(Apellidos,'') = 'General'",
            trans.Connection,
            trans)
            Dim encontrado = cmdBuscar.ExecuteScalar()
            If encontrado IsNot Nothing Then
                Return CInt(encontrado)
            End If
        End Using

        Using cmdInsertar As New SqlCommand(
            "INSERT INTO CLIENTES (Nombres_cl, Apellidos, Telefono) VALUES ('Publico', 'General', ''); " &
            "SELECT CAST(SCOPE_IDENTITY() AS INT);",
            trans.Connection,
            trans)
            Return CInt(cmdInsertar.ExecuteScalar())
        End Using
    End Function

End Module

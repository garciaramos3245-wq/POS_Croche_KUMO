' Centraliza la base SQLite local, las consultas reutilizables y los apoyos de clientes y pagos.

Imports System.Data
Imports System.Data.SQLite
Imports System.IO
Imports System.Text

Module DbKumo

    Public Const NombreArchivoBase As String = "KUMO.db"

    Private Function ObtenerDirectorioAplicacion() As String
        Return Path.GetDirectoryName(GetType(DbKumo).Assembly.Location)
    End Function

    ' Devuelve la ruta portable que viaja junto al ejecutable publicado.
    Public Function ObtenerRutaBaseDatos() As String
        Return Path.Combine(ObtenerDirectorioAplicacion(), "Datos", NombreArchivoBase)
    End Function

    ' Crea una conexion SQLite nueva apuntando al archivo local del programa.
    Public Function ObtenerConexion() As SQLiteConnection
        Dim carpeta As String = Path.GetDirectoryName(ObtenerRutaBaseDatos())
        If Not Directory.Exists(carpeta) Then Directory.CreateDirectory(carpeta)

        Dim cadena As New SQLiteConnectionStringBuilder(My.Settings.Con_Croche)
        cadena.DataSource = ObtenerRutaBaseDatos()
        Return New SQLiteConnection(cadena.ConnectionString)
    End Function

    ' Crea la base local y completa el esquema que requiere la aplicacion.
    Public Sub AsegurarBaseDatos()
        Dim archivoEsquema As String = Path.Combine(ObtenerDirectorioAplicacion(), "Datos", "KUMOBD.sql")
        If Not File.Exists(archivoEsquema) Then
            Throw New FileNotFoundException("No se encontro el esquema SQLite de KUMO.", archivoEsquema)
        End If

        Using cn = ObtenerConexion()
            cn.Open()
            Using cmd As New SQLiteCommand(File.ReadAllText(archivoEsquema, Encoding.UTF8), cn)
                cmd.ExecuteNonQuery()
            End Using
            AsegurarColumnasPedidos(cn)
        End Using
    End Sub

    ' Confirma que el archivo local puede abrirse y contiene las tablas principales.
    Public Function ProbarConexionAplicacion(ByRef mensaje As String) As Boolean
        Try
            AsegurarBaseDatos()

            Using cn = ObtenerConexion()
                cn.Open()
                Using cmd As New SQLiteCommand(
                    "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' " &
                    "AND name IN ('PRODUCTO', 'INVENTARIO', 'PEDIDOS', 'DET_PEDIDOS', @categorias)",
                    cn)
                    cmd.Parameters.AddWithValue("@categorias", NombreTablaCategorias())
                    If CInt(cmd.ExecuteScalar()) <> 5 Then
                        mensaje = "La base local existe, pero no tiene las tablas requeridas." & vbCrLf &
                                  "Copia nuevamente la carpeta Release completa."
                        Return False
                    End If
                End Using
            End Using

            mensaje = ""
            Return True
        Catch ex As Exception
            mensaje = CrearMensajeErrorDatos("abrir la base de datos local", ex)
            Return False
        End Try
    End Function

    ' Construye un mensaje legible para fallas del archivo SQLite.
    Public Function CrearMensajeErrorDatos(accion As String, ex As Exception) As String
        Dim real As Exception = ObtenerErrorReal(ex)
        Dim solucion As String

        If TypeOf real Is FileNotFoundException Then
            solucion = "Falta el esquema de la base. Copia la carpeta Release completa."
        ElseIf TypeOf real Is DllNotFoundException OrElse TypeOf real Is TypeLoadException Then
            solucion = "Faltan las DLL de SQLite. Copia System.Data.SQLite.dll y las carpetas x86 y x64."
        ElseIf TypeOf real Is UnauthorizedAccessException OrElse TypeOf real Is IOException Then
            solucion = "No se puede escribir la base. Ejecuta el programa desde una carpeta con permisos."
        ElseIf TypeOf real Is SQLiteException Then
            solucion = "No se pudo leer o actualizar KUMO.db. Cierra otra copia del programa y vuelve a intentar."
        Else
            solucion = "Revisa que los archivos de SQLite viajen junto al ejecutable."
        End If

        Return "No se pudo " & accion & "." & vbCrLf &
               "Base local: Datos\" & NombreArchivoBase & vbCrLf &
               solucion & vbCrLf &
               "Detalle: " & ResumirDetalle(real.Message)
    End Function

    ' Desenvuelve excepciones de tareas asincronas para mostrar la causa real.
    Private Function ObtenerErrorReal(ex As Exception) As Exception
        Dim agregado = TryCast(ex, AggregateException)
        If agregado IsNot Nothing AndAlso agregado.InnerExceptions.Count > 0 Then
            Return ObtenerErrorReal(agregado.Flatten().InnerExceptions(0))
        End If

        If ex.InnerException IsNot Nothing AndAlso Not TypeOf ex Is SQLiteException Then
            Return ObtenerErrorReal(ex.InnerException)
        End If

        Return ex
    End Function

    Private Function ResumirDetalle(detalle As String) As String
        If String.IsNullOrWhiteSpace(detalle) Then Return "Sin detalle adicional."

        Dim limpio As String = detalle.Replace(vbCr, " ").Replace(vbLf, " ").Trim()
        If limpio.Length > 115 Then Return limpio.Substring(0, 112) & "..."
        Return limpio
    End Function

    Private Function NombreTablaCategorias() As String
        Return "CATEGOR" & ChrW(205) & "A"
    End Function

    ' Ejecuta una consulta SELECT parametrizada y devuelve los resultados.
    Public Function ObtenerTabla(sql As String, ParamArray parametros() As SQLiteParameter) As DataTable
        Dim tabla As New DataTable()
        Using cn = ObtenerConexion()
            Using da As New SQLiteDataAdapter(sql, cn)
                If parametros IsNot Nothing AndAlso parametros.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(parametros)
                End If
                da.Fill(tabla)
            End Using
        End Using
        Return tabla
    End Function

    ' Ejecuta una consulta que devuelve un solo valor.
    Public Function ObtenerEscalar(sql As String, ParamArray parametros() As SQLiteParameter) As Object
        Using cn = ObtenerConexion()
            cn.Open()
            Using cmd As New SQLiteCommand(sql, cn)
                If parametros IsNot Nothing AndAlso parametros.Length > 0 Then
                    cmd.Parameters.AddRange(parametros)
                End If
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    ' Garantiza columnas agregadas por las pantallas de cobro y cancelacion.
    Public Sub AsegurarColumnasPagoPedido()
        If Not File.Exists(ObtenerRutaBaseDatos()) Then
            AsegurarBaseDatos()
            Return
        End If

        Using cn = ObtenerConexion()
            cn.Open()
            AsegurarColumnasPedidos(cn)
        End Using
    End Sub

    ' La ficha de pedidos utiliza el mismo conjunto de columnas del esquema actual.
    Public Sub AsegurarColumnasDetallePedido()
        AsegurarColumnasPagoPedido()
    End Sub

    Private Sub AsegurarColumnasPedidos(cn As SQLiteConnection)
        AsegurarColumna(cn, "PEDIDOS", "Subtotal", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "Descuento", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "BaseGravable", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "IVA", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "TasaIVA", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "MetodoPago", "TEXT NULL")
        AsegurarColumna(cn, "PEDIDOS", "PagoCon", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "Cambio", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "DescripcionPedido", "TEXT NULL")
        AsegurarColumna(cn, "PEDIDOS", "Colores", "TEXT NULL")
        AsegurarColumna(cn, "PEDIDOS", "Medidas", "TEXT NULL")
        AsegurarColumna(cn, "PEDIDOS", "Notas", "TEXT NULL")
        AsegurarColumna(cn, "PEDIDOS", "Anticipo", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "Saldo", "NUMERIC NULL")
        AsegurarColumna(cn, "PEDIDOS", "Cancelada", "INTEGER NULL")
        AsegurarColumna(cn, "PEDIDOS", "FechaCancelacion", "TEXT NULL")
        AsegurarColumna(cn, "PEDIDOS", "MotivoCancelacion", "TEXT NULL")

        EjecutarSql(cn, "UPDATE PEDIDOS SET Subtotal = IFNULL(Subtotal, Total) WHERE Subtotal IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET Descuento = IFNULL(Descuento, 0) WHERE Descuento IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET IVA = IFNULL(IVA, 0) WHERE IVA IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET BaseGravable = IFNULL(BaseGravable, Total - IFNULL(IVA, 0)) WHERE BaseGravable IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET TasaIVA = IFNULL(TasaIVA, 0) WHERE TasaIVA IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET MetodoPago = IFNULL(NULLIF(MetodoPago, ''), 'Efectivo') WHERE MetodoPago IS NULL OR MetodoPago = ''")
        EjecutarSql(cn, "UPDATE PEDIDOS SET PagoCon = IFNULL(PagoCon, Total) WHERE PagoCon IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET Cambio = IFNULL(Cambio, 0) WHERE Cambio IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET Anticipo = IFNULL(Anticipo, 0) WHERE Anticipo IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET Saldo = IFNULL(Saldo, IFNULL(Total, 0) - IFNULL(Anticipo, 0)) WHERE Saldo IS NULL")
        EjecutarSql(cn, "UPDATE PEDIDOS SET Cancelada = IFNULL(Cancelada, 0) WHERE Cancelada IS NULL")
    End Sub

    Private Sub AsegurarColumna(cn As SQLiteConnection, tabla As String, columna As String, declaracion As String)
        Using cmd As New SQLiteCommand("PRAGMA table_info([" & tabla & "])", cn)
            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    If String.Equals(reader("name").ToString(), columna, StringComparison.OrdinalIgnoreCase) Then Return
                End While
            End Using
        End Using

        EjecutarSql(cn, "ALTER TABLE [" & tabla & "] ADD COLUMN [" & columna & "] " & declaracion)
    End Sub

    Private Sub EjecutarSql(cn As SQLiteConnection, sql As String)
        Using cmd As New SQLiteCommand(sql, cn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' Busca un cliente por nombre y telefono; si no existe, lo crea.
    Public Function ObtenerIdCliente(nombreCompleto As String, telefono As String, trans As SQLiteTransaction) As Integer
        Dim nombres As String = nombreCompleto.Trim()
        Dim apellidos As String = ""

        If nombres = "" Then nombres = "Cliente"
        If nombres.Length > 50 Then nombres = nombres.Substring(0, 50)

        Using cmdBuscar As New SQLiteCommand(
            "SELECT ID_CLIENTE FROM CLIENTES " &
            "WHERE Nombres_cl = @nombres AND IFNULL(Apellidos,'') = @apellidos AND IFNULL(Telefono,'') = @telefono LIMIT 1",
            trans.Connection,
            trans)
            cmdBuscar.Parameters.AddWithValue("@nombres", nombres)
            cmdBuscar.Parameters.AddWithValue("@apellidos", apellidos)
            cmdBuscar.Parameters.AddWithValue("@telefono", If(telefono, ""))

            Dim encontrado = cmdBuscar.ExecuteScalar()
            If encontrado IsNot Nothing Then Return CInt(encontrado)
        End Using

        Using cmdInsertar As New SQLiteCommand(
            "INSERT INTO CLIENTES (Nombres_cl, Apellidos, Telefono) VALUES (@nombres, @apellidos, @telefono); " &
            "SELECT last_insert_rowid();",
            trans.Connection,
            trans)
            cmdInsertar.Parameters.AddWithValue("@nombres", nombres)
            cmdInsertar.Parameters.AddWithValue("@apellidos", apellidos)
            cmdInsertar.Parameters.AddWithValue("@telefono", If(telefono, ""))
            Return CInt(cmdInsertar.ExecuteScalar())
        End Using
    End Function

    ' Obtiene o crea el cliente generico para ventas de mostrador.
    Public Function ObtenerIdClienteGeneral(trans As SQLiteTransaction) As Integer
        Using cmdBuscar As New SQLiteCommand(
            "SELECT ID_CLIENTE FROM CLIENTES " &
            "WHERE Nombres_cl = 'Publico' AND IFNULL(Apellidos,'') = 'General' LIMIT 1",
            trans.Connection,
            trans)
            Dim encontrado = cmdBuscar.ExecuteScalar()
            If encontrado IsNot Nothing Then Return CInt(encontrado)
        End Using

        Using cmdInsertar As New SQLiteCommand(
            "INSERT INTO CLIENTES (Nombres_cl, Apellidos, Telefono) VALUES ('Publico', 'General', ''); " &
            "SELECT last_insert_rowid();",
            trans.Connection,
            trans)
            Return CInt(cmdInsertar.ExecuteScalar())
        End Using
    End Function

End Module

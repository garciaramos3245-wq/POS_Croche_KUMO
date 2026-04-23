Imports System.Data
Imports System.Data.SqlClient

Module DbKumo

    Public Function ObtenerConexion() As SqlConnection
        Return New SqlConnection(My.Settings.Con_Croche)
    End Function

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

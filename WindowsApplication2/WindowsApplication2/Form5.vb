Imports System.Runtime.InteropServices
Imports System.Data.SqlClient

Public Class Form5

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_MUTED_PREMIUM As Color = Color.FromArgb(136, 118, 94)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)

    Public Sub New()
        InitializeComponent()
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                If ModEstilo.EstaEnModoDisenio(Me) Then
                    ModEstilo.PrepararVentana(Me)
                End If
                If cbEstado.Items.Count > 0 Then
                    cbEstado.SelectedIndex = 0
                End If
                AplicarDisenoPedidos()
            End Sub)
    End Sub

    Private idSeleccionado As Integer = 0

    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(
        ByVal nLeftRect As Integer,
        ByVal nTopRect As Integer,
        ByVal nRightRect As Integer,
        ByVal nBottomRect As Integer,
        ByVal nWidthEllipse As Integer,
        ByVal nHeightEllipse As Integer
    ) As IntPtr
    End Function

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AddHandler ModActualizaciones.PedidosActualizados, AddressOf RefrescarPedidos
        cbEstado.SelectedIndex = 0
        CargarPedidos()
        AplicarDisenoPedidos()
    End Sub

    Private Sub AplicarDisenoPedidos()
        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarStatusStrip(StatusStrip1)
        ModEstilo.EstilarBotonPrimario(btnGuardar)
        ModEstilo.EstilarBotonPeligro(btnEliminar)
        ModEstilo.EstilarBotonSecundario(btnNuevo)
        ModEstilo.EstilarBotonSecundario(btnCargar)
        ModEstilo.EstilarBotonPeligro(btnRegresar)
        AplicarEstiloPedidosPremium()
        ConfigurarLayoutPedidos()
    End Sub

    Private Sub AplicarEstiloPedidosPremium()
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Pedidos"

        gbForm.BackColor = CLR_SURFACE_PREMIUM
        gbForm.ForeColor = CLR_TEXT_PREMIUM
        gbForm.Text = "Pedido especial"

        gbLista.BackColor = CLR_PANEL_PREMIUM
        gbLista.ForeColor = CLR_TEXT_PREMIUM
        gbLista.Text = "Agenda de pedidos"

        For Each lbl As Label In New Label() {lblNombreTxt, lblTelTxt, lblDescTxt, lblColTxt, lblMedTxt, lblNotasTxt, lblPrecioTxt, lblAnticTxt, lblSaldoTxt, lblFechaTxt, lblEstadoTxt}
            lbl.ForeColor = CLR_MUTED_PREMIUM
            lbl.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Next

        lblNombreTxt.Text = "Cliente"
        lblTelTxt.Text = "Telefono"
        lblDescTxt.Text = "Descripcion"
        lblColTxt.Text = "Paleta"
        lblMedTxt.Text = "Medidas"
        lblNotasTxt.Text = "Notas"
        lblPrecioTxt.Text = "Precio final"
        lblAnticTxt.Text = "Anticipo"
        lblSaldoTxt.Text = "Saldo"
        lblFechaTxt.Text = "Entrega"
        lblEstadoTxt.Text = "Estado"

        For Each tb As TextBox In New TextBox() {txtNombre, txtTel, txtDesc, txtColores, txtMedidas, txtNotas, txtPrecio, txtAnticipo, txtSaldo}
            tb.BackColor = CLR_SURFACE_PREMIUM
            tb.ForeColor = CLR_TEXT_PREMIUM
            tb.BorderStyle = BorderStyle.FixedSingle
            tb.Font = New Font("Segoe UI", 10.0F)
        Next

        cbEstado.BackColor = CLR_SURFACE_PREMIUM
        cbEstado.ForeColor = CLR_TEXT_PREMIUM
        cbEstado.FlatStyle = FlatStyle.Flat
        cbEstado.Font = New Font("Segoe UI", 9.5F)

        dgv.BackgroundColor = CLR_SURFACE_PREMIUM
        dgv.BorderStyle = BorderStyle.None
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = Color.FromArgb(229, 217, 201)
        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersDefaultCellStyle.BackColor = CLR_DARK_PREMIUM
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = CLR_DARK_PREMIUM
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
        dgv.DefaultCellStyle.BackColor = CLR_SURFACE_PREMIUM
        dgv.DefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgv.DefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 248, 242)
        dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgv.RowTemplate.Height = 32

        btnGuardar.Text = "Guardar pedido"
        btnNuevo.Text = "+ Nuevo"
        btnEliminar.Text = "Eliminar"
        btnCargar.Text = "Cargar seleccionado"
        btnRegresar.Text = "Cerrar"

        btnGuardar.BackColor = Color.FromArgb(74, 133, 95)
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 111, 78)

        btnNuevo.BackColor = CLR_PANEL_PREMIUM
        btnNuevo.ForeColor = CLR_TEXT_PREMIUM
        btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnCargar.BackColor = CLR_DARK_PREMIUM
        btnCargar.ForeColor = Color.White
        btnCargar.FlatAppearance.BorderColor = CLR_DARK_PREMIUM
        btnCargar.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)

        btnEliminar.BackColor = Color.FromArgb(154, 73, 64)
        btnEliminar.FlatAppearance.MouseOverBackColor = Color.FromArgb(133, 61, 53)

        btnRegresar.BackColor = CLR_DARK_PREMIUM
        btnRegresar.ForeColor = Color.FromArgb(244, 226, 193)
        btnRegresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 64, 73)
    End Sub

    Private Sub ConfigurarLayoutPedidos()
        Dim margen As Integer = 18
        Dim top As Integer = 14
        Dim altoBoton As Integer = 42
        Dim panelDerecho As Integer = Math.Max(420, Math.Min(520, CInt(Me.ClientSize.Width * 0.33)))
        Dim anchoIzquierdo As Integer = Me.ClientSize.Width - panelDerecho - (margen * 3)
        Dim yBloques As Integer = top + altoBoton + 14
        Dim altoDisponible As Integer = Me.ClientSize.Height - StatusStrip1.Height - yBloques - margen

        btnGuardar.SetBounds(margen, top, 128, altoBoton)
        btnNuevo.SetBounds(btnGuardar.Right + 12, top, 110, altoBoton)
        btnEliminar.SetBounds(btnNuevo.Right + 12, top, 128, altoBoton)
        btnRegresar.SetBounds(Me.ClientSize.Width - margen - 122, top, 122, altoBoton)

        gbForm.SetBounds(margen, yBloques, anchoIzquierdo, altoDisponible)
        gbLista.SetBounds(gbForm.Right + margen, yBloques, panelDerecho, altoDisponible)

        Dim pad As Integer = 18
        Dim espacioCol As Integer = 18
        Dim anchoMitad As Integer = (gbForm.Width - (pad * 2) - espacioCol) \ 2
        Dim anchoTercio As Integer = (gbForm.Width - (pad * 2) - (espacioCol * 2)) \ 3
        Dim y As Integer = 38

        lblNombreTxt.Location = New Point(pad, y)
        lblTelTxt.Location = New Point(pad + anchoMitad + espacioCol, y)
        txtNombre.SetBounds(pad, y + 24, anchoMitad, 34)
        txtTel.SetBounds(pad + anchoMitad + espacioCol, y + 24, anchoMitad, 34)

        y += 72
        lblDescTxt.Location = New Point(pad, y)
        txtDesc.SetBounds(pad, y + 24, gbForm.Width - (pad * 2), 34)

        y += 72
        lblColTxt.Location = New Point(pad, y)
        lblMedTxt.Location = New Point(pad + anchoMitad + espacioCol, y)
        txtColores.SetBounds(pad, y + 24, anchoMitad, 34)
        txtMedidas.SetBounds(pad + anchoMitad + espacioCol, y + 24, anchoMitad, 34)

        y += 72
        lblNotasTxt.Location = New Point(pad, y)
        txtNotas.SetBounds(pad, y + 24, gbForm.Width - (pad * 2), 34)

        y += 72
        lblPrecioTxt.Location = New Point(pad, y)
        lblAnticTxt.Location = New Point(pad + anchoTercio + espacioCol, y)
        lblSaldoTxt.Location = New Point(pad + (anchoTercio * 2) + (espacioCol * 2), y)
        txtPrecio.SetBounds(pad, y + 24, anchoTercio, 34)
        txtAnticipo.SetBounds(pad + anchoTercio + espacioCol, y + 24, anchoTercio, 34)
        txtSaldo.SetBounds(pad + (anchoTercio * 2) + (espacioCol * 2), y + 24, anchoTercio, 34)

        y += 78
        lblFechaTxt.Location = New Point(pad, y)
        lblEstadoTxt.Location = New Point(pad + anchoMitad + espacioCol, y)
        dtpEntrega.SetBounds(pad, y + 24, anchoMitad, 34)
        cbEstado.SetBounds(pad + anchoMitad + espacioCol, y + 24, anchoMitad, 34)

        dgv.SetBounds(14, 32, gbLista.Width - 28, gbLista.Height - 96)
        btnCargar.SetBounds(14, gbLista.Height - 48, gbLista.Width - 28, 34)

        gbForm.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbLista.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        dgv.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnCargar.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        btnRegresar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    Private Sub txtPrecio_TextChanged(sender As Object, e As EventArgs) Handles txtPrecio.TextChanged
        CalcSaldo()
    End Sub

    Private Sub txtAnticipo_TextChanged(sender As Object, e As EventArgs) Handles txtAnticipo.TextChanged
        CalcSaldo()
    End Sub

    Private Sub CalcSaldo()
        Dim p As Decimal = 0D
        Dim a As Decimal = 0D
        Decimal.TryParse(txtPrecio.Text, p)
        Decimal.TryParse(txtAnticipo.Text, a)
        txtSaldo.Text = (p - a).ToString("N2")
    End Sub

    Private Sub CargarPedidos()
        Try
            Dim dt = ObtenerTabla(
                "SELECT p.Id_Pedido AS ID_Pedido, " &
                "RTRIM(c.Nombres_cl + ' ' + ISNULL(c.Apellidos,'')) AS Cliente, " &
                "CONVERT(varchar, p.Fecha, 103) AS Entrega, " &
                "ISNULL(p.MetodoPago, 'Pendiente') AS Estado " &
                "FROM PEDIDOS p " &
                "INNER JOIN CLIENTES c ON c.ID_CLIENTE = p.ID_CLIENTE " &
                "ORDER BY p.Fecha DESC")

            dgv.DataSource = dt
            If dgv.Columns.Contains("ID_Pedido") Then dgv.Columns("ID_Pedido").Visible = False
            sbInfo.Text = "  " & dt.Rows.Count & " pedidos registrados"
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Pedidos no disponibles", "No se pudieron cargar los pedidos." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    Private Sub dgv_DoubleClick(sender As Object, e As EventArgs) Handles dgv.DoubleClick
        CargarDetalle()
    End Sub

    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        CargarDetalle()
    End Sub

    Private Sub CargarDetalle()
        If dgv.CurrentRow Is Nothing Then Return
        idSeleccionado = CInt(dgv.CurrentRow.Cells("ID_Pedido").Value)

        Try
            Dim dt = ObtenerTabla(
                "SELECT p.*, c.Nombres_cl, c.Apellidos, c.Telefono " &
                "FROM PEDIDOS p " &
                "INNER JOIN CLIENTES c ON c.ID_CLIENTE = p.ID_CLIENTE " &
                "WHERE p.Id_Pedido = @id",
                New SqlParameter("@id", idSeleccionado))

            If dt.Rows.Count = 0 Then Return

            Dim row = dt.Rows(0)
            txtNombre.Text = (row("Nombres_cl").ToString() & " " & row("Apellidos").ToString()).Trim()
            txtTel.Text = row("Telefono").ToString()
            txtDesc.Text = "Pedido registrado en la base KUMOBD"
            txtColores.Clear()
            txtMedidas.Clear()
            txtNotas.Clear()
            txtPrecio.Text = row("Total").ToString()
            txtAnticipo.Text = "0.00"

            If Not IsDBNull(row("Fecha")) Then
                dtpEntrega.Value = CDate(row("Fecha"))
            End If

            Dim idx As Integer = cbEstado.Items.IndexOf(row("MetodoPago").ToString())
            If idx >= 0 Then
                cbEstado.SelectedIndex = idx
            Else
                cbEstado.SelectedIndex = 0
            End If

        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Detalle no disponible", "No se pudo cargar el detalle del pedido." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        idSeleccionado = 0
        txtNombre.Clear()
        txtTel.Clear()
        txtDesc.Clear()
        txtColores.Clear()
        txtMedidas.Clear()
        txtNotas.Clear()
        txtPrecio.Clear()
        txtAnticipo.Clear()
        txtSaldo.Clear()
        dtpEntrega.Value = Today
        cbEstado.SelectedIndex = 0
        dgv.ClearSelection()
        txtNombre.Focus()
    End Sub

    Private Function ObtenerMetodoPedido() As String
        Dim metodo As String = cbEstado.Text.Trim()
        If metodo = "" Then metodo = "Pendiente"
        If metodo.Length > 20 Then metodo = metodo.Substring(0, 20)
        Return metodo
    End Function

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txtNombre.Text.Trim() = "" Then
            ModMensajes.Mostrar(Me, "Dato faltante", "Escribe el nombre del cliente antes de guardar.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        Dim total As Decimal = 0D
        Decimal.TryParse(txtPrecio.Text, total)

        Using cn = ObtenerConexion()
            cn.Open()
            Dim trans = cn.BeginTransaction()

            Try
                Dim idCliente = ObtenerIdCliente(txtNombre.Text.Trim(), txtTel.Text.Trim(), trans)
                Dim metodo = ObtenerMetodoPedido()

                If idSeleccionado = 0 Then
                    Using cmd As New SqlCommand(
                        "INSERT INTO PEDIDOS (ID_CLIENTE, Fecha, Total, MetodoPago) " &
                        "VALUES (@idCliente, @fecha, @total, @metodo)",
                        cn,
                        trans)
                        cmd.Parameters.AddWithValue("@idCliente", idCliente)
                        cmd.Parameters.AddWithValue("@fecha", dtpEntrega.Value)
                        cmd.Parameters.AddWithValue("@total", total)
                        cmd.Parameters.AddWithValue("@metodo", metodo)
                        cmd.ExecuteNonQuery()
                    End Using
                Else
                    Using cmd As New SqlCommand(
                        "UPDATE PEDIDOS SET ID_CLIENTE = @idCliente, Fecha = @fecha, " &
                        "Total = @total, MetodoPago = @metodo WHERE Id_Pedido = @idPedido",
                        cn,
                        trans)
                        cmd.Parameters.AddWithValue("@idCliente", idCliente)
                        cmd.Parameters.AddWithValue("@fecha", dtpEntrega.Value)
                        cmd.Parameters.AddWithValue("@total", total)
                        cmd.Parameters.AddWithValue("@metodo", metodo)
                        cmd.Parameters.AddWithValue("@idPedido", idSeleccionado)
                        cmd.ExecuteNonQuery()
                    End Using
                End If

                trans.Commit()
                ModMensajes.Mostrar(Me, "Pedido guardado", If(idSeleccionado = 0, "Pedido guardado correctamente.", "Pedido actualizado correctamente."), ModMensajes.TipoAviso.Exito)
                ModActualizaciones.NotificarPedidosActualizados()

            Catch ex As Exception
                trans.Rollback()
                ModMensajes.Mostrar(Me, "No se pudo guardar", "No se guardo el pedido." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
            End Try
        End Using

        CargarPedidos()
        btnNuevo_Click(Nothing, Nothing)
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If idSeleccionado = 0 Then
            ModMensajes.Mostrar(Me, "Selecciona un pedido", "Elige un pedido de la lista antes de eliminarlo.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        If Not ModMensajes.Confirmar(Me, "Eliminar pedido", "Deseas eliminar el pedido de " & txtNombre.Text & "?", "Eliminar", "Cancelar", ModMensajes.TipoAviso.Advertencia) Then Return

        Using cn = ObtenerConexion()
            cn.Open()
            Using cmdDet As New SqlCommand("DELETE FROM DET_PEDIDOS WHERE Id_Pedido = @id", cn)
                cmdDet.Parameters.AddWithValue("@id", idSeleccionado)
                cmdDet.ExecuteNonQuery()
            End Using
            Using cmd As New SqlCommand("DELETE FROM PEDIDOS WHERE Id_Pedido = @id", cn)
                cmd.Parameters.AddWithValue("@id", idSeleccionado)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ModMensajes.Mostrar(Me, "Pedido eliminado", "El pedido se elimino correctamente.", ModMensajes.TipoAviso.Exito)
        ModActualizaciones.NotificarPedidosActualizados()
        CargarPedidos()
        btnNuevo_Click(Nothing, Nothing)
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Close()
    End Sub

    Private Sub RefrescarPedidos()
        If Me.IsDisposed Then Return
        CargarPedidos()
    End Sub

    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.PedidosActualizados, AddressOf RefrescarPedidos
    End Sub

    Private Sub Form5_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutPedidos()
    End Sub
End Class

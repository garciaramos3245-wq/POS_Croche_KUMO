Imports System.Runtime.InteropServices
Imports System.Data.SqlClient

Public Class Form8

    Private idSeleccionado As Integer = 0
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
                dtpFecha.Value = Today
                AplicarDisenoCancelaciones()
            End Sub)
    End Sub

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

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AddHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
        Try
            dtpFecha.Value = Today
            CargarVentas()
            AplicarDisenoCancelaciones()
        Catch ex As Exception
            MsgBox("Error al cargar el formulario: " & ex.Message)
        End Try
    End Sub

    Private Sub AplicarDisenoCancelaciones()
        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarStatusStrip(StatusStrip1)
        ModEstilo.EstilarBotonPrimario(btnBuscar)
        ModEstilo.EstilarBotonSecundario(btnHoy)
        ModEstilo.EstilarBotonPeligro(btnCancelar)
        ModEstilo.EstilarBotonPeligro(btnRegresar)
        AplicarEstiloCancelacionesPremium()
        ConfigurarLayoutCancelaciones()
    End Sub

    Private Sub AplicarEstiloCancelacionesPremium()
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Cancelaciones premium"

        gbFiltro.BackColor = CLR_PANEL_PREMIUM
        gbFiltro.ForeColor = CLR_TEXT_PREMIUM
        gbFiltro.Text = "Filtro de ventas"

        gbVentas.BackColor = CLR_SURFACE_PREMIUM
        gbVentas.ForeColor = CLR_TEXT_PREMIUM
        gbVentas.Text = "Ventas elegibles"

        gbDetalle.BackColor = CLR_SURFACE_PREMIUM
        gbDetalle.ForeColor = CLR_TEXT_PREMIUM
        gbDetalle.Text = "Detalle de la venta"

        gbCancelar.BackColor = Color.FromArgb(255, 247, 244)
        gbCancelar.ForeColor = Color.FromArgb(141, 72, 63)
        gbCancelar.Text = "Cancelacion segura"

        lblFechaTxt.Text = "Fecha"
        lblFechaTxt.ForeColor = CLR_MUTED_PREMIUM
        lblFechaTxt.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblMotivoTxt.Visible = False
        txtMotivo.Visible = False

        btnBuscar.Text = "Ver ventas"
        btnHoy.Text = "Hoy"
        btnCancelar.Text = "Cancelar venta"
        btnRegresar.Text = "Cerrar"

        btnBuscar.BackColor = CLR_DARK_PREMIUM
        btnBuscar.ForeColor = Color.White
        btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)

        btnHoy.BackColor = CLR_PANEL_PREMIUM
        btnHoy.ForeColor = CLR_TEXT_PREMIUM
        btnHoy.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnHoy.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnCancelar.BackColor = Color.FromArgb(154, 73, 64)
        btnCancelar.ForeColor = Color.White
        btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(133, 61, 53)

        btnRegresar.BackColor = CLR_DARK_PREMIUM
        btnRegresar.ForeColor = Color.FromArgb(244, 226, 193)
        btnRegresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 64, 73)

        For Each dgv As DataGridView In New DataGridView() {dgvVentas, dgvDetalle}
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
        Next
    End Sub

    Private Sub ConfigurarLayoutCancelaciones()
        Dim margen As Integer = 18
        Dim top As Integer = 24
        Dim altoBoton As Integer = 40
        Dim esp As Integer = 18
        Dim altoFiltro As Integer = 66
        Dim altoCancelar As Integer = 92
        Dim panelIzquierdo As Integer = CInt(Me.ClientSize.Width * 0.52)

        gbFiltro.SetBounds(margen, top, 560, altoFiltro)
        btnRegresar.SetBounds(Me.ClientSize.Width - margen - 118, top, 118, altoBoton)

        lblFechaTxt.Location = New Point(18, 28)
        dtpFecha.SetBounds(80, 24, 170, 30)
        btnBuscar.SetBounds(266, 22, 110, 34)
        btnHoy.SetBounds(388, 22, 86, 34)

        Dim yBloques As Integer = gbFiltro.Bottom + esp
        Dim altoDisponible As Integer = Me.ClientSize.Height - StatusStrip1.Height - yBloques - margen

        gbVentas.SetBounds(margen, yBloques, panelIzquierdo - margen, altoDisponible - altoCancelar - esp)
        gbDetalle.SetBounds(gbVentas.Right + esp, yBloques, Me.ClientSize.Width - margen - gbVentas.Right - esp, altoDisponible - altoCancelar - esp)
        gbCancelar.SetBounds(margen, gbVentas.Bottom + esp, Me.ClientSize.Width - (margen * 2), altoCancelar)

        dgvVentas.SetBounds(14, 34, gbVentas.Width - 28, gbVentas.Height - 50)
        dgvDetalle.SetBounds(14, 34, gbDetalle.Width - 28, gbDetalle.Height - 50)

        btnCancelar.SetBounds((gbCancelar.Width - 176) \ 2, 28, 176, 40)

        gbVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbDetalle.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        gbCancelar.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        dgvVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvDetalle.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnCancelar.Anchor = AnchorStyles.Bottom
        btnRegresar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        CargarVentas()
    End Sub

    Private Sub btnHoy_Click(sender As Object, e As EventArgs) Handles btnHoy.Click
        dtpFecha.Value = Today
        CargarVentas()
    End Sub

    Private Sub CargarVentas()
        Dim fecha As Date = dtpFecha.Value.Date
        Using cn = ObtenerConexion()
            Try
                cn.Open()
                Dim da As New SqlDataAdapter(
                    "SELECT p.Id_Pedido AS [N Venta], " &
                    "LOWER(REPLACE(REPLACE(FORMAT(p.Fecha, 'h:mm tt', 'en-US'), 'AM', 'a.m.'), 'PM', 'p.m.')) AS [Hora], " &
                    "CONVERT(varchar,p.Fecha,103) AS [Fecha], " &
                    "p.Total " &
                    "FROM PEDIDOS p WHERE CAST(p.Fecha AS DATE)=@fecha ORDER BY p.Fecha DESC", cn)
                da.SelectCommand.Parameters.AddWithValue("@fecha", fecha)

                Dim dt As New DataTable
                da.Fill(dt)
                dgvVentas.DataSource = dt
                sbInfo.Text = "  Ventas del " & dtpFecha.Value.ToString("dd/MM/yyyy") &
                              "  |  Total registros: " & dt.Rows.Count
            Catch ex As Exception
                MsgBox("Error al cargar ventas: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub dgvVentas_SelectionChanged(sender As Object, e As EventArgs) Handles dgvVentas.SelectionChanged
        If dgvVentas.CurrentRow Is Nothing Then Return
        idSeleccionado = CInt(dgvVentas.CurrentRow.Cells("N Venta").Value)
        CargarDetalle(idSeleccionado)
    End Sub

    Private Sub CargarDetalle(id As Integer)
        Using cn = ObtenerConexion()
            Try
                cn.Open()
                Dim da As New SqlDataAdapter(
                    "SELECT p.NombrePr AS [Producto], d.Cantidad, " &
                    "(d.Cantidad * d.PrecioVentaMomento) AS Subtotal " &
                    "FROM DET_PEDIDOS d " &
                    "INNER JOIN PRODUCTO p ON p.Id_Producto = d.Id_Producto " &
                    "WHERE d.Id_Pedido = @id", cn)
                da.SelectCommand.Parameters.AddWithValue("@id", id)

                Dim dt As New DataTable
                da.Fill(dt)
                dgvDetalle.DataSource = dt
                gbDetalle.Text = "Detalle de Venta - V-" & id.ToString("000")
            Catch ex As Exception
                MsgBox("Error al cargar detalle: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        If idSeleccionado = 0 Then
            MsgBox("Selecciona una venta de la lista.")
            Return
        End If

        If MsgBox("Confirmar cancelacion de V-" & idSeleccionado.ToString("000") & "?" &
                  "Se restaurara el stock de los productos.",
                  MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation,
                  "Confirmar cancelacion") <> MsgBoxResult.Yes Then Return

        Using cn = ObtenerConexion()
            cn.Open()
            Dim trans = cn.BeginTransaction()

            Try
                Dim dtDetalle As New DataTable
                Using daDetalle As New SqlDataAdapter(
                    "SELECT Id_Producto, Cantidad FROM DET_PEDIDOS WHERE Id_Pedido=@idVenta",
                    cn)
                    daDetalle.SelectCommand.Transaction = trans
                    daDetalle.SelectCommand.Parameters.AddWithValue("@idVenta", idSeleccionado)
                    daDetalle.Fill(dtDetalle)
                End Using

                For Each row As DataRow In dtDetalle.Rows
                    Using ejStock As New SqlCommand(
                        "UPDATE INVENTARIO SET cant_disp = cant_disp + @qty WHERE Id_Producto=@idp",
                        cn,
                        trans)
                        ejStock.Parameters.AddWithValue("@qty", CInt(row("Cantidad")))
                        ejStock.Parameters.AddWithValue("@idp", CInt(row("Id_Producto")))
                        ejStock.ExecuteNonQuery()
                    End Using
                Next

                Using ejDet As New SqlCommand(
                    "DELETE FROM DET_PEDIDOS WHERE Id_Pedido=@id",
                    cn,
                    trans)
                    ejDet.Parameters.AddWithValue("@id", idSeleccionado)
                    ejDet.ExecuteNonQuery()
                End Using

                Using ejPedido As New SqlCommand(
                    "DELETE FROM PEDIDOS WHERE Id_Pedido=@id",
                    cn,
                    trans)
                    ejPedido.Parameters.AddWithValue("@id", idSeleccionado)
                    ejPedido.ExecuteNonQuery()
                End Using

                trans.Commit()
                MsgBox("Venta V-" & idSeleccionado.ToString("000") & " cancelada correctamente." &
                       vbNewLine & "Stock restaurado.", MsgBoxStyle.Information, "Cancelacion exitosa")
                ModActualizaciones.NotificarInventarioActualizado()
                ModActualizaciones.NotificarVentasActualizadas()

            Catch ex As Exception
                trans.Rollback()
                MsgBox("Error al cancelar la venta: " & ex.Message)
            End Try
        End Using

        idSeleccionado = 0
        CargarVentas()
        dgvDetalle.DataSource = Nothing
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Close()
    End Sub

    Private Sub RefrescarVentas()
        If Me.IsDisposed Then Return
        CargarVentas()
    End Sub

    Private Sub Form8_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
    End Sub

    Private Sub Form8_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutCancelaciones()
    End Sub
End Class

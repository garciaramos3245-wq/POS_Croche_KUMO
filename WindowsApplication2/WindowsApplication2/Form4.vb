Imports System.Runtime.InteropServices
Imports System.Data.SqlClient

Public Class Form4

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_MUTED_PREMIUM As Color = Color.FromArgb(136, 118, 94)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)

    Public Sub New()
        InitializeComponent()
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

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AddHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
        Try
            dtpFecha.Value = Today
            CargarVentas()
            ModEstilo.EstilarControles(Me)
            ModEstilo.EstilarStatusStrip(StatusStrip1)
            ModEstilo.EstilarBotonPrimario(btnBuscar)
            ModEstilo.EstilarBotonSecundario(btnHoy)
            ModEstilo.EstilarBotonSecundario(btnTicket)
            ModEstilo.EstilarBotonSecundario(btnImprimir)
            ModEstilo.EstilarBotonPeligro(btnRegresar)
            AplicarEstiloHistorialPremium()
            ConfigurarLayoutHistorial()
        Catch ex As Exception
            MsgBox("Error en Form4_Load: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub AplicarEstiloHistorialPremium()
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Historial premium"

        gbFiltro.BackColor = CLR_PANEL_PREMIUM
        gbFiltro.ForeColor = CLR_TEXT_PREMIUM
        gbFiltro.Text = "Filtro de ventas"

        gbTabla.BackColor = CLR_SURFACE_PREMIUM
        gbTabla.ForeColor = CLR_TEXT_PREMIUM
        gbTabla.Text = "Ventas del dia"

        lblFechaTxt.Text = "Fecha"
        lblFechaTxt.ForeColor = CLR_MUTED_PREMIUM
        lblFechaTxt.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)

        For Each pnl As Panel In New Panel() {pnlIngresos, pnlVentas, pnlPromedio, pnlArticulos}
            pnl.BackColor = CLR_SURFACE_PREMIUM
        Next

        btnBuscar.Text = "Ver ventas"
        btnHoy.Text = "Hoy"
        btnTicket.Text = "Abrir ticket"
        btnImprimir.Text = "Exportar vista"
        btnRegresar.Text = "Cerrar"

        btnBuscar.BackColor = CLR_DARK_PREMIUM
        btnBuscar.ForeColor = Color.White
        btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)

        btnHoy.BackColor = CLR_PANEL_PREMIUM
        btnHoy.ForeColor = CLR_TEXT_PREMIUM
        btnHoy.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnHoy.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnTicket.BackColor = CLR_SURFACE_PREMIUM
        btnTicket.ForeColor = CLR_TEXT_PREMIUM
        btnTicket.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnTicket.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnImprimir.BackColor = CLR_SURFACE_PREMIUM
        btnImprimir.ForeColor = CLR_TEXT_PREMIUM
        btnImprimir.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnImprimir.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnRegresar.BackColor = CLR_DARK_PREMIUM
        btnRegresar.ForeColor = Color.FromArgb(244, 226, 193)
        btnRegresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 64, 73)

        dgvVentas.BackgroundColor = CLR_SURFACE_PREMIUM
        dgvVentas.BorderStyle = BorderStyle.None
        dgvVentas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvVentas.GridColor = Color.FromArgb(229, 217, 201)
        dgvVentas.EnableHeadersVisualStyles = False
        dgvVentas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = CLR_DARK_PREMIUM
        dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvVentas.ColumnHeadersDefaultCellStyle.SelectionBackColor = CLR_DARK_PREMIUM
        dgvVentas.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
        dgvVentas.DefaultCellStyle.BackColor = CLR_SURFACE_PREMIUM
        dgvVentas.DefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
        dgvVentas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgvVentas.DefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
        dgvVentas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 248, 242)
        dgvVentas.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgvVentas.RowTemplate.Height = 32
    End Sub

    Private Sub ConfigurarLayoutHistorial()
        Dim margen As Integer = 18
        Dim top As Integer = 14
        Dim altoBoton As Integer = 40
        Dim anchoFiltro As Integer = 560
        Dim esp As Integer = 14
        Dim anchoPanel As Integer = CInt((Me.ClientSize.Width - (margen * 2) - (esp * 3)) / 4)
        Dim yResumen As Integer = top + altoBoton + 18
        Dim yTabla As Integer = yResumen + 96 + 18
        Dim altoTabla As Integer = Me.ClientSize.Height - yTabla - StatusStrip1.Height - margen

        gbFiltro.SetBounds(margen, top, anchoFiltro, 66)
        btnRegresar.SetBounds(Me.ClientSize.Width - margen - 118, top, 118, altoBoton)

        lblFechaTxt.Location = New Point(18, 28)
        dtpFecha.SetBounds(80, 24, 170, 30)
        btnBuscar.SetBounds(266, 22, 110, 34)
        btnHoy.SetBounds(388, 22, 86, 34)

        pnlIngresos.SetBounds(margen, yResumen, anchoPanel, 96)
        pnlVentas.SetBounds(pnlIngresos.Right + esp, yResumen, anchoPanel, 96)
        pnlPromedio.SetBounds(pnlVentas.Right + esp, yResumen, anchoPanel, 96)
        pnlArticulos.SetBounds(pnlPromedio.Right + esp, yResumen, Me.ClientSize.Width - margen - pnlPromedio.Right - esp, 96)

        gbTabla.SetBounds(margen, yTabla, Me.ClientSize.Width - (margen * 2), altoTabla)
        dgvVentas.SetBounds(14, 30, gbTabla.Width - 28, gbTabla.Height - 92)
        btnTicket.SetBounds(14, gbTabla.Height - 48, 150, 34)
        btnImprimir.SetBounds(btnTicket.Right + 12, gbTabla.Height - 48, 172, 34)

        gbFiltro.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        btnRegresar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnlIngresos.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        pnlVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        pnlPromedio.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        pnlArticulos.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        gbTabla.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnTicket.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
        btnImprimir.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
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
        Try
            dgvVentas.DataSource = ObtenerTabla(
                "SELECT Id_Pedido AS [N Venta], LOWER(REPLACE(REPLACE(FORMAT(Fecha, 'h:mm tt', 'en-US'), 'AM', 'a.m.'), 'PM', 'p.m.')) AS [Hora], " &
                "CONVERT(varchar, Fecha, 103) AS [Fecha], Total " &
                "FROM PEDIDOS WHERE CAST(Fecha AS DATE) = @fecha ORDER BY Fecha DESC",
                New SqlParameter("@fecha", fecha))

            Dim resumen = ObtenerTabla(
                "SELECT COUNT(*) AS VentasDia, ISNULL(SUM(Total),0) AS Ingresos, ISNULL(AVG(Total),0) AS Promedio " &
                "FROM PEDIDOS WHERE CAST(Fecha AS DATE) = @fecha",
                New SqlParameter("@fecha", fecha))

            If resumen.Rows.Count > 0 Then
                lblVentasVal.Text = resumen.Rows(0)("VentasDia").ToString()
                lblIngresosVal.Text = "$" & CDec(resumen.Rows(0)("Ingresos")).ToString("N2")
                lblPromedioVal.Text = "$" & CDec(resumen.Rows(0)("Promedio")).ToString("N2")
            End If

            lblArticulosVal.Text = ObtenerEscalar(
                "SELECT ISNULL(SUM(d.Cantidad),0) FROM DET_PEDIDOS d " &
                "INNER JOIN PEDIDOS p ON p.Id_Pedido = d.Id_Pedido " &
                "WHERE CAST(p.Fecha AS DATE) = @fecha",
                New SqlParameter("@fecha", fecha)).ToString()

        Catch ex As Exception
            MsgBox("Error al cargar ventas: " & ex.Message)
        End Try

        sbInfo.Text = "  Ventas: " & lblVentasVal.Text & "  |  Ingresos: " & lblIngresosVal.Text & "  |  " & dtpFecha.Value.ToString("dd/MM/yyyy")
        gbTabla.Text = "Ventas del dia - " & dtpFecha.Value.ToString("dd/MM/yyyy")
    End Sub

    Private Sub btnTicket_Click(sender As Object, e As EventArgs) Handles btnTicket.Click
        If dgvVentas.CurrentRow Is Nothing Then
            MsgBox("Selecciona una venta de la lista.")
            Return
        End If

        Dim id As Integer = CInt(dgvVentas.CurrentRow.Cells("N Venta").Value)
        Dim ticket As New Form6(id)
        ticket.ShowDialog()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        MsgBox("Funcion de impresion disponible proximamente.")
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Close()
    End Sub

    Private Sub RefrescarVentas()
        If Me.IsDisposed Then Return
        CargarVentas()
    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
    End Sub

    Private Sub Form4_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutHistorial()
    End Sub
End Class

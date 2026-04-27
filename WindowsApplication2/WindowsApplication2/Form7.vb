Imports System.Runtime.InteropServices
Imports System.Data.SqlClient

Public Class Form7

    Private _cargandoReporte As Boolean

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
                dtpFecha.Value = Today
                ModEstilo.EstilarControles(Me)
                ModEstilo.EstilarStatusStrip(StatusStrip1)
                ModEstilo.EstilarBotonPrimario(btnVer)
                ModEstilo.EstilarBotonSecundario(btnHoy)
                ModEstilo.EstilarBotonSecundario(btnImprimir)
                ModEstilo.EstilarBotonPeligro(btnRegresar)
                AplicarEstiloReportePremium()
                ConfigurarLayoutReporte()
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

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AddHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarReporte
        Try
            dtpFecha.Value = Today
            ModEstilo.EstilarControles(Me)
            ModEstilo.EstilarStatusStrip(StatusStrip1)
            ModEstilo.EstilarBotonPrimario(btnVer)
            ModEstilo.EstilarBotonSecundario(btnHoy)
            ModEstilo.EstilarBotonSecundario(btnImprimir)
            ModEstilo.EstilarBotonPeligro(btnRegresar)
            AplicarEstiloReportePremium()
            ConfigurarLayoutReporte()
            BeginInvoke(New MethodInvoker(AddressOf IniciarCargaReporte))
        Catch ex As Exception
            MsgBox("Error en Form7_Load: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Async Sub IniciarCargaReporte()
        Await CargarReporteAsync()
    End Sub

    Private Sub AplicarEstiloReportePremium()
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Reporte premium"

        gbResumen.BackColor = CLR_PANEL_PREMIUM
        gbResumen.ForeColor = CLR_TEXT_PREMIUM
        gbResumen.Text = "Resumen ejecutivo"

        gbVentas.BackColor = CLR_SURFACE_PREMIUM
        gbVentas.ForeColor = CLR_TEXT_PREMIUM
        gbVentas.Text = "Ventas del dia"

        gbTop.BackColor = CLR_SURFACE_PREMIUM
        gbTop.ForeColor = CLR_TEXT_PREMIUM
        gbTop.Text = "Top productos"

        lblFechaTxt.Text = "Fecha"
        lblFechaTxt.ForeColor = CLR_MUTED_PREMIUM
        lblFechaTxt.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)

        For Each pnl As Panel In New Panel() {pnlIngresos, pnlVentas, pnlPromedio, pnlArticulos}
            pnl.BackColor = CLR_SURFACE_PREMIUM
        Next

        For Each titulo As Label In New Label() {lblIngresosTitle, lblVentasTitle, lblPromedioTitle, lblArticulosTitle}
            titulo.ForeColor = CLR_MUTED_PREMIUM
            titulo.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
            titulo.AutoEllipsis = True
        Next

        For Each valor As Label In New Label() {lblIngresosVal, lblVentasVal, lblPromedioVal, lblArticulosVal}
            valor.ForeColor = CLR_DARK_PREMIUM
            valor.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Next

        lblIngresosVal.ForeColor = Color.FromArgb(74, 133, 95)

        For Each subtitulo As Label In New Label() {lblIngresosSub, lblVentasSub, lblPromedioSub, lblArticulosSub}
            subtitulo.ForeColor = CLR_MUTED_PREMIUM
            subtitulo.Font = New Font("Segoe UI", 8.0F, FontStyle.Regular)
        Next

        lblIngresosTitle.Text = "INGRESOS DEL DIA"
        lblIngresosSub.Text = "ventas cobradas"
        lblVentasTitle.Text = "VENTAS REGISTRADAS"
        lblVentasSub.Text = "tickets emitidos"
        lblPromedioTitle.Text = "TICKET PROMEDIO"
        lblPromedioSub.Text = "importe medio"
        lblArticulosTitle.Text = "ARTICULOS VENDIDOS"
        lblArticulosSub.Text = "piezas desplazadas"

        btnVer.Text = "Ver corte"
        btnHoy.Text = "Hoy"
        btnImprimir.Text = "Exportar"
        btnRegresar.Text = "Cerrar"

        btnVer.BackColor = CLR_DARK_PREMIUM
        btnVer.ForeColor = Color.White
        btnVer.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)

        btnHoy.BackColor = CLR_PANEL_PREMIUM
        btnHoy.ForeColor = CLR_TEXT_PREMIUM
        btnHoy.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnHoy.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnImprimir.BackColor = CLR_SURFACE_PREMIUM
        btnImprimir.ForeColor = CLR_TEXT_PREMIUM
        btnImprimir.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnImprimir.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnRegresar.BackColor = CLR_DARK_PREMIUM
        btnRegresar.ForeColor = Color.FromArgb(244, 226, 193)
        btnRegresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 64, 73)

        For Each dgv As DataGridView In New DataGridView() {dgvVentas, dgvTop}
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

    Private Sub ConfigurarLayoutReporte()
        Dim margen As Integer = 18
        Dim top As Integer = 14
        Dim altoBoton As Integer = 40
        Dim esp As Integer = 14
        Dim yResumen As Integer = top + altoBoton + 16
        Dim yBloques As Integer = yResumen + 166
        Dim altoDisponible As Integer = Me.ClientSize.Height - yBloques - StatusStrip1.Height - margen
        Dim anchoVentas As Integer = CInt(Me.ClientSize.Width * 0.68)

        lblFechaTxt.Location = New Point(margen, top + 10)
        dtpFecha.SetBounds(lblFechaTxt.Right + 10, top + 6, 176, 30)
        btnVer.SetBounds(dtpFecha.Right + 12, top + 4, 126, altoBoton)
        btnHoy.SetBounds(btnVer.Right + 12, top + 4, 84, altoBoton)
        btnRegresar.SetBounds(Me.ClientSize.Width - margen - 118, top + 4, 118, altoBoton)

        gbResumen.SetBounds(margen, yResumen, Me.ClientSize.Width - (margen * 2), 150)

        Dim panelPad As Integer = 12
        Dim anchoPanel As Integer = CInt((gbResumen.Width - (panelPad * 5)) / 4)
        pnlIngresos.SetBounds(panelPad, 42, anchoPanel, 96)
        pnlVentas.SetBounds(pnlIngresos.Right + panelPad, 42, anchoPanel, 96)
        pnlPromedio.SetBounds(pnlVentas.Right + panelPad, 42, anchoPanel, 96)
        pnlArticulos.SetBounds(pnlPromedio.Right + panelPad, 42, gbResumen.Width - pnlPromedio.Right - (panelPad * 2), 96)

        PosicionarPanelResumen(pnlIngresos, lblIngresosTitle, lblIngresosVal, lblIngresosSub)
        PosicionarPanelResumen(pnlVentas, lblVentasTitle, lblVentasVal, lblVentasSub)
        PosicionarPanelResumen(pnlPromedio, lblPromedioTitle, lblPromedioVal, lblPromedioSub)
        PosicionarPanelResumen(pnlArticulos, lblArticulosTitle, lblArticulosVal, lblArticulosSub)

        gbVentas.SetBounds(margen, yBloques, anchoVentas - (margen * 2), altoDisponible)
        gbTop.SetBounds(gbVentas.Right + esp, yBloques, Me.ClientSize.Width - margen - gbVentas.Right - esp, altoDisponible)

        dgvVentas.SetBounds(14, 30, gbVentas.Width - 28, gbVentas.Height - 90)
        btnImprimir.SetBounds(14, gbVentas.Height - 48, 176, 34)
        dgvTop.SetBounds(14, 30, gbTop.Width - 28, gbTop.Height - 44)

        gbResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        gbVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbTop.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        dgvVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvTop.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnImprimir.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
        btnRegresar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    Private Sub PosicionarPanelResumen(panel As Panel, titulo As Label, valor As Label, subtitulo As Label)
        Dim pad As Integer = 14
        titulo.AutoSize = False
        valor.AutoSize = False
        subtitulo.AutoSize = False

        titulo.SetBounds(pad, 12, panel.Width - (pad * 2), 26)
        valor.SetBounds(pad, 40, panel.Width - (pad * 2), 36)
        subtitulo.SetBounds(pad, panel.Height - 32, panel.Width - (pad * 2), 20)
    End Sub

    Private Async Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click
        Await CargarReporteAsync()
    End Sub

    Private Async Sub btnHoy_Click(sender As Object, e As EventArgs) Handles btnHoy.Click
        dtpFecha.Value = Today
        Await CargarReporteAsync()
    End Sub

    Private Async Function CargarReporteAsync() As Task
        If _cargandoReporte Then Return
        _cargandoReporte = True
        CambiarEstadoCarga(True)

        Dim fecha As Date = dtpFecha.Value.Date
        Try
            Dim ventas = Await Task.Run(Function()
                                            Return ObtenerTabla(
                                                "SELECT Id_Pedido AS [N Venta], LOWER(REPLACE(REPLACE(FORMAT(Fecha, 'h:mm tt', 'en-US'), 'AM', 'a.m.'), 'PM', 'p.m.')) AS [Hora], Total " &
                                                "FROM PEDIDOS WHERE CAST(Fecha AS DATE)=@fecha ORDER BY Fecha DESC",
                                                New SqlParameter("@fecha", fecha))
                                        End Function)

            Dim resumen = Await Task.Run(Function()
                                             Return ObtenerTabla(
                                                 "SELECT COUNT(*) AS VentasDia, ISNULL(SUM(Total),0) AS Ingresos, ISNULL(AVG(Total),0) AS Promedio " &
                                                 "FROM PEDIDOS WHERE CAST(Fecha AS DATE)=@fecha",
                                                 New SqlParameter("@fecha", fecha))
                                         End Function)

            Dim articulos = Await Task.Run(Function()
                                               Return ObtenerEscalar(
                                                   "SELECT ISNULL(SUM(d.Cantidad),0) FROM DET_PEDIDOS d " &
                                                   "INNER JOIN PEDIDOS p ON p.Id_Pedido = d.Id_Pedido " &
                                                   "WHERE CAST(p.Fecha AS DATE)=@fecha",
                                                   New SqlParameter("@fecha", fecha))
                                           End Function)

            Dim topProductos = Await Task.Run(Function()
                                                  Return ObtenerTabla(
                                                      "SELECT TOP 5 p.NombrePr AS [Producto], SUM(d.Cantidad) AS [Unidades] " &
                                                      "FROM DET_PEDIDOS d " &
                                                      "INNER JOIN PRODUCTO p ON p.Id_Producto = d.Id_Producto " &
                                                      "INNER JOIN PEDIDOS pd ON pd.Id_Pedido = d.Id_Pedido " &
                                                      "WHERE CAST(pd.Fecha AS DATE)=@fecha " &
                                                      "GROUP BY p.NombrePr ORDER BY Unidades DESC",
                                                      New SqlParameter("@fecha", fecha))
                                              End Function)

            dgvVentas.DataSource = ventas

            If resumen.Rows.Count > 0 Then
                lblVentasVal.Text = resumen.Rows(0)("VentasDia").ToString()
                lblIngresosVal.Text = "$" & CDec(resumen.Rows(0)("Ingresos")).ToString("N2")
                lblPromedioVal.Text = "$" & CDec(resumen.Rows(0)("Promedio")).ToString("N2")
            End If

            lblArticulosVal.Text = articulos.ToString()
            dgvTop.DataSource = topProductos

        Catch ex As Exception
            MsgBox("Error al cargar reporte: " & ex.Message)
        Finally
            sbInfo.Text = "  Reporte: " & dtpFecha.Value.ToString("dd/MM/yyyy") &
                          "  |  Ventas: " & lblVentasVal.Text &
                          "  |  Ingresos: " & lblIngresosVal.Text
            gbVentas.Text = "Ventas del dia - " & dtpFecha.Value.ToString("dd/MM/yyyy")
            CambiarEstadoCarga(False)
            _cargandoReporte = False
        End Try
    End Function

    Private Sub CambiarEstadoCarga(cargando As Boolean)
        btnVer.Enabled = Not cargando
        btnHoy.Enabled = Not cargando
        btnImprimir.Enabled = Not cargando
        UseWaitCursor = cargando
        sbInfo.Text = If(cargando, "  Cargando reporte...", sbInfo.Text)
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        MsgBox("Funcion de impresion disponible proximamente.")
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Close()
    End Sub

    Private Sub RefrescarReporte()
        If Me.IsDisposed Then Return
        BeginInvoke(New MethodInvoker(AddressOf IniciarCargaReporte))
    End Sub

    Private Sub Form7_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarReporte
    End Sub

    Private Sub Form7_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutReporte()
    End Sub
End Class

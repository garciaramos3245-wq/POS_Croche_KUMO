Imports System.Data.SqlClient

Public Class Form4

    Private _cargandoVentas As Boolean

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_MUTED_PREMIUM As Color = Color.FromArgb(136, 118, 94)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)
    Private ReadOnly CLR_GOLD_PREMIUM As Color = Color.FromArgb(214, 189, 150)
    Private ReadOnly CLR_GREEN_PREMIUM As Color = Color.FromArgb(74, 133, 95)
    Private ReadOnly CLR_RED_PREMIUM As Color = Color.FromArgb(154, 73, 64)

    Public Sub New()
        InitializeComponent()
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                PrepararFormularioHistorial()
                AplicarEstilo()
                dtpFecha.Value = Today
                ConfigurarLayoutHistorial()
            End Sub)
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
        ModEstilo.PrepararVentana(Me)
        PrepararFormularioHistorial()
        AplicarEstilo()
        dtpFecha.Value = Today
        BeginInvoke(New MethodInvoker(AddressOf IniciarCargaVentas))
    End Sub

    Private Async Sub IniciarCargaVentas()
        Await CargarVentasAsync()
    End Sub

    Private Sub PrepararFormularioHistorial()
        Me.MinimumSize = New Size(1180, 720)
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Historial premium"
        Me.DoubleBuffered = True
    End Sub

    Private Sub AplicarEstilo()
        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarStatusStrip(StatusStrip1)
        ModEstilo.EstilarBotonPrimario(btnBuscar)
        ModEstilo.EstilarBotonSecundario(btnHoy)
        ModEstilo.EstilarBotonSecundario(btnTicket)
        ModEstilo.EstilarBotonSecundario(btnImprimir)
        ModEstilo.EstilarBotonPeligro(btnRegresar)

        Me.BackColor = CLR_BG_PREMIUM

        EstilarGroupBox(gbFiltro, "Filtro de ventas")
        EstilarGroupBox(gbTabla, "Historial de ventas")

        lblFechaTxt.Text = "Fecha"
        lblFechaTxt.ForeColor = CLR_MUTED_PREMIUM
        lblFechaTxt.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)

        EstilarPanelResumen(pnlIngresos, lblIngresosTitle, lblIngresosVal, lblIngresosSub, CLR_GREEN_PREMIUM)
        EstilarPanelResumen(pnlVentas, lblVentasTitle, lblVentasVal, lblVentasSub, CLR_DARK_PREMIUM)
        EstilarPanelResumen(pnlPromedio, lblPromedioTitle, lblPromedioVal, lblPromedioSub, CLR_DARK_PREMIUM)
        EstilarPanelResumen(pnlArticulos, lblArticulosTitle, lblArticulosVal, lblArticulosSub, CLR_DARK_PREMIUM)

        lblIngresosTitle.Text = "INGRESOS DEL DIA"
        lblIngresosSub.Text = "ventas cobradas"
        lblVentasTitle.Text = "VENTAS REGISTRADAS"
        lblVentasSub.Text = "tickets emitidos"
        lblPromedioTitle.Text = "TICKET PROMEDIO"
        lblPromedioSub.Text = "importe medio"
        lblArticulosTitle.Text = "ARTICULOS VENDIDOS"
        lblArticulosSub.Text = "piezas desplazadas"

        btnBuscar.Text = "Ver corte"
        btnHoy.Text = "Hoy"
        btnTicket.Text = "Abrir ticket"
        btnImprimir.Text = "Exportar"
        btnRegresar.Text = "Cerrar"

        btnBuscar.BackColor = CLR_DARK_PREMIUM
        btnBuscar.ForeColor = Color.White
        btnBuscar.FlatAppearance.BorderColor = CLR_DARK_PREMIUM
        btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)

        btnHoy.BackColor = CLR_PANEL_PREMIUM
        btnHoy.ForeColor = CLR_TEXT_PREMIUM
        btnHoy.FlatAppearance.BorderColor = CLR_GOLD_PREMIUM
        btnHoy.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnTicket.BackColor = CLR_SURFACE_PREMIUM
        btnTicket.ForeColor = CLR_TEXT_PREMIUM
        btnTicket.FlatAppearance.BorderColor = CLR_GOLD_PREMIUM
        btnTicket.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnImprimir.BackColor = CLR_SURFACE_PREMIUM
        btnImprimir.ForeColor = CLR_TEXT_PREMIUM
        btnImprimir.FlatAppearance.BorderColor = CLR_GOLD_PREMIUM
        btnImprimir.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnRegresar.BackColor = CLR_DARK_PREMIUM
        btnRegresar.ForeColor = Color.FromArgb(244, 226, 193)
        btnRegresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 64, 73)

        EstilarFecha()
        EstilarTabla()
        EstilarBarraEstado()
        ConfigurarLayoutHistorial()
    End Sub

    Private Sub EstilarGroupBox(gb As GroupBox, titulo As String)
        gb.Text = titulo
        gb.BackColor = CLR_SURFACE_PREMIUM
        gb.ForeColor = CLR_TEXT_PREMIUM
        gb.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        gb.Padding = New Padding(8)
    End Sub

    Private Sub EstilarPanelResumen(panel As Panel, titulo As Label, valor As Label, subtitulo As Label, colorValor As Color)
        panel.BackColor = CLR_PANEL_PREMIUM
        panel.BorderStyle = BorderStyle.None

        titulo.ForeColor = CLR_MUTED_PREMIUM
        titulo.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
        titulo.AutoEllipsis = True

        valor.ForeColor = colorValor
        valor.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)

        subtitulo.ForeColor = CLR_MUTED_PREMIUM
        subtitulo.Font = New Font("Segoe UI", 8.0F, FontStyle.Regular)
    End Sub

    Private Sub EstilarFecha()
        dtpFecha.Font = New Font("Segoe UI", 9.5F)
        dtpFecha.CalendarForeColor = CLR_TEXT_PREMIUM
        dtpFecha.CalendarMonthBackground = CLR_SURFACE_PREMIUM
        dtpFecha.CalendarTitleBackColor = CLR_DARK_PREMIUM
        dtpFecha.CalendarTitleForeColor = Color.White
    End Sub

    Private Sub EstilarTabla()
        dgvVentas.BackgroundColor = CLR_SURFACE_PREMIUM
        dgvVentas.BorderStyle = BorderStyle.None
        dgvVentas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvVentas.GridColor = Color.FromArgb(229, 217, 201)
        dgvVentas.RowHeadersVisible = False
        dgvVentas.EnableHeadersVisualStyles = False
        dgvVentas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvVentas.ColumnHeadersHeight = 34
        dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = CLR_DARK_PREMIUM
        dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvVentas.ColumnHeadersDefaultCellStyle.SelectionBackColor = CLR_DARK_PREMIUM
        dgvVentas.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
        dgvVentas.DefaultCellStyle.BackColor = CLR_SURFACE_PREMIUM
        dgvVentas.DefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
        dgvVentas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgvVentas.DefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
        dgvVentas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 248, 242)
        dgvVentas.AlternatingRowsDefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
        dgvVentas.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgvVentas.AlternatingRowsDefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
        dgvVentas.RowTemplate.Height = 32
    End Sub

    Private Sub EstilarBarraEstado()
        StatusStrip1.BackColor = CLR_DARK_PREMIUM
        StatusStrip1.SizingGrip = False
        sbInfo.ForeColor = Color.FromArgb(244, 226, 193)
        sbInfo.Font = New Font("Segoe UI", 8.0F)
    End Sub

    Private Sub ConfigurarLayoutHistorial()
        Dim margen As Integer = 18
        Dim top As Integer = 24
        Dim altoBoton As Integer = 40
        Dim anchoFiltro As Integer = 560
        Dim esp As Integer = 18
        Dim altoResumen As Integer = 128
        Dim anchoPanel As Integer = CInt((Me.ClientSize.Width - (margen * 2) - (esp * 3)) / 4)
        Dim yResumen As Integer = gbFiltro.Bottom + 20
        Dim yTabla As Integer = yResumen + altoResumen + 22
        Dim altoTabla As Integer = Me.ClientSize.Height - yTabla - StatusStrip1.Height - margen

        gbFiltro.SetBounds(margen, top, anchoFiltro, 74)
        btnRegresar.SetBounds(Me.ClientSize.Width - margen - 118, top + 4, 118, altoBoton)

        lblFechaTxt.Location = New Point(18, 30)
        dtpFecha.SetBounds(80, 25, 176, 30)
        btnBuscar.SetBounds(270, 23, 112, 34)
        btnHoy.SetBounds(394, 23, 88, 34)

        pnlIngresos.SetBounds(margen, yResumen, anchoPanel, altoResumen)
        pnlVentas.SetBounds(pnlIngresos.Right + esp, yResumen, anchoPanel, altoResumen)
        pnlPromedio.SetBounds(pnlVentas.Right + esp, yResumen, anchoPanel, altoResumen)
        pnlArticulos.SetBounds(pnlPromedio.Right + esp, yResumen, Me.ClientSize.Width - margen - pnlPromedio.Right - esp, altoResumen)

        gbTabla.SetBounds(margen, yTabla, Me.ClientSize.Width - (margen * 2), altoTabla)
        dgvVentas.SetBounds(14, 30, gbTabla.Width - 28, gbTabla.Height - 94)
        btnTicket.SetBounds(14, gbTabla.Height - 48, 150, 34)
        btnImprimir.SetBounds(btnTicket.Right + 12, gbTabla.Height - 48, 150, 34)

        PosicionarContenidoPanelResumen(pnlIngresos, lblIngresosTitle, lblIngresosVal, lblIngresosSub)
        PosicionarContenidoPanelResumen(pnlVentas, lblVentasTitle, lblVentasVal, lblVentasSub)
        PosicionarContenidoPanelResumen(pnlPromedio, lblPromedioTitle, lblPromedioVal, lblPromedioSub)
        PosicionarContenidoPanelResumen(pnlArticulos, lblArticulosTitle, lblArticulosVal, lblArticulosSub)

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

    Private Sub PosicionarContenidoPanelResumen(panel As Panel, titulo As Label, valor As Label, subtitulo As Label)
        Dim pad As Integer = 16
        titulo.AutoSize = False
        valor.AutoSize = False
        subtitulo.AutoSize = False

        titulo.SetBounds(pad, 14, panel.Width - (pad * 2), 26)
        valor.SetBounds(pad, 46, panel.Width - (pad * 2), 36)
        subtitulo.SetBounds(pad, panel.Height - 34, panel.Width - (pad * 2), 22)
    End Sub

    Private Async Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Await CargarVentasAsync()
    End Sub

    Private Async Sub btnHoy_Click(sender As Object, e As EventArgs) Handles btnHoy.Click
        dtpFecha.Value = Today
        Await CargarVentasAsync()
    End Sub

    Private Async Function CargarVentasAsync() As Task
        If _cargandoVentas Then Return
        _cargandoVentas = True
        CambiarEstadoCarga(True)

        Dim fecha As Date = dtpFecha.Value.Date
        Try
            Dim ventas = Await Task.Run(Function()
                                            Return ObtenerTabla(
                                                "SELECT Id_Pedido AS [N Venta], LOWER(REPLACE(REPLACE(FORMAT(Fecha, 'h:mm tt', 'en-US'), 'AM', 'a.m.'), 'PM', 'p.m.')) AS [Hora], " &
                                                "CONVERT(varchar, Fecha, 103) AS [Fecha], Total " &
                                                "FROM PEDIDOS WHERE CAST(Fecha AS DATE) = @fecha ORDER BY Fecha DESC",
                                                New SqlParameter("@fecha", fecha))
                                        End Function)

            Dim resumen = Await Task.Run(Function()
                                             Return ObtenerTabla(
                                                 "SELECT COUNT(*) AS VentasDia, ISNULL(SUM(Total),0) AS Ingresos, ISNULL(AVG(Total),0) AS Promedio " &
                                                 "FROM PEDIDOS WHERE CAST(Fecha AS DATE) = @fecha",
                                                 New SqlParameter("@fecha", fecha))
                                         End Function)

            Dim articulos = Await Task.Run(Function()
                                               Return ObtenerEscalar(
                                                   "SELECT ISNULL(SUM(d.Cantidad),0) FROM DET_PEDIDOS d " &
                                                   "INNER JOIN PEDIDOS p ON p.Id_Pedido = d.Id_Pedido " &
                                                   "WHERE CAST(p.Fecha AS DATE) = @fecha",
                                                   New SqlParameter("@fecha", fecha))
                                           End Function)

            dgvVentas.DataSource = ventas

            If resumen.Rows.Count > 0 Then
                lblVentasVal.Text = resumen.Rows(0)("VentasDia").ToString()
                lblIngresosVal.Text = "$" & CDec(resumen.Rows(0)("Ingresos")).ToString("N2")
                lblPromedioVal.Text = "$" & CDec(resumen.Rows(0)("Promedio")).ToString("N2")
            End If

            lblArticulosVal.Text = articulos.ToString()

        Catch ex As Exception
            MsgBox("Error al cargar ventas: " & ex.Message)
        Finally
            sbInfo.Text = "  Ventas: " & lblVentasVal.Text & "  |  Ingresos: " & lblIngresosVal.Text & "  |  " & dtpFecha.Value.ToString("dd/MM/yyyy")
            gbTabla.Text = "Ventas del dia - " & dtpFecha.Value.ToString("dd/MM/yyyy")
            CambiarEstadoCarga(False)
            _cargandoVentas = False
        End Try
    End Function

    Private Sub CambiarEstadoCarga(cargando As Boolean)
        btnBuscar.Enabled = Not cargando
        btnHoy.Enabled = Not cargando
        btnTicket.Enabled = Not cargando
        UseWaitCursor = cargando
        sbInfo.Text = If(cargando, "  Cargando historial...", sbInfo.Text)
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
        BeginInvoke(New MethodInvoker(AddressOf IniciarCargaVentas))
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

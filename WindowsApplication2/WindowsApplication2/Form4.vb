' Muestra el historial diario de ventas, resumenes y detalle de tickets.

Imports System.Data.SqlClient

Public Class Form4

    ' Estado de carga, controles dinamicos y paleta visual del historial.

    Private _cargandoVentas As Boolean
    Private gbDetalleVenta As GroupBox
    Private dgvDetalleVenta As DataGridView
    Private lblDetalleResumen As Label

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_MUTED_PREMIUM As Color = Color.FromArgb(136, 118, 94)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)
    Private ReadOnly CLR_GOLD_PREMIUM As Color = Color.FromArgb(214, 189, 150)
    Private ReadOnly CLR_GREEN_PREMIUM As Color = Color.FromArgb(74, 133, 95)
    Private ReadOnly CLR_RED_PREMIUM As Color = Color.FromArgb(154, 73, 64)

    ' Inicializa el formulario y aplica configuracion visual inicial.
    Public Sub New()
        InitializeComponent()
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                If ModEstilo.EstaEnModoDisenio(Me) Then
                    ModEstilo.PrepararVentana(Me)
                End If
                AplicarDisenoHistorial()
                dtpFecha.Value = Today
            End Sub)
    End Sub

    ' Prepara historial, se suscribe a ventas y arranca la carga del dia.
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
        ModEstilo.PrepararVentana(Me)
        AplicarDisenoHistorial()
        dtpFecha.Value = Today
        BeginInvoke(New MethodInvoker(AddressOf IniciarCargaVentas))
    End Sub

    ' Prepara el formulario y aplica los estilos del historial.
    Private Sub AplicarDisenoHistorial()
        PrepararFormularioHistorial()
        AplicarEstilo()
    End Sub

    ' Inicia la carga asincrona de ventas.
    Private Async Sub IniciarCargaVentas()
        Await CargarVentasAsync()
    End Sub

    ' Define tamano minimo, color base, titulo y doble buffer.
    Private Sub PrepararFormularioHistorial()
        Me.MinimumSize = New Size(1180, 720)
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Historial"
        Me.DoubleBuffered = True
    End Sub

    ' Aplica colores, fuentes, botones, tablas y barra de estado del historial.
    Private Sub AplicarEstilo()
        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarStatusStrip(StatusStrip1)
        ModEstilo.EstilarBotonPrimario(btnBuscar)
        ModEstilo.EstilarBotonSecundario(btnHoy)
        ModEstilo.EstilarBotonSecundario(btnTicket)
        ModEstilo.EstilarBotonSecundario(btnImprimir)
        ModEstilo.EstilarBotonPeligro(btnRegresar)

        Me.BackColor = CLR_BG_PREMIUM

        InicializarDetalleVenta()
        EstilarGroupBox(gbFiltro, "Filtro de ventas")
        EstilarGroupBox(gbTabla, "Historial de ventas")
        EstilarGroupBox(gbDetalleVenta, "Detalle de venta")

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
        btnImprimir.Text = "Imprimir ticket"
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

    ' Aplica color, fuente y esquinas redondeadas a un GroupBox.
    Private Sub EstilarGroupBox(gb As GroupBox, titulo As String)
        gb.Text = titulo
        gb.BackColor = CLR_SURFACE_PREMIUM
        gb.ForeColor = CLR_TEXT_PREMIUM
        gb.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        gb.Padding = New Padding(8)
    End Sub

    ' Da formato a una tarjeta de resumen de ventas.
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

    ' Aplica estilo al selector de fecha.
    Private Sub EstilarFecha()
        dtpFecha.Font = New Font("Segoe UI", 9.5F)
        dtpFecha.CalendarForeColor = CLR_TEXT_PREMIUM
        dtpFecha.CalendarMonthBackground = CLR_SURFACE_PREMIUM
        dtpFecha.CalendarTitleBackColor = CLR_DARK_PREMIUM
        dtpFecha.CalendarTitleForeColor = Color.White
    End Sub

    ' Aplica formato comun a las tablas de ventas y detalle.
    Private Sub EstilarTabla()
        For Each dgv As DataGridView In New DataGridView() {dgvVentas, dgvDetalleVenta}
            If dgv Is Nothing Then Continue For
            dgv.BackgroundColor = CLR_SURFACE_PREMIUM
            dgv.BorderStyle = BorderStyle.None
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            dgv.GridColor = Color.FromArgb(229, 217, 201)
            dgv.RowHeadersVisible = False
            dgv.EnableHeadersVisualStyles = False
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            dgv.ColumnHeadersHeight = 34
            dgv.ColumnHeadersDefaultCellStyle.BackColor = CLR_DARK_PREMIUM
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = CLR_DARK_PREMIUM
            dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
            dgv.DefaultCellStyle.BackColor = CLR_SURFACE_PREMIUM
            dgv.DefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
            dgv.DefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 248, 242)
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
            dgv.RowTemplate.Height = 32
            dgv.AllowUserToAddRows = False
            dgv.AllowUserToDeleteRows = False
            dgv.ReadOnly = True
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.MultiSelect = False
        Next
    End Sub

    ' Crea el bloque dinamico donde se muestra el detalle de una venta.
    Private Sub InicializarDetalleVenta()
        If gbDetalleVenta IsNot Nothing Then Return

        gbDetalleVenta = New GroupBox() With {.Name = "gbDetalleVenta", .Text = "Detalle de venta"}
        lblDetalleResumen = New Label() With {
            .Name = "lblDetalleResumen",
            .Text = "Selecciona una venta para ver sus productos.",
            .ForeColor = CLR_MUTED_PREMIUM,
            .Font = New Font("Segoe UI", 8.75F, FontStyle.Bold),
            .AutoEllipsis = True
        }
        dgvDetalleVenta = New DataGridView() With {
            .Name = "dgvDetalleVenta",
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }

        gbDetalleVenta.Controls.Add(lblDetalleResumen)
        gbDetalleVenta.Controls.Add(dgvDetalleVenta)
        Me.Controls.Add(gbDetalleVenta)
    End Sub

    ' Configura la barra inferior del historial.
    Private Sub EstilarBarraEstado()
        StatusStrip1.BackColor = ModEstilo.CLR_HEADER
        StatusStrip1.SizingGrip = False
        sbInfo.ForeColor = Color.White
        sbInfo.Font = New Font("Segoe UI", 8.0F)
        ModEstilo.ConfigurarRelojStatusStrip(Me, StatusStrip1)
    End Sub

    ' Calcula la posicion de filtros, metricas, tablas y botones.
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

        Dim anchoTabla As Integer = CInt((Me.ClientSize.Width - (margen * 2) - esp) * 0.58)
        gbTabla.SetBounds(margen, yTabla, anchoTabla, altoTabla)
        gbDetalleVenta.SetBounds(gbTabla.Right + esp, yTabla, Me.ClientSize.Width - margen - gbTabla.Right - esp, altoTabla)
        dgvVentas.SetBounds(14, 30, gbTabla.Width - 28, gbTabla.Height - 94)
        btnTicket.SetBounds(14, gbTabla.Height - 48, 150, 34)
        btnImprimir.SetBounds(btnTicket.Right + 12, gbTabla.Height - 48, 150, 34)
        lblDetalleResumen.SetBounds(14, 30, gbDetalleVenta.Width - 28, 24)
        dgvDetalleVenta.SetBounds(14, 60, gbDetalleVenta.Width - 28, gbDetalleVenta.Height - 74)

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
        gbDetalleVenta.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        dgvVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvDetalleVenta.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnTicket.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
        btnImprimir.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
    End Sub

    ' Acomoda titulo, valor y subtitulo dentro de una tarjeta de resumen.
    Private Sub PosicionarContenidoPanelResumen(panel As Panel, titulo As Label, valor As Label, subtitulo As Label)
        Dim pad As Integer = 16
        titulo.AutoSize = False
        valor.AutoSize = False
        subtitulo.AutoSize = False

        titulo.SetBounds(pad, 14, panel.Width - (pad * 2), 26)
        valor.SetBounds(pad, 46, panel.Width - (pad * 2), 36)
        subtitulo.SetBounds(pad, panel.Height - 34, panel.Width - (pad * 2), 22)
    End Sub

    ' Carga ventas para la fecha seleccionada.
    Private Async Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Await CargarVentasAsync()
    End Sub

    ' Regresa la fecha a hoy y recarga ventas.
    Private Async Sub btnHoy_Click(sender As Object, e As EventArgs) Handles btnHoy.Click
        dtpFecha.Value = Today
        Await CargarVentasAsync()
    End Sub

    ' Carga ventas, resumen y articulos del dia en segundo plano.
    Private Async Function CargarVentasAsync() As Task
        If _cargandoVentas Then Return
        _cargandoVentas = True
        CambiarEstadoCarga(True)

        Dim fecha As Date = dtpFecha.Value.Date
        Try
            Await Task.Run(Sub() AsegurarColumnasPagoPedido())

            Dim ventas = Await Task.Run(Function()
                                            Return ObtenerTabla(
                                                "SELECT p.Id_Pedido AS [N Venta], LOWER(REPLACE(REPLACE(FORMAT(p.Fecha, 'h:mm tt', 'en-US'), 'AM', 'a.m.'), 'PM', 'p.m.')) AS [Hora], " &
                                                "CONVERT(varchar, p.Fecha, 103) AS [Fecha], ISNULL(p.MetodoPago, 'Efectivo') AS [Metodo], " &
                                                "ISNULL(p.Subtotal, p.Total) AS [Subtotal], ISNULL(p.Descuento, 0) AS [Descuento], ISNULL(p.IVA, 0) AS [IVA], p.Total, " &
                                                "CASE WHEN ISNULL(Cancelada, 0) = 1 THEN 'Cancelada' ELSE 'Activa' END AS [Estado] " &
                                                "FROM PEDIDOS p " &
                                                "WHERE CAST(p.Fecha AS DATE) = @fecha " &
                                                "AND EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido) " &
                                                "ORDER BY p.Fecha DESC",
                                                New SqlParameter("@fecha", fecha))
                                        End Function)

            Dim resumen = Await Task.Run(Function()
                                                 Return ObtenerTabla(
                                                     "SELECT COUNT(*) AS VentasDia, ISNULL(SUM(Total),0) AS Ingresos, ISNULL(AVG(Total),0) AS Promedio " &
                                                 "FROM PEDIDOS p WHERE CAST(p.Fecha AS DATE) = @fecha AND ISNULL(p.Cancelada, 0) = 0 " &
                                                 "AND EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido)",
                                                 New SqlParameter("@fecha", fecha))
                                         End Function)

            Dim articulos = Await Task.Run(Function()
                                               Return ObtenerEscalar(
                                                   "SELECT ISNULL(SUM(d.Cantidad),0) FROM DET_PEDIDOS d " &
                                                   "INNER JOIN PEDIDOS p ON p.Id_Pedido = d.Id_Pedido " &
                                                   "WHERE CAST(p.Fecha AS DATE) = @fecha AND ISNULL(p.Cancelada, 0) = 0",
                                                   New SqlParameter("@fecha", fecha))
                                           End Function)

            dgvVentas.DataSource = ventas
            FormatearColumnasVentas()

            If resumen.Rows.Count > 0 Then
                lblVentasVal.Text = resumen.Rows(0)("VentasDia").ToString()
                lblIngresosVal.Text = "$" & CDec(resumen.Rows(0)("Ingresos")).ToString("N2")
                lblPromedioVal.Text = "$" & CDec(resumen.Rows(0)("Promedio")).ToString("N2")
            End If

            lblArticulosVal.Text = articulos.ToString()
            CargarDetalleVentaSeleccionada()

        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Ventas no disponibles", CrearMensajeErrorDatos("cargar el historial", ex), ModMensajes.TipoAviso.Error)
        Finally
            sbInfo.Text = "  Ventas: " & lblVentasVal.Text & "  |  Ingresos: " & lblIngresosVal.Text & "  |  " & dtpFecha.Value.ToString("dd/MM/yyyy")
            gbTabla.Text = "Ventas del dia - " & dtpFecha.Value.ToString("dd/MM/yyyy")
            CambiarEstadoCarga(False)
            _cargandoVentas = False
        End Try
    End Function

    ' Aplica formato de moneda a columnas de importes.
    Private Sub FormatearColumnasVentas()
        For Each nombre As String In New String() {"Subtotal", "Descuento", "IVA", "Total"}
            If dgvVentas.Columns.Contains(nombre) Then dgvVentas.Columns(nombre).DefaultCellStyle.Format = "C2"
        Next
    End Sub

    ' Guarda el folio seleccionado y carga sus productos.
    Private Sub dgvVentas_SelectionChanged(sender As Object, e As EventArgs) Handles dgvVentas.SelectionChanged
        If _cargandoVentas Then Return
        CargarDetalleVentaSeleccionada()
    End Sub

    ' Obtiene el folio seleccionado y carga su detalle.
    Private Sub CargarDetalleVentaSeleccionada()
        If dgvVentas.CurrentRow Is Nothing Then
            LimpiarDetalleVenta()
            Return
        End If

        Dim id As Integer = 0
        If Not Integer.TryParse(dgvVentas.CurrentRow.Cells("N Venta").Value.ToString(), id) Then
            LimpiarDetalleVenta()
            Return
        End If

        CargarDetalleVenta(id)
    End Sub

    ' Consulta datos de pago y productos de una venta concreta.
    Private Sub CargarDetalleVenta(id As Integer)
        Try
            Dim venta = ObtenerTabla(
                "SELECT ISNULL(MetodoPago, 'Efectivo') AS MetodoPago, ISNULL(PagoCon, Total) AS PagoCon, " &
                "ISNULL(Cambio, 0) AS Cambio, Total, ISNULL(Cancelada, 0) AS Cancelada, FechaCancelacion " &
                "FROM PEDIDOS p WHERE p.Id_Pedido = @id " &
                "AND EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido)",
                New SqlParameter("@id", id))

            Dim detalle = ObtenerTabla(
                "SELECT p.NombrePr AS [Producto], d.Cantidad, d.PrecioVentaMomento AS [Precio unitario], " &
                "(d.Cantidad * d.PrecioVentaMomento) AS [Importe] " &
                "FROM DET_PEDIDOS d " &
                "INNER JOIN PRODUCTO p ON p.Id_Producto = d.Id_Producto " &
                "WHERE d.Id_Pedido = @id ORDER BY p.NombrePr",
                New SqlParameter("@id", id))

            dgvDetalleVenta.DataSource = detalle
            If dgvDetalleVenta.Columns.Contains("Precio unitario") Then dgvDetalleVenta.Columns("Precio unitario").DefaultCellStyle.Format = "C2"
            If dgvDetalleVenta.Columns.Contains("Importe") Then dgvDetalleVenta.Columns("Importe").DefaultCellStyle.Format = "C2"

            If venta.Rows.Count > 0 Then
                Dim row = venta.Rows(0)
                Dim estado As String = If(CBool(row("Cancelada")), "Cancelada", "Activa")
                lblDetalleResumen.Text = "Venta V-" & id.ToString("000") &
                    " | " & estado &
                    " | " & row("MetodoPago").ToString() &
                    " | Total $" & CDec(row("Total")).ToString("N2") &
                    " | Pago $" & CDec(row("PagoCon")).ToString("N2") &
                    " | Cambio $" & CDec(row("Cambio")).ToString("N2")
            Else
                lblDetalleResumen.Text = "Venta V-" & id.ToString("000")
            End If
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Detalle no disponible", CrearMensajeErrorDatos("cargar el detalle de la venta", ex), ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    ' Limpia la tabla y mensaje de detalle cuando no hay venta seleccionada.
    Private Sub LimpiarDetalleVenta()
        If dgvDetalleVenta IsNot Nothing Then dgvDetalleVenta.DataSource = Nothing
        If lblDetalleResumen IsNot Nothing Then lblDetalleResumen.Text = "Selecciona una venta para ver sus productos."
    End Sub

    ' Activa o desactiva botones y cursor mientras se cargan datos.
    Private Sub CambiarEstadoCarga(cargando As Boolean)
        btnBuscar.Enabled = Not cargando
        btnHoy.Enabled = Not cargando
        btnTicket.Enabled = Not cargando
        UseWaitCursor = cargando
        sbInfo.Text = If(cargando, "  Cargando historial...", sbInfo.Text)
    End Sub

    ' Abre el formulario de ticket para la venta seleccionada.
    Private Sub btnTicket_Click(sender As Object, e As EventArgs) Handles btnTicket.Click
        If dgvVentas.CurrentRow Is Nothing Then
            ModMensajes.Mostrar(Me, "Selecciona una venta", "Elige una venta de la lista para abrir su ticket.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        Dim id As Integer = CInt(dgvVentas.CurrentRow.Cells("N Venta").Value)
        Dim ticket As New Form6(id)
        ticket.ShowDialog()
    End Sub

    ' Genera el texto del ticket seleccionado y abre la vista previa de impresion.
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If dgvVentas.CurrentRow Is Nothing Then
            ModMensajes.Mostrar(Me, "Selecciona una venta", "Elige una venta de la lista para imprimir su ticket.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        Dim id As Integer = CInt(dgvVentas.CurrentRow.Cells("N Venta").Value)
        Dim texto = Form6.ObtenerTextoTicket(id)
        Form6.MostrarVistaPreviaTicket(texto, Me, "Ticket de venta V-" & id.ToString("000"))
    End Sub

    ' Cierra el formulario actual.
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Close()
    End Sub

    ' Vuelve a cargar ventas cuando otro modulo registra cambios.
    Private Sub RefrescarVentas()
        If Me.IsDisposed Then Return
        BeginInvoke(New MethodInvoker(AddressOf IniciarCargaVentas))
    End Sub

    ' Quita la suscripcion al evento de ventas al cerrar.
    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
    End Sub

    ' Reacomoda el historial al cambiar el tamano.
    Private Sub Form4_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutHistorial()
    End Sub
End Class

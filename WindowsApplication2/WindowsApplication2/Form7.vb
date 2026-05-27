' Genera reportes diarios y exporta la informacion a un archivo Excel.

Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Text

Public Class Form7

    ' Estado de carga, controles dinamicos y estilos usados para reportes y Excel.

    Private _cargandoReporte As Boolean
    Private gbDetalleVenta As GroupBox
    Private dgvDetalleVenta As DataGridView
    Private lblDetalleResumen As Label

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_MUTED_PREMIUM As Color = Color.FromArgb(136, 118, 94)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)

    ' Estilo base de celda sin formato especial.
    Private Const EST_NORMAL As Integer = 0
    ' Estilo para titulos principales de las hojas Excel.
    Private Const EST_TITULO As Integer = 1
    ' Estilo para encabezados de tablas Excel.
    Private Const EST_ENCABEZADO As Integer = 2
    ' Estilo de moneda para filas normales.
    Private Const EST_DINERO As Integer = 3
    ' Estilo de texto para filas normales.
    Private Const EST_TEXTO As Integer = 4
    ' Estilo de texto para filas alternas.
    Private Const EST_TEXTO_ALTERNO As Integer = 5
    ' Estilo de moneda para filas alternas.
    Private Const EST_DINERO_ALTERNO As Integer = 6
    ' Estilo de etiquetas dentro del resumen ejecutivo.
    Private Const EST_ETIQUETA_RESUMEN As Integer = 7
    ' Estilo de valores numericos del resumen ejecutivo.
    Private Const EST_VALOR_RESUMEN As Integer = 8
    ' Estilo de valores de dinero del resumen ejecutivo.
    Private Const EST_DINERO_RESUMEN As Integer = 9
    ' Estilo para subtitulos y notas de contexto.
    Private Const EST_SUBTITULO As Integer = 10
    ' Estilo numerico para filas normales.
    Private Const EST_NUMERO As Integer = 11
    ' Estilo numerico para filas alternas.
    Private Const EST_NUMERO_ALTERNO As Integer = 12
    ' Estilo para mensajes o notas dentro de Excel.
    Private Const EST_NOTA As Integer = 13

    ' Inicializa el formulario y aplica configuracion visual inicial.
    Public Sub New()
        InitializeComponent()
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                If ModEstilo.EstaEnModoDisenio(Me) Then
                    ModEstilo.PrepararVentana(Me)
                End If
                dtpFecha.Value = Today
                AplicarDisenoReporte()
            End Sub)
    End Sub

    ' Prepara la pantalla de reporte y carga los datos de hoy.
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AddHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarReporte
        Try
            dtpFecha.Value = Today
            AplicarDisenoReporte()
            BeginInvoke(New MethodInvoker(AddressOf IniciarCargaReporte))
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Reporte no disponible", "No se pudo abrir el reporte." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    ' Aplica estilos generales, botones y layout del reporte.
    Private Sub AplicarDisenoReporte()
        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarStatusStrip(StatusStrip1)
        ModEstilo.ConfigurarRelojStatusStrip(Me, StatusStrip1)
        ModEstilo.EstilarBotonPrimario(btnVer)
        ModEstilo.EstilarBotonSecundario(btnHoy)
        ModEstilo.EstilarBotonSecundario(btnImprimir)
        ModEstilo.EstilarBotonPeligro(btnRegresar)
        AplicarEstiloReportePremium()
        ConfigurarLayoutReporte()
    End Sub

    ' Inicia la carga asincrona del reporte.
    Private Async Sub IniciarCargaReporte()
        Await CargarReporteAsync()
    End Sub

    ' Configura colores, textos, tablas y tarjetas del reporte.
    Private Sub AplicarEstiloReportePremium()
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Reporte"

        gbResumen.BackColor = CLR_PANEL_PREMIUM
        gbResumen.ForeColor = CLR_TEXT_PREMIUM
        gbResumen.Text = "Resumen ejecutivo"

        gbVentas.BackColor = CLR_SURFACE_PREMIUM
        gbVentas.ForeColor = CLR_TEXT_PREMIUM
        gbVentas.Text = "Ventas del dia"

        gbTop.BackColor = CLR_SURFACE_PREMIUM
        gbTop.ForeColor = CLR_TEXT_PREMIUM
        gbTop.Text = "Top productos"

        InicializarDetalleVenta()
        gbDetalleVenta.BackColor = CLR_SURFACE_PREMIUM
        gbDetalleVenta.ForeColor = CLR_TEXT_PREMIUM
        gbDetalleVenta.Text = "Detalle de venta"

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

        For Each dgv As DataGridView In New DataGridView() {dgvVentas, dgvTop, dgvDetalleVenta}
            If dgv Is Nothing Then Continue For
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

    ' Distribuye filtros, resumen, ventas, top productos y detalle.
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
        Dim anchoDerecho As Integer = Me.ClientSize.Width - margen - gbVentas.Right - esp
        Dim altoTop As Integer = Math.Max(150, CInt((altoDisponible - esp) * 0.44))
        gbTop.SetBounds(gbVentas.Right + esp, yBloques, anchoDerecho, altoTop)
        gbDetalleVenta.SetBounds(gbTop.Left, gbTop.Bottom + esp, anchoDerecho, altoDisponible - altoTop - esp)

        dgvVentas.SetBounds(14, 30, gbVentas.Width - 28, gbVentas.Height - 90)
        btnImprimir.SetBounds(14, gbVentas.Height - 48, 176, 34)
        dgvTop.SetBounds(14, 30, gbTop.Width - 28, gbTop.Height - 44)
        lblDetalleResumen.SetBounds(14, 30, gbDetalleVenta.Width - 28, 24)
        dgvDetalleVenta.SetBounds(14, 60, gbDetalleVenta.Width - 28, gbDetalleVenta.Height - 74)

        gbResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        gbVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbTop.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        gbDetalleVenta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        dgvVentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvTop.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvDetalleVenta.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnImprimir.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
        btnRegresar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    ' Acomoda titulo, valor y subtitulo dentro de una tarjeta del reporte.
    Private Sub PosicionarPanelResumen(panel As Panel, titulo As Label, valor As Label, subtitulo As Label)
        Dim pad As Integer = 14
        titulo.AutoSize = False
        valor.AutoSize = False
        subtitulo.AutoSize = False

        titulo.SetBounds(pad, 12, panel.Width - (pad * 2), 26)
        valor.SetBounds(pad, 40, panel.Width - (pad * 2), 36)
        subtitulo.SetBounds(pad, panel.Height - 32, panel.Width - (pad * 2), 20)
    End Sub

    ' Carga el reporte para la fecha seleccionada.
    Private Async Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click
        Await CargarReporteAsync()
    End Sub

    ' Regresa la fecha a hoy y recarga ventas.
    Private Async Sub btnHoy_Click(sender As Object, e As EventArgs) Handles btnHoy.Click
        dtpFecha.Value = Today
        Await CargarReporteAsync()
    End Sub

    ' Consulta ventas, resumen, articulos y top productos del dia.
    Private Async Function CargarReporteAsync() As Task
        If _cargandoReporte Then Return
        _cargandoReporte = True
        CambiarEstadoCarga(True)

        Dim fecha As Date = dtpFecha.Value.Date
        Try
            Await Task.Run(Sub() AsegurarColumnasPagoPedido())

            Dim ventas = Await Task.Run(Function()
                                            Return ObtenerTabla(
                                                "SELECT p.Id_Pedido AS [N Venta], LOWER(REPLACE(REPLACE(FORMAT(p.Fecha, 'h:mm tt', 'en-US'), 'AM', 'a.m.'), 'PM', 'p.m.')) AS [Hora], " &
                                                "ISNULL(p.MetodoPago, 'Efectivo') AS [Metodo], ISNULL(p.Subtotal, p.Total) AS [Subtotal], " &
                                                "ISNULL(p.Descuento, 0) AS [Descuento], ISNULL(p.IVA, 0) AS [IVA], p.Total, " &
                                                "ISNULL(p.PagoCon, p.Total) AS [Pago], ISNULL(p.Cambio, 0) AS [Cambio], " &
                                                "CASE WHEN ISNULL(p.Cancelada, 0) = 1 THEN 'Cancelada' ELSE 'Activa' END AS [Estado] " &
                                                "FROM PEDIDOS p WHERE CAST(p.Fecha AS DATE)=@fecha " &
                                                "AND EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido) " &
                                                "ORDER BY p.Fecha DESC",
                                                New SqlParameter("@fecha", fecha))
                                        End Function)

            Dim resumen = Await Task.Run(Function()
                                                 Return ObtenerTabla(
                                                     "SELECT COUNT(*) AS VentasDia, ISNULL(SUM(Total),0) AS Ingresos, ISNULL(AVG(Total),0) AS Promedio " &
                                                 "FROM PEDIDOS p WHERE CAST(p.Fecha AS DATE)=@fecha AND ISNULL(p.Cancelada, 0) = 0 " &
                                                 "AND EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido)",
                                                 New SqlParameter("@fecha", fecha))
                                         End Function)

            Dim articulos = Await Task.Run(Function()
                                               Return ObtenerEscalar(
                                                   "SELECT ISNULL(SUM(d.Cantidad),0) FROM DET_PEDIDOS d " &
                                                   "INNER JOIN PEDIDOS p ON p.Id_Pedido = d.Id_Pedido " &
                                                   "WHERE CAST(p.Fecha AS DATE)=@fecha AND ISNULL(p.Cancelada, 0) = 0",
                                                   New SqlParameter("@fecha", fecha))
                                           End Function)

            Dim topProductos = Await Task.Run(Function()
                                                  Return ObtenerTabla(
                                                      "SELECT TOP 5 p.NombrePr AS [Producto], SUM(d.Cantidad) AS [Unidades] " &
                                                      "FROM DET_PEDIDOS d " &
                                                      "INNER JOIN PRODUCTO p ON p.Id_Producto = d.Id_Producto " &
                                                      "INNER JOIN PEDIDOS pd ON pd.Id_Pedido = d.Id_Pedido " &
                                                      "WHERE CAST(pd.Fecha AS DATE)=@fecha AND ISNULL(pd.Cancelada, 0) = 0 " &
                                                      "GROUP BY p.NombrePr ORDER BY Unidades DESC",
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
            dgvTop.DataSource = topProductos
            CargarDetalleVentaSeleccionada()

        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Reporte no disponible", CrearMensajeErrorDatos("cargar el reporte", ex), ModMensajes.TipoAviso.Error)
        Finally
            sbInfo.Text = "  Reporte: " & dtpFecha.Value.ToString("dd/MM/yyyy") &
                          "  |  Ventas: " & lblVentasVal.Text &
                          "  |  Ingresos: " & lblIngresosVal.Text
            gbVentas.Text = "Ventas del dia - " & dtpFecha.Value.ToString("dd/MM/yyyy")
            CambiarEstadoCarga(False)
            _cargandoReporte = False
        End Try
    End Function

    ' Aplica formato de moneda a columnas de importes.
    Private Sub FormatearColumnasVentas()
        For Each nombre As String In New String() {"Subtotal", "Descuento", "IVA", "Total", "Pago", "Cambio"}
            If dgvVentas.Columns.Contains(nombre) Then dgvVentas.Columns(nombre).DefaultCellStyle.Format = "C2"
        Next
    End Sub

    ' Guarda el folio seleccionado y carga sus productos.
    Private Sub dgvVentas_SelectionChanged(sender As Object, e As EventArgs) Handles dgvVentas.SelectionChanged
        If _cargandoReporte Then Return
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
                "ISNULL(Cambio, 0) AS Cambio, Total, ISNULL(Cancelada, 0) AS Cancelada " &
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
                lblDetalleResumen.Text = "Venta V-" & id.ToString("000") &
                    " | " & If(CBool(row("Cancelada")), "Cancelada", "Activa") &
                    " | " & row("MetodoPago").ToString() &
                    " | Total $" & CDec(row("Total")).ToString("N2") &
                    " | Pago $" & CDec(row("PagoCon")).ToString("N2") &
                    " | Cambio $" & CDec(row("Cambio")).ToString("N2")
            Else
                lblDetalleResumen.Text = "Venta V-" & id.ToString("000")
            End If
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Detalle no disponible", CrearMensajeErrorDatos("cargar el detalle del reporte", ex), ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    ' Limpia la tabla y mensaje de detalle cuando no hay venta seleccionada.
    Private Sub LimpiarDetalleVenta()
        If dgvDetalleVenta IsNot Nothing Then dgvDetalleVenta.DataSource = Nothing
        If lblDetalleResumen IsNot Nothing Then lblDetalleResumen.Text = "Selecciona una venta para ver sus productos."
    End Sub

    ' Activa o desactiva botones y cursor mientras se cargan datos.
    Private Sub CambiarEstadoCarga(cargando As Boolean)
        btnVer.Enabled = Not cargando
        btnHoy.Enabled = Not cargando
        btnImprimir.Enabled = Not cargando
        UseWaitCursor = cargando
        sbInfo.Text = If(cargando, "  Cargando reporte...", sbInfo.Text)
    End Sub

    ' Exporta el reporte diario a Excel.
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        ExportarReporteExcel()
    End Sub

    ' Muestra el selector de archivo y controla la exportacion del reporte.
    Private Sub ExportarReporteExcel()
        If _cargandoReporte Then
            ModMensajes.Mostrar(Me, "Reporte en carga", "Espera a que termine de cargar el reporte antes de exportarlo.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Title = "Exportar reporte a Excel"
            sfd.Filter = "Libro de Excel (*.xlsx)|*.xlsx"
            sfd.FileName = "Reporte_KUMO_" & dtpFecha.Value.ToString("yyyyMMdd") & ".xlsx"
            sfd.OverwritePrompt = True

            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return

            Try
                CrearArchivoExcel(sfd.FileName)
                ModMensajes.Mostrar(Me, "Reporte exportado", "El archivo de Excel se genero correctamente." & vbCrLf & sfd.FileName, ModMensajes.TipoAviso.Exito)
            Catch ex As Exception
                ModMensajes.Mostrar(Me, "No se pudo exportar", "No se pudo exportar el reporte." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
            End Try
        End Using
    End Sub

    ' Crea un archivo XLSX como paquete ZIP con hojas XML internas.
    Private Sub CrearArchivoExcel(ruta As String)
        If File.Exists(ruta) Then File.Delete(ruta)

        Using fs As New FileStream(ruta, FileMode.CreateNew, FileAccess.ReadWrite)
            Using zip As New ZipArchive(fs, ZipArchiveMode.Create)
                EscribirEntradaZip(zip, "[Content_Types].xml", ObtenerContentTypesExcel())
                EscribirEntradaZip(zip, "_rels/.rels", ObtenerRelacionesRaizExcel())
                EscribirEntradaZip(zip, "xl/workbook.xml", ObtenerWorkbookExcel())
                EscribirEntradaZip(zip, "xl/_rels/workbook.xml.rels", ObtenerRelacionesWorkbookExcel())
                EscribirEntradaZip(zip, "xl/styles.xml", ObtenerEstilosExcel())
                EscribirEntradaZip(zip, "xl/worksheets/sheet1.xml", ConstruirHojaResumenExcel())
                EscribirEntradaZip(zip, "xl/worksheets/sheet2.xml", ConstruirHojaGridExcel("Ventas del dia", dgvVentas))
                EscribirEntradaZip(zip, "xl/worksheets/sheet3.xml", ConstruirHojaGridExcel("Top productos", dgvTop))
                EscribirEntradaZip(zip, "xl/worksheets/sheet4.xml", ConstruirHojaTablaExcel("Detalle de ventas", ObtenerDetalleVentasReporte()))
                EscribirEntradaZip(zip, "xl/worksheets/sheet5.xml", ConstruirHojaTablaExcel("Metodos de pago", ObtenerMetodosPagoReporte()))
            End Using
        End Using
    End Sub

    ' Consulta el detalle de productos vendidos por ticket para el reporte.
    Private Function ObtenerDetalleVentasReporte() As DataTable
        Return ObtenerTabla(
            "SELECT pd.Id_Pedido AS [N Venta], LOWER(REPLACE(REPLACE(FORMAT(pd.Fecha, 'h:mm tt', 'en-US'), 'AM', 'a.m.'), 'PM', 'p.m.')) AS [Hora], " &
            "CASE WHEN ISNULL(pd.Cancelada, 0) = 1 THEN 'Cancelada' ELSE 'Activa' END AS [Estado], " &
            "ISNULL(pd.MetodoPago, 'Efectivo') AS [Metodo], p.NombrePr AS [Producto], d.Cantidad, " &
            "d.PrecioVentaMomento AS [Precio unitario], (d.Cantidad * d.PrecioVentaMomento) AS [Importe] " &
            "FROM DET_PEDIDOS d " &
            "INNER JOIN PEDIDOS pd ON pd.Id_Pedido = d.Id_Pedido " &
            "INNER JOIN PRODUCTO p ON p.Id_Producto = d.Id_Producto " &
            "WHERE CAST(pd.Fecha AS DATE)=@fecha " &
            "AND EXISTS (SELECT 1 FROM DET_PEDIDOS dv WHERE dv.Id_Pedido = pd.Id_Pedido) " &
            "ORDER BY pd.Fecha DESC, pd.Id_Pedido DESC, p.NombrePr",
            New SqlParameter("@fecha", dtpFecha.Value.Date))
    End Function

    ' Resume ventas, total, pago y cambio por metodo de pago.
    Private Function ObtenerMetodosPagoReporte() As DataTable
        Return ObtenerTabla(
            "SELECT ISNULL(MetodoPago, 'Efectivo') AS [Metodo de pago], COUNT(*) AS [Ventas], " &
            "ISNULL(SUM(ISNULL(Subtotal, Total)), 0) AS [Subtotal], ISNULL(SUM(ISNULL(Descuento, 0)), 0) AS [Descuentos], " &
            "ISNULL(SUM(ISNULL(IVA, 0)), 0) AS [IVA], ISNULL(SUM(Total), 0) AS [Total] " &
            "FROM PEDIDOS p WHERE CAST(p.Fecha AS DATE)=@fecha AND ISNULL(p.Cancelada, 0) = 0 " &
            "AND EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido) " &
            "GROUP BY ISNULL(MetodoPago, 'Efectivo') ORDER BY [Total] DESC",
            New SqlParameter("@fecha", dtpFecha.Value.Date))
    End Function

    ' Agrega una entrada de texto UTF-8 dentro del archivo XLSX.
    Private Sub EscribirEntradaZip(zip As ZipArchive, rutaEntrada As String, contenido As String)
        Dim entry = zip.CreateEntry(rutaEntrada)
        Using writer As New StreamWriter(entry.Open(), New UTF8Encoding(False))
            writer.Write(contenido)
        End Using
    End Sub

    ' Devuelve el XML de tipos de contenido requerido por XLSX.
    Private Function ObtenerContentTypesExcel() As String
        Return "<?xml version=""1.0"" encoding=""UTF-8""?>" &
               "<Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types"">" &
               "<Default Extension=""rels"" ContentType=""application/vnd.openxmlformats-package.relationships+xml""/>" &
               "<Default Extension=""xml"" ContentType=""application/xml""/>" &
               "<Override PartName=""/xl/workbook.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml""/>" &
               "<Override PartName=""/xl/styles.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml""/>" &
               "<Override PartName=""/xl/worksheets/sheet1.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>" &
               "<Override PartName=""/xl/worksheets/sheet2.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>" &
               "<Override PartName=""/xl/worksheets/sheet3.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>" &
               "<Override PartName=""/xl/worksheets/sheet4.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>" &
               "<Override PartName=""/xl/worksheets/sheet5.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>" &
               "</Types>"
    End Function

    ' Devuelve las relaciones raiz del paquete XLSX.
    Private Function ObtenerRelacionesRaizExcel() As String
        Return "<?xml version=""1.0"" encoding=""UTF-8""?>" &
               "<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">" &
               "<Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""xl/workbook.xml""/>" &
               "</Relationships>"
    End Function

    ' Devuelve el XML del libro con la lista de hojas.
    Private Function ObtenerWorkbookExcel() As String
        Return "<?xml version=""1.0"" encoding=""UTF-8""?>" &
               "<workbook xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"" xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships"">" &
               "<bookViews><workbookView xWindow=""0"" yWindow=""0"" windowWidth=""22000"" windowHeight=""13000""/></bookViews>" &
               "<sheets>" &
               "<sheet name=""Resumen"" sheetId=""1"" r:id=""rId1""/>" &
               "<sheet name=""Ventas"" sheetId=""2"" r:id=""rId2""/>" &
               "<sheet name=""Top productos"" sheetId=""3"" r:id=""rId3""/>" &
               "<sheet name=""Detalle ventas"" sheetId=""4"" r:id=""rId4""/>" &
               "<sheet name=""Metodos pago"" sheetId=""5"" r:id=""rId5""/>" &
               "</sheets>" &
               "</workbook>"
    End Function

    ' Devuelve las relaciones internas del libro hacia hojas y estilos.
    Private Function ObtenerRelacionesWorkbookExcel() As String
        Return "<?xml version=""1.0"" encoding=""UTF-8""?>" &
               "<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">" &
               "<Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet1.xml""/>" &
               "<Relationship Id=""rId2"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet2.xml""/>" &
               "<Relationship Id=""rId3"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet3.xml""/>" &
               "<Relationship Id=""rId4"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet4.xml""/>" &
               "<Relationship Id=""rId5"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet5.xml""/>" &
               "<Relationship Id=""rId6"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles"" Target=""styles.xml""/>" &
               "</Relationships>"
    End Function

    ' Devuelve el XML de estilos, fuentes, rellenos, bordes y formatos numericos.
    Private Function ObtenerEstilosExcel() As String
        Return "<?xml version=""1.0"" encoding=""UTF-8""?>" &
               "<styleSheet xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">" &
               "<numFmts count=""1""><numFmt numFmtId=""164"" formatCode=""&quot;$&quot;#,##0.00""/></numFmts>" &
               "<fonts count=""6"">" &
               "<font><sz val=""10""/><color rgb=""FF4C4237""/><name val=""Segoe UI""/></font>" &
               "<font><b/><sz val=""18""/><color rgb=""FFFFFFFF""/><name val=""Segoe UI""/></font>" &
               "<font><b/><sz val=""10""/><color rgb=""FFFFFFFF""/><name val=""Segoe UI""/></font>" &
               "<font><b/><sz val=""9""/><color rgb=""FF88765E""/><name val=""Segoe UI""/></font>" &
               "<font><b/><sz val=""16""/><color rgb=""FF2E343C""/><name val=""Segoe UI""/></font>" &
               "<font><b/><sz val=""16""/><color rgb=""FF4A855F""/><name val=""Segoe UI""/></font>" &
               "</fonts>" &
               "<fills count=""7"">" &
               "<fill><patternFill patternType=""none""/></fill>" &
               "<fill><patternFill patternType=""gray125""/></fill>" &
               "<fill><patternFill patternType=""solid""><fgColor rgb=""FF2E343C""/><bgColor indexed=""64""/></patternFill></fill>" &
               "<fill><patternFill patternType=""solid""><fgColor rgb=""FFE8D7B7""/><bgColor indexed=""64""/></patternFill></fill>" &
               "<fill><patternFill patternType=""solid""><fgColor rgb=""FFFFFCF7""/><bgColor indexed=""64""/></patternFill></fill>" &
               "<fill><patternFill patternType=""solid""><fgColor rgb=""FFF7F1E8""/><bgColor indexed=""64""/></patternFill></fill>" &
               "<fill><patternFill patternType=""solid""><fgColor rgb=""FFFBF7EF""/><bgColor indexed=""64""/></patternFill></fill>" &
               "</fills>" &
               "<borders count=""3"">" &
               "<border><left/><right/><top/><bottom/><diagonal/></border>" &
               "<border><left style=""thin""><color rgb=""FFE5D9C9""/></left><right style=""thin""><color rgb=""FFE5D9C9""/></right><top style=""thin""><color rgb=""FFE5D9C9""/></top><bottom style=""thin""><color rgb=""FFE5D9C9""/></bottom><diagonal/></border>" &
               "<border><left/><right/><top/><bottom style=""medium""><color rgb=""FFD6BD96""/></bottom><diagonal/></border>" &
               "</borders>" &
               "<cellStyleXfs count=""1""><xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""0""/></cellStyleXfs>" &
               "<cellXfs count=""14"">" &
               "<xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""0"" xfId=""0""/>" &
               "<xf numFmtId=""0"" fontId=""1"" fillId=""2"" borderId=""0"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyAlignment=""1""><alignment horizontal=""center"" vertical=""center""/></xf>" &
               "<xf numFmtId=""0"" fontId=""2"" fillId=""2"" borderId=""1"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""center"" vertical=""center"" wrapText=""1""/></xf>" &
               "<xf numFmtId=""164"" fontId=""0"" fillId=""4"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""right"" vertical=""center""/></xf>" &
               "<xf numFmtId=""0"" fontId=""0"" fillId=""4"" borderId=""1"" xfId=""0"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""left"" vertical=""center""/></xf>" &
               "<xf numFmtId=""0"" fontId=""0"" fillId=""6"" borderId=""1"" xfId=""0"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""left"" vertical=""center""/></xf>" &
               "<xf numFmtId=""164"" fontId=""0"" fillId=""6"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""right"" vertical=""center""/></xf>" &
               "<xf numFmtId=""0"" fontId=""3"" fillId=""5"" borderId=""1"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""center"" vertical=""center""/></xf>" &
               "<xf numFmtId=""0"" fontId=""4"" fillId=""5"" borderId=""1"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""center"" vertical=""center""/></xf>" &
               "<xf numFmtId=""164"" fontId=""5"" fillId=""5"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFont=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""center"" vertical=""center""/></xf>" &
               "<xf numFmtId=""0"" fontId=""3"" fillId=""3"" borderId=""2"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""center"" vertical=""center""/></xf>" &
               "<xf numFmtId=""3"" fontId=""0"" fillId=""4"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""right"" vertical=""center""/></xf>" &
               "<xf numFmtId=""3"" fontId=""0"" fillId=""6"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""right"" vertical=""center""/></xf>" &
               "<xf numFmtId=""0"" fontId=""3"" fillId=""4"" borderId=""1"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1""><alignment horizontal=""left"" vertical=""center"" wrapText=""1""/></xf>" &
               "</cellXfs>" &
               "<cellStyles count=""1""><cellStyle name=""Normal"" xfId=""0"" builtinId=""0""/></cellStyles>" &
               "<dxfs count=""0""/>" &
               "<tableStyles count=""0"" defaultTableStyle=""TableStyleMedium2"" defaultPivotStyle=""PivotStyleMedium9""/>" &
               "</styleSheet>"
    End Function

    ' Construye la hoja de resumen ejecutivo del reporte.
    Private Function ConstruirHojaResumenExcel() As String
        Dim sb As New StringBuilder()
        IniciarHojaExcel(sb, New String() {"Indicador", "Valor", "Indicador", "Valor", "Indicador", "Valor"})

        AgregarFilaExcel(sb, 1, New Object() {"KUMO | Reporte diario"}, EST_TITULO, 30D)
        AgregarFilaExcel(sb, 2, New Object() {"Fecha: " & dtpFecha.Value.ToString("dd/MM/yyyy") & " | Exportado: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm")}, EST_SUBTITULO, 22D)

        AbrirFilaExcel(sb, 4, 22D)
        AgregarCeldaExcel(sb, 4, 1, "Ingresos del dia", EST_ETIQUETA_RESUMEN)
        AgregarCeldaExcel(sb, 4, 3, "Ventas registradas", EST_ETIQUETA_RESUMEN)
        AgregarCeldaExcel(sb, 4, 5, "Ticket promedio", EST_ETIQUETA_RESUMEN)
        CerrarFilaExcel(sb)

        AbrirFilaExcel(sb, 5, 32D)
        AgregarCeldaExcel(sb, 5, 1, ObtenerDecimalDesdeTextoMoneda(lblIngresosVal.Text), EST_DINERO_RESUMEN)
        AgregarCeldaExcel(sb, 5, 3, ObtenerEnteroDesdeTexto(lblVentasVal.Text), EST_VALOR_RESUMEN)
        AgregarCeldaExcel(sb, 5, 5, ObtenerDecimalDesdeTextoMoneda(lblPromedioVal.Text), EST_DINERO_RESUMEN)
        CerrarFilaExcel(sb)

        AbrirFilaExcel(sb, 8, 22D)
        AgregarCeldaExcel(sb, 8, 1, "Articulos vendidos", EST_ETIQUETA_RESUMEN)
        AgregarCeldaExcel(sb, 8, 3, "Lectura rapida", EST_ETIQUETA_RESUMEN)
        CerrarFilaExcel(sb)

        AbrirFilaExcel(sb, 9, 34D)
        AgregarCeldaExcel(sb, 9, 1, ObtenerEnteroDesdeTexto(lblArticulosVal.Text), EST_VALOR_RESUMEN)
        AgregarCeldaExcel(sb, 9, 3, "El libro incluye ventas del dia, top productos, detalle por producto y metodos de pago.", EST_NOTA)
        CerrarFilaExcel(sb)

        AgregarFilaExcel(sb, 12, New Object() {"Hojas incluidas"}, EST_ENCABEZADO, 22D)
        AgregarFilaDirectorioExcel(sb, 13, "Ventas", "Tickets y montos del dia", EST_TEXTO)
        AgregarFilaDirectorioExcel(sb, 14, "Top productos", "Productos con mayor movimiento", EST_TEXTO_ALTERNO)
        AgregarFilaDirectorioExcel(sb, 15, "Detalle ventas", "Productos vendidos por ticket", EST_TEXTO)
        AgregarFilaDirectorioExcel(sb, 16, "Metodos pago", "Resumen por forma de cobro", EST_TEXTO_ALTERNO)

        FinalizarHojaExcel(sb, Nothing, New String() {"A1:F1", "A2:F2", "A4:B4", "C4:D4", "E4:F4", "A5:B6", "C5:D6", "E5:F6", "A8:B8", "C8:F8", "A9:B10", "C9:F10", "A12:F12", "B13:F13", "B14:F14", "B15:F15", "B16:F16"})
        Return sb.ToString()
    End Function

    ' Agrega una fila del directorio de hojas incluidas.
    Private Sub AgregarFilaDirectorioExcel(sb As StringBuilder, rowIndex As Integer, hoja As String, descripcion As String, estilo As Integer)
        AbrirFilaExcel(sb, rowIndex, 20D)
        AgregarCeldaExcel(sb, rowIndex, 1, hoja, estilo)
        AgregarCeldaExcel(sb, rowIndex, 2, descripcion, estilo)
        CerrarFilaExcel(sb)
    End Sub

    ' Convierte un DataGridView visible en una hoja XML de Excel.
    Private Function ConstruirHojaGridExcel(titulo As String, dgv As DataGridView) As String
        Dim columnas = ObtenerColumnasVisibles(dgv)
        Dim sb As New StringBuilder()
        Dim nombresColumnas As New List(Of String)()
        For Each columna As DataGridViewColumn In columnas
            nombresColumnas.Add(columna.HeaderText)
        Next

        IniciarHojaExcel(sb, nombresColumnas, 3)
        Dim ultimaColumna As String = ObtenerColumnaExcel(Math.Max(1, columnas.Count))

        AgregarFilaExcel(sb, 1, New Object() {titulo & " - " & dtpFecha.Value.ToString("dd/MM/yyyy")}, EST_TITULO, 28D)
        AgregarFilaExcel(sb, 2, New Object() {"Filtra o ordena desde los encabezados para revisar el corte mas rapido."}, EST_SUBTITULO, 20D)

        AbrirFilaExcel(sb, 3, 24D)
        For colIndex As Integer = 0 To columnas.Count - 1
            AgregarCeldaExcel(sb, 3, colIndex + 1, columnas(colIndex).HeaderText, EST_ENCABEZADO)
        Next
        CerrarFilaExcel(sb)

        Dim rowIndex As Integer = 4
        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For
            AbrirFilaExcel(sb, rowIndex, 20D)
            Dim filaAlterna As Boolean = ((rowIndex - 4) Mod 2 = 1)
            For colIndex As Integer = 0 To columnas.Count - 1
                Dim columna = columnas(colIndex)
                Dim estilo As Integer = ObtenerEstiloDatoExcel(columna.HeaderText, filaAlterna)
                AgregarCeldaExcel(sb, rowIndex, colIndex + 1, row.Cells(columna.Index).Value, estilo)
            Next
            CerrarFilaExcel(sb)
            rowIndex += 1
        Next

        If rowIndex = 4 Then
            AgregarFilaExcel(sb, rowIndex, New Object() {"Sin datos para exportar"}, EST_NOTA, 22D)
            rowIndex += 1
        End If

        Dim filtro As String = Nothing
        If columnas.Count > 0 Then filtro = "A3:" & ultimaColumna & Math.Max(3, rowIndex - 1).ToString(CultureInfo.InvariantCulture)
        FinalizarHojaExcel(sb, filtro, New String() {"A1:" & ultimaColumna & "1", "A2:" & ultimaColumna & "2"})
        Return sb.ToString()
    End Function

    ' Convierte un DataTable en una hoja XML de Excel.
    Private Function ConstruirHojaTablaExcel(titulo As String, tabla As DataTable) As String
        Dim sb As New StringBuilder()
        Dim columnas As Integer = If(tabla Is Nothing, 1, Math.Max(1, tabla.Columns.Count))
        Dim nombresColumnas As New List(Of String)()
        If tabla IsNot Nothing Then
            For Each columna As DataColumn In tabla.Columns
                nombresColumnas.Add(columna.ColumnName)
            Next
        End If

        IniciarHojaExcel(sb, nombresColumnas, 3)
        Dim ultimaColumna As String = ObtenerColumnaExcel(columnas)

        AgregarFilaExcel(sb, 1, New Object() {titulo & " - " & dtpFecha.Value.ToString("dd/MM/yyyy")}, EST_TITULO, 28D)
        AgregarFilaExcel(sb, 2, New Object() {"Datos listos para filtrar, ordenar y compartir."}, EST_SUBTITULO, 20D)

        If tabla Is Nothing OrElse tabla.Columns.Count = 0 Then
            AgregarFilaExcel(sb, 3, New Object() {"Sin datos para exportar"}, EST_NOTA, 22D)
            FinalizarHojaExcel(sb, Nothing, New String() {"A1:" & ultimaColumna & "1", "A2:" & ultimaColumna & "2"})
            Return sb.ToString()
        End If

        AbrirFilaExcel(sb, 3, 24D)
        For colIndex As Integer = 0 To tabla.Columns.Count - 1
            AgregarCeldaExcel(sb, 3, colIndex + 1, tabla.Columns(colIndex).ColumnName, EST_ENCABEZADO)
        Next
        CerrarFilaExcel(sb)

        Dim rowIndex As Integer = 4
        For Each row As DataRow In tabla.Rows
            AbrirFilaExcel(sb, rowIndex, 20D)
            Dim filaAlterna As Boolean = ((rowIndex - 4) Mod 2 = 1)
            For colIndex As Integer = 0 To tabla.Columns.Count - 1
                Dim columna = tabla.Columns(colIndex)
                Dim estilo As Integer = ObtenerEstiloDatoExcel(columna.ColumnName, filaAlterna)
                AgregarCeldaExcel(sb, rowIndex, colIndex + 1, row(columna), estilo)
            Next
            CerrarFilaExcel(sb)
            rowIndex += 1
        Next

        If rowIndex = 4 Then
            AgregarFilaExcel(sb, rowIndex, New Object() {"Sin datos para exportar"}, EST_NOTA, 22D)
            rowIndex += 1
        End If

        Dim filtro As String = "A3:" & ultimaColumna & Math.Max(3, rowIndex - 1).ToString(CultureInfo.InvariantCulture)
        FinalizarHojaExcel(sb, filtro, New String() {"A1:" & ultimaColumna & "1", "A2:" & ultimaColumna & "2"})
        Return sb.ToString()
    End Function

    ' Abre el XML de una hoja, define columnas y opcionalmente congela filas.
    Private Sub IniciarHojaExcel(sb As StringBuilder, columnas As Integer)
        Dim nombresColumnas As New List(Of String)()
        For i As Integer = 1 To columnas
            nombresColumnas.Add("")
        Next
        IniciarHojaExcel(sb, nombresColumnas)
    End Sub

    ' Abre el XML de una hoja, define columnas y opcionalmente congela filas.
    Private Sub IniciarHojaExcel(sb As StringBuilder, nombresColumnas As IList(Of String), Optional filaCongelada As Integer = 0)
        Dim totalColumnas As Integer = Math.Max(1, If(nombresColumnas Is Nothing, 0, nombresColumnas.Count))
        sb.AppendLine("<?xml version=""1.0"" encoding=""UTF-8""?>")
        sb.AppendLine("<worksheet xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">")
        If filaCongelada > 0 Then
            sb.Append("<sheetViews><sheetView workbookViewId=""0"" showGridLines=""0""><pane ySplit=""").Append(filaCongelada.ToString(CultureInfo.InvariantCulture)).Append(""" topLeftCell=""A").Append((filaCongelada + 1).ToString(CultureInfo.InvariantCulture)).Append(""" activePane=""bottomLeft"" state=""frozen""/><selection pane=""bottomLeft"" activeCell=""A").Append((filaCongelada + 1).ToString(CultureInfo.InvariantCulture)).Append(""" sqref=""A").Append((filaCongelada + 1).ToString(CultureInfo.InvariantCulture)).AppendLine("""/></sheetView></sheetViews>")
        Else
            sb.AppendLine("<sheetViews><sheetView workbookViewId=""0"" showGridLines=""0""/></sheetViews>")
        End If
        sb.AppendLine("<sheetFormatPr defaultRowHeight=""18""/>")
        sb.AppendLine("<cols>")
        For i As Integer = 1 To totalColumnas
            Dim nombreColumna As String = ""
            If nombresColumnas IsNot Nothing AndAlso i <= nombresColumnas.Count Then nombreColumna = nombresColumnas(i - 1)
            Dim ancho As Decimal = ObtenerAnchoColumnaExcel(nombreColumna)
            sb.Append("<col min=""").Append(i.ToString(CultureInfo.InvariantCulture)).Append(""" max=""").Append(i.ToString(CultureInfo.InvariantCulture)).Append(""" width=""").Append(ancho.ToString("0.##", CultureInfo.InvariantCulture)).AppendLine(""" customWidth=""1""/>")
        Next
        sb.AppendLine("</cols>")
        sb.AppendLine("<sheetData>")
    End Sub

    ' Cierra la hoja XML agregando filtros, combinaciones y configuracion de pagina.
    Private Sub FinalizarHojaExcel(sb As StringBuilder, Optional autoFiltro As String = Nothing, Optional celdasCombinadas As IEnumerable(Of String) = Nothing)
        sb.AppendLine("</sheetData>")
        If Not String.IsNullOrWhiteSpace(autoFiltro) Then
            sb.Append("<autoFilter ref=""").Append(EscapeXml(autoFiltro)).AppendLine("""/>")
        End If
        If celdasCombinadas IsNot Nothing Then
            Dim combinadas As New List(Of String)(celdasCombinadas)
            If combinadas.Count > 0 Then
                sb.Append("<mergeCells count=""").Append(combinadas.Count.ToString(CultureInfo.InvariantCulture)).AppendLine(""">")
                For Each rango As String In combinadas
                    sb.Append("<mergeCell ref=""").Append(EscapeXml(rango)).AppendLine("""/>")
                Next
                sb.AppendLine("</mergeCells>")
            End If
        End If
        sb.AppendLine("<pageMargins left=""0.25"" right=""0.25"" top=""0.5"" bottom=""0.5"" header=""0.3"" footer=""0.3""/>")
        sb.AppendLine("<pageSetup orientation=""landscape""/>")
        sb.AppendLine("</worksheet>")
    End Sub

    ' Agrega una fila completa con valores y estilo inicial.
    Private Sub AgregarFilaExcel(sb As StringBuilder, rowIndex As Integer, valores As Object(), Optional estiloPrimeraCelda As Integer = 0, Optional altura As Decimal = 0D)
        AbrirFilaExcel(sb, rowIndex, altura)
        For i As Integer = 0 To valores.Length - 1
            AgregarCeldaExcel(sb, rowIndex, i + 1, valores(i), If(i = 0, estiloPrimeraCelda, 0))
        Next
        CerrarFilaExcel(sb)
    End Sub

    ' Escribe la etiqueta inicial de una fila XML.
    Private Sub AbrirFilaExcel(sb As StringBuilder, rowIndex As Integer, Optional altura As Decimal = 0D)
        sb.Append("<row r=""").Append(rowIndex.ToString(CultureInfo.InvariantCulture)).Append("""")
        If altura > 0D Then
            sb.Append(" ht=""").Append(altura.ToString("0.##", CultureInfo.InvariantCulture)).Append(""" customHeight=""1""")
        End If
        sb.AppendLine(">")
    End Sub

    ' Escribe la etiqueta final de una fila XML.
    Private Sub CerrarFilaExcel(sb As StringBuilder)
        sb.AppendLine("</row>")
    End Sub

    ' Escribe una celda XML como numero, texto o vacia segun el valor recibido.
    Private Sub AgregarCeldaExcel(sb As StringBuilder, rowIndex As Integer, colIndex As Integer, valor As Object, Optional estilo As Integer = 0)
        Dim referencia As String = ObtenerColumnaExcel(colIndex) & rowIndex.ToString(CultureInfo.InvariantCulture)
        Dim estiloTexto As String = If(estilo > 0, " s=""" & estilo.ToString(CultureInfo.InvariantCulture) & """", "")

        If valor Is Nothing OrElse valor Is DBNull.Value Then
            sb.Append("<c r=""").Append(referencia).Append("""").Append(estiloTexto).AppendLine("/>")
            Return
        End If

        If TypeOf valor Is Byte OrElse TypeOf valor Is Short OrElse TypeOf valor Is Integer OrElse
           TypeOf valor Is Long OrElse TypeOf valor Is Single OrElse TypeOf valor Is Double OrElse TypeOf valor Is Decimal Then
            Dim numero As String = Convert.ToDecimal(valor).ToString(CultureInfo.InvariantCulture)
            sb.Append("<c r=""").Append(referencia).Append("""").Append(estiloTexto).Append("><v>").Append(numero).AppendLine("</v></c>")
            Return
        End If

        Dim texto As String = valor.ToString()
        sb.Append("<c r=""").Append(referencia).Append("""").Append(estiloTexto).Append(" t=""inlineStr""><is><t>")
        sb.Append(EscapeXml(texto))
        sb.AppendLine("</t></is></c>")
    End Sub

    ' Obtiene las columnas visibles del grid ordenadas por DisplayIndex.
    Private Function ObtenerColumnasVisibles(dgv As DataGridView) As List(Of DataGridViewColumn)
        Dim columnas As New List(Of DataGridViewColumn)()
        For Each col As DataGridViewColumn In dgv.Columns
            If col.Visible Then columnas.Add(col)
        Next
        columnas.Sort(Function(a, b) a.DisplayIndex.CompareTo(b.DisplayIndex))
        Return columnas
    End Function

    ' Convierte un indice numerico en nombre de columna estilo Excel.
    Private Function ObtenerColumnaExcel(indice As Integer) As String
        Dim nombre As String = ""
        Dim n As Integer = indice
        While n > 0
            n -= 1
            nombre = ChrW(65 + (n Mod 26)) & nombre
            n \= 26
        End While
        Return nombre
    End Function

    ' Calcula el ancho de columna segun el encabezado.
    Private Function ObtenerAnchoColumnaExcel(nombreColumna As String) As Decimal
        Dim nombre = If(nombreColumna, "").ToLowerInvariant()

        If nombre.Contains("producto") Then Return 34D
        If nombre.Contains("metodo") Then Return 18D
        If nombre.Contains("hora") Then Return 13D
        If nombre.Contains("venta") Then Return 12D
        If EsColumnaDinero(nombre) Then Return 15D
        If nombre.Contains("cantidad") OrElse nombre.Contains("unidades") Then Return 13D
        If String.IsNullOrWhiteSpace(nombre) Then Return 18D

        Return Math.Min(28D, Math.Max(14D, CDec(nombreColumna.Length + 4)))
    End Function

    ' Elige el estilo de celda segun tipo de dato y fila alterna.
    Private Function ObtenerEstiloDatoExcel(nombreColumna As String, filaAlterna As Boolean) As Integer
        If EsColumnaDinero(nombreColumna) Then Return If(filaAlterna, EST_DINERO_ALTERNO, EST_DINERO)
        If EsColumnaNumerica(nombreColumna) Then Return If(filaAlterna, EST_NUMERO_ALTERNO, EST_NUMERO)
        Return If(filaAlterna, EST_TEXTO_ALTERNO, EST_TEXTO)
    End Function

    ' Detecta columnas que deben usar formato de moneda.
    Private Function EsColumnaDinero(nombreColumna As String) As Boolean
        Dim nombre = nombreColumna.ToLowerInvariant()
        Return nombre.Contains("total") OrElse nombre.Contains("subtotal") OrElse nombre.Contains("descuento") OrElse nombre.Contains("iva") OrElse nombre.Contains("pago") OrElse nombre.Contains("cambio") OrElse nombre.Contains("importe") OrElse nombre.Contains("precio")
    End Function

    ' Detecta columnas que deben usar formato numerico entero.
    Private Function EsColumnaNumerica(nombreColumna As String) As Boolean
        Dim nombre = nombreColumna.ToLowerInvariant()
        Return nombre.Contains("cantidad") OrElse nombre.Contains("unidades") OrElse nombre.Contains("venta") OrElse nombre.Contains("articulo")
    End Function

    ' Convierte texto con signo de moneda a Decimal.
    Private Function ObtenerDecimalDesdeTextoMoneda(texto As String) As Decimal
        Dim limpio As String = If(texto, "").Replace("$", "").Trim()
        Dim valor As Decimal
        If Decimal.TryParse(limpio, NumberStyles.Any, CultureInfo.CurrentCulture, valor) Then Return valor
        If Decimal.TryParse(limpio, NumberStyles.Any, CultureInfo.InvariantCulture, valor) Then Return valor
        Return 0D
    End Function

    ' Convierte texto a entero usando cultura actual o invariante.
    Private Function ObtenerEnteroDesdeTexto(texto As String) As Integer
        Dim valor As Integer
        If Integer.TryParse(If(texto, "").Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, valor) Then Return valor
        If Integer.TryParse(If(texto, "").Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, valor) Then Return valor
        Return 0
    End Function

    ' Escapa texto para insertarlo de forma segura en XML.
    Private Function EscapeXml(texto As String) As String
        Dim escapado = System.Security.SecurityElement.Escape(texto)
        If escapado Is Nothing Then Return ""
        Return escapado
    End Function

    ' Cierra el formulario actual.
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Close()
    End Sub

    ' Recarga el reporte cuando se registran cambios de ventas.
    Private Sub RefrescarReporte()
        If Me.IsDisposed Then Return
        BeginInvoke(New MethodInvoker(AddressOf IniciarCargaReporte))
    End Sub

    ' Quita la suscripcion al evento de ventas al cerrar.
    Private Sub Form7_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarReporte
    End Sub

    ' Reacomoda el reporte al cambiar el tamano.
    Private Sub Form7_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutReporte()
    End Sub
End Class

Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class Form2

    Public ReadOnly Property InicioCorrecto As Boolean
        Get
            Return _inicioCorrecto
        End Get
    End Property

    Private pnlTopBar As Panel
    Private picTopLogo As PictureBox
    Private lblTopTitle As Label
    Private lblTopSub As Label
    Private lblTopState As Label
    Private pnlBadgeProductos As Panel
    Private pnlBadgeCarrito As Panel
    Private lblBadgeProductosTitle As Label
    Private lblBadgeProductosValue As Label
    Private lblBadgeCarritoTitle As Label
    Private lblBadgeCarritoValue As Label
    Private pnlCatalogoHero As Panel
    Private lblCatalogoHeroTitle As Label
    Private lblCatalogoHeroSub As Label
    Private lblCatalogoHeroHint As Label
    Private pnlCarritoResumen As Panel
    Private lblCarritoResumenTitle As Label
    Private lblCarritoResumenSub As Label
    Private lblCarritoResumenItems As Label
    Private pnlTotales As Panel
    Private _inicioCorrecto As Boolean
    Private _cargandoDatosPOS As Boolean

    Public Sub New()
        InitializeComponent()
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                If ModEstilo.EstaEnModoDisenio(Me) Then
                    ModEstilo.PrepararVentana(Me)
                End If
                AplicarDisenoPOS()
            End Sub)
    End Sub

    Private dtCarrito As New DataTable
    Private dtProductos As New DataTable

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

    <DllImport("gdi32.dll")>
    Private Shared Function DeleteObject(hObject As IntPtr) As Boolean
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ModEstilo.PrepararVentana(Me)
            AddHandler ModActualizaciones.InventarioActualizado, AddressOf RefrescarInventario
            AddHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
            InicializarComponentesPOS()

            InicializarCarrito()
            PrepararVistaInicialPOS()
            sbFecha.Text = ModEstilo.FormatoFechaHora24(Now)

            AplicarDisenoPOS()
            RecalcularTotales()
            _inicioCorrecto = True
            BeginInvoke(New MethodInvoker(AddressOf IniciarCargaInicialPOS))
        Catch ex As Exception
            _inicioCorrecto = False
            CargarModoSeguroPOS(ex)
        End Try
    End Sub

    Private Sub AplicarDisenoPOS()
        InicializarComponentesPOS()
        ModEstilo.EstilarControles(Me)
        ModEstilo.CargarLogo(picMarca)
        ModEstilo.CargarLogo(picTopLogo)
        ModEstilo.EstilarBotonPrimario(btnAgregar)
        ModEstilo.EstilarBotonCobrar(btnCobrar)
        ModEstilo.EstilarBotonSecundario(btnLimpiar)
        ModEstilo.EstilarBotonSecundario(btnQuitar)
        ModEstilo.EstilarBotonSecundario(btnSalida)
        ModEstilo.EstilarMenuStrip(MenuStrip1)
        ModEstilo.EstilarStatusStrip(StatusStrip1)
        AplicarEstiloPOS()
        ConfigurarPantallaPOS()
    End Sub

    Private Sub PrepararVistaInicialPOS()
        If dgvCarrito.DataSource Is Nothing Then
            dgvCarrito.DataSource = dtCarrito
        End If

        If dgvProductos.DataSource Is Nothing Then
            dtProductos = CrearTablaProductosVacia()
            dgvProductos.DataSource = dtProductos
        End If

        ConfigurarColumnasCarrito()
        ConfigurarColumnasProductos()

        cbCategoria.Items.Clear()
        cbCategoria.Items.Add("(Todas)")
        cbCategoria.SelectedIndex = 0
        lblNumVenta.Text = "Ticket #V-..."
        sbInfo.Text = "   Mostrando caja. Cargando catalogo..."
    End Sub

    Private Async Sub IniciarCargaInicialPOS()
        Await CargarDatosInicialesPOSAsync()
    End Sub

    Private Async Function CargarDatosInicialesPOSAsync() As Task
        If _cargandoDatosPOS OrElse Me.IsDisposed Then Return

        _cargandoDatosPOS = True
        btnAgregar.Enabled = False
        btnCobrar.Enabled = False
        btnQuitar.Enabled = False

        Try
            sbInfo.Text = "   Cargando catalogo y folio..."

            Dim sqlCategorias = "SELECT NombreCat FROM " & TablaCategorias() & " ORDER BY NombreCat"
            Dim sqlProductos = ObtenerSqlProductos()
            Dim sqlNumVenta = "SELECT ISNULL(MAX(Id_Pedido),0)+1 FROM PEDIDOS"

            Dim categoriasTask = Task.Run(Function() ObtenerTabla(sqlCategorias))
            Dim productosTask = Task.Run(Function() ObtenerTabla(sqlProductos))
            Dim numVentaTask = Task.Run(Function() ObtenerEscalar(sqlNumVenta))

            Await Task.WhenAll(categoriasTask, productosTask, numVentaTask)

            If Me.IsDisposed Then Return

            AplicarCategorias(categoriasTask.Result)
            AplicarProductos(productosTask.Result)
            AplicarNumeroVenta(numVentaTask.Result)
            RecalcularTotales()
            sbInfo.Text = "   Catalogo listo para vender."
        Catch ex As Exception
            If Me.IsDisposed Then Return
            lblNumVenta.Text = "Ticket #V-001"
            sbInfo.Text = "   La caja abrio, pero no se pudo cargar el catalogo."
            MsgBox("Error al cargar catalogo inicial: " & ex.Message)
        Finally
            If Not Me.IsDisposed Then
                btnAgregar.Enabled = True
                btnCobrar.Enabled = True
                btnQuitar.Enabled = True
            End If
            _cargandoDatosPOS = False
        End Try
    End Function

    Private Sub InicializarComponentesPOS()
        If pnlTopBar IsNot Nothing Then Return

        pnlTopBar = New Panel() With {.Name = "pnlTopBar"}
        picTopLogo = New PictureBox() With {.Name = "picTopLogo", .SizeMode = PictureBoxSizeMode.Zoom, .BackColor = Color.Transparent}
        lblTopTitle = New Label() With {.Name = "lblTopTitle", .AutoSize = False, .Text = "Punto de venta"}
        lblTopSub = New Label() With {.Name = "lblTopSub", .AutoSize = False, .Text = "Operacion en mostrador, inventario visible y cobro mas ordenado."}
        lblTopState = New Label() With {.Name = "lblTopState", .AutoSize = False, .Text = "Caja lista para operar"}

        pnlBadgeProductos = New Panel() With {.Name = "pnlBadgeProductos"}
        lblBadgeProductosTitle = New Label() With {.Name = "lblBadgeProductosTitle", .AutoSize = False, .Text = "Categorias activas"}
        lblBadgeProductosValue = New Label() With {.Name = "lblBadgeProductosValue", .AutoSize = False, .Text = "000"}
        pnlBadgeProductos.Controls.Add(lblBadgeProductosTitle)
        pnlBadgeProductos.Controls.Add(lblBadgeProductosValue)

        pnlBadgeCarrito = New Panel() With {.Name = "pnlBadgeCarrito"}
        lblBadgeCarritoTitle = New Label() With {.Name = "lblBadgeCarritoTitle", .AutoSize = False, .Text = "Items en ticket"}
        lblBadgeCarritoValue = New Label() With {.Name = "lblBadgeCarritoValue", .AutoSize = False, .Text = "00"}
        pnlBadgeCarrito.Controls.Add(lblBadgeCarritoTitle)
        pnlBadgeCarrito.Controls.Add(lblBadgeCarritoValue)

        pnlTopBar.Controls.Add(picTopLogo)
        pnlTopBar.Controls.Add(lblTopTitle)
        pnlTopBar.Controls.Add(lblTopSub)
        pnlTopBar.Controls.Add(lblTopState)
        pnlTopBar.Controls.Add(pnlBadgeProductos)
        pnlTopBar.Controls.Add(pnlBadgeCarrito)

        pnlCatalogoHero = New Panel() With {.Name = "pnlCatalogoHero"}
        lblCatalogoHeroTitle = New Label() With {.Name = "lblCatalogoHeroTitle", .AutoSize = False, .Text = "Catalogo de venta"}
        lblCatalogoHeroSub = New Label() With {.Name = "lblCatalogoHeroSub", .AutoSize = False, .Text = "Busca productos, filtra por categoria y agrega articulos al ticket actual."}
        lblCatalogoHeroHint = New Label() With {.Name = "lblCatalogoHeroHint", .AutoSize = False, .Text = "Stock en tiempo real."}
        pnlCatalogoHero.Controls.Add(lblCatalogoHeroTitle)
        pnlCatalogoHero.Controls.Add(lblCatalogoHeroSub)
        pnlCatalogoHero.Controls.Add(lblCatalogoHeroHint)
        pnlCatalogoHero.Controls.Add(picMarca)
        gbProductos.Controls.Add(pnlCatalogoHero)

        pnlCarritoResumen = New Panel() With {.Name = "pnlCarritoResumen"}
        lblCarritoResumenTitle = New Label() With {.Name = "lblCarritoResumenTitle", .AutoSize = False, .Text = "Venta activa"}
        lblCarritoResumenSub = New Label() With {.Name = "lblCarritoResumenSub", .AutoSize = False, .Text = "MOSTRADOR"}
        lblCarritoResumenItems = New Label() With {.Name = "lblCarritoResumenItems", .AutoSize = False, .Text = "0 items"}
        pnlCarritoResumen.Controls.Add(lblCarritoResumenTitle)
        pnlCarritoResumen.Controls.Add(lblCarritoResumenSub)
        pnlCarritoResumen.Controls.Add(lblCarritoResumenItems)
        pnlCarritoResumen.Controls.Add(lblNumVenta)
        gbCarrito.Controls.Add(pnlCarritoResumen)

        pnlTotales = New Panel() With {.Name = "pnlTotales"}
        gbCarrito.Controls.Add(pnlTotales)

        lblSubtotalTxt.Parent = pnlTotales
        lblSubtotal.Parent = pnlTotales
        lblDescPctTxt.Parent = pnlTotales
        txtDescPct.Parent = pnlTotales
        lblDescValTxt.Parent = pnlTotales
        lblDescuento.Parent = pnlTotales
        lblLinea.Parent = pnlTotales
        lblTotalTxt.Parent = pnlTotales
        lblTotal.Parent = pnlTotales
        btnCobrar.Parent = pnlTotales
        btnLimpiar.Parent = pnlTotales

        pnlTopBar.Controls.Add(btnSalida)
        Me.Controls.Add(pnlTopBar)
        pnlTopBar.BringToFront()
    End Sub

    Private Sub AplicarEstiloPOS()
        If pnlTopBar Is Nothing Then Return

        Me.BackColor = Color.FromArgb(244, 240, 234)
        Me.Text = "KUMO | Caja premium"

        pnlTopBar.BackColor = Color.FromArgb(34, 39, 46)
        lblTopTitle.ForeColor = Color.FromArgb(255, 248, 239)
        lblTopTitle.Font = New Font("Segoe UI", 20.0F, FontStyle.Bold)
        lblTopTitle.Text = "KUMO POS"
        lblTopSub.ForeColor = Color.FromArgb(206, 198, 186)
        lblTopSub.Font = New Font("Segoe UI", 10.0F)
        lblTopSub.Text = ""
        lblTopState.ForeColor = Color.FromArgb(132, 203, 163)
        lblTopState.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblTopState.Text = ""

        ConfigurarBadge(pnlBadgeProductos, lblBadgeProductosTitle, lblBadgeProductosValue, Color.FromArgb(250, 245, 237), Color.FromArgb(142, 119, 82), Color.FromArgb(63, 53, 40))
        ConfigurarBadge(pnlBadgeCarrito, lblBadgeCarritoTitle, lblBadgeCarritoValue, Color.FromArgb(233, 241, 235), Color.FromArgb(84, 120, 97), Color.FromArgb(33, 73, 50))

        MenuStrip1.Dock = DockStyle.None
        MenuStrip1.BackColor = Color.FromArgb(250, 247, 242)
        MenuStrip1.Padding = New Padding(14, 8, 14, 8)
        MenuStrip1.RenderMode = ToolStripRenderMode.System

        For Each item As ToolStripMenuItem In New ToolStripMenuItem() {mnuInventario, mnuHistorial, mnuPedidos, mnuReporte, mnuCancelarVenta}
            item.ForeColor = Color.FromArgb(62, 58, 52)
            item.BackColor = Color.FromArgb(250, 247, 242)
            item.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            item.Padding = New Padding(16, 0, 16, 0)
        Next

        mnuCancelarVenta.ForeColor = Color.FromArgb(155, 68, 58)

        gbProductos.Text = ""
        gbProductos.BackColor = Color.FromArgb(255, 252, 247)
        gbProductos.Padding = New Padding(10)

        gbCarrito.Text = ""
        gbCarrito.BackColor = Color.FromArgb(247, 243, 237)
        gbCarrito.Padding = New Padding(10)

        pnlCatalogoHero.BackColor = Color.FromArgb(247, 241, 232)
        lblCatalogoHeroTitle.ForeColor = Color.FromArgb(73, 60, 47)
        lblCatalogoHeroTitle.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        lblCatalogoHeroTitle.Text = "Catalogo"
        lblCatalogoHeroSub.ForeColor = Color.FromArgb(123, 108, 90)
        lblCatalogoHeroSub.Font = New Font("Segoe UI", 9.5F)
        lblCatalogoHeroSub.Text = ""
        lblCatalogoHeroHint.ForeColor = Color.FromArgb(78, 122, 95)
        lblCatalogoHeroHint.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)

        lblCatalogoHeroHint.Text = "Filtro automatico por nombre y categoria"

        pnlCarritoResumen.BackColor = Color.FromArgb(58, 68, 80)
        lblCarritoResumenTitle.ForeColor = Color.FromArgb(255, 248, 239)
        lblCarritoResumenTitle.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        lblCarritoResumenTitle.Text = "Venta"
        lblCarritoResumenSub.ForeColor = Color.FromArgb(213, 196, 166)
        lblCarritoResumenSub.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        lblCarritoResumenSub.Text = ""
        lblCarritoResumenItems.ForeColor = Color.FromArgb(244, 212, 141)
        lblCarritoResumenItems.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)

        pnlTotales.BackColor = Color.FromArgb(255, 252, 247)

        lblBuscar.Text = "Busqueda"
        lblCategoria.Text = "Categoria"
        lblCantidadTxt.Text = "Piezas"
        lblSubtotalTxt.Text = "Subtotal"
        lblDescPctTxt.Text = "Descuento %"
        lblDescValTxt.Text = "Descuento aplicado"
        lblTotalTxt.Text = "TOTAL DEL TICKET"

        btnAgregar.Text = "Agregar"
        btnQuitar.Text = "Quitar"
        btnCobrar.Text = "Cobrar"
        btnLimpiar.Text = "Cancelar"
        btnSalida.Text = "Cerrar"

        EstilarBotonRetail(btnAgregar, Color.FromArgb(49, 55, 63), Color.White, Color.FromArgb(49, 55, 63), Color.FromArgb(70, 78, 88), 10.5F)
        EstilarBotonRetail(btnQuitar, Color.FromArgb(255, 249, 246), Color.FromArgb(141, 72, 63), Color.FromArgb(225, 186, 180), Color.FromArgb(252, 239, 235), 9.5F)
        EstilarBotonRetail(btnCobrar, Color.FromArgb(74, 133, 95), Color.White, Color.FromArgb(74, 133, 95), Color.FromArgb(58, 111, 78), 12.0F)
        EstilarBotonRetail(btnLimpiar, Color.FromArgb(249, 243, 234), Color.FromArgb(98, 84, 69), Color.FromArgb(216, 198, 172), Color.FromArgb(243, 235, 224), 9.5F)
        EstilarBotonRetail(btnSalida, Color.FromArgb(46, 52, 60), Color.FromArgb(244, 226, 193), Color.FromArgb(96, 87, 72), Color.FromArgb(57, 64, 73), 9.5F)
        btnBuscar.Visible = False
        btnBuscar.Enabled = False
        btnBuscar.TabStop = False

        txtCantidad.TextAlign = HorizontalAlignment.Center
        txtDescPct.TextAlign = HorizontalAlignment.Center
        EstilarCampoRetail(txtBuscar, False)
        EstilarCampoRetail(txtCantidad, True)
        EstilarCampoRetail(txtDescPct, True)
        EstilarComboRetail(cbCategoria)
        EstilarGridRetail(dgvProductos, False)
        EstilarGridRetail(dgvCarrito, True)

        lblBuscar.ForeColor = Color.FromArgb(133, 116, 91)
        lblCategoria.ForeColor = Color.FromArgb(133, 116, 91)
        lblCantidadTxt.ForeColor = Color.FromArgb(120, 104, 85)
        lblSubtotalTxt.ForeColor = Color.FromArgb(120, 104, 85)
        lblDescPctTxt.ForeColor = Color.FromArgb(120, 104, 85)
        lblDescValTxt.ForeColor = Color.FromArgb(120, 104, 85)
        lblTotalTxt.ForeColor = Color.FromArgb(83, 69, 53)

        lblTotal.Font = New Font("Segoe UI", 24.0F, FontStyle.Bold)
        lblTotal.ForeColor = Color.FromArgb(46, 52, 60)
        lblSubtotal.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        lblSubtotal.ForeColor = Color.FromArgb(72, 63, 52)
        lblDescuento.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        lblDescuento.ForeColor = Color.FromArgb(146, 79, 67)
        lblNumVenta.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        lblNumVenta.ForeColor = Color.FromArgb(255, 248, 239)
        lblLinea.BackColor = Color.FromArgb(217, 199, 171)

        AplicarCurvasPOS()
    End Sub

    Private Sub ConfigurarBadge(panel As Panel, titulo As Label, valor As Label, colorFondo As Color, colorTitulo As Color, colorValor As Color)
        panel.BackColor = colorFondo
        titulo.ForeColor = colorTitulo
        titulo.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        titulo.TextAlign = ContentAlignment.MiddleLeft
        valor.ForeColor = colorValor
        valor.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        valor.TextAlign = ContentAlignment.MiddleLeft
    End Sub

    Private Sub EstilarBotonRetail(btn As Button, colorFondo As Color, colorTexto As Color, colorBorde As Color, colorHover As Color, fontSize As Single)
        btn.BackColor = colorFondo
        btn.ForeColor = colorTexto
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.BorderColor = colorBorde
        btn.FlatAppearance.MouseOverBackColor = colorHover
        btn.FlatAppearance.MouseDownBackColor = colorHover
        btn.Font = New Font("Segoe UI", fontSize, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        btn.UseVisualStyleBackColor = False
    End Sub

    Private Sub EstilarCampoRetail(tb As TextBox, centrado As Boolean)
        tb.BackColor = Color.FromArgb(255, 252, 247)
        tb.ForeColor = Color.FromArgb(71, 63, 54)
        tb.BorderStyle = BorderStyle.FixedSingle
        tb.Font = New Font("Segoe UI", 10.0F)
        tb.TextAlign = If(centrado, HorizontalAlignment.Center, HorizontalAlignment.Left)
    End Sub

    Private Sub EstilarComboRetail(cb As ComboBox)
        cb.BackColor = Color.FromArgb(255, 252, 247)
        cb.ForeColor = Color.FromArgb(71, 63, 54)
        cb.FlatStyle = FlatStyle.Flat
        cb.Font = New Font("Segoe UI", 9.5F)
    End Sub

    Private Sub EstilarGridRetail(dgv As DataGridView, esCarrito As Boolean)
        dgv.BackgroundColor = Color.FromArgb(255, 252, 247)
        dgv.BorderStyle = BorderStyle.None
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = If(esCarrito, Color.FromArgb(225, 214, 198), Color.FromArgb(231, 221, 205))
        dgv.EnableHeadersVisualStyles = False
        dgv.RowHeadersVisible = False
        dgv.AllowUserToResizeRows = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersHeight = 34
        dgv.ColumnHeadersDefaultCellStyle.BackColor = If(esCarrito, Color.FromArgb(70, 78, 88), Color.FromArgb(60, 54, 48))
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
        dgv.DefaultCellStyle.BackColor = Color.FromArgb(255, 252, 247)
        dgv.DefaultCellStyle.ForeColor = Color.FromArgb(74, 65, 56)
        dgv.DefaultCellStyle.SelectionBackColor = If(esCarrito, Color.FromArgb(230, 238, 232), Color.FromArgb(245, 236, 223))
        dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(56, 51, 45)
        dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)
        dgv.AlternatingRowsDefaultCellStyle.BackColor = If(esCarrito, Color.FromArgb(249, 246, 242), Color.FromArgb(252, 248, 242))
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(74, 65, 56)
        dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.SelectionBackColor
        dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.SelectionForeColor
        dgv.RowTemplate.Height = 32
    End Sub

    Private Sub AplicarCurvasPOS()
        RedondearControl(pnlTopBar, 26)
        RedondearControl(gbProductos, 24)
        RedondearControl(gbCarrito, 24)
        RedondearControl(pnlCatalogoHero, 22)
        RedondearControl(pnlCarritoResumen, 22)
        RedondearControl(pnlTotales, 22)
        RedondearControl(pnlBadgeProductos, 20)
        RedondearControl(pnlBadgeCarrito, 20)
        RedondearControl(btnAgregar, 18)
        RedondearControl(btnQuitar, 18)
        RedondearControl(btnCobrar, 18)
        RedondearControl(btnLimpiar, 18)
        RedondearControl(btnSalida, 18)
    End Sub

    Private Sub RedondearControl(ctrl As Control, radius As Integer)
        If ctrl Is Nothing OrElse ctrl.Width <= 0 OrElse ctrl.Height <= 0 Then Return

        Try
            Dim hrgn = CreateRoundRectRgn(0, 0, ctrl.Width + 1, ctrl.Height + 1, radius, radius)
            Try
                ctrl.Region = Region.FromHrgn(hrgn)
            Finally
                DeleteObject(hrgn)
            End Try
        Catch
            ctrl.Region = Nothing
        End Try
    End Sub

    Private Sub CargarModoSeguroPOS(ex As Exception)
        Try
            If pnlTopBar Is Nothing Then
                InicializarComponentesPOS()
            End If

            ModEstilo.EstilarControles(Me)
            ModEstilo.EstilarMenuStrip(MenuStrip1)
            ModEstilo.EstilarStatusStrip(StatusStrip1)
            ConfigurarPantallaPOS()
            RecalcularTotales()

            If sbInfo IsNot Nothing Then
                sbInfo.Text = "  POS cargado en modo seguro."
            End If
        Catch
            ' Si incluso el modo seguro falla, conservamos el mensaje original.
        End Try

        MsgBox("Se detecto un error al cargar el estilo premium del POS." & vbCrLf &
               "La caja quedo en modo seguro para que no se cierre la app." & vbCrLf & vbCrLf &
               "Detalle: " & ex.Message,
               MsgBoxStyle.Exclamation,
               "Carga del POS")
    End Sub

    Private Function TablaCategorias() As String
        Return "[CATEGOR" & ChrW(205) & "A]"
    End Function

    Private Sub ConfigurarPantallaPOS()
        If pnlTopBar Is Nothing Then Return

        Dim margen As Integer = 18
        Dim separacion As Integer = 16
        Dim headerH As Integer = 108
        Dim altoBuscador As Integer = 40
        Dim altoBoton As Integer = 46
        Dim altoMenu As Integer = 46
        Dim panelDerecho As Integer = Math.Max(442, Math.Min(540, CInt(Me.ClientSize.Width * 0.31)))

        pnlTopBar.SetBounds(margen, 12, Me.ClientSize.Width - (margen * 2), headerH)
        picTopLogo.SetBounds(22, 18, 92, 72)
        lblTopTitle.SetBounds(126, 18, 320, 34)
        lblTopSub.SetBounds(126, 50, 470, 0)
        lblTopState.SetBounds(126, 66, 340, 22)

        btnSalida.SetBounds(pnlTopBar.Width - 144, 31, 120, 46)
        pnlBadgeCarrito.SetBounds(btnSalida.Left - 174, 18, 160, 74)
        pnlBadgeProductos.SetBounds(pnlBadgeCarrito.Left - 172, 18, 160, 74)
        lblBadgeProductosTitle.SetBounds(14, 12, pnlBadgeProductos.Width - 28, 18)
        lblBadgeProductosValue.SetBounds(14, 31, pnlBadgeProductos.Width - 28, 28)
        lblBadgeCarritoTitle.SetBounds(14, 12, pnlBadgeCarrito.Width - 28, 18)
        lblBadgeCarritoValue.SetBounds(14, 31, pnlBadgeCarrito.Width - 28, 28)

        MenuStrip1.SetBounds(margen, pnlTopBar.Bottom + 8, Me.ClientSize.Width - (margen * 2), altoMenu)

        Dim topArea As Integer = MenuStrip1.Bottom + 12
        Dim altoDisponible As Integer = Me.ClientSize.Height - topArea - StatusStrip1.Height - margen
        Dim anchoProductos As Integer = Me.ClientSize.Width - panelDerecho - (margen * 3)

        gbProductos.SetBounds(margen, topArea, anchoProductos, altoDisponible)
        gbCarrito.SetBounds(gbProductos.Right + separacion, topArea, panelDerecho, altoDisponible)

        pnlCatalogoHero.SetBounds(18, 18, gbProductos.Width - 36, 104)
        lblCatalogoHeroTitle.SetBounds(20, 16, 260, 28)
        lblCatalogoHeroSub.SetBounds(20, 44, pnlCatalogoHero.Width - 250, 0)
        lblCatalogoHeroHint.SetBounds(20, 56, pnlCatalogoHero.Width - 250, 20)
        picMarca.SetBounds(pnlCatalogoHero.Width - 198, 18, 176, 66)

        Dim filtrosTop As Integer = pnlCatalogoHero.Bottom + 18
        lblBuscar.Location = New Point(18, filtrosTop)
        Dim anchoCategoria As Integer = Math.Max(190, Math.Min(240, CInt(gbProductos.Width * 0.23)))
        Dim espacioEntreFiltros As Integer = 18
        Dim xCategoria As Integer = gbProductos.Width - 18 - anchoCategoria
        txtBuscar.SetBounds(18, lblBuscar.Bottom + 8, Math.Max(320, xCategoria - 18 - espacioEntreFiltros), altoBuscador)
        lblCategoria.Location = New Point(xCategoria, filtrosTop)
        cbCategoria.SetBounds(xCategoria, lblCategoria.Bottom + 7, anchoCategoria, altoBuscador)
        btnBuscar.SetBounds(-200, -200, 1, 1)

        dgvProductos.SetBounds(18, txtBuscar.Bottom + 22, gbProductos.Width - 36, gbProductos.Height - (txtBuscar.Bottom + 22) - 72)
        btnAgregar.SetBounds(gbProductos.Width - 238, gbProductos.Height - 60, 220, 46)

        pnlCarritoResumen.SetBounds(18, 18, gbCarrito.Width - 36, 90)
        lblCarritoResumenTitle.SetBounds(18, 14, 120, 22)
        lblNumVenta.SetBounds(18, 38, 180, 28)
        lblCarritoResumenSub.SetBounds(pnlCarritoResumen.Width - 126, 16, 108, 0)
        lblCarritoResumenItems.SetBounds(pnlCarritoResumen.Width - 126, 34, 108, 26)
        lblCarritoResumenSub.TextAlign = ContentAlignment.MiddleRight
        lblCarritoResumenItems.TextAlign = ContentAlignment.MiddleRight

        pnlTotales.SetBounds(18, gbCarrito.Height - 318, gbCarrito.Width - 36, 300)

        Dim topCarrito As Integer = pnlCarritoResumen.Bottom + 16
        Dim yCantidad As Integer = pnlTotales.Top - 54
        dgvCarrito.SetBounds(18, topCarrito, gbCarrito.Width - 36, Math.Max(150, yCantidad - topCarrito - 16))
        lblCantidadTxt.Location = New Point(18, yCantidad + 8)
        txtCantidad.SetBounds(106, yCantidad + 2, 76, altoBuscador)
        btnQuitar.SetBounds(txtCantidad.Right + 12, yCantidad, 118, altoBoton)

        lblSubtotalTxt.Location = New Point(20, 22)
        lblSubtotal.Location = New Point(pnlTotales.Width - 126, 22)
        lblDescPctTxt.Location = New Point(20, 58)
        txtDescPct.SetBounds(132, 54, 70, altoBuscador)
        lblDescValTxt.Location = New Point(20, 96)
        lblDescuento.Location = New Point(pnlTotales.Width - 126, 96)
        lblLinea.SetBounds(20, 130, pnlTotales.Width - 40, 2)
        lblTotalTxt.Location = New Point(20, 146)
        lblTotal.Location = New Point(pnlTotales.Width - 170, 138)
        btnCobrar.SetBounds(20, 184, pnlTotales.Width - 40, 54)
        btnLimpiar.SetBounds(20, 246, pnlTotales.Width - 40, 38)

        pnlTopBar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        MenuStrip1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        gbProductos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbCarrito.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pnlCatalogoHero.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        picMarca.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        dgvProductos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        pnlCarritoResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        dgvCarrito.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        pnlTotales.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        btnCobrar.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        btnLimpiar.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        btnSalida.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblLinea.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        lblTotal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lblTotalTxt.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblSubtotal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lblDescuento.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right

        AplicarCurvasPOS()
    End Sub

    Private Sub Form2_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarPantallaPOS()
    End Sub

    Private Sub RefrescarInventario()
        If Me.IsDisposed Then Return
        CargarCategorias()
        CargarProductos()
        FiltrarProductos()
    End Sub

    Private Sub RefrescarVentas()
        If Me.IsDisposed Then Return
        CargarProductos()
        FiltrarProductos()
        ActualizarNumVenta()
    End Sub

    Private Sub mnuCancelarVenta_Click(sender As Object, e As EventArgs) Handles mnuCancelarVenta.Click
        Using frm As New Form8()
            frm.ShowDialog()
        End Using
        CargarProductos()
    End Sub

    Private Sub mnuInventario_Click(sender As Object, e As EventArgs) Handles mnuInventario.Click
        Using frm As New Form3()
            frm.ShowDialog()
        End Using
        CargarProductos()
    End Sub

    Private Sub mnuHistorial_Click(sender As Object, e As EventArgs) Handles mnuHistorial.Click
        Using frm As New Form4()
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub mnuPedidos_Click(sender As Object, e As EventArgs) Handles mnuPedidos.Click
        Using frm As New Form5()
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub mnuReporte_Click(sender As Object, e As EventArgs) Handles mnuReporte.Click
        Using frm As New Form7()
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub InicializarCarrito()
        If dtCarrito.Columns.Count = 0 Then
            dtCarrito.Columns.Add("ID_Producto", GetType(Integer))
            dtCarrito.Columns.Add("Nombre", GetType(String))
            dtCarrito.Columns.Add("Precio", GetType(Decimal))
            dtCarrito.Columns.Add("Cantidad", GetType(Integer))
            dtCarrito.Columns.Add("SubTotal", GetType(Decimal))
        End If
    End Sub

    Private Sub CargarCategorias()
        Dim tabla = ObtenerTabla("SELECT NombreCat FROM " & TablaCategorias() & " ORDER BY NombreCat")
        AplicarCategorias(tabla)
    End Sub

    Private Sub AplicarCategorias(tabla As DataTable)
        Dim seleccionAnterior As String = cbCategoria.Text

        cbCategoria.BeginUpdate()
        Try
            cbCategoria.Items.Clear()
            cbCategoria.Items.Add("(Todas)")

            If tabla IsNot Nothing Then
                For Each row As DataRow In tabla.Rows
                    cbCategoria.Items.Add(row("NombreCat").ToString())
                Next
            End If
        Finally
            cbCategoria.EndUpdate()
        End Try

        If seleccionAnterior <> "" AndAlso cbCategoria.Items.Contains(seleccionAnterior) Then
            cbCategoria.SelectedItem = seleccionAnterior
        Else
            cbCategoria.SelectedIndex = 0
        End If

        ActualizarIndicadoresPOS()
    End Sub

    Private Sub CargarProductos()
        If dgvCarrito.DataSource Is Nothing Then
            dgvCarrito.DataSource = dtCarrito
        End If

        ConfigurarColumnasCarrito()

        Try
            AplicarProductos(ObtenerTabla(ObtenerSqlProductos()))
        Catch ex As Exception
            MsgBox("Error al cargar productos: " & ex.Message)
        End Try
    End Sub

    Private Function ObtenerSqlProductos() As String
        Return "SELECT p.Id_Producto AS ID_Producto, p.NombrePr AS Nombre, p.Precio, " &
               "ISNULL(i.cant_disp, 0) AS Stock, c.NombreCat AS Categoria " &
               "FROM PRODUCTO p " &
               "LEFT JOIN INVENTARIO i ON i.Id_Producto = p.Id_Producto " &
               "LEFT JOIN " & TablaCategorias() & " c ON c.Id_Categoria = p.Id_Categoria " &
               "ORDER BY c.NombreCat, p.NombrePr"
    End Function

    Private Function CrearTablaProductosVacia() As DataTable
        Dim tabla As New DataTable()
        tabla.Columns.Add("ID_Producto", GetType(Integer))
        tabla.Columns.Add("Nombre", GetType(String))
        tabla.Columns.Add("Precio", GetType(Decimal))
        tabla.Columns.Add("Stock", GetType(Integer))
        tabla.Columns.Add("Categoria", GetType(String))
        Return tabla
    End Function

    Private Sub ConfigurarColumnasCarrito()
        If dgvCarrito.Columns.Contains("ID_Producto") Then dgvCarrito.Columns("ID_Producto").Visible = False
        If dgvCarrito.Columns.Contains("Precio") Then dgvCarrito.Columns("Precio").Visible = False
        If dgvCarrito.Columns.Contains("Nombre") Then dgvCarrito.Columns("Nombre").HeaderText = "Producto"
        If dgvCarrito.Columns.Contains("Cantidad") Then dgvCarrito.Columns("Cantidad").HeaderText = "Qty"
        If dgvCarrito.Columns.Contains("SubTotal") Then dgvCarrito.Columns("SubTotal").HeaderText = "Subtotal"
    End Sub

    Private Sub ConfigurarColumnasProductos()
        If dgvProductos.Columns.Contains("ID_Producto") Then dgvProductos.Columns("ID_Producto").Visible = False
        If dgvProductos.Columns.Contains("Nombre") Then dgvProductos.Columns("Nombre").HeaderText = "Nombre"
        If dgvProductos.Columns.Contains("Precio") Then dgvProductos.Columns("Precio").HeaderText = "Precio"
        If dgvProductos.Columns.Contains("Stock") Then dgvProductos.Columns("Stock").HeaderText = "Stock"
        If dgvProductos.Columns.Contains("Categoria") Then dgvProductos.Columns("Categoria").HeaderText = "Categoria"
    End Sub

    Private Sub AplicarProductos(tabla As DataTable)
        dtProductos = If(tabla, CrearTablaProductosVacia())
        dgvProductos.DataSource = dtProductos
        ConfigurarColumnasProductos()
        ActualizarIndicadoresPOS()
    End Sub

    Private Sub ActualizarNumVenta()
        Try
            AplicarNumeroVenta(ObtenerEscalar("SELECT ISNULL(MAX(Id_Pedido),0)+1 FROM PEDIDOS"))
        Catch ex As Exception
            lblNumVenta.Text = "Ticket #V-001"
        End Try

        ActualizarIndicadoresPOS()
    End Sub

    Private Sub AplicarNumeroVenta(valor As Object)
        Dim num As Integer = 1

        If valor IsNot Nothing AndAlso Not IsDBNull(valor) Then
            Integer.TryParse(valor.ToString(), num)
        End If

        If num <= 0 Then num = 1
        lblNumVenta.Text = "Ticket #V-" & num.ToString("000")
        ActualizarIndicadoresPOS()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        FiltrarProductos()
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        FiltrarProductos()
    End Sub

    Private Sub cbCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCategoria.SelectedIndexChanged
        FiltrarProductos()
    End Sub

    Private Sub FiltrarProductos()
        Dim texto As String = txtBuscar.Text.Trim().Replace("'", "''")
        Dim cat As String = cbCategoria.Text.Replace("'", "''")
        Dim filtros As New List(Of String)

        If texto <> "" Then filtros.Add("Nombre LIKE '%" & texto & "%'")
        If cat <> "" AndAlso cat <> "(Todas)" Then filtros.Add("Categoria = '" & cat & "'")

        dtProductos.DefaultView.RowFilter = String.Join(" AND ", filtros.ToArray())
        ActualizarIndicadoresPOS()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If dgvProductos.CurrentRow Is Nothing Then
            MsgBox("Selecciona un producto de la lista.")
            Return
        End If

        Dim id As Integer = CInt(dgvProductos.CurrentRow.Cells("ID_Producto").Value)
        Dim nombre As String = dgvProductos.CurrentRow.Cells("Nombre").Value.ToString()
        Dim precio As Decimal = CDec(dgvProductos.CurrentRow.Cells("Precio").Value)
        Dim stock As Integer = CInt(dgvProductos.CurrentRow.Cells("Stock").Value)
        Dim qty As Integer = 0

        If Not Integer.TryParse(txtCantidad.Text, qty) OrElse qty <= 0 Then
            MsgBox("Ingresa una cantidad valida.")
            Return
        End If

        If qty > stock Then
            MsgBox("Stock insuficiente. Disponible: " & stock)
            Return
        End If

        For Each row As DataRow In dtCarrito.Rows
            If CInt(row("ID_Producto")) = id Then
                Dim nueva As Integer = CInt(row("Cantidad")) + qty
                If nueva > stock Then
                    MsgBox("Stock insuficiente.")
                    Return
                End If
                row("Cantidad") = nueva
                row("SubTotal") = CDec(row("Precio")) * nueva
                RecalcularTotales()
                Return
            End If
        Next

        dtCarrito.Rows.Add(id, nombre, precio, qty, precio * qty)
        RecalcularTotales()
    End Sub

    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        If dgvCarrito.CurrentRow Is Nothing Then
            MsgBox("Selecciona un producto del carrito.")
            Return
        End If

        Dim fila = TryCast(dgvCarrito.CurrentRow.DataBoundItem, DataRowView)
        If fila Is Nothing Then Return

        Dim cantidadActual As Integer = CInt(fila.Row("Cantidad"))
        If cantidadActual > 1 Then
            Dim nuevaCantidad As Integer = cantidadActual - 1
            fila.Row("Cantidad") = nuevaCantidad
            fila.Row("SubTotal") = CDec(fila.Row("Precio")) * nuevaCantidad
        Else
            fila.Row.Delete()
        End If

        RecalcularTotales()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        dtCarrito.Rows.Clear()
        RecalcularTotales()
    End Sub

    Private Sub txtDescPct_TextChanged(sender As Object, e As EventArgs) Handles txtDescPct.TextChanged
        RecalcularTotales()
    End Sub

    Private Function CalcularSubtotal() As Decimal
        Dim subtotal As Decimal = 0D
        For Each row As DataRow In dtCarrito.Rows
            If row.RowState <> DataRowState.Deleted Then
                subtotal += CDec(row("SubTotal"))
            End If
        Next
        Return subtotal
    End Function

    Private Function ObtenerDescuentoPct() As Decimal
        Dim pct As Decimal = 0D
        Decimal.TryParse(txtDescPct.Text, pct)
        If pct < 0D Then pct = 0D
        If pct > 100D Then pct = 100D
        Return pct
    End Function

    Private Function ObtenerTotalPiezasCarrito() As Integer
        Dim total As Integer = 0
        For Each row As DataRow In dtCarrito.Rows
            If row.RowState <> DataRowState.Deleted Then
                total += CInt(row("Cantidad"))
            End If
        Next
        Return total
    End Function

    Private Sub RecalcularTotales()
        Dim subtotal As Decimal = CalcularSubtotal()
        Dim pct As Decimal = ObtenerDescuentoPct()
        Dim descuento As Decimal = subtotal * pct / 100D
        Dim total As Decimal = subtotal - descuento
        Dim piezas As Integer = ObtenerTotalPiezasCarrito()

        lblSubtotal.Text = "$" & subtotal.ToString("N2")
        lblDescuento.Text = "-$" & descuento.ToString("N2")
        lblTotal.Text = "$" & total.ToString("N2")
        sbInfo.Text = "   Ticket activo: " & piezas & " pieza(s)   |   Cobro estimado: $" & total.ToString("N2")
        ActualizarIndicadoresPOS()
    End Sub

    Private Sub ActualizarIndicadoresPOS()
        If lblBadgeProductosValue Is Nothing Then Return

        Dim visibles As Integer = 0
        Dim categorias As Integer = Math.Max(0, cbCategoria.Items.Count - 1)
        Dim articulosEnCarrito As Integer = ObtenerTotalPiezasCarrito()

        If dtProductos IsNot Nothing Then
            visibles = dtProductos.DefaultView.Count
        End If

        lblBadgeProductosTitle.Text = "Categorias"
        lblBadgeProductosValue.Text = visibles.ToString("000")
        lblBadgeCarritoTitle.Text = "Piezas"
        lblBadgeCarritoValue.Text = articulosEnCarrito.ToString("00")
        If lblCarritoResumenItems IsNot Nothing Then lblCarritoResumenItems.Text = articulosEnCarrito.ToString() & " piezas"
        If lblTopState IsNot Nothing Then lblTopState.Text = ModEstilo.FormatoFechaHora24(Now)
        If lblCatalogoHeroHint IsNot Nothing Then lblCatalogoHeroHint.Text = visibles.ToString() & " productos visibles"
        sbFecha.Text = ModEstilo.FormatoFechaHora24(Now)
    End Sub

    Private Sub btnCobrar_Click(sender As Object, e As EventArgs) Handles btnCobrar.Click
        If dtCarrito.Rows.Count = 0 Then
            MsgBox("El carrito esta vacio.")
            Return
        End If

        Dim subtotal As Decimal = CalcularSubtotal()
        Dim pct As Decimal = ObtenerDescuentoPct()
        Dim descuento As Decimal = subtotal * pct / 100D
        Dim total As Decimal = subtotal - descuento
        Dim piezas As Integer = ObtenerTotalPiezasCarrito()

        Dim msg As String = "Confirmar venta?" & vbNewLine & vbNewLine &
                            "Articulos: " & piezas & vbNewLine &
                            "Subtotal:  $" & subtotal.ToString("N2") & vbNewLine &
                            "Descuento: -$" & descuento.ToString("N2") & vbNewLine &
                            "Total:     $" & total.ToString("N2")

        If MsgBox(msg, MsgBoxStyle.YesNo, "Confirmar venta") = MsgBoxResult.Yes Then
            GuardarVenta(total)
        End If
    End Sub

    Private Sub GuardarVenta(total As Decimal)
        Dim idPedido As Integer = 0

        Using cn = ObtenerConexion()
            cn.Open()
            Dim trans = cn.BeginTransaction()

            Try
                Dim idCliente As Integer = ObtenerIdClienteGeneral(trans)

                Using cmdPedido As New SqlCommand(
                    "INSERT INTO PEDIDOS (ID_CLIENTE, Total, MetodoPago) " &
                    "VALUES (@idCliente, @total, @metodo); " &
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    cn,
                    trans)
                    cmdPedido.Parameters.AddWithValue("@idCliente", idCliente)
                    cmdPedido.Parameters.AddWithValue("@total", total)
                    cmdPedido.Parameters.AddWithValue("@metodo", "Efectivo")
                    idPedido = CInt(cmdPedido.ExecuteScalar())
                End Using

                For Each row As DataRow In dtCarrito.Rows
                    Dim idP As Integer = CInt(row("ID_Producto"))
                    Dim qty As Integer = CInt(row("Cantidad"))
                    Dim precio As Decimal = CDec(row("Precio"))

                    Using cmdDet As New SqlCommand(
                        "INSERT INTO DET_PEDIDOS (Id_Pedido, Id_Producto, Cantidad, PrecioVentaMomento) " &
                        "VALUES (@idPedido, @idProducto, @cantidad, @precio)",
                        cn,
                        trans)
                        cmdDet.Parameters.AddWithValue("@idPedido", idPedido)
                        cmdDet.Parameters.AddWithValue("@idProducto", idP)
                        cmdDet.Parameters.AddWithValue("@cantidad", qty)
                        cmdDet.Parameters.AddWithValue("@precio", precio)
                        cmdDet.ExecuteNonQuery()
                    End Using

                    Using cmdStock As New SqlCommand(
                        "UPDATE INVENTARIO SET cant_disp = cant_disp - @qty " &
                        "WHERE Id_Producto = @idProducto AND cant_disp >= @qty",
                        cn,
                        trans)
                        cmdStock.Parameters.AddWithValue("@qty", qty)
                        cmdStock.Parameters.AddWithValue("@idProducto", idP)
                        If cmdStock.ExecuteNonQuery() = 0 Then
                            Throw New Exception("No se pudo actualizar el stock del producto " & idP.ToString())
                        End If
                    End Using
                Next

                trans.Commit()
                MsgBox("Venta registrada. Folio: V-" & idPedido.ToString("000"))
                ModActualizaciones.NotificarInventarioActualizado()
                ModActualizaciones.NotificarVentasActualizadas()

            Catch ex As Exception
                trans.Rollback()
                MsgBox("Error al guardar venta: " & ex.Message)
                idPedido = 0
            End Try
        End Using

        If idPedido > 0 Then
            If MsgBox("Deseas imprimir el ticket?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Imprimir ticket") = MsgBoxResult.Yes Then
                ImprimirTicketVenta(idPedido)
            End If
        End If

        dtCarrito.Rows.Clear()
        RecalcularTotales()
        CargarProductos()
        ActualizarNumVenta()
    End Sub

    Private Sub btnSalida_Click(sender As Object, e As EventArgs) Handles btnSalida.Click
        If MsgBox("Desea salir?", MsgBoxStyle.YesNo, "Salir") = MsgBoxResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.InventarioActualizado, AddressOf RefrescarInventario
        RemoveHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
    End Sub

    Private Sub ImprimirTicketVenta(idPedido As Integer)
        Try
            Dim texto = Form6.ObtenerTextoTicket(idPedido)
            If texto.Trim() = "" Then Return

            Dim pd As New PrintDocument()
            AddHandler pd.PrintPage,
                Sub(s, ev)
                    ev.Graphics.DrawString(
                        texto,
                        New Font("Courier New", 8),
                        Brushes.Black, 10, 10)
                End Sub

            Dim preview As New PrintPreviewDialog()
            preview.Document = pd
            preview.ShowDialog()
        Catch ex As Exception
            MsgBox("No se pudo preparar el ticket: " & ex.Message)
        End Try
    End Sub
End Class

' Administra el punto de venta: catalogo, carrito, cobro, guardado de ventas y navegacion.

Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class Form2

    ' Estado interno, controles dinamicos y tablas de trabajo del punto de venta.

    ' Tasa decimal usada para calcular el IVA sobre la base gravable.
    Private Const TASA_IVA As Decimal = 0.16D
    ' Tasa de IVA en porcentaje que se guarda y se muestra en tickets.
    Private Const TASA_IVA_PCT As Decimal = 16D

    ' Indica al login si la caja se abrio sin errores criticos.
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
    Private pnlRecordatorioPedidos As Panel
    Private lblRecordatorioPedidosTitulo As Label
    Private lblRecordatorioPedidosDetalle As Label
    Private tmrOcultarRecordatorioPedidos As Timer
    Private tmrRevisarRecordatoriosPedidos As Timer
    Private _inicioCorrecto As Boolean
    Private _cargandoDatosPOS As Boolean
    Private _ultimaClaveRecordatorioPedidos As String = ""

    ' Inicializa el formulario y aplica configuracion visual inicial.
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

    ' Importa la funcion de Windows que crea regiones con esquinas redondeadas.
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

    ' Libera objetos GDI creados al aplicar regiones redondeadas.
    <DllImport("gdi32.dll")>
    Private Shared Function DeleteObject(hObject As IntPtr) As Boolean
    End Function

    ' Inicializa la caja, se suscribe a eventos y comienza la carga de datos.
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ModEstilo.PrepararVentana(Me)
            AddHandler ModActualizaciones.InventarioActualizado, AddressOf RefrescarInventario
            AddHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
            AddHandler ModActualizaciones.PedidosActualizados, AddressOf RefrescarRecordatoriosPedidos
            InicializarComponentesPOS()

            InicializarCarrito()
            PrepararVistaInicialPOS()
            sbFecha.Text = ModEstilo.FormatoDiaFechaHora(Now)

            AplicarDisenoPOS()
            RecalcularTotales()
            _inicioCorrecto = True
            BeginInvoke(New MethodInvoker(AddressOf IniciarCargaInicialPOS))
            BeginInvoke(New MethodInvoker(AddressOf RevisarRecordatoriosPedidos))
            tmrRevisarRecordatoriosPedidos.Start()
        Catch ex As Exception
            _inicioCorrecto = False
            CargarModoSeguroPOS(ex)
        End Try
    End Sub

    ' Aplica estilos compartidos y particulares de la pantalla de caja.
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
        ModEstilo.ConfigurarRelojStatusStrip(Me, StatusStrip1, "sbFecha")
        AplicarEstiloPOS()
        ConfigurarPantallaPOS()
    End Sub

    ' Conecta tablas vacias y deja la caja visible mientras carga el catalogo.
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

    ' Dispara la carga asincrona inicial del catalogo y folio.
    Private Async Sub IniciarCargaInicialPOS()
        Await CargarDatosInicialesPOSAsync()
    End Sub

    ' Carga categorias, productos y numero de venta en segundo plano.
    Private Async Function CargarDatosInicialesPOSAsync() As Task
        If _cargandoDatosPOS OrElse Me.IsDisposed Then Return

        _cargandoDatosPOS = True
        btnAgregar.Enabled = False
        btnCobrar.Enabled = False
        btnQuitar.Enabled = False

        Try
            sbInfo.Text = "   Cargando catalogo y folio..."

            Dim validacion = Await Task.Run(Function()
                                                 Dim mensaje As String = ""
                                                 If ProbarConexionAplicacion(mensaje) Then Return ""
                                                 Return mensaje
                                             End Function)
            If validacion <> "" Then
                lblNumVenta.Text = "Ticket #V-001"
                sbInfo.Text = "   SQL Server Express no esta listo."
                ModMensajes.Mostrar(Me, "Conexion no disponible", validacion, ModMensajes.TipoAviso.Error)
                Return
            End If

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
            ModMensajes.Mostrar(Me, "Catalogo no disponible", CrearMensajeErrorDatos("cargar el catalogo", ex), ModMensajes.TipoAviso.Error)
        Finally
            If Not Me.IsDisposed Then
                btnAgregar.Enabled = True
                btnCobrar.Enabled = True
                btnQuitar.Enabled = True
            End If
            _cargandoDatosPOS = False
        End Try
    End Function

    ' Crea paneles dinamicos del encabezado, catalogo, carrito y totales.
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

        pnlRecordatorioPedidos = New Panel() With {
            .Name = "pnlRecordatorioPedidos",
            .BackColor = Color.FromArgb(255, 252, 247),
            .Visible = False
        }

        lblRecordatorioPedidosTitulo = New Label() With {
            .Name = "lblRecordatorioPedidosTitulo",
            .AutoSize = False,
            .Text = "Pedido cercano",
            .ForeColor = Color.FromArgb(76, 66, 55),
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleLeft
        }

        lblRecordatorioPedidosDetalle = New Label() With {
            .Name = "lblRecordatorioPedidosDetalle",
            .AutoSize = False,
            .ForeColor = Color.FromArgb(120, 104, 85),
            .Font = New Font("Segoe UI", 8.75F),
            .TextAlign = ContentAlignment.TopLeft
        }

        pnlRecordatorioPedidos.Controls.Add(lblRecordatorioPedidosTitulo)
        pnlRecordatorioPedidos.Controls.Add(lblRecordatorioPedidosDetalle)
        Me.Controls.Add(pnlRecordatorioPedidos)

        tmrOcultarRecordatorioPedidos = New Timer() With {.Interval = 3000}
        AddHandler tmrOcultarRecordatorioPedidos.Tick, AddressOf OcultarRecordatorioPedidos

        tmrRevisarRecordatoriosPedidos = New Timer() With {.Interval = 300000}
        AddHandler tmrRevisarRecordatoriosPedidos.Tick, AddressOf tmrRevisarRecordatoriosPedidos_Tick

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

    ' Aplica la identidad visual retail al punto de venta.
    Private Sub AplicarEstiloPOS()
        If pnlTopBar Is Nothing Then Return

        Me.BackColor = Color.FromArgb(244, 240, 234)
        Me.Text = "KUMO | Caja"

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
        lblTotalTxt.Text = "TOTAL A PAGAR"

        btnAgregar.Text = "Agregar"
        btnQuitar.Text = "Quitar"
        btnCobrar.Text = "Cobrar"
        btnLimpiar.Text = "Cancelar"
        btnSalida.Text = "Salir"

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

    ' Configura el formato de una tarjeta pequena de indicador.
    Private Sub ConfigurarBadge(panel As Panel, titulo As Label, valor As Label, colorFondo As Color, colorTitulo As Color, colorValor As Color)
        panel.BackColor = colorFondo
        titulo.ForeColor = colorTitulo
        titulo.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        titulo.TextAlign = ContentAlignment.MiddleLeft
        valor.ForeColor = colorValor
        valor.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        valor.TextAlign = ContentAlignment.MiddleLeft
    End Sub

    ' Aplica estilo retail a un boton con colores y tamano de fuente recibidos.
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

    ' Aplica estilo de campo de captura retail y alinea su texto.
    Private Sub EstilarCampoRetail(tb As TextBox, centrado As Boolean)
        tb.BackColor = Color.FromArgb(255, 252, 247)
        tb.ForeColor = Color.FromArgb(71, 63, 54)
        tb.BorderStyle = BorderStyle.FixedSingle
        tb.Font = New Font("Segoe UI", 10.0F)
        tb.TextAlign = If(centrado, HorizontalAlignment.Center, HorizontalAlignment.Left)
    End Sub

    ' Aplica estilo visual al combo de categorias.
    Private Sub EstilarComboRetail(cb As ComboBox)
        cb.BackColor = Color.FromArgb(255, 252, 247)
        cb.ForeColor = Color.FromArgb(71, 63, 54)
        cb.FlatStyle = FlatStyle.Flat
        cb.Font = New Font("Segoe UI", 9.5F)
    End Sub

    ' Aplica formato de tabla a catalogo o carrito segun el caso.
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

    ' Redondea los paneles y botones principales de la caja.
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

    ' Redondea controles del dialogo y limpia la region si falla el recurso nativo.
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

    ' Muestra una version minima de la caja si ocurre un error al cargar.
    Private Sub CargarModoSeguroPOS(ex As Exception)
        Try
            If pnlTopBar Is Nothing Then
                InicializarComponentesPOS()
            End If

            ModEstilo.EstilarControles(Me)
            ModEstilo.EstilarMenuStrip(MenuStrip1)
            ModEstilo.EstilarStatusStrip(StatusStrip1)
            ModEstilo.ConfigurarRelojStatusStrip(Me, StatusStrip1, "sbFecha")
            ConfigurarPantallaPOS()
            RecalcularTotales()

            If sbInfo IsNot Nothing Then
                sbInfo.Text = "  POS cargado en modo seguro."
            End If
        Catch
            ' Si incluso el modo seguro falla, conservamos el mensaje original.
        End Try

        ModMensajes.Mostrar(Me, "Carga del POS", "Se detecto un error al cargar el estilo del POS." & vbCrLf &
                             "La caja quedo en modo seguro para que no se cierre la app." & vbCrLf &
                             "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
    End Sub

    ' Devuelve el nombre real de la tabla de categorias con el caracter acentuado.
    Private Function TablaCategorias() As String
        Return "[CATEGOR" & ChrW(205) & "A]"
    End Function

    ' Calcula y asigna posiciones para la pantalla completa del POS.
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

        ConfigurarRecordatorioPedidos()
        AplicarCurvasPOS()
    End Sub

    ' Acomoda el aviso temporal de pedidos cercanos en la esquina superior derecha.
    Private Sub ConfigurarRecordatorioPedidos()
        If pnlRecordatorioPedidos Is Nothing Then Return

        Dim ancho As Integer = 360
        Dim alto As Integer = 92
        Dim margen As Integer = 24
        Dim topAviso As Integer = If(MenuStrip1 IsNot Nothing, MenuStrip1.Bottom + 14, 18)

        pnlRecordatorioPedidos.SetBounds(Me.ClientSize.Width - ancho - margen, topAviso, ancho, alto)
        lblRecordatorioPedidosTitulo.SetBounds(18, 12, ancho - 36, 24)
        lblRecordatorioPedidosDetalle.SetBounds(18, 39, ancho - 36, 42)
        pnlRecordatorioPedidos.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnlRecordatorioPedidos.BringToFront()
        RedondearControl(pnlRecordatorioPedidos, 18)
    End Sub

    ' Reacomoda los controles cuando cambia el tamano del formulario.
    Private Sub Form2_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarPantallaPOS()
    End Sub

    ' Recarga categorias y productos despues de cambios externos.
    Private Sub RefrescarInventario()
        If Me.IsDisposed Then Return
        CargarCategorias()
        CargarProductos()
        FiltrarProductos()
    End Sub

    ' Vuelve a cargar ventas cuando otro modulo registra cambios.
    Private Sub RefrescarVentas()
        If Me.IsDisposed Then Return
        CargarProductos()
        FiltrarProductos()
        ActualizarNumVenta()
    End Sub

    ' Vuelve a revisar pedidos cuando se guardan o eliminan desde la agenda.
    Private Sub RefrescarRecordatoriosPedidos()
        If Me.IsDisposed Then Return
        _ultimaClaveRecordatorioPedidos = ""
        RevisarRecordatoriosPedidos()
    End Sub

    ' Abre el formulario de cancelaciones desde el menu.
    Private Sub mnuCancelarVenta_Click(sender As Object, e As EventArgs) Handles mnuCancelarVenta.Click
        Using frm As New Form8()
            frm.ShowDialog()
        End Using
        CargarProductos()
    End Sub

    ' Abre el formulario de inventario desde el menu.
    Private Sub mnuInventario_Click(sender As Object, e As EventArgs) Handles mnuInventario.Click
        Using frm As New Form3()
            frm.ShowDialog()
        End Using
        CargarProductos()
    End Sub

    ' Abre el historial de ventas desde el menu.
    Private Sub mnuHistorial_Click(sender As Object, e As EventArgs) Handles mnuHistorial.Click
        Using frm As New Form4()
            frm.ShowDialog()
        End Using
    End Sub

    ' Abre la administracion de pedidos desde el menu.
    Private Sub mnuPedidos_Click(sender As Object, e As EventArgs) Handles mnuPedidos.Click
        Using frm As New Form5()
            frm.ShowDialog()
        End Using
    End Sub

    ' Abre el reporte diario desde el menu.
    Private Sub mnuReporte_Click(sender As Object, e As EventArgs) Handles mnuReporte.Click
        Using frm As New Form7()
            frm.ShowDialog()
        End Using
    End Sub

    ' Define las columnas del DataTable que guarda los productos del ticket activo.
    Private Sub InicializarCarrito()
        If dtCarrito.Columns.Count = 0 Then
            dtCarrito.Columns.Add("ID_Producto", GetType(Integer))
            dtCarrito.Columns.Add("Nombre", GetType(String))
            dtCarrito.Columns.Add("Precio", GetType(Decimal))
            dtCarrito.Columns.Add("Cantidad", GetType(Integer))
            dtCarrito.Columns.Add("SubTotal", GetType(Decimal))
        End If
    End Sub

    ' Consulta las categorias desde la base de datos y las aplica al combo.
    Private Sub CargarCategorias()
        Try
            Dim tabla = ObtenerTabla("SELECT NombreCat FROM " & TablaCategorias() & " ORDER BY NombreCat")
            AplicarCategorias(tabla)
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Categorias no disponibles", CrearMensajeErrorDatos("cargar las categorias", ex), ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    ' Llena el combo de categorias y actualiza el contador visual.
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

    ' Carga productos con precio, stock y categoria desde la base.
    Private Sub CargarProductos()
        If dgvCarrito.DataSource Is Nothing Then
            dgvCarrito.DataSource = dtCarrito
        End If

        ConfigurarColumnasCarrito()

        Try
            AplicarProductos(ObtenerTabla(ObtenerSqlProductos()))
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Catalogo no disponible", CrearMensajeErrorDatos("cargar los productos", ex), ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    ' Construye el SELECT principal para listar productos, stock y categoria.
    Private Function ObtenerSqlProductos() As String
        Return "SELECT p.Id_Producto AS ID_Producto, p.NombrePr AS Nombre, p.Precio, " &
               "ISNULL(i.cant_disp, 0) AS Stock, c.NombreCat AS Categoria " &
               "FROM PRODUCTO p " &
               "LEFT JOIN INVENTARIO i ON i.Id_Producto = p.Id_Producto " &
               "LEFT JOIN " & TablaCategorias() & " c ON c.Id_Categoria = p.Id_Categoria " &
               "ORDER BY c.NombreCat, p.NombrePr"
    End Function

    ' Crea una tabla vacia con las columnas esperadas del catalogo.
    Private Function CrearTablaProductosVacia() As DataTable
        Dim tabla As New DataTable()
        tabla.Columns.Add("ID_Producto", GetType(Integer))
        tabla.Columns.Add("Nombre", GetType(String))
        tabla.Columns.Add("Precio", GetType(Decimal))
        tabla.Columns.Add("Stock", GetType(Integer))
        tabla.Columns.Add("Categoria", GetType(String))
        Return tabla
    End Function

    ' Oculta y formatea columnas del carrito de venta.
    Private Sub ConfigurarColumnasCarrito()
        If dgvCarrito.Columns.Contains("ID_Producto") Then dgvCarrito.Columns("ID_Producto").Visible = False
        If dgvCarrito.Columns.Contains("Precio") Then dgvCarrito.Columns("Precio").Visible = False
        If dgvCarrito.Columns.Contains("Nombre") Then dgvCarrito.Columns("Nombre").HeaderText = "Producto"
        If dgvCarrito.Columns.Contains("Cantidad") Then dgvCarrito.Columns("Cantidad").HeaderText = "Qty"
        If dgvCarrito.Columns.Contains("SubTotal") Then dgvCarrito.Columns("SubTotal").HeaderText = "Subtotal"
    End Sub

    ' Oculta y formatea columnas del catalogo de productos.
    Private Sub ConfigurarColumnasProductos()
        If dgvProductos.Columns.Contains("ID_Producto") Then dgvProductos.Columns("ID_Producto").Visible = False
        If dgvProductos.Columns.Contains("Nombre") Then dgvProductos.Columns("Nombre").HeaderText = "Nombre"
        If dgvProductos.Columns.Contains("Precio") Then dgvProductos.Columns("Precio").HeaderText = "Precio"
        If dgvProductos.Columns.Contains("Stock") Then dgvProductos.Columns("Stock").HeaderText = "Stock"
        If dgvProductos.Columns.Contains("Categoria") Then dgvProductos.Columns("Categoria").HeaderText = "Categoria"
    End Sub

    ' Asigna productos al grid y actualiza columnas e indicadores.
    Private Sub AplicarProductos(tabla As DataTable)
        dtProductos = If(tabla, CrearTablaProductosVacia())
        dgvProductos.DataSource = dtProductos
        ConfigurarColumnasProductos()
        ActualizarIndicadoresPOS()
    End Sub

    ' Consulta el siguiente folio de venta y lo muestra en pantalla.
    Private Sub ActualizarNumVenta()
        Try
            AplicarNumeroVenta(ObtenerEscalar("SELECT ISNULL(MAX(Id_Pedido),0)+1 FROM PEDIDOS"))
        Catch ex As Exception
            lblNumVenta.Text = "Ticket #V-001"
        End Try

        ActualizarIndicadoresPOS()
    End Sub

    ' Convierte el folio recibido a texto de ticket visible.
    Private Sub AplicarNumeroVenta(valor As Object)
        Dim num As Integer = 1

        If valor IsNot Nothing AndAlso Not IsDBNull(valor) Then
            Integer.TryParse(valor.ToString(), num)
        End If

        If num <= 0 Then num = 1
        lblNumVenta.Text = "Ticket #V-" & num.ToString("000")
        ActualizarIndicadoresPOS()
    End Sub

    ' Ejecuta el filtro del catalogo cuando se presiona buscar.
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        FiltrarProductos()
    End Sub

    ' Filtra productos al escribir en el cuadro de busqueda.
    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        FiltrarProductos()
    End Sub

    ' Filtra productos cuando cambia la categoria seleccionada.
    Private Sub cbCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCategoria.SelectedIndexChanged
        FiltrarProductos()
    End Sub

    ' Aplica filtros de nombre y categoria al DataView del catalogo.
    Private Sub FiltrarProductos()
        Dim texto As String = txtBuscar.Text.Trim().Replace("'", "''")
        Dim cat As String = cbCategoria.Text.Replace("'", "''")
        Dim filtros As New List(Of String)

        If texto <> "" Then filtros.Add("Nombre LIKE '%" & texto & "%'")
        If cat <> "" AndAlso cat <> "(Todas)" Then filtros.Add("Categoria = '" & cat & "'")

        dtProductos.DefaultView.RowFilter = String.Join(" AND ", filtros.ToArray())
        ActualizarIndicadoresPOS()
    End Sub

    ' Valida producto y cantidad, agrega o acumula el articulo en el carrito.
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If dgvProductos.CurrentRow Is Nothing Then
            ModMensajes.Mostrar(Me, "Selecciona un producto", "Elige un producto del catalogo antes de agregarlo al ticket.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        Dim id As Integer = CInt(dgvProductos.CurrentRow.Cells("ID_Producto").Value)
        Dim nombre As String = dgvProductos.CurrentRow.Cells("Nombre").Value.ToString()
        Dim precio As Decimal = CDec(dgvProductos.CurrentRow.Cells("Precio").Value)
        Dim stock As Integer = CInt(dgvProductos.CurrentRow.Cells("Stock").Value)
        Dim qty As Integer = 0

        If Not Integer.TryParse(txtCantidad.Text, qty) OrElse qty <= 0 Then
            ModMensajes.Mostrar(Me, "Cantidad no valida", "Ingresa una cantidad mayor a cero para agregar al carrito.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        If qty > stock Then
            ModMensajes.Mostrar(Me, "Stock insuficiente", "Solo hay " & stock.ToString() & " pieza(s) disponibles para este producto.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        For Each row As DataRow In dtCarrito.Rows
            If CInt(row("ID_Producto")) = id Then
                Dim nueva As Integer = CInt(row("Cantidad")) + qty
                If nueva > stock Then
                    ModMensajes.Mostrar(Me, "Stock insuficiente", "La cantidad del carrito supera el stock disponible.", ModMensajes.TipoAviso.Advertencia)
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

    ' Quita del carrito la linea seleccionada.
    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        If dgvCarrito.CurrentRow Is Nothing Then
            ModMensajes.Mostrar(Me, "Selecciona un producto", "Elige un producto del carrito antes de quitar piezas.", ModMensajes.TipoAviso.Advertencia)
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

    ' Limpia el carrito completo y recalcula los totales.
    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        dtCarrito.Rows.Clear()
        RecalcularTotales()
    End Sub

    ' Recalcula los importes cuando cambia el porcentaje de descuento.
    Private Sub txtDescPct_TextChanged(sender As Object, e As EventArgs) Handles txtDescPct.TextChanged
        RecalcularTotales()
    End Sub

    ' Suma los importes de todas las lineas del carrito.
    Private Function CalcularSubtotal() As Decimal
        Dim subtotal As Decimal = 0D
        For Each row As DataRow In dtCarrito.Rows
            If row.RowState <> DataRowState.Deleted Then
                subtotal += CDec(row("SubTotal"))
            End If
        Next
        Return subtotal
    End Function

    ' Lee y normaliza el porcentaje de descuento capturado.
    Private Function ObtenerDescuentoPct() As Decimal
        Dim pct As Decimal = 0D
        Decimal.TryParse(txtDescPct.Text, pct)
        If pct < 0D Then pct = 0D
        If pct > 100D Then pct = 100D
        Return pct
    End Function

    ' Suma todas las piezas contenidas en el carrito.
    Private Function ObtenerTotalPiezasCarrito() As Integer
        Dim total As Integer = 0
        For Each row As DataRow In dtCarrito.Rows
            If row.RowState <> DataRowState.Deleted Then
                total += CInt(row("Cantidad"))
            End If
        Next
        Return total
    End Function

    ' Calcula el IVA de la base gravable usando la tasa definida.
    Private Function CalcularIva(baseGravable As Decimal) As Decimal
        If baseGravable <= 0D Then Return 0D
        Return Math.Round(baseGravable * TASA_IVA, 2, MidpointRounding.AwayFromZero)
    End Function

    ' Actualiza subtotal, descuento, total e indicadores del POS.
    Private Sub RecalcularTotales()
        Dim subtotal As Decimal = CalcularSubtotal()
        Dim pct As Decimal = ObtenerDescuentoPct()
        Dim descuento As Decimal = subtotal * pct / 100D
        Dim baseGravable As Decimal = subtotal - descuento
        Dim iva As Decimal = CalcularIva(baseGravable)
        Dim total As Decimal = baseGravable + iva
        Dim piezas As Integer = ObtenerTotalPiezasCarrito()

        lblSubtotal.Text = "$" & subtotal.ToString("N2")
        lblDescuento.Text = "-$" & descuento.ToString("N2")
        lblTotal.Text = "$" & total.ToString("N2")
        sbInfo.Text = "   Ticket activo: " & piezas & " pieza(s)   |   Cobro estimado: $" & total.ToString("N2")
        ActualizarIndicadoresPOS()
    End Sub

    ' Actualiza contadores visuales de categorias e items del carrito.
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
        sbFecha.Text = ModEstilo.FormatoDiaFechaHora(Now)
    End Sub

    ' Consulta pedidos próximos y muestra un aviso corto si hay entregas cercanas.
    Private Sub RevisarRecordatoriosPedidos()
        If Me.IsDisposed OrElse pnlRecordatorioPedidos Is Nothing Then Return

        Try
            Dim hoy As Date = Today
            Dim limite As Date = hoy.AddDays(2)
            Dim tabla = ObtenerTabla(
                "SELECT TOP 1 p.Id_Pedido, " &
                "RTRIM(LTRIM(c.Nombres_cl + ' ' + ISNULL(c.Apellidos,''))) AS Cliente, " &
                "p.Fecha AS Entrega, " &
                "DATEDIFF(day, @hoy, CAST(p.Fecha AS date)) AS DiasRestantes, " &
                "COUNT(*) OVER() AS TotalPedidos " &
                "FROM PEDIDOS p " &
                "INNER JOIN CLIENTES c ON c.ID_CLIENTE = p.ID_CLIENTE " &
                "WHERE CAST(p.Fecha AS date) BETWEEN @hoy AND @limite " &
                "AND NOT EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido) " &
                "AND ISNULL(p.MetodoPago, 'Pendiente') IN ('Pendiente', 'En proceso', 'Listo para entregar') " &
                "ORDER BY p.Fecha, p.Id_Pedido",
                New SqlParameter("@hoy", hoy),
                New SqlParameter("@limite", limite))

            If tabla.Rows.Count = 0 Then Return

            Dim row = tabla.Rows(0)
            Dim idPedido As Integer = CInt(row("Id_Pedido"))
            Dim cliente As String = row("Cliente").ToString()
            Dim diasRestantes As Integer = CInt(row("DiasRestantes"))
            Dim totalPedidos As Integer = CInt(row("TotalPedidos"))
            Dim entrega As Date = CDate(row("Entrega"))
            Dim clave As String = hoy.ToString("yyyyMMdd") & "|" & idPedido.ToString() & "|" & totalPedidos.ToString()

            If clave = _ultimaClaveRecordatorioPedidos Then Return
            _ultimaClaveRecordatorioPedidos = clave

            MostrarRecordatorioPedidos(idPedido, cliente, entrega, diasRestantes, totalPedidos)
        Catch
            ' El aviso no debe interrumpir la venta si la agenda no esta disponible.
        End Try
    End Sub

    ' Presenta el aviso visual de pedido cercano durante unos segundos.
    Private Sub MostrarRecordatorioPedidos(idPedido As Integer, cliente As String, entrega As Date, diasRestantes As Integer, totalPedidos As Integer)
        If pnlRecordatorioPedidos Is Nothing Then Return

        Dim tiempoEntrega As String
        If diasRestantes = 0 Then
            tiempoEntrega = "hoy"
        ElseIf diasRestantes = 1 Then
            tiempoEntrega = "manana"
        Else
            tiempoEntrega = "en " & diasRestantes.ToString() & " dias"
        End If

        lblRecordatorioPedidosTitulo.Text = If(totalPedidos = 1, "Pedido cercano", totalPedidos.ToString() & " pedidos cercanos")
        lblRecordatorioPedidosDetalle.Text =
            "Pedido #" & idPedido.ToString("000") & " para " & cliente & vbCrLf &
            "Entrega " & tiempoEntrega & " (" & ModEstilo.FormatoFechaHora24(entrega) & ")"

        ConfigurarRecordatorioPedidos()
        pnlRecordatorioPedidos.Visible = True
        pnlRecordatorioPedidos.BringToFront()
        tmrOcultarRecordatorioPedidos.Stop()
        tmrOcultarRecordatorioPedidos.Start()
    End Sub

    ' Oculta el aviso temporal de pedidos.
    Private Sub OcultarRecordatorioPedidos(sender As Object, e As EventArgs)
        tmrOcultarRecordatorioPedidos.Stop()
        If pnlRecordatorioPedidos IsNot Nothing Then pnlRecordatorioPedidos.Visible = False
    End Sub

    ' Revisa periodicamente pedidos cercanos mientras la caja esta abierta.
    Private Sub tmrRevisarRecordatoriosPedidos_Tick(sender As Object, e As EventArgs)
        RevisarRecordatoriosPedidos()
    End Sub

    ' Muestra el dialogo de pago y devuelve metodo, pago recibido y cambio.
    Private Function SolicitarPago(subtotal As Decimal,
                                   descuento As Decimal,
                                   baseGravable As Decimal,
                                   iva As Decimal,
                                   total As Decimal,
                                   ByRef pagoCon As Decimal,
                                   ByRef cambio As Decimal,
                                   ByRef metodoPago As String) As Boolean
        Dim clrDark As Color = Color.FromArgb(46, 52, 60)
        Dim clrSurface As Color = Color.FromArgb(255, 252, 247)
        Dim clrPanel As Color = Color.FromArgb(247, 243, 237)
        Dim clrGold As Color = Color.FromArgb(213, 196, 166)
        Dim clrText As Color = Color.FromArgb(76, 66, 55)
        Dim clrMuted As Color = Color.FromArgb(120, 104, 85)
        Dim clrGreen As Color = Color.FromArgb(74, 133, 95)
        Dim clrRed As Color = Color.FromArgb(146, 79, 67)

        Using dlg As New Form()
            dlg.Text = "Caja de cobro"
            dlg.FormBorderStyle = FormBorderStyle.None
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.ClientSize = New Size(640, 620)
            dlg.BackColor = Color.FromArgb(244, 240, 234)
            dlg.ShowInTaskbar = False
            dlg.KeyPreview = True

            Dim pnlHeader As New Panel() With {
                .BackColor = clrDark,
                .Dock = DockStyle.Top,
                .Height = 92
            }

            Dim lblTitulo As New Label() With {
                .Text = "Caja de cobro",
                .ForeColor = Color.FromArgb(255, 248, 239),
                .Font = New Font("Segoe UI", 18.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 14, 280, 36),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblSubtitulo As New Label() With {
                .Text = "Elige el metodo de pago y confirma la venta",
                .ForeColor = Color.FromArgb(244, 212, 141),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(28, 54, 320, 22),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblTicket As New Label() With {
                .Text = lblNumVenta.Text,
                .ForeColor = Color.FromArgb(244, 226, 193),
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                .Bounds = New Rectangle(382, 24, 150, 34),
                .TextAlign = ContentAlignment.MiddleRight
            }

            Dim btnCerrar As New Button() With {
                .Text = "X",
                .Bounds = New Rectangle(582, 24, 34, 34),
                .FlatStyle = FlatStyle.Flat,
                .BackColor = Color.FromArgb(57, 64, 73),
                .ForeColor = Color.FromArgb(244, 226, 193),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand,
                .DialogResult = DialogResult.Cancel
            }
            btnCerrar.FlatAppearance.BorderColor = Color.FromArgb(96, 87, 72)
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 78, 88)

            pnlHeader.Controls.Add(lblTitulo)
            pnlHeader.Controls.Add(lblSubtitulo)
            pnlHeader.Controls.Add(lblTicket)
            pnlHeader.Controls.Add(btnCerrar)
            dlg.Controls.Add(pnlHeader)

            Dim pnlMetodo As New Panel() With {
                .BackColor = clrSurface,
                .Bounds = New Rectangle(30, 106, 580, 50)
            }

            Dim lblMetodo As New Label() With {
                .Text = "Metodo de pago",
                .ForeColor = clrMuted,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(18, 12, 132, 26),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim btnMetodoEfectivo As New Button() With {.Text = "Efectivo", .Bounds = New Rectangle(154, 8, 126, 34)}
            Dim btnMetodoTarjeta As New Button() With {.Text = "Tarjeta", .Bounds = New Rectangle(292, 8, 126, 34)}
            Dim btnMetodoTransferencia As New Button() With {.Text = "Transferencia", .Bounds = New Rectangle(430, 8, 132, 34)}

            pnlMetodo.Controls.Add(lblMetodo)
            pnlMetodo.Controls.Add(btnMetodoEfectivo)
            pnlMetodo.Controls.Add(btnMetodoTarjeta)
            pnlMetodo.Controls.Add(btnMetodoTransferencia)
            dlg.Controls.Add(pnlMetodo)

            Dim pnlCard As New Panel() With {
                .BackColor = clrSurface,
                .Bounds = New Rectangle(30, 172, 580, 370)
            }

            Dim lblSubtotalCaption As New Label() With {
                .Text = "Subtotal",
                .ForeColor = clrMuted,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 24, 160, 24),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblSubtotalValor As New Label() With {
                .Text = "$" & subtotal.ToString("N2"),
                .ForeColor = clrText,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .Bounds = New Rectangle(340, 24, 210, 24),
                .TextAlign = ContentAlignment.MiddleRight
            }

            Dim lblDescuentoCaption As New Label() With {
                .Text = "Descuento",
                .ForeColor = clrMuted,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 54, 160, 24),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblDescuentoValor As New Label() With {
                .Text = "-$" & descuento.ToString("N2"),
                .ForeColor = clrRed,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .Bounds = New Rectangle(340, 54, 210, 24),
                .TextAlign = ContentAlignment.MiddleRight
            }

            Dim lblBaseCaption As New Label() With {
                .Text = "Base gravable",
                .ForeColor = clrMuted,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 84, 160, 24),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblBaseValor As New Label() With {
                .Text = "$" & baseGravable.ToString("N2"),
                .ForeColor = clrText,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .Bounds = New Rectangle(340, 84, 210, 24),
                .TextAlign = ContentAlignment.MiddleRight
            }

            Dim lblIvaCaption As New Label() With {
                .Text = "IVA trasladado (" & TASA_IVA_PCT.ToString("N0") & "%)",
                .ForeColor = Color.FromArgb(74, 133, 95),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 114, 190, 24),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblIvaValor As New Label() With {
                .Text = "$" & iva.ToString("N2"),
                .ForeColor = Color.FromArgb(74, 133, 95),
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .Bounds = New Rectangle(340, 114, 210, 24),
                .TextAlign = ContentAlignment.MiddleRight
            }

            Dim linea As New Panel() With {
                .BackColor = Color.FromArgb(217, 199, 171),
                .Bounds = New Rectangle(26, 150, 528, 2)
            }

            Dim lblTotalCaption As New Label() With {
                .Text = "Total a pagar",
                .ForeColor = clrDark,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 168, 170, 28),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblTotalValor As New Label() With {
                .Text = "$" & total.ToString("N2"),
                .ForeColor = clrDark,
                .Font = New Font("Segoe UI", 23.0F, FontStyle.Bold),
                .Bounds = New Rectangle(280, 154, 270, 54),
                .TextAlign = ContentAlignment.MiddleRight
            }

            Dim lineaPago As New Panel() With {
                .BackColor = Color.FromArgb(230, 219, 203),
                .Bounds = New Rectangle(26, 218, 528, 2)
            }

            Dim lblPago As New Label() With {
                .Text = "Efectivo recibido",
                .ForeColor = clrMuted,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 238, 160, 24),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim txtPago As New TextBox() With {
                .Text = total.ToString("N2"),
                .BackColor = clrSurface,
                .ForeColor = Color.FromArgb(71, 63, 54),
                .BorderStyle = BorderStyle.FixedSingle,
                .Font = New Font("Segoe UI", 15.0F, FontStyle.Bold),
                .Bounds = New Rectangle(340, 232, 210, 34),
                .TextAlign = HorizontalAlignment.Right
            }

            Dim lblCambio As New Label() With {
                .Text = "Cambio",
                .ForeColor = clrMuted,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 280, 160, 24),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblCambioValor As New Label() With {
                .Text = "$0.00",
                .ForeColor = clrGreen,
                .Font = New Font("Segoe UI", 18.0F, FontStyle.Bold),
                .Bounds = New Rectangle(340, 270, 210, 42),
                .TextAlign = ContentAlignment.MiddleRight
            }

            Dim pnlEstado As New Panel() With {
                .BackColor = Color.FromArgb(236, 249, 241),
                .Bounds = New Rectangle(26, 320, 528, 36)
            }

            Dim lblEstado As New Label() With {
                .Text = "",
                .ForeColor = clrGreen,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(14, 5, 500, 26),
                .TextAlign = ContentAlignment.MiddleLeft
            }
            pnlEstado.Controls.Add(lblEstado)

            pnlCard.Controls.Add(lblSubtotalCaption)
            pnlCard.Controls.Add(lblSubtotalValor)
            pnlCard.Controls.Add(lblDescuentoCaption)
            pnlCard.Controls.Add(lblDescuentoValor)
            pnlCard.Controls.Add(lblBaseCaption)
            pnlCard.Controls.Add(lblBaseValor)
            pnlCard.Controls.Add(lblIvaCaption)
            pnlCard.Controls.Add(lblIvaValor)
            pnlCard.Controls.Add(lblTotalCaption)
            pnlCard.Controls.Add(lblTotalValor)
            pnlCard.Controls.Add(linea)
            pnlCard.Controls.Add(lineaPago)
            pnlCard.Controls.Add(lblPago)
            pnlCard.Controls.Add(txtPago)
            pnlCard.Controls.Add(lblCambio)
            pnlCard.Controls.Add(lblCambioValor)
            pnlCard.Controls.Add(pnlEstado)
            dlg.Controls.Add(pnlCard)

            Dim btnCancelar As New Button() With {
                .Text = "Cancelar",
                .Bounds = New Rectangle(30, 562, 280, 40),
                .DialogResult = DialogResult.Cancel
            }
            Dim btnConfirmar As New Button() With {
                .Text = "Confirmar venta",
                .Bounds = New Rectangle(330, 562, 280, 40)
            }
            EstilarBotonRetail(btnCancelar, Color.FromArgb(249, 243, 234), Color.FromArgb(98, 84, 69), Color.FromArgb(216, 198, 172), Color.FromArgb(243, 235, 224), 9.5F)
            EstilarBotonRetail(btnConfirmar, clrGreen, Color.White, clrGreen, Color.FromArgb(58, 111, 78), 10.0F)

            dlg.Controls.Add(btnCancelar)
            dlg.Controls.Add(btnConfirmar)

            Dim pagoCapturado As Decimal = 0D
            Dim cambioCapturado As Decimal = 0D
            Dim metodoSeleccionado As String = "Efectivo"
            Dim actualizarCambio As Action = Nothing

            Dim pintarMetodo As Action(Of Button, Boolean) =
                Sub(btn, activo)
                    If activo Then
                        EstilarBotonRetail(btn, clrDark, Color.White, clrDark, Color.FromArgb(57, 64, 73), 8.75F)
                    Else
                        EstilarBotonRetail(btn, Color.FromArgb(249, 243, 234), clrText, Color.FromArgb(216, 198, 172), Color.FromArgb(243, 235, 224), 8.75F)
                    End If
                End Sub

            Dim aplicarMetodo As Action(Of String) =
                Sub(metodo)
                    metodoSeleccionado = metodo
                    pintarMetodo(btnMetodoEfectivo, metodo = "Efectivo")
                    pintarMetodo(btnMetodoTarjeta, metodo = "Tarjeta")
                    pintarMetodo(btnMetodoTransferencia, metodo = "Transferencia")
                    lblTitulo.Text = If(metodo = "Efectivo", "Cobro en efectivo", "Cobro con " & metodo.ToLower())
                    If actualizarCambio IsNot Nothing Then actualizarCambio()
                End Sub

            actualizarCambio =
                Sub()
                    If metodoSeleccionado <> "Efectivo" Then
                        If txtPago.Text <> total.ToString("N2") Then txtPago.Text = total.ToString("N2")
                        txtPago.Enabled = False
                        txtPago.BackColor = Color.FromArgb(249, 243, 234)
                        lblPago.Text = "Monto a cobrar"
                        lblCambio.Text = "Cambio"
                        lblCambioValor.Text = "$0.00"
                        lblCambioValor.ForeColor = clrGreen
                        pnlEstado.BackColor = Color.FromArgb(236, 249, 241)
                        lblEstado.ForeColor = clrGreen
                        lblEstado.Text = "Pago con " & metodoSeleccionado.ToLower() & " listo para registrar."
                        btnConfirmar.Enabled = True
                        Return
                    End If

                    txtPago.Enabled = True
                    txtPago.BackColor = clrSurface
                    lblPago.Text = "Efectivo recibido"
                    lblCambio.Text = "Cambio"

                    Dim recibido As Decimal = 0D
                    If txtPago.Text.Trim() = "" Then
                        lblCambioValor.Text = "$0.00"
                        lblCambioValor.ForeColor = clrMuted
                        pnlEstado.BackColor = Color.FromArgb(249, 243, 234)
                        lblEstado.ForeColor = clrMuted
                        lblEstado.Text = "Ingresa el efectivo recibido."
                        btnConfirmar.Enabled = False
                        Return
                    End If

                    If Not TryParseMonto(txtPago.Text, recibido) Then
                        lblCambioValor.Text = "$0.00"
                        lblCambioValor.ForeColor = clrRed
                        pnlEstado.BackColor = Color.FromArgb(255, 242, 242)
                        lblEstado.ForeColor = clrRed
                        lblEstado.Text = "Monto no valido."
                        btnConfirmar.Enabled = False
                        Return
                    End If

                    Dim diferencia As Decimal = recibido - total
                    If diferencia < 0D Then
                        lblCambioValor.Text = "-$" & Math.Abs(diferencia).ToString("N2")
                        lblCambioValor.ForeColor = clrRed
                        pnlEstado.BackColor = Color.FromArgb(255, 242, 242)
                        lblEstado.ForeColor = clrRed
                        lblEstado.Text = "Faltan $" & Math.Abs(diferencia).ToString("N2") & " para completar el pago."
                        btnConfirmar.Enabled = False
                    Else
                        lblCambioValor.Text = "$" & diferencia.ToString("N2")
                        lblCambioValor.ForeColor = clrGreen
                        pnlEstado.BackColor = Color.FromArgb(236, 249, 241)
                        lblEstado.ForeColor = clrGreen
                        lblEstado.Text = "Pago listo para registrar."
                        btnConfirmar.Enabled = True
                    End If
                End Sub

            AddHandler btnMetodoEfectivo.Click, Sub(sender, e) aplicarMetodo("Efectivo")
            AddHandler btnMetodoTarjeta.Click, Sub(sender, e) aplicarMetodo("Tarjeta")
            AddHandler btnMetodoTransferencia.Click, Sub(sender, e) aplicarMetodo("Transferencia")
            AddHandler txtPago.TextChanged, Sub(sender, e) actualizarCambio()
            AddHandler btnConfirmar.Click,
                Sub(sender, e)
                    If metodoSeleccionado <> "Efectivo" Then
                        pagoCapturado = total
                        cambioCapturado = 0D
                        dlg.DialogResult = DialogResult.OK
                        dlg.Close()
                        Return
                    End If

                    Dim recibido As Decimal = 0D
                    If Not TryParseMonto(txtPago.Text, recibido) OrElse recibido < total Then
                        actualizarCambio()
                        txtPago.Focus()
                        txtPago.SelectAll()
                        Return
                    End If

                    pagoCapturado = recibido
                    cambioCapturado = recibido - total
                    dlg.DialogResult = DialogResult.OK
                    dlg.Close()
                End Sub

            dlg.AcceptButton = btnConfirmar
            dlg.CancelButton = btnCancelar
            RedondearControl(dlg, 28)
            RedondearControl(pnlHeader, 24)
            RedondearControl(pnlMetodo, 18)
            RedondearControl(pnlCard, 24)
            RedondearControl(pnlEstado, 16)
            RedondearControl(btnCerrar, 16)
            RedondearControl(btnMetodoEfectivo, 14)
            RedondearControl(btnMetodoTarjeta, 14)
            RedondearControl(btnMetodoTransferencia, 14)
            RedondearControl(btnCancelar, 16)
            RedondearControl(btnConfirmar, 16)

            AddHandler dlg.Shown,
                Sub(sender, e)
                    aplicarMetodo("Efectivo")
                    actualizarCambio()
                    txtPago.Focus()
                    txtPago.SelectAll()
                End Sub

            If dlg.ShowDialog(Me) = DialogResult.OK Then
                pagoCon = pagoCapturado
                cambio = cambioCapturado
                metodoPago = metodoSeleccionado
                Return True
            End If

            Return False
        End Using
    End Function

    ' Muestra un aviso modal con estilo propio de la caja.
    Private Function MostrarAvisoPOS(titulo As String,
                                     mensaje As String,
                                     textoPrimario As String,
                                     Optional textoSecundario As String = "",
                                     Optional esError As Boolean = False) As Boolean
        Dim clrDark As Color = Color.FromArgb(46, 52, 60)
        Dim clrSurface As Color = Color.FromArgb(255, 252, 247)
        Dim clrWarm As Color = Color.FromArgb(247, 243, 237)
        Dim clrGold As Color = Color.FromArgb(244, 212, 141)
        Dim clrGreen As Color = Color.FromArgb(74, 133, 95)
        Dim clrRed As Color = Color.FromArgb(146, 79, 67)
        Dim clrText As Color = Color.FromArgb(76, 66, 55)
        Dim clrMuted As Color = Color.FromArgb(120, 104, 85)

        Using dlg As New Form()
            dlg.Text = titulo
            dlg.FormBorderStyle = FormBorderStyle.None
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.ClientSize = New Size(520, 300)
            dlg.BackColor = Color.FromArgb(244, 240, 234)
            dlg.ShowInTaskbar = False
            dlg.KeyPreview = True

            Dim pnlHeader As New Panel() With {
                .BackColor = clrDark,
                .Dock = DockStyle.Top,
                .Height = 84
            }

            Dim lblTitulo As New Label() With {
                .Text = titulo,
                .ForeColor = Color.FromArgb(255, 248, 239),
                .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
                .Bounds = New Rectangle(24, 14, 390, 30),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim lblSubtitulo As New Label() With {
                .Text = If(esError, "Revisa el detalle antes de continuar", "Operacion de caja"),
                .ForeColor = If(esError, Color.FromArgb(255, 204, 194), clrGold),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 48, 390, 22),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim btnCerrar As New Button() With {
                .Text = "X",
                .Bounds = New Rectangle(462, 22, 34, 34),
                .FlatStyle = FlatStyle.Flat,
                .BackColor = Color.FromArgb(57, 64, 73),
                .ForeColor = Color.FromArgb(244, 226, 193),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand,
                .DialogResult = DialogResult.Cancel
            }
            btnCerrar.FlatAppearance.BorderColor = Color.FromArgb(96, 87, 72)
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 78, 88)

            pnlHeader.Controls.Add(lblTitulo)
            pnlHeader.Controls.Add(lblSubtitulo)
            pnlHeader.Controls.Add(btnCerrar)
            dlg.Controls.Add(pnlHeader)

            Dim pnlCard As New Panel() With {
                .BackColor = clrSurface,
                .Bounds = New Rectangle(28, 108, 464, 112)
            }

            Dim barra As New Panel() With {
                .BackColor = If(esError, clrRed, clrGreen),
                .Bounds = New Rectangle(18, 22, 6, 68)
            }

            Dim lblMensaje As New Label() With {
                .Text = mensaje,
                .ForeColor = clrText,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .Bounds = New Rectangle(38, 18, 402, 78),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            pnlCard.Controls.Add(barra)
            pnlCard.Controls.Add(lblMensaje)
            dlg.Controls.Add(pnlCard)

            Dim haySecundario As Boolean = textoSecundario.Trim() <> ""
            Dim btnPrimario As New Button() With {
                .Text = textoPrimario,
                .Bounds = If(haySecundario, New Rectangle(270, 242, 222, 38), New Rectangle(154, 242, 212, 38)),
                .DialogResult = DialogResult.OK
            }
            EstilarBotonRetail(btnPrimario, If(esError, clrDark, clrGreen), Color.White, If(esError, clrDark, clrGreen), If(esError, Color.FromArgb(57, 64, 73), Color.FromArgb(58, 111, 78)), 9.5F)
            dlg.Controls.Add(btnPrimario)

            If haySecundario Then
                Dim btnSecundario As New Button() With {
                    .Text = textoSecundario,
                    .Bounds = New Rectangle(28, 242, 222, 38),
                    .DialogResult = DialogResult.Cancel
                }
                EstilarBotonRetail(btnSecundario, Color.FromArgb(249, 243, 234), Color.FromArgb(98, 84, 69), Color.FromArgb(216, 198, 172), Color.FromArgb(243, 235, 224), 9.5F)
                RedondearControl(btnSecundario, 16)
                dlg.Controls.Add(btnSecundario)
                dlg.CancelButton = btnSecundario
            Else
                dlg.CancelButton = btnPrimario
            End If

            dlg.AcceptButton = btnPrimario
            RedondearControl(dlg, 26)
            RedondearControl(pnlHeader, 22)
            RedondearControl(pnlCard, 20)
            RedondearControl(btnCerrar, 16)
            RedondearControl(btnPrimario, 16)

            Return dlg.ShowDialog(Me) = DialogResult.OK
        End Using
    End Function

    ' Intenta convertir texto de dinero con cultura actual, mexicana o invariante.
    Private Function TryParseMonto(valor As String, ByRef monto As Decimal) As Boolean
        Dim estilos As System.Globalization.NumberStyles =
            System.Globalization.NumberStyles.Number Or System.Globalization.NumberStyles.AllowCurrencySymbol

        If Decimal.TryParse(valor, estilos, System.Globalization.CultureInfo.CurrentCulture, monto) Then Return True
        If Decimal.TryParse(valor, estilos, System.Globalization.CultureInfo.GetCultureInfo("es-MX"), monto) Then Return True
        If Decimal.TryParse(valor.Replace(",", ""), estilos, System.Globalization.CultureInfo.InvariantCulture, monto) Then Return True

        Return False
    End Function

    ' Calcula importes finales, solicita pago y guarda la venta.
    Private Sub btnCobrar_Click(sender As Object, e As EventArgs) Handles btnCobrar.Click
        If dtCarrito.Rows.Count = 0 Then
            ModMensajes.Mostrar(Me, "Carrito vacio", "Agrega productos al ticket antes de cobrar.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        Dim subtotal As Decimal = CalcularSubtotal()
        Dim pct As Decimal = ObtenerDescuentoPct()
        Dim descuento As Decimal = subtotal * pct / 100D
        Dim baseGravable As Decimal = subtotal - descuento
        Dim iva As Decimal = CalcularIva(baseGravable)
        Dim total As Decimal = baseGravable + iva
        Dim pagoCon As Decimal = 0D
        Dim cambio As Decimal = 0D
        Dim metodoPago As String = "Efectivo"

        If Not SolicitarPago(subtotal, descuento, baseGravable, iva, total, pagoCon, cambio, metodoPago) Then Return

        GuardarVenta(subtotal, descuento, baseGravable, iva, total, metodoPago, pagoCon, cambio)
    End Sub

    ' Guarda el pedido y detalle en una transaccion, descuenta stock y ofrece imprimir ticket.
    Private Sub GuardarVenta(subtotal As Decimal,
                             descuento As Decimal,
                             baseGravable As Decimal,
                             iva As Decimal,
                             total As Decimal,
                             metodoPago As String,
                             pagoCon As Decimal,
                             cambio As Decimal)
        Dim idPedido As Integer = 0

        Try
            AsegurarColumnasPagoPedido()
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "No se pudo guardar", CrearMensajeErrorDatos("preparar la venta", ex), ModMensajes.TipoAviso.Error)
            Return
        End Try

        Using cn = ObtenerConexion()
            Dim trans As SqlTransaction = Nothing
            Try
                cn.Open()
                trans = cn.BeginTransaction()
                Dim idCliente As Integer = ObtenerIdClienteGeneral(trans)

                Using cmdPedido As New SqlCommand(
                    "INSERT INTO PEDIDOS (ID_CLIENTE, Fecha, Subtotal, Descuento, BaseGravable, IVA, TasaIVA, Total, MetodoPago, PagoCon, Cambio, Cancelada) " &
                    "VALUES (@idCliente, @fecha, @subtotal, @descuento, @baseGravable, @iva, @tasaIva, @total, @metodo, @pagoCon, @cambio, 0); " &
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    cn,
                    trans)
                    cmdPedido.Parameters.AddWithValue("@idCliente", idCliente)
                    cmdPedido.Parameters.AddWithValue("@fecha", DateTime.Now)
                    cmdPedido.Parameters.AddWithValue("@subtotal", subtotal)
                    cmdPedido.Parameters.AddWithValue("@descuento", descuento)
                    cmdPedido.Parameters.AddWithValue("@baseGravable", baseGravable)
                    cmdPedido.Parameters.AddWithValue("@iva", iva)
                    cmdPedido.Parameters.AddWithValue("@tasaIva", TASA_IVA_PCT)
                    cmdPedido.Parameters.AddWithValue("@total", total)
                    cmdPedido.Parameters.AddWithValue("@metodo", metodoPago)
                    cmdPedido.Parameters.AddWithValue("@pagoCon", pagoCon)
                    cmdPedido.Parameters.AddWithValue("@cambio", cambio)
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
                ModActualizaciones.NotificarInventarioActualizado()
                ModActualizaciones.NotificarVentasActualizadas()

            Catch ex As Exception
                If trans IsNot Nothing Then trans.Rollback()
                ModMensajes.Mostrar(Me, "No se pudo guardar", CrearMensajeErrorDatos("registrar la venta", ex), ModMensajes.TipoAviso.Error)
                idPedido = 0
            End Try
        End Using

        If idPedido > 0 Then
            Dim mensajeExito As String =
                "Venta V-" & idPedido.ToString("000") & " registrada correctamente." & vbCrLf &
                "Metodo: " & metodoPago & "   Pago: $" & pagoCon.ToString("N2") & "   Cambio: $" & cambio.ToString("N2") & vbCrLf &
                "Puedes imprimir el ticket ahora."

            If ModMensajes.Confirmar(Me, "Venta registrada", mensajeExito, "Imprimir ticket", "Cerrar", ModMensajes.TipoAviso.Exito) Then
                ImprimirTicketVenta(idPedido)
            End If
        End If

        dtCarrito.Rows.Clear()
        RecalcularTotales()
        CargarProductos()
        ActualizarNumVenta()
    End Sub

    ' Pide confirmacion y cierra la aplicacion de caja.
    Private Sub btnSalida_Click(sender As Object, e As EventArgs) Handles btnSalida.Click
        If ModMensajes.Confirmar(Me, "Cerrar caja", "Deseas salir del sistema de caja?", "Salir", "Seguir aqui", ModMensajes.TipoAviso.Advertencia) Then
            Application.Exit()
        End If
    End Sub

    ' Quita suscripciones a eventos antes de cerrar la caja.
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.InventarioActualizado, AddressOf RefrescarInventario
        RemoveHandler ModActualizaciones.VentasActualizadas, AddressOf RefrescarVentas
        RemoveHandler ModActualizaciones.PedidosActualizados, AddressOf RefrescarRecordatoriosPedidos
        If tmrOcultarRecordatorioPedidos IsNot Nothing Then tmrOcultarRecordatorioPedidos.Stop()
        If tmrRevisarRecordatoriosPedidos IsNot Nothing Then tmrRevisarRecordatoriosPedidos.Stop()
    End Sub

    ' Obtiene el texto del ticket y abre la vista previa de impresion.
    Private Sub ImprimirTicketVenta(idPedido As Integer)
        Try
            Dim texto = Form6.ObtenerTextoTicket(idPedido)
            Form6.MostrarVistaPreviaTicket(texto, Me, "Ticket de venta V-" & idPedido.ToString("000"))
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "No se pudo imprimir", "La venta se registro, pero no se pudo preparar el ticket." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
        End Try
    End Sub
End Class

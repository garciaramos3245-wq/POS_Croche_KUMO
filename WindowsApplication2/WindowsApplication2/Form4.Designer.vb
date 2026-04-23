Partial Class Form4
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub
    Private Sub InitializeComponent()
        Me.gbFiltro = New System.Windows.Forms.GroupBox()
        Me.lblFechaTxt = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnHoy = New System.Windows.Forms.Button()
        Me.pnlIngresos = New System.Windows.Forms.Panel()
        Me.lblIngresosTitle = New System.Windows.Forms.Label()
        Me.lblIngresosVal = New System.Windows.Forms.Label()
        Me.lblIngresosSub = New System.Windows.Forms.Label()
        Me.pnlVentas = New System.Windows.Forms.Panel()
        Me.lblVentasTitle = New System.Windows.Forms.Label()
        Me.lblVentasVal = New System.Windows.Forms.Label()
        Me.lblVentasSub = New System.Windows.Forms.Label()
        Me.pnlPromedio = New System.Windows.Forms.Panel()
        Me.lblPromedioTitle = New System.Windows.Forms.Label()
        Me.lblPromedioVal = New System.Windows.Forms.Label()
        Me.lblPromedioSub = New System.Windows.Forms.Label()
        Me.pnlArticulos = New System.Windows.Forms.Panel()
        Me.lblArticulosTitle = New System.Windows.Forms.Label()
        Me.lblArticulosVal = New System.Windows.Forms.Label()
        Me.lblArticulosSub = New System.Windows.Forms.Label()
        Me.gbTabla = New System.Windows.Forms.GroupBox()
        Me.dgvVentas = New System.Windows.Forms.DataGridView()
        Me.btnTicket = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sbInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnRegresar = New System.Windows.Forms.Button()
        Me.gbFiltro.SuspendLayout()
        Me.pnlIngresos.SuspendLayout()
        Me.pnlVentas.SuspendLayout()
        Me.pnlPromedio.SuspendLayout()
        Me.pnlArticulos.SuspendLayout()
        Me.gbTabla.SuspendLayout()
        CType(Me.dgvVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbFiltro
        '
        Me.gbFiltro.BackColor = System.Drawing.Color.White
        Me.gbFiltro.Controls.Add(Me.lblFechaTxt)
        Me.gbFiltro.Controls.Add(Me.dtpFecha)
        Me.gbFiltro.Controls.Add(Me.btnBuscar)
        Me.gbFiltro.Controls.Add(Me.btnHoy)
        Me.gbFiltro.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbFiltro.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbFiltro.Location = New System.Drawing.Point(13, 12)
        Me.gbFiltro.Name = "gbFiltro"
        Me.gbFiltro.Padding = New System.Windows.Forms.Padding(4)
        Me.gbFiltro.Size = New System.Drawing.Size(560, 62)
        Me.gbFiltro.TabIndex = 0
        Me.gbFiltro.TabStop = False
        Me.gbFiltro.Text = "Filtro de ventas"
        '
        'lblFechaTxt
        '
        Me.lblFechaTxt.AutoSize = True
        Me.lblFechaTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblFechaTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblFechaTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblFechaTxt.Location = New System.Drawing.Point(11, 25)
        Me.lblFechaTxt.Name = "lblFechaTxt"
        Me.lblFechaTxt.Size = New System.Drawing.Size(54, 20)
        Me.lblFechaTxt.TabIndex = 0
        Me.lblFechaTxt.Text = "Fecha"
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.dtpFecha.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFecha.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.dtpFecha.CalendarTitleForeColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.dtpFecha.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(73, 21)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(172, 27)
        Me.dtpFecha.TabIndex = 1
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBuscar.FlatAppearance.BorderSize = 0
        Me.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuscar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnBuscar.ForeColor = System.Drawing.Color.White
        Me.btnBuscar.Location = New System.Drawing.Point(257, 18)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(100, 30)
        Me.btnBuscar.TabIndex = 2
        Me.btnBuscar.Text = "Ver ventas"
        Me.btnBuscar.UseVisualStyleBackColor = False
        '
        'btnHoy
        '
        Me.btnHoy.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnHoy.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHoy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnHoy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnHoy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHoy.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnHoy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnHoy.Location = New System.Drawing.Point(367, 18)
        Me.btnHoy.Name = "btnHoy"
        Me.btnHoy.Size = New System.Drawing.Size(80, 30)
        Me.btnHoy.TabIndex = 3
        Me.btnHoy.Text = "Hoy"
        Me.btnHoy.UseVisualStyleBackColor = False
        '
        'pnlIngresos
        '
        Me.pnlIngresos.BackColor = System.Drawing.Color.White
        Me.pnlIngresos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlIngresos.Controls.Add(Me.lblIngresosTitle)
        Me.pnlIngresos.Controls.Add(Me.lblIngresosVal)
        Me.pnlIngresos.Controls.Add(Me.lblIngresosSub)
        Me.pnlIngresos.Location = New System.Drawing.Point(13, 84)
        Me.pnlIngresos.Name = "pnlIngresos"
        Me.pnlIngresos.Size = New System.Drawing.Size(227, 82)
        Me.pnlIngresos.TabIndex = 1
        '
        'lblIngresosTitle
        '
        Me.lblIngresosTitle.AutoSize = True
        Me.lblIngresosTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblIngresosTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblIngresosTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblIngresosTitle.Location = New System.Drawing.Point(10, 8)
        Me.lblIngresosTitle.Name = "lblIngresosTitle"
        Me.lblIngresosTitle.Size = New System.Drawing.Size(119, 17)
        Me.lblIngresosTitle.TabIndex = 0
        Me.lblIngresosTitle.Text = "INGRESOS DEL DÍA"
        '
        'lblIngresosVal
        '
        Me.lblIngresosVal.AutoSize = True
        Me.lblIngresosVal.BackColor = System.Drawing.Color.Transparent
        Me.lblIngresosVal.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblIngresosVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblIngresosVal.Location = New System.Drawing.Point(10, 26)
        Me.lblIngresosVal.Name = "lblIngresosVal"
        Me.lblIngresosVal.Size = New System.Drawing.Size(78, 32)
        Me.lblIngresosVal.TabIndex = 1
        Me.lblIngresosVal.Text = "$0.00"
        '
        'lblIngresosSub
        '
        Me.lblIngresosSub.AutoSize = True
        Me.lblIngresosSub.BackColor = System.Drawing.Color.Transparent
        Me.lblIngresosSub.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblIngresosSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblIngresosSub.Location = New System.Drawing.Point(10, 58)
        Me.lblIngresosSub.Name = "lblIngresosSub"
        Me.lblIngresosSub.Size = New System.Drawing.Size(48, 17)
        Me.lblIngresosSub.TabIndex = 2
        Me.lblIngresosSub.Text = "del día"
        '
        'pnlVentas
        '
        Me.pnlVentas.BackColor = System.Drawing.Color.White
        Me.pnlVentas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlVentas.Controls.Add(Me.lblVentasTitle)
        Me.pnlVentas.Controls.Add(Me.lblVentasVal)
        Me.pnlVentas.Controls.Add(Me.lblVentasSub)
        Me.pnlVentas.Location = New System.Drawing.Point(251, 84)
        Me.pnlVentas.Name = "pnlVentas"
        Me.pnlVentas.Size = New System.Drawing.Size(227, 82)
        Me.pnlVentas.TabIndex = 2
        '
        'lblVentasTitle
        '
        Me.lblVentasTitle.AutoSize = True
        Me.lblVentasTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblVentasTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblVentasTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblVentasTitle.Location = New System.Drawing.Point(10, 8)
        Me.lblVentasTitle.Name = "lblVentasTitle"
        Me.lblVentasTitle.Size = New System.Drawing.Size(129, 17)
        Me.lblVentasTitle.TabIndex = 0
        Me.lblVentasTitle.Text = "VENTAS REALIZADAS"
        '
        'lblVentasVal
        '
        Me.lblVentasVal.AutoSize = True
        Me.lblVentasVal.BackColor = System.Drawing.Color.Transparent
        Me.lblVentasVal.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblVentasVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblVentasVal.Location = New System.Drawing.Point(10, 26)
        Me.lblVentasVal.Name = "lblVentasVal"
        Me.lblVentasVal.Size = New System.Drawing.Size(29, 32)
        Me.lblVentasVal.TabIndex = 1
        Me.lblVentasVal.Text = "0"
        '
        'lblVentasSub
        '
        Me.lblVentasSub.AutoSize = True
        Me.lblVentasSub.BackColor = System.Drawing.Color.Transparent
        Me.lblVentasSub.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblVentasSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblVentasSub.Location = New System.Drawing.Point(10, 58)
        Me.lblVentasSub.Name = "lblVentasSub"
        Me.lblVentasSub.Size = New System.Drawing.Size(87, 17)
        Me.lblVentasSub.TabIndex = 2
        Me.lblVentasSub.Text = "transacciones"
        '
        'pnlPromedio
        '
        Me.pnlPromedio.BackColor = System.Drawing.Color.White
        Me.pnlPromedio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPromedio.Controls.Add(Me.lblPromedioTitle)
        Me.pnlPromedio.Controls.Add(Me.lblPromedioVal)
        Me.pnlPromedio.Controls.Add(Me.lblPromedioSub)
        Me.pnlPromedio.Location = New System.Drawing.Point(489, 84)
        Me.pnlPromedio.Name = "pnlPromedio"
        Me.pnlPromedio.Size = New System.Drawing.Size(227, 82)
        Me.pnlPromedio.TabIndex = 3
        '
        'lblPromedioTitle
        '
        Me.lblPromedioTitle.AutoSize = True
        Me.lblPromedioTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblPromedioTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblPromedioTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblPromedioTitle.Location = New System.Drawing.Point(10, 8)
        Me.lblPromedioTitle.Name = "lblPromedioTitle"
        Me.lblPromedioTitle.Size = New System.Drawing.Size(118, 17)
        Me.lblPromedioTitle.TabIndex = 0
        Me.lblPromedioTitle.Text = "TICKET PROMEDIO"
        '
        'lblPromedioVal
        '
        Me.lblPromedioVal.AutoSize = True
        Me.lblPromedioVal.BackColor = System.Drawing.Color.Transparent
        Me.lblPromedioVal.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblPromedioVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblPromedioVal.Location = New System.Drawing.Point(10, 26)
        Me.lblPromedioVal.Name = "lblPromedioVal"
        Me.lblPromedioVal.Size = New System.Drawing.Size(78, 32)
        Me.lblPromedioVal.TabIndex = 1
        Me.lblPromedioVal.Text = "$0.00"
        '
        'lblPromedioSub
        '
        Me.lblPromedioSub.AutoSize = True
        Me.lblPromedioSub.BackColor = System.Drawing.Color.Transparent
        Me.lblPromedioSub.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblPromedioSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblPromedioSub.Location = New System.Drawing.Point(10, 58)
        Me.lblPromedioSub.Name = "lblPromedioSub"
        Me.lblPromedioSub.Size = New System.Drawing.Size(64, 17)
        Me.lblPromedioSub.TabIndex = 2
        Me.lblPromedioSub.Text = "por venta"
        '
        'pnlArticulos
        '
        Me.pnlArticulos.BackColor = System.Drawing.Color.White
        Me.pnlArticulos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlArticulos.Controls.Add(Me.lblArticulosTitle)
        Me.pnlArticulos.Controls.Add(Me.lblArticulosVal)
        Me.pnlArticulos.Controls.Add(Me.lblArticulosSub)
        Me.pnlArticulos.Location = New System.Drawing.Point(727, 84)
        Me.pnlArticulos.Name = "pnlArticulos"
        Me.pnlArticulos.Size = New System.Drawing.Size(227, 82)
        Me.pnlArticulos.TabIndex = 4
        '
        'lblArticulosTitle
        '
        Me.lblArticulosTitle.AutoSize = True
        Me.lblArticulosTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblArticulosTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblArticulosTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblArticulosTitle.Location = New System.Drawing.Point(10, 8)
        Me.lblArticulosTitle.Name = "lblArticulosTitle"
        Me.lblArticulosTitle.Size = New System.Drawing.Size(140, 17)
        Me.lblArticulosTitle.TabIndex = 0
        Me.lblArticulosTitle.Text = "ARTÍCULOS VENDIDOS"
        '
        'lblArticulosVal
        '
        Me.lblArticulosVal.AutoSize = True
        Me.lblArticulosVal.BackColor = System.Drawing.Color.Transparent
        Me.lblArticulosVal.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblArticulosVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblArticulosVal.Location = New System.Drawing.Point(10, 26)
        Me.lblArticulosVal.Name = "lblArticulosVal"
        Me.lblArticulosVal.Size = New System.Drawing.Size(29, 32)
        Me.lblArticulosVal.TabIndex = 1
        Me.lblArticulosVal.Text = "0"
        '
        'lblArticulosSub
        '
        Me.lblArticulosSub.AutoSize = True
        Me.lblArticulosSub.BackColor = System.Drawing.Color.Transparent
        Me.lblArticulosSub.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblArticulosSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblArticulosSub.Location = New System.Drawing.Point(10, 58)
        Me.lblArticulosSub.Name = "lblArticulosSub"
        Me.lblArticulosSub.Size = New System.Drawing.Size(61, 17)
        Me.lblArticulosSub.TabIndex = 2
        Me.lblArticulosSub.Text = "unidades"
        '
        'gbTabla
        '
        Me.gbTabla.BackColor = System.Drawing.Color.White
        Me.gbTabla.Controls.Add(Me.dgvVentas)
        Me.gbTabla.Controls.Add(Me.btnTicket)
        Me.gbTabla.Controls.Add(Me.btnImprimir)
        Me.gbTabla.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbTabla.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbTabla.Location = New System.Drawing.Point(13, 177)
        Me.gbTabla.Name = "gbTabla"
        Me.gbTabla.Padding = New System.Windows.Forms.Padding(4)
        Me.gbTabla.Size = New System.Drawing.Size(960, 480)
        Me.gbTabla.TabIndex = 5
        Me.gbTabla.TabStop = False
        Me.gbTabla.Text = "Ventas del dia"
        '
        'dgvVentas
        '
        Me.dgvVentas.AllowUserToAddRows = False
        Me.dgvVentas.AllowUserToDeleteRows = False
        Me.dgvVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvVentas.Location = New System.Drawing.Point(11, 24)
        Me.dgvVentas.MultiSelect = False
        Me.dgvVentas.Name = "dgvVentas"
        Me.dgvVentas.ReadOnly = True
        Me.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvVentas.Size = New System.Drawing.Size(933, 418)
        Me.dgvVentas.TabIndex = 0
        '
        'btnTicket
        '
        Me.btnTicket.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnTicket.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTicket.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnTicket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTicket.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnTicket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnTicket.Location = New System.Drawing.Point(11, 450)
        Me.btnTicket.Name = "btnTicket"
        Me.btnTicket.Size = New System.Drawing.Size(140, 30)
        Me.btnTicket.TabIndex = 1
        Me.btnTicket.Text = "Abrir ticket"
        Me.btnTicket.UseVisualStyleBackColor = False
        '
        'btnImprimir
        '
        Me.btnImprimir.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnImprimir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnImprimir.Location = New System.Drawing.Point(162, 450)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(160, 30)
        Me.btnImprimir.TabIndex = 2
        Me.btnImprimir.Text = "Exportar vista"
        Me.btnImprimir.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbInfo})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 685)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(6, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1000, 24)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 6
        '
        'sbInfo
        '
        Me.sbInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.sbInfo.ForeColor = System.Drawing.Color.White
        Me.sbInfo.Name = "sbInfo"
        Me.sbInfo.Size = New System.Drawing.Size(982, 19)
        Me.sbInfo.Spring = True
        Me.sbInfo.Text = "  Listo"
        Me.sbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRegresar
        '
        Me.btnRegresar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRegresar.FlatAppearance.BorderSize = 0
        Me.btnRegresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegresar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRegresar.ForeColor = System.Drawing.Color.White
        Me.btnRegresar.Location = New System.Drawing.Point(866, 12)
        Me.btnRegresar.Name = "btnRegresar"
        Me.btnRegresar.Size = New System.Drawing.Size(107, 36)
        Me.btnRegresar.TabIndex = 7
        Me.btnRegresar.Text = "Cerrar"
        Me.btnRegresar.UseVisualStyleBackColor = False
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1000, 709)
        Me.Controls.Add(Me.btnRegresar)
        Me.Controls.Add(Me.gbFiltro)
        Me.Controls.Add(Me.pnlIngresos)
        Me.Controls.Add(Me.pnlVentas)
        Me.Controls.Add(Me.pnlPromedio)
        Me.Controls.Add(Me.pnlArticulos)
        Me.Controls.Add(Me.gbTabla)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "Form4"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KUMO | Historial premium"
        Me.gbFiltro.ResumeLayout(False)
        Me.gbFiltro.PerformLayout()
        Me.pnlIngresos.ResumeLayout(False)
        Me.pnlIngresos.PerformLayout()
        Me.pnlVentas.ResumeLayout(False)
        Me.pnlVentas.PerformLayout()
        Me.pnlPromedio.ResumeLayout(False)
        Me.pnlPromedio.PerformLayout()
        Me.pnlArticulos.ResumeLayout(False)
        Me.pnlArticulos.PerformLayout()
        Me.gbTabla.ResumeLayout(False)
        CType(Me.dgvVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbFiltro As System.Windows.Forms.GroupBox
    Friend WithEvents lblFechaTxt As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents btnHoy As System.Windows.Forms.Button
    Friend WithEvents pnlIngresos As System.Windows.Forms.Panel
    Friend WithEvents lblIngresosTitle As System.Windows.Forms.Label
    Friend WithEvents lblIngresosVal As System.Windows.Forms.Label
    Friend WithEvents lblIngresosSub As System.Windows.Forms.Label
    Friend WithEvents pnlVentas As System.Windows.Forms.Panel
    Friend WithEvents lblVentasTitle As System.Windows.Forms.Label
    Friend WithEvents lblVentasVal As System.Windows.Forms.Label
    Friend WithEvents lblVentasSub As System.Windows.Forms.Label
    Friend WithEvents pnlPromedio As System.Windows.Forms.Panel
    Friend WithEvents lblPromedioTitle As System.Windows.Forms.Label
    Friend WithEvents lblPromedioVal As System.Windows.Forms.Label
    Friend WithEvents lblPromedioSub As System.Windows.Forms.Label
    Friend WithEvents pnlArticulos As System.Windows.Forms.Panel
    Friend WithEvents lblArticulosTitle As System.Windows.Forms.Label
    Friend WithEvents lblArticulosVal As System.Windows.Forms.Label
    Friend WithEvents lblArticulosSub As System.Windows.Forms.Label
    Friend WithEvents gbTabla As System.Windows.Forms.GroupBox
    Friend WithEvents dgvVentas As System.Windows.Forms.DataGridView
    Friend WithEvents btnTicket As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sbInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnRegresar As Button
End Class


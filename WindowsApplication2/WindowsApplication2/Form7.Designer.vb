Partial Class Form7
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then components.Dispose()
        MyBase.Dispose(disposing)

    End Sub
    Private Sub InitializeComponent()
        Me.lblFechaTxt = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.btnVer = New System.Windows.Forms.Button()
        Me.btnHoy = New System.Windows.Forms.Button()
        Me.gbResumen = New System.Windows.Forms.GroupBox()
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
        Me.gbVentas = New System.Windows.Forms.GroupBox()
        Me.dgvVentas = New System.Windows.Forms.DataGridView()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.gbTop = New System.Windows.Forms.GroupBox()
        Me.dgvTop = New System.Windows.Forms.DataGridView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sbInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnRegresar = New System.Windows.Forms.Button()
        Me.gbResumen.SuspendLayout()
        Me.pnlIngresos.SuspendLayout()
        Me.pnlVentas.SuspendLayout()
        Me.pnlPromedio.SuspendLayout()
        Me.pnlArticulos.SuspendLayout()
        Me.gbVentas.SuspendLayout()
        CType(Me.dgvVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTop.SuspendLayout()
        CType(Me.dgvTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblFechaTxt
        '
        Me.lblFechaTxt.AutoSize = True
        Me.lblFechaTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblFechaTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblFechaTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblFechaTxt.Location = New System.Drawing.Point(13, 17)
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
        Me.dtpFecha.Location = New System.Drawing.Point(69, 12)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(172, 27)
        Me.dtpFecha.TabIndex = 1
        '
        'btnVer
        '
        Me.btnVer.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnVer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnVer.FlatAppearance.BorderSize = 0
        Me.btnVer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.btnVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnVer.ForeColor = System.Drawing.Color.White
        Me.btnVer.Location = New System.Drawing.Point(253, 12)
        Me.btnVer.Name = "btnVer"
        Me.btnVer.Size = New System.Drawing.Size(120, 30)
        Me.btnVer.TabIndex = 2
        Me.btnVer.Text = "Ver corte"
        Me.btnVer.UseVisualStyleBackColor = False
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
        Me.btnHoy.Location = New System.Drawing.Point(384, 12)
        Me.btnHoy.Name = "btnHoy"
        Me.btnHoy.Size = New System.Drawing.Size(80, 30)
        Me.btnHoy.TabIndex = 3
        Me.btnHoy.Text = "Hoy"
        Me.btnHoy.UseVisualStyleBackColor = False
        '
        'gbResumen
        '
        Me.gbResumen.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gbResumen.Controls.Add(Me.pnlIngresos)
        Me.gbResumen.Controls.Add(Me.pnlVentas)
        Me.gbResumen.Controls.Add(Me.pnlPromedio)
        Me.gbResumen.Controls.Add(Me.pnlArticulos)
        Me.gbResumen.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbResumen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbResumen.Location = New System.Drawing.Point(13, 52)
        Me.gbResumen.Name = "gbResumen"
        Me.gbResumen.Padding = New System.Windows.Forms.Padding(4)
        Me.gbResumen.Size = New System.Drawing.Size(973, 111)
        Me.gbResumen.TabIndex = 4
        Me.gbResumen.TabStop = False
        Me.gbResumen.Text = "Resumen ejecutivo"
        '
        'pnlIngresos
        '
        Me.pnlIngresos.BackColor = System.Drawing.Color.White
        Me.pnlIngresos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlIngresos.Controls.Add(Me.lblIngresosTitle)
        Me.pnlIngresos.Controls.Add(Me.lblIngresosVal)
        Me.pnlIngresos.Controls.Add(Me.lblIngresosSub)
        Me.pnlIngresos.Location = New System.Drawing.Point(11, 22)
        Me.pnlIngresos.Name = "pnlIngresos"
        Me.pnlIngresos.Size = New System.Drawing.Size(225, 76)
        Me.pnlIngresos.TabIndex = 0
        '
        'lblIngresosTitle
        '
        Me.lblIngresosTitle.AutoSize = True
        Me.lblIngresosTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblIngresosTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblIngresosTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblIngresosTitle.Location = New System.Drawing.Point(8, 6)
        Me.lblIngresosTitle.Name = "lblIngresosTitle"
        Me.lblIngresosTitle.Size = New System.Drawing.Size(122, 17)
        Me.lblIngresosTitle.TabIndex = 0
        Me.lblIngresosTitle.Text = "INGRESOS TOTALES"
        '
        'lblIngresosVal
        '
        Me.lblIngresosVal.AutoSize = True
        Me.lblIngresosVal.BackColor = System.Drawing.Color.Transparent
        Me.lblIngresosVal.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblIngresosVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblIngresosVal.Location = New System.Drawing.Point(8, 22)
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
        Me.lblIngresosSub.Location = New System.Drawing.Point(8, 54)
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
        Me.pnlVentas.Location = New System.Drawing.Point(246, 22)
        Me.pnlVentas.Name = "pnlVentas"
        Me.pnlVentas.Size = New System.Drawing.Size(225, 76)
        Me.pnlVentas.TabIndex = 1
        '
        'lblVentasTitle
        '
        Me.lblVentasTitle.AutoSize = True
        Me.lblVentasTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblVentasTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblVentasTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblVentasTitle.Location = New System.Drawing.Point(8, 6)
        Me.lblVentasTitle.Name = "lblVentasTitle"
        Me.lblVentasTitle.Size = New System.Drawing.Size(93, 17)
        Me.lblVentasTitle.TabIndex = 0
        Me.lblVentasTitle.Text = "TOTAL VENTAS"
        '
        'lblVentasVal
        '
        Me.lblVentasVal.AutoSize = True
        Me.lblVentasVal.BackColor = System.Drawing.Color.Transparent
        Me.lblVentasVal.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblVentasVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblVentasVal.Location = New System.Drawing.Point(8, 22)
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
        Me.lblVentasSub.Location = New System.Drawing.Point(8, 54)
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
        Me.pnlPromedio.Location = New System.Drawing.Point(481, 22)
        Me.pnlPromedio.Name = "pnlPromedio"
        Me.pnlPromedio.Size = New System.Drawing.Size(225, 76)
        Me.pnlPromedio.TabIndex = 2
        '
        'lblPromedioTitle
        '
        Me.lblPromedioTitle.AutoSize = True
        Me.lblPromedioTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblPromedioTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblPromedioTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblPromedioTitle.Location = New System.Drawing.Point(8, 6)
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
        Me.lblPromedioVal.Location = New System.Drawing.Point(8, 22)
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
        Me.lblPromedioSub.Location = New System.Drawing.Point(8, 54)
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
        Me.pnlArticulos.Location = New System.Drawing.Point(716, 22)
        Me.pnlArticulos.Name = "pnlArticulos"
        Me.pnlArticulos.Size = New System.Drawing.Size(225, 76)
        Me.pnlArticulos.TabIndex = 3
        '
        'lblArticulosTitle
        '
        Me.lblArticulosTitle.AutoSize = True
        Me.lblArticulosTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblArticulosTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblArticulosTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblArticulosTitle.Location = New System.Drawing.Point(8, 6)
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
        Me.lblArticulosVal.Location = New System.Drawing.Point(8, 22)
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
        Me.lblArticulosSub.Location = New System.Drawing.Point(8, 54)
        Me.lblArticulosSub.Name = "lblArticulosSub"
        Me.lblArticulosSub.Size = New System.Drawing.Size(61, 17)
        Me.lblArticulosSub.TabIndex = 2
        Me.lblArticulosSub.Text = "unidades"
        '
        'gbVentas
        '
        Me.gbVentas.BackColor = System.Drawing.Color.White
        Me.gbVentas.Controls.Add(Me.dgvVentas)
        Me.gbVentas.Controls.Add(Me.btnImprimir)
        Me.gbVentas.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbVentas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbVentas.Location = New System.Drawing.Point(13, 175)
        Me.gbVentas.Name = "gbVentas"
        Me.gbVentas.Padding = New System.Windows.Forms.Padding(4)
        Me.gbVentas.Size = New System.Drawing.Size(933, 486)
        Me.gbVentas.TabIndex = 5
        Me.gbVentas.TabStop = False
        Me.gbVentas.Text = "Ventas del dia"
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
        Me.dgvVentas.Size = New System.Drawing.Size(904, 425)
        Me.dgvVentas.TabIndex = 0
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
        Me.btnImprimir.Location = New System.Drawing.Point(11, 453)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(160, 30)
        Me.btnImprimir.TabIndex = 1
        Me.btnImprimir.Text = "Exportar"
        Me.btnImprimir.UseVisualStyleBackColor = False
        '
        'gbTop
        '
        Me.gbTop.BackColor = System.Drawing.Color.White
        Me.gbTop.Controls.Add(Me.dgvTop)
        Me.gbTop.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbTop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbTop.Location = New System.Drawing.Point(960, 175)
        Me.gbTop.Name = "gbTop"
        Me.gbTop.Padding = New System.Windows.Forms.Padding(4)
        Me.gbTop.Size = New System.Drawing.Size(400, 486)
        Me.gbTop.TabIndex = 6
        Me.gbTop.TabStop = False
        Me.gbTop.Text = "Top productos"
        '
        'dgvTop
        '
        Me.dgvTop.AllowUserToAddRows = False
        Me.dgvTop.AllowUserToDeleteRows = False
        Me.dgvTop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTop.Location = New System.Drawing.Point(11, 24)
        Me.dgvTop.MultiSelect = False
        Me.dgvTop.Name = "dgvTop"
        Me.dgvTop.ReadOnly = True
        Me.dgvTop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTop.Size = New System.Drawing.Size(374, 450)
        Me.dgvTop.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbInfo})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 666)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(6, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1387, 24)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 7
        '
        'sbInfo
        '
        Me.sbInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.sbInfo.ForeColor = System.Drawing.Color.White
        Me.sbInfo.Name = "sbInfo"
        Me.sbInfo.Size = New System.Drawing.Size(1369, 19)
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
        Me.btnRegresar.Location = New System.Drawing.Point(1238, 17)
        Me.btnRegresar.Name = "btnRegresar"
        Me.btnRegresar.Size = New System.Drawing.Size(107, 36)
        Me.btnRegresar.TabIndex = 8
        Me.btnRegresar.Text = "Cerrar"
        Me.btnRegresar.UseVisualStyleBackColor = False
        '
        'Form7
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1387, 690)
        Me.Controls.Add(Me.btnRegresar)
        Me.Controls.Add(Me.lblFechaTxt)
        Me.Controls.Add(Me.dtpFecha)
        Me.Controls.Add(Me.btnVer)
        Me.Controls.Add(Me.btnHoy)
        Me.Controls.Add(Me.gbResumen)
        Me.Controls.Add(Me.gbVentas)
        Me.Controls.Add(Me.gbTop)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "Form7"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KUMO | Reporte premium"
        Me.gbResumen.ResumeLayout(False)
        Me.pnlIngresos.ResumeLayout(False)
        Me.pnlIngresos.PerformLayout()
        Me.pnlVentas.ResumeLayout(False)
        Me.pnlVentas.PerformLayout()
        Me.pnlPromedio.ResumeLayout(False)
        Me.pnlPromedio.PerformLayout()
        Me.pnlArticulos.ResumeLayout(False)
        Me.pnlArticulos.PerformLayout()
        Me.gbVentas.ResumeLayout(False)
        CType(Me.dgvVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTop.ResumeLayout(False)
        CType(Me.dgvTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

        ' Runtime premium design snapshot. Keep this block aligned with the executable view.
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.ClientSize = New System.Drawing.Size(1536, 864)
        Me.BackColor = System.Drawing.Color.FromArgb(244, 240, 234)
        Me.Text = "KUMO | Reporte premium"
        Me.btnHoy.SetBounds(392, 18, 84, 40)
        Me.btnHoy.BackColor = System.Drawing.Color.FromArgb(247, 241, 232)
        Me.btnHoy.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.btnHoy.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.btnHoy.Text = "Hoy"
        Me.btnHoy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHoy.FlatAppearance.BorderSize = 1
        Me.btnHoy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 189, 150)
        Me.btnHoy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 235, 224)
        Me.btnHoy.UseVisualStyleBackColor = False
        Me.btnImprimir.SetBounds(14, 540, 176, 34)
        Me.btnImprimir.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.btnImprimir.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.btnImprimir.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.btnImprimir.Text = "Exportar"
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.FlatAppearance.BorderSize = 1
        Me.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 189, 150)
        Me.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 235, 224)
        Me.btnImprimir.UseVisualStyleBackColor = False
        Me.btnRegresar.SetBounds(1400, 18, 118, 40)
        Me.btnRegresar.BackColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(244, 226, 193)
        Me.btnRegresar.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.btnRegresar.Text = "Cerrar"
        Me.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegresar.FlatAppearance.BorderSize = 0
        Me.btnRegresar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 226, 241)
        Me.btnRegresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(57, 64, 73)
        Me.btnRegresar.UseVisualStyleBackColor = False
        Me.btnVer.SetBounds(254, 18, 126, 40)
        Me.btnVer.BackColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.btnVer.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.btnVer.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.btnVer.Text = "Ver corte"
        Me.btnVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVer.FlatAppearance.BorderSize = 0
        Me.btnVer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 226, 241)
        Me.btnVer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(67, 74, 84)
        Me.btnVer.UseVisualStyleBackColor = False
        Me.dgvTop.SetBounds(14, 30, 450, 544)
        Me.dgvTop.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgvTop.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.dgvTop.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.dgvTop.BackgroundColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgvTop.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTop.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTop.EnableHeadersVisualStyles = False
        Me.dgvTop.RowHeadersVisible = False
        Me.dgvTop.ColumnHeadersHeight = 30
        Me.dgvTop.RowTemplate.Height = 32
        Me.dgvTop.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.dgvTop.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.dgvTop.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgvTop.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.dgvVentas.SetBounds(14, 30, 980, 498)
        Me.dgvVentas.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgvVentas.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.dgvVentas.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.dgvVentas.BackgroundColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgvVentas.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvVentas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvVentas.EnableHeadersVisualStyles = False
        Me.dgvVentas.RowHeadersVisible = False
        Me.dgvVentas.ColumnHeadersHeight = 30
        Me.dgvVentas.RowTemplate.Height = 32
        Me.dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.dgvVentas.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgvVentas.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.dtpFecha.SetBounds(66, 20, 176, 23)
        Me.dtpFecha.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.dtpFecha.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
        Me.dtpFecha.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.gbResumen.SetBounds(18, 70, 1500, 150)
        Me.gbResumen.BackColor = System.Drawing.Color.FromArgb(247, 241, 232)
        Me.gbResumen.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.gbResumen.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.gbResumen.Text = "Resumen ejecutivo"
        Me.gbTop.SetBounds(1040, 236, 478, 588)
        Me.gbTop.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.gbTop.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.gbTop.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.gbTop.Text = "Top productos"
        Me.gbVentas.SetBounds(18, 236, 1008, 588)
        Me.gbVentas.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.gbVentas.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.gbVentas.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.gbVentas.Text = "Ventas del dia"
        Me.lblArticulosSub.SetBounds(14, 64, 332, 20)
        Me.lblArticulosSub.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblArticulosSub.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblArticulosSub.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Regular)
        Me.lblArticulosSub.Text = "piezas desplazadas"
        Me.lblArticulosSub.AutoSize = False
        Me.lblArticulosSub.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblArticulosTitle.SetBounds(14, 12, 332, 26)
        Me.lblArticulosTitle.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblArticulosTitle.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblArticulosTitle.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblArticulosTitle.Text = "ARTICULOS VENDIDOS"
        Me.lblArticulosTitle.AutoSize = False
        Me.lblArticulosTitle.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblArticulosVal.SetBounds(14, 40, 332, 36)
        Me.lblArticulosVal.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblArticulosVal.ForeColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.lblArticulosVal.Font = New System.Drawing.Font("Segoe UI", 16!, System.Drawing.FontStyle.Bold)
        Me.lblArticulosVal.Text = "0"
        Me.lblArticulosVal.AutoSize = False
        Me.lblArticulosVal.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblFechaTxt.SetBounds(18, 24, 38, 21)
        Me.lblFechaTxt.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblFechaTxt.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblFechaTxt.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblFechaTxt.Text = "Fecha"
        Me.lblFechaTxt.AutoSize = True
        Me.lblFechaTxt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblIngresosSub.SetBounds(14, 64, 332, 20)
        Me.lblIngresosSub.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblIngresosSub.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblIngresosSub.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Regular)
        Me.lblIngresosSub.Text = "ventas cobradas"
        Me.lblIngresosSub.AutoSize = False
        Me.lblIngresosSub.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblIngresosTitle.SetBounds(14, 12, 332, 26)
        Me.lblIngresosTitle.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblIngresosTitle.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblIngresosTitle.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblIngresosTitle.Text = "INGRESOS DEL DIA"
        Me.lblIngresosTitle.AutoSize = False
        Me.lblIngresosTitle.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblIngresosVal.SetBounds(14, 40, 332, 36)
        Me.lblIngresosVal.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblIngresosVal.ForeColor = System.Drawing.Color.FromArgb(74, 133, 95)
        Me.lblIngresosVal.Font = New System.Drawing.Font("Segoe UI", 16!, System.Drawing.FontStyle.Bold)
        Me.lblIngresosVal.Text = "$0.00"
        Me.lblIngresosVal.AutoSize = False
        Me.lblIngresosVal.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPromedioSub.SetBounds(14, 64, 332, 20)
        Me.lblPromedioSub.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblPromedioSub.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblPromedioSub.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Regular)
        Me.lblPromedioSub.Text = "importe medio"
        Me.lblPromedioSub.AutoSize = False
        Me.lblPromedioSub.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPromedioTitle.SetBounds(14, 12, 332, 26)
        Me.lblPromedioTitle.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblPromedioTitle.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblPromedioTitle.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblPromedioTitle.Text = "TICKET PROMEDIO"
        Me.lblPromedioTitle.AutoSize = False
        Me.lblPromedioTitle.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPromedioVal.SetBounds(14, 40, 332, 36)
        Me.lblPromedioVal.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblPromedioVal.ForeColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.lblPromedioVal.Font = New System.Drawing.Font("Segoe UI", 16!, System.Drawing.FontStyle.Bold)
        Me.lblPromedioVal.Text = "$0.00"
        Me.lblPromedioVal.AutoSize = False
        Me.lblPromedioVal.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblVentasSub.SetBounds(14, 64, 332, 20)
        Me.lblVentasSub.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblVentasSub.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblVentasSub.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Regular)
        Me.lblVentasSub.Text = "tickets emitidos"
        Me.lblVentasSub.AutoSize = False
        Me.lblVentasSub.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblVentasTitle.SetBounds(14, 12, 332, 26)
        Me.lblVentasTitle.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblVentasTitle.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblVentasTitle.Font = New System.Drawing.Font("Segoe UI", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblVentasTitle.Text = "VENTAS REGISTRADAS"
        Me.lblVentasTitle.AutoSize = False
        Me.lblVentasTitle.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblVentasVal.SetBounds(14, 40, 332, 36)
        Me.lblVentasVal.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblVentasVal.ForeColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.lblVentasVal.Font = New System.Drawing.Font("Segoe UI", 16!, System.Drawing.FontStyle.Bold)
        Me.lblVentasVal.Text = "0"
        Me.lblVentasVal.AutoSize = False
        Me.lblVentasVal.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.pnlArticulos.SetBounds(1128, 42, 360, 96)
        Me.pnlArticulos.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.pnlArticulos.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.pnlArticulos.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.pnlIngresos.SetBounds(12, 42, 360, 96)
        Me.pnlIngresos.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.pnlIngresos.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.pnlIngresos.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.pnlPromedio.SetBounds(756, 42, 360, 96)
        Me.pnlPromedio.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.pnlPromedio.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.pnlPromedio.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.pnlVentas.SetBounds(384, 42, 360, 96)
        Me.pnlVentas.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.pnlVentas.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.pnlVentas.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.StatusStrip1.SetBounds(0, 842, 1536, 22)
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.StatusStrip1.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.StatusStrip1.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)

    End Sub
    Friend WithEvents lblFechaTxt As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnVer As System.Windows.Forms.Button
    Friend WithEvents btnHoy As System.Windows.Forms.Button
    Friend WithEvents gbResumen As System.Windows.Forms.GroupBox
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
    Friend WithEvents gbVentas As System.Windows.Forms.GroupBox
    Friend WithEvents dgvVentas As System.Windows.Forms.DataGridView
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents gbTop As System.Windows.Forms.GroupBox
    Friend WithEvents dgvTop As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sbInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnRegresar As Button
End Class





Partial Class Form2
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuInventario = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHistorial = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPedidos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReporte = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCancelarVenta = New System.Windows.Forms.ToolStripMenuItem()
        Me.gbProductos = New System.Windows.Forms.GroupBox()
        Me.picMarca = New System.Windows.Forms.PictureBox()
        Me.lblBuscar = New System.Windows.Forms.Label()
        Me.txtBuscar = New System.Windows.Forms.TextBox()
        Me.lblCategoria = New System.Windows.Forms.Label()
        Me.cbCategoria = New System.Windows.Forms.ComboBox()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.dgvProductos = New System.Windows.Forms.DataGridView()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.gbCarrito = New System.Windows.Forms.GroupBox()
        Me.btnSalida = New System.Windows.Forms.Button()
        Me.lblNumVenta = New System.Windows.Forms.Label()
        Me.dgvCarrito = New System.Windows.Forms.DataGridView()
        Me.lblCantidadTxt = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.lblSubtotalTxt = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.lblDescPctTxt = New System.Windows.Forms.Label()
        Me.txtDescPct = New System.Windows.Forms.TextBox()
        Me.lblDescValTxt = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.lblLinea = New System.Windows.Forms.Label()
        Me.lblTotalTxt = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.btnCobrar = New System.Windows.Forms.Button()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sbInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sbFecha = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1.SuspendLayout()
        Me.gbProductos.SuspendLayout()
        CType(Me.picMarca, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCarrito.SuspendLayout()
        CType(Me.dgvCarrito, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuInventario, Me.mnuHistorial, Me.mnuPedidos, Me.mnuReporte, Me.mnuCancelarVenta})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.MenuStrip1.Size = New System.Drawing.Size(1413, 24)
        Me.MenuStrip1.TabIndex = 3
        '
        'mnuInventario
        '
        Me.mnuInventario.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.mnuInventario.ForeColor = System.Drawing.Color.White
        Me.mnuInventario.Name = "mnuInventario"
        Me.mnuInventario.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.mnuInventario.Size = New System.Drawing.Size(103, 24)
        Me.mnuInventario.Text = "Inventario"
        '
        'mnuHistorial
        '
        Me.mnuHistorial.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.mnuHistorial.ForeColor = System.Drawing.Color.White
        Me.mnuHistorial.Name = "mnuHistorial"
        Me.mnuHistorial.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.mnuHistorial.Size = New System.Drawing.Size(93, 24)
        Me.mnuHistorial.Text = "Historial"
        '
        'mnuPedidos
        '
        Me.mnuPedidos.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.mnuPedidos.ForeColor = System.Drawing.Color.White
        Me.mnuPedidos.Name = "mnuPedidos"
        Me.mnuPedidos.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.mnuPedidos.Size = New System.Drawing.Size(89, 24)
        Me.mnuPedidos.Text = "Pedidos"
        '
        'mnuReporte
        '
        Me.mnuReporte.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.mnuReporte.ForeColor = System.Drawing.Color.White
        Me.mnuReporte.Name = "mnuReporte"
        Me.mnuReporte.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.mnuReporte.Size = New System.Drawing.Size(90, 24)
        Me.mnuReporte.Text = "Reporte"
        '
        'mnuCancelarVenta
        '
        Me.mnuCancelarVenta.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.mnuCancelarVenta.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.mnuCancelarVenta.ForeColor = System.Drawing.Color.White
        Me.mnuCancelarVenta.Name = "mnuCancelarVenta"
        Me.mnuCancelarVenta.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.mnuCancelarVenta.Size = New System.Drawing.Size(134, 24)
        Me.mnuCancelarVenta.Text = "Cancelar venta"
        '
        'gbProductos
        '
        Me.gbProductos.BackColor = System.Drawing.Color.White
        Me.gbProductos.Controls.Add(Me.picMarca)
        Me.gbProductos.Controls.Add(Me.lblBuscar)
        Me.gbProductos.Controls.Add(Me.txtBuscar)
        Me.gbProductos.Controls.Add(Me.lblCategoria)
        Me.gbProductos.Controls.Add(Me.cbCategoria)
        Me.gbProductos.Controls.Add(Me.btnBuscar)
        Me.gbProductos.Controls.Add(Me.dgvProductos)
        Me.gbProductos.Controls.Add(Me.btnAgregar)
        Me.gbProductos.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbProductos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbProductos.Location = New System.Drawing.Point(13, 40)
        Me.gbProductos.Name = "gbProductos"
        Me.gbProductos.Padding = New System.Windows.Forms.Padding(4)
        Me.gbProductos.Size = New System.Drawing.Size(907, 640)
        Me.gbProductos.TabIndex = 0
        Me.gbProductos.TabStop = False
        '
        'picMarca
        '
        Me.picMarca.Location = New System.Drawing.Point(692, 21)
        Me.picMarca.Name = "picMarca"
        Me.picMarca.Size = New System.Drawing.Size(194, 53)
        Me.picMarca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picMarca.TabIndex = 7
        Me.picMarca.TabStop = False
        '
        'lblBuscar
        '
        Me.lblBuscar.AutoSize = True
        Me.lblBuscar.BackColor = System.Drawing.Color.Transparent
        Me.lblBuscar.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblBuscar.Location = New System.Drawing.Point(13, 27)
        Me.lblBuscar.Name = "lblBuscar"
        Me.lblBuscar.Size = New System.Drawing.Size(121, 20)
        Me.lblBuscar.TabIndex = 0
        Me.lblBuscar.Text = "Busqueda"
        '
        'txtBuscar
        '
        Me.txtBuscar.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuscar.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtBuscar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtBuscar.Location = New System.Drawing.Point(13, 46)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(319, 29)
        Me.txtBuscar.TabIndex = 1
        '
        'lblCategoria
        '
        Me.lblCategoria.AutoSize = True
        Me.lblCategoria.BackColor = System.Drawing.Color.Transparent
        Me.lblCategoria.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblCategoria.Location = New System.Drawing.Point(349, 27)
        Me.lblCategoria.Name = "lblCategoria"
        Me.lblCategoria.Size = New System.Drawing.Size(74, 20)
        Me.lblCategoria.TabIndex = 2
        Me.lblCategoria.Text = "Categoria"
        '
        'cbCategoria
        '
        Me.cbCategoria.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCategoria.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cbCategoria.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.cbCategoria.Items.AddRange(New Object() {"(Todas)", "Amigurumis", "Accesorios", "Decoracion", "Hilos"})
        Me.cbCategoria.Location = New System.Drawing.Point(349, 46)
        Me.cbCategoria.Name = "cbCategoria"
        Me.cbCategoria.Size = New System.Drawing.Size(172, 28)
        Me.cbCategoria.TabIndex = 3
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuscar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnBuscar.Location = New System.Drawing.Point(539, 43)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(100, 30)
        Me.btnBuscar.TabIndex = 4
        Me.btnBuscar.Text = "Filtrar"
        Me.btnBuscar.UseVisualStyleBackColor = False
        Me.btnBuscar.Visible = False
        '
        'dgvProductos
        '
        Me.dgvProductos.AllowUserToAddRows = False
        Me.dgvProductos.AllowUserToDeleteRows = False
        Me.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvProductos.Location = New System.Drawing.Point(13, 84)
        Me.dgvProductos.MultiSelect = False
        Me.dgvProductos.Name = "dgvProductos"
        Me.dgvProductos.ReadOnly = True
        Me.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProductos.Size = New System.Drawing.Size(873, 500)
        Me.dgvProductos.TabIndex = 5
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAgregar.FlatAppearance.BorderSize = 0
        Me.btnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgregar.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnAgregar.ForeColor = System.Drawing.Color.White
        Me.btnAgregar.Location = New System.Drawing.Point(693, 596)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(193, 34)
        Me.btnAgregar.TabIndex = 6
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'gbCarrito
        '
        Me.gbCarrito.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.gbCarrito.Controls.Add(Me.btnSalida)
        Me.gbCarrito.Controls.Add(Me.lblNumVenta)
        Me.gbCarrito.Controls.Add(Me.dgvCarrito)
        Me.gbCarrito.Controls.Add(Me.lblCantidadTxt)
        Me.gbCarrito.Controls.Add(Me.txtCantidad)
        Me.gbCarrito.Controls.Add(Me.btnQuitar)
        Me.gbCarrito.Controls.Add(Me.lblSubtotalTxt)
        Me.gbCarrito.Controls.Add(Me.lblSubtotal)
        Me.gbCarrito.Controls.Add(Me.lblDescPctTxt)
        Me.gbCarrito.Controls.Add(Me.txtDescPct)
        Me.gbCarrito.Controls.Add(Me.lblDescValTxt)
        Me.gbCarrito.Controls.Add(Me.lblDescuento)
        Me.gbCarrito.Controls.Add(Me.lblLinea)
        Me.gbCarrito.Controls.Add(Me.lblTotalTxt)
        Me.gbCarrito.Controls.Add(Me.lblTotal)
        Me.gbCarrito.Controls.Add(Me.btnCobrar)
        Me.gbCarrito.Controls.Add(Me.btnLimpiar)
        Me.gbCarrito.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbCarrito.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbCarrito.Location = New System.Drawing.Point(933, 40)
        Me.gbCarrito.Name = "gbCarrito"
        Me.gbCarrito.Padding = New System.Windows.Forms.Padding(4)
        Me.gbCarrito.Size = New System.Drawing.Size(453, 640)
        Me.gbCarrito.TabIndex = 1
        Me.gbCarrito.TabStop = False
        '
        'btnSalida
        '
        Me.btnSalida.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnSalida.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalida.FlatAppearance.BorderSize = 0
        Me.btnSalida.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.btnSalida.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalida.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnSalida.ForeColor = System.Drawing.Color.White
        Me.btnSalida.Location = New System.Drawing.Point(370, 602)
        Me.btnSalida.Name = "btnSalida"
        Me.btnSalida.Size = New System.Drawing.Size(80, 34)
        Me.btnSalida.TabIndex = 7
        Me.btnSalida.Text = "Cerrar"
        Me.btnSalida.UseVisualStyleBackColor = False
        '
        'lblNumVenta
        '
        Me.lblNumVenta.AutoSize = True
        Me.lblNumVenta.BackColor = System.Drawing.Color.Transparent
        Me.lblNumVenta.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblNumVenta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblNumVenta.Location = New System.Drawing.Point(13, 27)
        Me.lblNumVenta.Name = "lblNumVenta"
        Me.lblNumVenta.Size = New System.Drawing.Size(122, 23)
        Me.lblNumVenta.TabIndex = 0
        Me.lblNumVenta.Text = "Ticket #V-001"
        '
        'dgvCarrito
        '
        Me.dgvCarrito.AllowUserToAddRows = False
        Me.dgvCarrito.AllowUserToDeleteRows = False
        Me.dgvCarrito.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCarrito.Location = New System.Drawing.Point(13, 52)
        Me.dgvCarrito.MultiSelect = False
        Me.dgvCarrito.Name = "dgvCarrito"
        Me.dgvCarrito.ReadOnly = True
        Me.dgvCarrito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCarrito.Size = New System.Drawing.Size(420, 240)
        Me.dgvCarrito.TabIndex = 1
        '
        'lblCantidadTxt
        '
        Me.lblCantidadTxt.AutoSize = True
        Me.lblCantidadTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblCantidadTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblCantidadTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblCantidadTxt.Location = New System.Drawing.Point(13, 305)
        Me.lblCantidadTxt.Name = "lblCantidadTxt"
        Me.lblCantidadTxt.Size = New System.Drawing.Size(50, 20)
        Me.lblCantidadTxt.TabIndex = 2
        Me.lblCantidadTxt.Text = "Piezas"
        '
        'txtCantidad
        '
        Me.txtCantidad.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidad.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtCantidad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtCantidad.Location = New System.Drawing.Point(104, 302)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(60, 29)
        Me.txtCantidad.TabIndex = 3
        Me.txtCantidad.Text = "1"
        '
        'btnQuitar
        '
        Me.btnQuitar.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnQuitar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnQuitar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnQuitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuitar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnQuitar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnQuitar.Location = New System.Drawing.Point(179, 299)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(87, 30)
        Me.btnQuitar.TabIndex = 4
        Me.btnQuitar.Text = "Quitar"
        Me.btnQuitar.UseVisualStyleBackColor = False
        '
        'lblSubtotalTxt
        '
        Me.lblSubtotalTxt.AutoSize = True
        Me.lblSubtotalTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblSubtotalTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblSubtotalTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblSubtotalTxt.Location = New System.Drawing.Point(13, 348)
        Me.lblSubtotalTxt.Name = "lblSubtotalTxt"
        Me.lblSubtotalTxt.Size = New System.Drawing.Size(65, 20)
        Me.lblSubtotalTxt.TabIndex = 5
        Me.lblSubtotalTxt.Text = "Subtotal"
        '
        'lblSubtotal
        '
        Me.lblSubtotal.AutoSize = True
        Me.lblSubtotal.BackColor = System.Drawing.Color.Transparent
        Me.lblSubtotal.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.lblSubtotal.Location = New System.Drawing.Point(280, 348)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(50, 21)
        Me.lblSubtotal.TabIndex = 6
        Me.lblSubtotal.Text = "$0.00"
        '
        'lblDescPctTxt
        '
        Me.lblDescPctTxt.AutoSize = True
        Me.lblDescPctTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblDescPctTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblDescPctTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblDescPctTxt.Location = New System.Drawing.Point(13, 380)
        Me.lblDescPctTxt.Name = "lblDescPctTxt"
        Me.lblDescPctTxt.Size = New System.Drawing.Size(95, 20)
        Me.lblDescPctTxt.TabIndex = 7
        Me.lblDescPctTxt.Text = "Descuento %"
        '
        'txtDescPct
        '
        Me.txtDescPct.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDescPct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescPct.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtDescPct.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtDescPct.Location = New System.Drawing.Point(133, 376)
        Me.txtDescPct.Name = "txtDescPct"
        Me.txtDescPct.Size = New System.Drawing.Size(52, 29)
        Me.txtDescPct.TabIndex = 8
        Me.txtDescPct.Text = "0"
        '
        'lblDescValTxt
        '
        Me.lblDescValTxt.AutoSize = True
        Me.lblDescValTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblDescValTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblDescValTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblDescValTxt.Location = New System.Drawing.Point(13, 412)
        Me.lblDescValTxt.Name = "lblDescValTxt"
        Me.lblDescValTxt.Size = New System.Drawing.Size(141, 20)
        Me.lblDescValTxt.TabIndex = 9
        Me.lblDescValTxt.Text = "Descuento aplicado"
        '
        'lblDescuento
        '
        Me.lblDescuento.AutoSize = True
        Me.lblDescuento.BackColor = System.Drawing.Color.Transparent
        Me.lblDescuento.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblDescuento.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.lblDescuento.Location = New System.Drawing.Point(280, 412)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(55, 21)
        Me.lblDescuento.TabIndex = 10
        Me.lblDescuento.Text = "-$0.00"
        '
        'lblLinea
        '
        Me.lblLinea.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblLinea.Location = New System.Drawing.Point(13, 440)
        Me.lblLinea.Name = "lblLinea"
        Me.lblLinea.Size = New System.Drawing.Size(420, 1)
        Me.lblLinea.TabIndex = 11
        '
        'lblTotalTxt
        '
        Me.lblTotalTxt.AutoSize = True
        Me.lblTotalTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalTxt.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblTotalTxt.Location = New System.Drawing.Point(13, 450)
        Me.lblTotalTxt.Name = "lblTotalTxt"
        Me.lblTotalTxt.Size = New System.Drawing.Size(174, 25)
        Me.lblTotalTxt.TabIndex = 12
        Me.lblTotalTxt.Text = "TOTAL DEL TICKET"
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.BackColor = System.Drawing.Color.Transparent
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(247, 448)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(71, 30)
        Me.lblTotal.TabIndex = 13
        Me.lblTotal.Text = "$0.00"
        '
        'btnCobrar
        '
        Me.btnCobrar.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnCobrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCobrar.FlatAppearance.BorderSize = 0
        Me.btnCobrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.btnCobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCobrar.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCobrar.ForeColor = System.Drawing.Color.White
        Me.btnCobrar.Location = New System.Drawing.Point(13, 490)
        Me.btnCobrar.Name = "btnCobrar"
        Me.btnCobrar.Size = New System.Drawing.Size(420, 46)
        Me.btnCobrar.TabIndex = 14
        Me.btnCobrar.Text = "Cobrar"
        Me.btnCobrar.UseVisualStyleBackColor = False
        '
        'btnLimpiar
        '
        Me.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLimpiar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnLimpiar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnLimpiar.Location = New System.Drawing.Point(13, 546)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(420, 32)
        Me.btnLimpiar.TabIndex = 15
        Me.btnLimpiar.Text = "Cancelar"
        Me.btnLimpiar.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbInfo, Me.sbFecha})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 703)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(6, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1413, 24)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 2
        '
        'sbInfo
        '
        Me.sbInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.sbInfo.ForeColor = System.Drawing.Color.White
        Me.sbInfo.Name = "sbInfo"
        Me.sbInfo.Size = New System.Drawing.Size(1395, 19)
        Me.sbInfo.Spring = True
        Me.sbInfo.Text = "   Listo"
        Me.sbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'sbFecha
        '
        Me.sbFecha.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.sbFecha.ForeColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.sbFecha.Name = "sbFecha"
        Me.sbFecha.Size = New System.Drawing.Size(0, 19)
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1413, 727)
        Me.Controls.Add(Me.gbProductos)
        Me.Controls.Add(Me.gbCarrito)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KUMO | Caja premium"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.gbProductos.ResumeLayout(False)
        Me.gbProductos.PerformLayout()
        CType(Me.picMarca, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCarrito.ResumeLayout(False)
        Me.gbCarrito.PerformLayout()
        CType(Me.dgvCarrito, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuCancelarVenta As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInventario As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHistorial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPedidos As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReporte As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbProductos As System.Windows.Forms.GroupBox
    Friend WithEvents picMarca As System.Windows.Forms.PictureBox
    Friend WithEvents lblBuscar As System.Windows.Forms.Label
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents lblCategoria As System.Windows.Forms.Label
    Friend WithEvents cbCategoria As System.Windows.Forms.ComboBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents dgvProductos As System.Windows.Forms.DataGridView
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents gbCarrito As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumVenta As System.Windows.Forms.Label
    Friend WithEvents dgvCarrito As System.Windows.Forms.DataGridView
    Friend WithEvents lblCantidadTxt As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents btnQuitar As System.Windows.Forms.Button
    Friend WithEvents lblSubtotalTxt As System.Windows.Forms.Label
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents lblDescPctTxt As System.Windows.Forms.Label
    Friend WithEvents txtDescPct As System.Windows.Forms.TextBox
    Friend WithEvents lblDescValTxt As System.Windows.Forms.Label
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
    Friend WithEvents lblLinea As System.Windows.Forms.Label
    Friend WithEvents lblTotalTxt As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents btnCobrar As System.Windows.Forms.Button
    Friend WithEvents btnLimpiar As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sbInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sbFecha As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnSalida As Button
End Class



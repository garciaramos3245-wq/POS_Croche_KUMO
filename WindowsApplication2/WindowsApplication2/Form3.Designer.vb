Partial Class Form3
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then components.Dispose()
        MyBase.Dispose(disposing)

    End Sub
    Private Sub InitializeComponent()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.gbFiltro = New System.Windows.Forms.GroupBox()
        Me.lblBuscar = New System.Windows.Forms.Label()
        Me.txtBuscar = New System.Windows.Forms.TextBox()
        Me.lblCatTxt = New System.Windows.Forms.Label()
        Me.cbCategoria = New System.Windows.Forms.ComboBox()
        Me.btnFiltrar = New System.Windows.Forms.Button()
        Me.gbTabla = New System.Windows.Forms.GroupBox()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.gbDetalle = New System.Windows.Forms.GroupBox()
        Me.lblNombreTxt = New System.Windows.Forms.Label()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.lblPrecioTxt = New System.Windows.Forms.Label()
        Me.txtPrecio = New System.Windows.Forms.TextBox()
        Me.lblStockTxt = New System.Windows.Forms.Label()
        Me.txtStock = New System.Windows.Forms.TextBox()
        Me.lblCatDetTxt = New System.Windows.Forms.Label()
        Me.cbCatDetalle = New System.Windows.Forms.ComboBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sbInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnRegresar = New System.Windows.Forms.Button()
        Me.gbFiltro.SuspendLayout()
        Me.gbTabla.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDetalle.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnNuevo
        '
        Me.btnNuevo.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNuevo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnNuevo.Location = New System.Drawing.Point(13, 12)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(100, 30)
        Me.btnNuevo.TabIndex = 0
        Me.btnNuevo.Text = "+ Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = False
        '
        'btnGuardar
        '
        Me.btnGuardar.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGuardar.FlatAppearance.BorderSize = 0
        Me.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuardar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnGuardar.ForeColor = System.Drawing.Color.White
        Me.btnGuardar.Location = New System.Drawing.Point(124, 12)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(100, 30)
        Me.btnGuardar.TabIndex = 1
        Me.btnGuardar.Text = "Guardar producto"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar.FlatAppearance.BorderSize = 0
        Me.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnEliminar.ForeColor = System.Drawing.Color.White
        Me.btnEliminar.Location = New System.Drawing.Point(235, 12)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(100, 30)
        Me.btnEliminar.TabIndex = 2
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnActualizar
        '
        Me.btnActualizar.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnActualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnActualizar.Location = New System.Drawing.Point(345, 12)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(110, 30)
        Me.btnActualizar.TabIndex = 3
        Me.btnActualizar.Text = "Recargar"
        Me.btnActualizar.UseVisualStyleBackColor = False
        '
        'gbFiltro
        '
        Me.gbFiltro.BackColor = System.Drawing.Color.White
        Me.gbFiltro.Controls.Add(Me.lblBuscar)
        Me.gbFiltro.Controls.Add(Me.txtBuscar)
        Me.gbFiltro.Controls.Add(Me.lblCatTxt)
        Me.gbFiltro.Controls.Add(Me.cbCategoria)
        Me.gbFiltro.Controls.Add(Me.btnFiltrar)
        Me.gbFiltro.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbFiltro.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbFiltro.Location = New System.Drawing.Point(13, 52)
        Me.gbFiltro.Name = "gbFiltro"
        Me.gbFiltro.Padding = New System.Windows.Forms.Padding(4)
        Me.gbFiltro.Size = New System.Drawing.Size(933, 68)
        Me.gbFiltro.TabIndex = 4
        Me.gbFiltro.TabStop = False
        Me.gbFiltro.Text = "Filtro comercial"
        '
        'lblBuscar
        '
        Me.lblBuscar.AutoSize = True
        Me.lblBuscar.BackColor = System.Drawing.Color.Transparent
        Me.lblBuscar.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblBuscar.Location = New System.Drawing.Point(11, 27)
        Me.lblBuscar.Name = "lblBuscar"
        Me.lblBuscar.Size = New System.Drawing.Size(64, 20)
        Me.lblBuscar.TabIndex = 0
        Me.lblBuscar.Text = "Busqueda rapida"
        '
        'txtBuscar
        '
        Me.txtBuscar.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuscar.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtBuscar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtBuscar.Location = New System.Drawing.Point(80, 22)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(239, 29)
        Me.txtBuscar.TabIndex = 1
        '
        'lblCatTxt
        '
        Me.lblCatTxt.AutoSize = True
        Me.lblCatTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblCatTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblCatTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblCatTxt.Location = New System.Drawing.Point(335, 27)
        Me.lblCatTxt.Name = "lblCatTxt"
        Me.lblCatTxt.Size = New System.Drawing.Size(87, 20)
        Me.lblCatTxt.TabIndex = 2
        Me.lblCatTxt.Text = "Coleccion"
        '
        'cbCategoria
        '
        Me.cbCategoria.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCategoria.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cbCategoria.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.cbCategoria.Items.AddRange(New Object() {"(Todas)", "Amigurumis", "Accesorios", "Decoracion", "Hilos"})
        Me.cbCategoria.Location = New System.Drawing.Point(420, 22)
        Me.cbCategoria.Name = "cbCategoria"
        Me.cbCategoria.Size = New System.Drawing.Size(172, 28)
        Me.cbCategoria.TabIndex = 3
        '
        'btnFiltrar
        '
        Me.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFiltrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnFiltrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFiltrar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnFiltrar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnFiltrar.Location = New System.Drawing.Point(608, 20)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Size = New System.Drawing.Size(90, 30)
        Me.btnFiltrar.TabIndex = 4
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.UseVisualStyleBackColor = False
        '
        'gbTabla
        '
        Me.gbTabla.BackColor = System.Drawing.Color.White
        Me.gbTabla.Controls.Add(Me.dgv)
        Me.gbTabla.Controls.Add(Me.lblInfo)
        Me.gbTabla.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbTabla.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbTabla.Location = New System.Drawing.Point(13, 129)
        Me.gbTabla.Name = "gbTabla"
        Me.gbTabla.Padding = New System.Windows.Forms.Padding(4)
        Me.gbTabla.Size = New System.Drawing.Size(933, 480)
        Me.gbTabla.TabIndex = 5
        Me.gbTabla.TabStop = False
        Me.gbTabla.Text = "Catalogo en piso"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv.Location = New System.Drawing.Point(11, 22)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(907, 425)
        Me.dgv.TabIndex = 0
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = True
        Me.lblInfo.BackColor = System.Drawing.Color.Transparent
        Me.lblInfo.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblInfo.Location = New System.Drawing.Point(11, 453)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(0, 20)
        Me.lblInfo.TabIndex = 1
        '
        'gbDetalle
        '
        Me.gbDetalle.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.gbDetalle.Controls.Add(Me.lblNombreTxt)
        Me.gbDetalle.Controls.Add(Me.txtNombre)
        Me.gbDetalle.Controls.Add(Me.lblPrecioTxt)
        Me.gbDetalle.Controls.Add(Me.txtPrecio)
        Me.gbDetalle.Controls.Add(Me.lblStockTxt)
        Me.gbDetalle.Controls.Add(Me.txtStock)
        Me.gbDetalle.Controls.Add(Me.lblCatDetTxt)
        Me.gbDetalle.Controls.Add(Me.cbCatDetalle)
        Me.gbDetalle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbDetalle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbDetalle.Location = New System.Drawing.Point(960, 52)
        Me.gbDetalle.Name = "gbDetalle"
        Me.gbDetalle.Padding = New System.Windows.Forms.Padding(12)
        Me.gbDetalle.Size = New System.Drawing.Size(420, 300)
        Me.gbDetalle.TabIndex = 6
        Me.gbDetalle.TabStop = False
        Me.gbDetalle.Text = "Ficha del producto"
        '
        'lblNombreTxt
        '
        Me.lblNombreTxt.AutoSize = True
        Me.lblNombreTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblNombreTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblNombreTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblNombreTxt.Location = New System.Drawing.Point(14, 30)
        Me.lblNombreTxt.Name = "lblNombreTxt"
        Me.lblNombreTxt.Size = New System.Drawing.Size(70, 20)
        Me.lblNombreTxt.TabIndex = 0
        Me.lblNombreTxt.Text = "Nombre comercial"
        '
        'txtNombre
        '
        Me.txtNombre.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNombre.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNombre.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtNombre.Location = New System.Drawing.Point(14, 50)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(385, 30)
        Me.txtNombre.TabIndex = 1
        '
        'lblPrecioTxt
        '
        Me.lblPrecioTxt.AutoSize = True
        Me.lblPrecioTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblPrecioTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblPrecioTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblPrecioTxt.Location = New System.Drawing.Point(14, 90)
        Me.lblPrecioTxt.Name = "lblPrecioTxt"
        Me.lblPrecioTxt.Size = New System.Drawing.Size(80, 20)
        Me.lblPrecioTxt.TabIndex = 2
        Me.lblPrecioTxt.Text = "Precio de venta"
        '
        'txtPrecio
        '
        Me.txtPrecio.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrecio.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPrecio.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtPrecio.Location = New System.Drawing.Point(14, 110)
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(180, 30)
        Me.txtPrecio.TabIndex = 3
        '
        'lblStockTxt
        '
        Me.lblStockTxt.AutoSize = True
        Me.lblStockTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblStockTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblStockTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblStockTxt.Location = New System.Drawing.Point(210, 90)
        Me.lblStockTxt.Name = "lblStockTxt"
        Me.lblStockTxt.Size = New System.Drawing.Size(53, 20)
        Me.lblStockTxt.TabIndex = 4
        Me.lblStockTxt.Text = "Stock disponible"
        '
        'txtStock
        '
        Me.txtStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStock.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtStock.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtStock.Location = New System.Drawing.Point(210, 110)
        Me.txtStock.Name = "txtStock"
        Me.txtStock.Size = New System.Drawing.Size(189, 30)
        Me.txtStock.TabIndex = 5
        '
        'lblCatDetTxt
        '
        Me.lblCatDetTxt.AutoSize = True
        Me.lblCatDetTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblCatDetTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblCatDetTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblCatDetTxt.Location = New System.Drawing.Point(14, 150)
        Me.lblCatDetTxt.Name = "lblCatDetTxt"
        Me.lblCatDetTxt.Size = New System.Drawing.Size(87, 20)
        Me.lblCatDetTxt.TabIndex = 6
        Me.lblCatDetTxt.Text = "Categoria"
        '
        'cbCatDetalle
        '
        Me.cbCatDetalle.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbCatDetalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCatDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCatDetalle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cbCatDetalle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.cbCatDetalle.Items.AddRange(New Object() {"Amigurumis", "Accesorios", "Decoracion", "Hilos"})
        Me.cbCatDetalle.Location = New System.Drawing.Point(14, 170)
        Me.cbCatDetalle.Name = "cbCatDetalle"
        Me.cbCatDetalle.Size = New System.Drawing.Size(385, 28)
        Me.cbCatDetalle.TabIndex = 7
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbInfo})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 629)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(6, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1413, 24)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 7
        '
        'sbInfo
        '
        Me.sbInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.sbInfo.ForeColor = System.Drawing.Color.White
        Me.sbInfo.Name = "sbInfo"
        Me.sbInfo.Size = New System.Drawing.Size(1395, 19)
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
        Me.btnRegresar.Location = New System.Drawing.Point(1273, 566)
        Me.btnRegresar.Name = "btnRegresar"
        Me.btnRegresar.Size = New System.Drawing.Size(107, 36)
        Me.btnRegresar.TabIndex = 8
        Me.btnRegresar.Text = "Cerrar"
        Me.btnRegresar.UseVisualStyleBackColor = False
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1413, 653)
        Me.Controls.Add(Me.btnRegresar)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnActualizar)
        Me.Controls.Add(Me.gbFiltro)
        Me.Controls.Add(Me.gbTabla)
        Me.Controls.Add(Me.gbDetalle)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "Form3"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KUMO | Inventario premium"
        Me.gbFiltro.ResumeLayout(False)
        Me.gbFiltro.PerformLayout()
        Me.gbTabla.ResumeLayout(False)
        Me.gbTabla.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDetalle.ResumeLayout(False)
        Me.gbDetalle.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

        ' Runtime premium design snapshot. Keep this block aligned with the executable view.
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.ClientSize = New System.Drawing.Size(1536, 864)
        Me.BackColor = System.Drawing.Color.FromArgb(245, 247, 250)
        Me.Text = "KUMO | Inventario premium"
        Me.btnActualizar.SetBounds(414, 14, 138, 40)
        Me.btnActualizar.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.btnActualizar.Text = "Recargar"
        Me.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActualizar.FlatAppearance.BorderSize = 1
        Me.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 189, 150)
        Me.btnActualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 235, 224)
        Me.btnActualizar.UseVisualStyleBackColor = False
        Me.btnEliminar.SetBounds(282, 14, 120, 40)
        Me.btnEliminar.BackColor = System.Drawing.Color.FromArgb(154, 73, 64)
        Me.btnEliminar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.btnEliminar.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminar.FlatAppearance.BorderSize = 0
        Me.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 226, 241)
        Me.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(133, 61, 53)
        Me.btnEliminar.UseVisualStyleBackColor = False
        Me.btnFiltrar.SetBounds(892, 54, 96, 36)
        Me.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.btnFiltrar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.btnFiltrar.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFiltrar.FlatAppearance.BorderSize = 1
        Me.btnFiltrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.btnFiltrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(67, 74, 84)
        Me.btnFiltrar.UseVisualStyleBackColor = False
        Me.btnGuardar.SetBounds(150, 14, 120, 40)
        Me.btnGuardar.BackColor = System.Drawing.Color.FromArgb(74, 133, 95)
        Me.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.btnGuardar.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.btnGuardar.Text = "Guardar producto"
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuardar.FlatAppearance.BorderSize = 0
        Me.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 226, 241)
        Me.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(58, 111, 78)
        Me.btnGuardar.UseVisualStyleBackColor = False
        Me.btnNuevo.SetBounds(18, 14, 120, 40)
        Me.btnNuevo.BackColor = System.Drawing.Color.FromArgb(247, 241, 232)
        Me.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.btnNuevo.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.btnNuevo.Text = "+ Nuevo"
        Me.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNuevo.FlatAppearance.BorderSize = 1
        Me.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 189, 150)
        Me.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 235, 224)
        Me.btnNuevo.UseVisualStyleBackColor = False
        Me.btnRegresar.SetBounds(1400, 14, 118, 40)
        Me.btnRegresar.BackColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(244, 226, 193)
        Me.btnRegresar.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.btnRegresar.Text = "Cerrar"
        Me.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegresar.FlatAppearance.BorderSize = 0
        Me.btnRegresar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 226, 241)
        Me.btnRegresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(57, 64, 73)
        Me.btnRegresar.UseVisualStyleBackColor = False
        Me.cbCatDetalle.SetBounds(18, 222, 440, 25)
        Me.cbCatDetalle.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.cbCatDetalle.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.cbCatDetalle.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular)
        Me.cbCatDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCategoria.SetBounds(666, 54, 210, 25)
        Me.cbCategoria.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.cbCategoria.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.cbCategoria.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular)
        Me.cbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.dgv.SetBounds(14, 30, 978, 548)
        Me.dgv.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgv.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.dgv.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.dgv.BackgroundColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgv.EnableHeadersVisualStyles = False
        Me.dgv.RowHeadersVisible = False
        Me.dgv.ColumnHeadersHeight = 30
        Me.dgv.RowTemplate.Height = 32
        Me.dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.dgv.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.dgv.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.gbDetalle.SetBounds(1042, 82, 476, 752)
        Me.gbDetalle.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.gbDetalle.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.gbDetalle.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.gbDetalle.Text = "Ficha del producto"
        Me.gbFiltro.SetBounds(18, 82, 1006, 118)
        Me.gbFiltro.BackColor = System.Drawing.Color.FromArgb(247, 241, 232)
        Me.gbFiltro.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.gbFiltro.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.gbFiltro.Text = "Filtro comercial"
        Me.gbTabla.SetBounds(18, 214, 1006, 620)
        Me.gbTabla.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.gbTabla.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.gbTabla.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.gbTabla.Text = "Catalogo en piso"
        Me.lblBuscar.SetBounds(18, 30, 102, 21)
        Me.lblBuscar.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblBuscar.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblBuscar.Text = "Busqueda rapida"
        Me.lblBuscar.AutoSize = True
        Me.lblBuscar.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCatDetTxt.SetBounds(18, 198, 61, 21)
        Me.lblCatDetTxt.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblCatDetTxt.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblCatDetTxt.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblCatDetTxt.Text = "Categoria"
        Me.lblCatDetTxt.AutoSize = True
        Me.lblCatDetTxt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCatTxt.SetBounds(666, 30, 60, 21)
        Me.lblCatTxt.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblCatTxt.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblCatTxt.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblCatTxt.Text = "Coleccion"
        Me.lblCatTxt.AutoSize = True
        Me.lblCatTxt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblInfo.SetBounds(16, 586, 0, 18)
        Me.lblInfo.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblInfo.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblInfo.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblInfo.Text = ""
        Me.lblInfo.AutoSize = True
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblNombreTxt.SetBounds(18, 42, 112, 21)
        Me.lblNombreTxt.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblNombreTxt.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblNombreTxt.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblNombreTxt.Text = "Nombre comercial"
        Me.lblNombreTxt.AutoSize = True
        Me.lblNombreTxt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPrecioTxt.SetBounds(18, 120, 94, 21)
        Me.lblPrecioTxt.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblPrecioTxt.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblPrecioTxt.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblPrecioTxt.Text = "Precio de venta"
        Me.lblPrecioTxt.AutoSize = True
        Me.lblPrecioTxt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblStockTxt.SetBounds(244, 120, 101, 21)
        Me.lblStockTxt.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblStockTxt.ForeColor = System.Drawing.Color.FromArgb(136, 118, 94)
        Me.lblStockTxt.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblStockTxt.Text = "Stock disponible"
        Me.lblStockTxt.AutoSize = True
        Me.lblStockTxt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.StatusStrip1.SetBounds(0, 842, 1536, 22)
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.StatusStrip1.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.StatusStrip1.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.txtBuscar.SetBounds(18, 54, 632, 25)
        Me.txtBuscar.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.txtBuscar.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.txtBuscar.Font = New System.Drawing.Font("Segoe UI", 10!, System.Drawing.FontStyle.Regular)
        Me.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuscar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtNombre.SetBounds(18, 66, 440, 25)
        Me.txtNombre.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.txtNombre.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.txtNombre.Font = New System.Drawing.Font("Segoe UI", 10!, System.Drawing.FontStyle.Regular)
        Me.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtPrecio.SetBounds(18, 144, 214, 25)
        Me.txtPrecio.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.txtPrecio.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.txtPrecio.Font = New System.Drawing.Font("Segoe UI", 10!, System.Drawing.FontStyle.Regular)
        Me.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtStock.SetBounds(244, 144, 214, 25)
        Me.txtStock.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.txtStock.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.txtStock.Font = New System.Drawing.Font("Segoe UI", 10!, System.Drawing.FontStyle.Regular)
        Me.txtStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Left

    End Sub
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnActualizar As System.Windows.Forms.Button
    Friend WithEvents gbFiltro As System.Windows.Forms.GroupBox
    Friend WithEvents lblBuscar As System.Windows.Forms.Label
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents lblCatTxt As System.Windows.Forms.Label
    Friend WithEvents cbCategoria As System.Windows.Forms.ComboBox
    Friend WithEvents btnFiltrar As System.Windows.Forms.Button
    Friend WithEvents gbTabla As System.Windows.Forms.GroupBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents gbDetalle As System.Windows.Forms.GroupBox
    Friend WithEvents lblNombreTxt As System.Windows.Forms.Label
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents lblPrecioTxt As System.Windows.Forms.Label
    Friend WithEvents txtPrecio As System.Windows.Forms.TextBox
    Friend WithEvents lblStockTxt As System.Windows.Forms.Label
    Friend WithEvents txtStock As System.Windows.Forms.TextBox
    Friend WithEvents lblCatDetTxt As System.Windows.Forms.Label
    Friend WithEvents cbCatDetalle As System.Windows.Forms.ComboBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sbInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnRegresar As Button
End Class





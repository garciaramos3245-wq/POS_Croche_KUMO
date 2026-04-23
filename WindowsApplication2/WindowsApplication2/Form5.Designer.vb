Partial Class Form5
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub
    Private Sub InitializeComponent()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.gbForm = New System.Windows.Forms.GroupBox()
        Me.lblNombreTxt = New System.Windows.Forms.Label()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.lblTelTxt = New System.Windows.Forms.Label()
        Me.txtTel = New System.Windows.Forms.TextBox()
        Me.lblDescTxt = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.lblColTxt = New System.Windows.Forms.Label()
        Me.txtColores = New System.Windows.Forms.TextBox()
        Me.lblMedTxt = New System.Windows.Forms.Label()
        Me.txtMedidas = New System.Windows.Forms.TextBox()
        Me.lblNotasTxt = New System.Windows.Forms.Label()
        Me.txtNotas = New System.Windows.Forms.TextBox()
        Me.lblPrecioTxt = New System.Windows.Forms.Label()
        Me.txtPrecio = New System.Windows.Forms.TextBox()
        Me.lblAnticTxt = New System.Windows.Forms.Label()
        Me.txtAnticipo = New System.Windows.Forms.TextBox()
        Me.lblSaldoTxt = New System.Windows.Forms.Label()
        Me.txtSaldo = New System.Windows.Forms.TextBox()
        Me.lblFechaTxt = New System.Windows.Forms.Label()
        Me.dtpEntrega = New System.Windows.Forms.DateTimePicker()
        Me.lblEstadoTxt = New System.Windows.Forms.Label()
        Me.cbEstado = New System.Windows.Forms.ComboBox()
        Me.gbLista = New System.Windows.Forms.GroupBox()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnCargar = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sbInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnRegresar = New System.Windows.Forms.Button()
        Me.gbForm.SuspendLayout()
        Me.gbLista.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.btnGuardar.Location = New System.Drawing.Point(13, 12)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(110, 30)
        Me.btnGuardar.TabIndex = 0
        Me.btnGuardar.Text = "Guardar pedido"
        Me.btnGuardar.UseVisualStyleBackColor = False
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
        Me.btnNuevo.Location = New System.Drawing.Point(134, 12)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(110, 30)
        Me.btnNuevo.TabIndex = 1
        Me.btnNuevo.Text = "+ Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = False
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
        Me.btnEliminar.Location = New System.Drawing.Point(255, 12)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(110, 30)
        Me.btnEliminar.TabIndex = 2
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'gbForm
        '
        Me.gbForm.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.gbForm.Controls.Add(Me.lblNombreTxt)
        Me.gbForm.Controls.Add(Me.txtNombre)
        Me.gbForm.Controls.Add(Me.lblTelTxt)
        Me.gbForm.Controls.Add(Me.txtTel)
        Me.gbForm.Controls.Add(Me.lblDescTxt)
        Me.gbForm.Controls.Add(Me.txtDesc)
        Me.gbForm.Controls.Add(Me.lblColTxt)
        Me.gbForm.Controls.Add(Me.txtColores)
        Me.gbForm.Controls.Add(Me.lblMedTxt)
        Me.gbForm.Controls.Add(Me.txtMedidas)
        Me.gbForm.Controls.Add(Me.lblNotasTxt)
        Me.gbForm.Controls.Add(Me.txtNotas)
        Me.gbForm.Controls.Add(Me.lblPrecioTxt)
        Me.gbForm.Controls.Add(Me.txtPrecio)
        Me.gbForm.Controls.Add(Me.lblAnticTxt)
        Me.gbForm.Controls.Add(Me.txtAnticipo)
        Me.gbForm.Controls.Add(Me.lblSaldoTxt)
        Me.gbForm.Controls.Add(Me.txtSaldo)
        Me.gbForm.Controls.Add(Me.lblFechaTxt)
        Me.gbForm.Controls.Add(Me.dtpEntrega)
        Me.gbForm.Controls.Add(Me.lblEstadoTxt)
        Me.gbForm.Controls.Add(Me.cbEstado)
        Me.gbForm.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbForm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbForm.Location = New System.Drawing.Point(13, 52)
        Me.gbForm.Name = "gbForm"
        Me.gbForm.Padding = New System.Windows.Forms.Padding(12)
        Me.gbForm.Size = New System.Drawing.Size(747, 610)
        Me.gbForm.TabIndex = 3
        Me.gbForm.TabStop = False
        Me.gbForm.Text = "Pedido especial"
        '
        'lblNombreTxt
        '
        Me.lblNombreTxt.AutoSize = True
        Me.lblNombreTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblNombreTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblNombreTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblNombreTxt.Location = New System.Drawing.Point(14, 30)
        Me.lblNombreTxt.Name = "lblNombreTxt"
        Me.lblNombreTxt.Size = New System.Drawing.Size(159, 20)
        Me.lblNombreTxt.TabIndex = 0
        Me.lblNombreTxt.Text = "Cliente"
        '
        'txtNombre
        '
        Me.txtNombre.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNombre.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNombre.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtNombre.Location = New System.Drawing.Point(14, 50)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(350, 30)
        Me.txtNombre.TabIndex = 1
        '
        'lblTelTxt
        '
        Me.lblTelTxt.AutoSize = True
        Me.lblTelTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblTelTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblTelTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblTelTxt.Location = New System.Drawing.Point(380, 30)
        Me.lblTelTxt.Name = "lblTelTxt"
        Me.lblTelTxt.Size = New System.Drawing.Size(80, 20)
        Me.lblTelTxt.TabIndex = 2
        Me.lblTelTxt.Text = "Telefono"
        '
        'txtTel
        '
        Me.txtTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtTel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtTel.Location = New System.Drawing.Point(380, 50)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(340, 30)
        Me.txtTel.TabIndex = 3
        '
        'lblDescTxt
        '
        Me.lblDescTxt.AutoSize = True
        Me.lblDescTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblDescTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblDescTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblDescTxt.Location = New System.Drawing.Point(14, 90)
        Me.lblDescTxt.Name = "lblDescTxt"
        Me.lblDescTxt.Size = New System.Drawing.Size(211, 20)
        Me.lblDescTxt.TabIndex = 4
        Me.lblDescTxt.Text = "Descripcion"
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtDesc.Location = New System.Drawing.Point(14, 110)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(706, 30)
        Me.txtDesc.TabIndex = 5
        '
        'lblColTxt
        '
        Me.lblColTxt.AutoSize = True
        Me.lblColTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblColTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblColTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblColTxt.Location = New System.Drawing.Point(14, 150)
        Me.lblColTxt.Name = "lblColTxt"
        Me.lblColTxt.Size = New System.Drawing.Size(81, 20)
        Me.lblColTxt.TabIndex = 6
        Me.lblColTxt.Text = "Paleta"
        '
        'txtColores
        '
        Me.txtColores.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtColores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtColores.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtColores.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtColores.Location = New System.Drawing.Point(14, 170)
        Me.txtColores.Name = "txtColores"
        Me.txtColores.Size = New System.Drawing.Size(340, 30)
        Me.txtColores.TabIndex = 7
        '
        'lblMedTxt
        '
        Me.lblMedTxt.AutoSize = True
        Me.lblMedTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblMedTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblMedTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblMedTxt.Location = New System.Drawing.Point(370, 150)
        Me.lblMedTxt.Name = "lblMedTxt"
        Me.lblMedTxt.Size = New System.Drawing.Size(74, 20)
        Me.lblMedTxt.TabIndex = 8
        Me.lblMedTxt.Text = "Medidas"
        '
        'txtMedidas
        '
        Me.txtMedidas.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtMedidas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMedidas.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtMedidas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtMedidas.Location = New System.Drawing.Point(370, 170)
        Me.txtMedidas.Name = "txtMedidas"
        Me.txtMedidas.Size = New System.Drawing.Size(350, 30)
        Me.txtMedidas.TabIndex = 9
        '
        'lblNotasTxt
        '
        Me.lblNotasTxt.AutoSize = True
        Me.lblNotasTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblNotasTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblNotasTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblNotasTxt.Location = New System.Drawing.Point(14, 210)
        Me.lblNotasTxt.Name = "lblNotasTxt"
        Me.lblNotasTxt.Size = New System.Drawing.Size(152, 20)
        Me.lblNotasTxt.TabIndex = 10
        Me.lblNotasTxt.Text = "Notas"
        '
        'txtNotas
        '
        Me.txtNotas.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNotas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotas.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNotas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtNotas.Location = New System.Drawing.Point(14, 230)
        Me.txtNotas.Name = "txtNotas"
        Me.txtNotas.Size = New System.Drawing.Size(706, 30)
        Me.txtNotas.TabIndex = 11
        '
        'lblPrecioTxt
        '
        Me.lblPrecioTxt.AutoSize = True
        Me.lblPrecioTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblPrecioTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblPrecioTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblPrecioTxt.Location = New System.Drawing.Point(14, 275)
        Me.lblPrecioTxt.Name = "lblPrecioTxt"
        Me.lblPrecioTxt.Size = New System.Drawing.Size(125, 20)
        Me.lblPrecioTxt.TabIndex = 12
        Me.lblPrecioTxt.Text = "Precio final"
        '
        'txtPrecio
        '
        Me.txtPrecio.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrecio.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPrecio.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtPrecio.Location = New System.Drawing.Point(14, 295)
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(200, 30)
        Me.txtPrecio.TabIndex = 13
        '
        'lblAnticTxt
        '
        Me.lblAnticTxt.AutoSize = True
        Me.lblAnticTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblAnticTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblAnticTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblAnticTxt.Location = New System.Drawing.Point(228, 275)
        Me.lblAnticTxt.Name = "lblAnticTxt"
        Me.lblAnticTxt.Size = New System.Drawing.Size(96, 20)
        Me.lblAnticTxt.TabIndex = 14
        Me.lblAnticTxt.Text = "Anticipo"
        '
        'txtAnticipo
        '
        Me.txtAnticipo.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAnticipo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAnticipo.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtAnticipo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtAnticipo.Location = New System.Drawing.Point(228, 295)
        Me.txtAnticipo.Name = "txtAnticipo"
        Me.txtAnticipo.Size = New System.Drawing.Size(200, 30)
        Me.txtAnticipo.TabIndex = 15
        '
        'lblSaldoTxt
        '
        Me.lblSaldoTxt.AutoSize = True
        Me.lblSaldoTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblSaldoTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblSaldoTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblSaldoTxt.Location = New System.Drawing.Point(442, 275)
        Me.lblSaldoTxt.Name = "lblSaldoTxt"
        Me.lblSaldoTxt.Size = New System.Drawing.Size(159, 20)
        Me.lblSaldoTxt.TabIndex = 16
        Me.lblSaldoTxt.Text = "Saldo"
        '
        'txtSaldo
        '
        Me.txtSaldo.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSaldo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaldo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtSaldo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.txtSaldo.Location = New System.Drawing.Point(442, 295)
        Me.txtSaldo.Name = "txtSaldo"
        Me.txtSaldo.ReadOnly = True
        Me.txtSaldo.Size = New System.Drawing.Size(278, 30)
        Me.txtSaldo.TabIndex = 17
        '
        'lblFechaTxt
        '
        Me.lblFechaTxt.AutoSize = True
        Me.lblFechaTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblFechaTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblFechaTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblFechaTxt.Location = New System.Drawing.Point(14, 340)
        Me.lblFechaTxt.Name = "lblFechaTxt"
        Me.lblFechaTxt.Size = New System.Drawing.Size(145, 20)
        Me.lblFechaTxt.TabIndex = 18
        Me.lblFechaTxt.Text = "Entrega"
        '
        'dtpEntrega
        '
        Me.dtpEntrega.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.dtpEntrega.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpEntrega.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.dtpEntrega.CalendarTitleForeColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.dtpEntrega.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpEntrega.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEntrega.Location = New System.Drawing.Point(14, 360)
        Me.dtpEntrega.Name = "dtpEntrega"
        Me.dtpEntrega.Size = New System.Drawing.Size(200, 27)
        Me.dtpEntrega.TabIndex = 19
        '
        'lblEstadoTxt
        '
        Me.lblEstadoTxt.AutoSize = True
        Me.lblEstadoTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblEstadoTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblEstadoTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblEstadoTxt.Location = New System.Drawing.Point(230, 340)
        Me.lblEstadoTxt.Name = "lblEstadoTxt"
        Me.lblEstadoTxt.Size = New System.Drawing.Size(64, 20)
        Me.lblEstadoTxt.TabIndex = 20
        Me.lblEstadoTxt.Text = "Estado"
        '
        'cbEstado
        '
        Me.cbEstado.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbEstado.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cbEstado.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.cbEstado.Items.AddRange(New Object() {"Pendiente", "En proceso", "Listo para entregar", "Entregado"})
        Me.cbEstado.Location = New System.Drawing.Point(230, 360)
        Me.cbEstado.Name = "cbEstado"
        Me.cbEstado.Size = New System.Drawing.Size(490, 28)
        Me.cbEstado.TabIndex = 21
        '
        'gbLista
        '
        Me.gbLista.BackColor = System.Drawing.Color.White
        Me.gbLista.Controls.Add(Me.dgv)
        Me.gbLista.Controls.Add(Me.btnCargar)
        Me.gbLista.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbLista.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbLista.Location = New System.Drawing.Point(773, 52)
        Me.gbLista.Name = "gbLista"
        Me.gbLista.Padding = New System.Windows.Forms.Padding(4)
        Me.gbLista.Size = New System.Drawing.Size(453, 610)
        Me.gbLista.TabIndex = 4
        Me.gbLista.TabStop = False
        Me.gbLista.Text = "Agenda de pedidos"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv.Location = New System.Drawing.Point(11, 24)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(427, 536)
        Me.dgv.TabIndex = 0
        '
        'btnCargar
        '
        Me.btnCargar.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnCargar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCargar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnCargar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCargar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCargar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.btnCargar.Location = New System.Drawing.Point(11, 568)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.Size = New System.Drawing.Size(207, 30)
        Me.btnCargar.TabIndex = 1
        Me.btnCargar.Text = "Cargar seleccionado"
        Me.btnCargar.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbInfo})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 679)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(6, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1253, 24)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 5
        '
        'sbInfo
        '
        Me.sbInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.sbInfo.ForeColor = System.Drawing.Color.White
        Me.sbInfo.Name = "sbInfo"
        Me.sbInfo.Size = New System.Drawing.Size(1235, 19)
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
        Me.btnRegresar.Location = New System.Drawing.Point(380, 12)
        Me.btnRegresar.Name = "btnRegresar"
        Me.btnRegresar.Size = New System.Drawing.Size(110, 30)
        Me.btnRegresar.TabIndex = 6
        Me.btnRegresar.Text = "Cerrar"
        Me.btnRegresar.UseVisualStyleBackColor = False
        '
        'Form5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1253, 703)
        Me.Controls.Add(Me.btnRegresar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.gbForm)
        Me.Controls.Add(Me.gbLista)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "Form5"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KUMO | Pedidos premium"
        Me.gbForm.ResumeLayout(False)
        Me.gbForm.PerformLayout()
        Me.gbLista.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents gbForm As System.Windows.Forms.GroupBox
    Friend WithEvents lblNombreTxt As System.Windows.Forms.Label
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents lblTelTxt As System.Windows.Forms.Label
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents lblDescTxt As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents lblColTxt As System.Windows.Forms.Label
    Friend WithEvents txtColores As System.Windows.Forms.TextBox
    Friend WithEvents lblMedTxt As System.Windows.Forms.Label
    Friend WithEvents txtMedidas As System.Windows.Forms.TextBox
    Friend WithEvents lblNotasTxt As System.Windows.Forms.Label
    Friend WithEvents txtNotas As System.Windows.Forms.TextBox
    Friend WithEvents lblPrecioTxt As System.Windows.Forms.Label
    Friend WithEvents txtPrecio As System.Windows.Forms.TextBox
    Friend WithEvents lblAnticTxt As System.Windows.Forms.Label
    Friend WithEvents txtAnticipo As System.Windows.Forms.TextBox
    Friend WithEvents lblSaldoTxt As System.Windows.Forms.Label
    Friend WithEvents txtSaldo As System.Windows.Forms.TextBox
    Friend WithEvents lblFechaTxt As System.Windows.Forms.Label
    Friend WithEvents dtpEntrega As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEstadoTxt As System.Windows.Forms.Label
    Friend WithEvents cbEstado As System.Windows.Forms.ComboBox
    Friend WithEvents gbLista As System.Windows.Forms.GroupBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnCargar As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sbInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnRegresar As Button
End Class


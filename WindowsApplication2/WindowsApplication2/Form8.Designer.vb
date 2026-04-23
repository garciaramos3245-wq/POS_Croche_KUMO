Partial Class Form8
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
        Me.gbVentas = New System.Windows.Forms.GroupBox()
        Me.dgvVentas = New System.Windows.Forms.DataGridView()
        Me.gbDetalle = New System.Windows.Forms.GroupBox()
        Me.dgvDetalle = New System.Windows.Forms.DataGridView()
        Me.gbCancelar = New System.Windows.Forms.GroupBox()
        Me.lblMotivoTxt = New System.Windows.Forms.Label()
        Me.txtMotivo = New System.Windows.Forms.TextBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sbInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnRegresar = New System.Windows.Forms.Button()
        Me.gbFiltro.SuspendLayout()
        Me.gbVentas.SuspendLayout()
        CType(Me.dgvVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDetalle.SuspendLayout()
        CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCancelar.SuspendLayout()
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
        'gbVentas
        '
        Me.gbVentas.BackColor = System.Drawing.Color.White
        Me.gbVentas.Controls.Add(Me.dgvVentas)
        Me.gbVentas.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbVentas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbVentas.Location = New System.Drawing.Point(13, 84)
        Me.gbVentas.Name = "gbVentas"
        Me.gbVentas.Padding = New System.Windows.Forms.Padding(4)
        Me.gbVentas.Size = New System.Drawing.Size(600, 500)
        Me.gbVentas.TabIndex = 1
        Me.gbVentas.TabStop = False
        Me.gbVentas.Text = "Ventas elegibles"
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
        Me.dgvVentas.Size = New System.Drawing.Size(572, 462)
        Me.dgvVentas.TabIndex = 0
        '
        'gbDetalle
        '
        Me.gbDetalle.BackColor = System.Drawing.Color.White
        Me.gbDetalle.Controls.Add(Me.dgvDetalle)
        Me.gbDetalle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbDetalle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.gbDetalle.Location = New System.Drawing.Point(627, 84)
        Me.gbDetalle.Name = "gbDetalle"
        Me.gbDetalle.Padding = New System.Windows.Forms.Padding(4)
        Me.gbDetalle.Size = New System.Drawing.Size(560, 500)
        Me.gbDetalle.TabIndex = 2
        Me.gbDetalle.TabStop = False
        Me.gbDetalle.Text = "Detalle de la venta"
        '
        'dgvDetalle
        '
        Me.dgvDetalle.AllowUserToAddRows = False
        Me.dgvDetalle.AllowUserToDeleteRows = False
        Me.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDetalle.Location = New System.Drawing.Point(11, 24)
        Me.dgvDetalle.MultiSelect = False
        Me.dgvDetalle.Name = "dgvDetalle"
        Me.dgvDetalle.ReadOnly = True
        Me.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetalle.Size = New System.Drawing.Size(532, 462)
        Me.dgvDetalle.TabIndex = 0
        '
        'gbCancelar
        '
        Me.gbCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.gbCancelar.Controls.Add(Me.lblMotivoTxt)
        Me.gbCancelar.Controls.Add(Me.txtMotivo)
        Me.gbCancelar.Controls.Add(Me.btnCancelar)
        Me.gbCancelar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.gbCancelar.Location = New System.Drawing.Point(13, 596)
        Me.gbCancelar.Name = "gbCancelar"
        Me.gbCancelar.Padding = New System.Windows.Forms.Padding(4)
        Me.gbCancelar.Size = New System.Drawing.Size(1174, 72)
        Me.gbCancelar.TabIndex = 3
        Me.gbCancelar.TabStop = False
        Me.gbCancelar.Text = "Cancelacion segura"
        '
        'lblMotivoTxt
        '
        Me.lblMotivoTxt.AutoSize = True
        Me.lblMotivoTxt.BackColor = System.Drawing.Color.Transparent
        Me.lblMotivoTxt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblMotivoTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(116, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblMotivoTxt.Location = New System.Drawing.Point(11, 27)
        Me.lblMotivoTxt.Name = "lblMotivoTxt"
        Me.lblMotivoTxt.Size = New System.Drawing.Size(64, 20)
        Me.lblMotivoTxt.TabIndex = 0
        Me.lblMotivoTxt.Text = "MOTIVO"
        '
        'txtMotivo
        '
        Me.txtMotivo.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtMotivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMotivo.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtMotivo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.txtMotivo.Location = New System.Drawing.Point(80, 23)
        Me.txtMotivo.Name = "txtMotivo"
        Me.txtMotivo.Size = New System.Drawing.Size(880, 29)
        Me.txtMotivo.TabIndex = 1
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.FlatAppearance.BorderSize = 0
        Me.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancelar.ForeColor = System.Drawing.Color.White
        Me.btnCancelar.Location = New System.Drawing.Point(980, 18)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(180, 36)
        Me.btnCancelar.TabIndex = 2
        Me.btnCancelar.Text = "Cancelar venta"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbInfo})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 685)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(6, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1210, 24)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 4
        '
        'sbInfo
        '
        Me.sbInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.sbInfo.ForeColor = System.Drawing.Color.White
        Me.sbInfo.Name = "sbInfo"
        Me.sbInfo.Size = New System.Drawing.Size(1192, 19)
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
        Me.btnRegresar.Location = New System.Drawing.Point(1063, 27)
        Me.btnRegresar.Name = "btnRegresar"
        Me.btnRegresar.Size = New System.Drawing.Size(107, 36)
        Me.btnRegresar.TabIndex = 5
        Me.btnRegresar.Text = "Cerrar"
        Me.btnRegresar.UseVisualStyleBackColor = False
        '
        'Form8
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1210, 709)
        Me.Controls.Add(Me.btnRegresar)
        Me.Controls.Add(Me.gbFiltro)
        Me.Controls.Add(Me.gbVentas)
        Me.Controls.Add(Me.gbDetalle)
        Me.Controls.Add(Me.gbCancelar)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "Form8"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KUMO | Cancelaciones premium"
        Me.gbFiltro.ResumeLayout(False)
        Me.gbFiltro.PerformLayout()
        Me.gbVentas.ResumeLayout(False)
        CType(Me.dgvVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDetalle.ResumeLayout(False)
        CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCancelar.ResumeLayout(False)
        Me.gbCancelar.PerformLayout()
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
    Friend WithEvents gbVentas As System.Windows.Forms.GroupBox
    Friend WithEvents dgvVentas As System.Windows.Forms.DataGridView
    Friend WithEvents gbDetalle As System.Windows.Forms.GroupBox
    Friend WithEvents dgvDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents gbCancelar As System.Windows.Forms.GroupBox
    Friend WithEvents lblMotivoTxt As System.Windows.Forms.Label
    Friend WithEvents txtMotivo As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sbInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnRegresar As Button
End Class


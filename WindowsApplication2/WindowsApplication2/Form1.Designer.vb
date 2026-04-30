Partial Class Form1
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)

    End Sub

    Private Sub InitializeComponent()
        Me.pnlLogo = New System.Windows.Forms.Panel()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.lblLogo = New System.Windows.Forms.Label()
        Me.lblAppName = New System.Windows.Forms.Label()
        Me.lblAppSub = New System.Windows.Forms.Label()
        Me.pnlLinea = New System.Windows.Forms.Panel()
        Me.gbCredenciales = New System.Windows.Forms.GroupBox()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnEntrar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sbInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sbVersion = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLogo.SuspendLayout()
        Me.gbCredenciales.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()

        ' ── pnlLogo (café header) ─────────────────
        Me.pnlLogo.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.pnlLogo.Controls.Add(Me.picLogo)
        Me.pnlLogo.Controls.Add(Me.lblLogo)
        Me.pnlLogo.Controls.Add(Me.lblAppName)
        Me.pnlLogo.Controls.Add(Me.lblAppSub)
        Me.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLogo.Location = New System.Drawing.Point(0, 0)
        Me.pnlLogo.Name = "pnlLogo"
        Me.pnlLogo.Size = New System.Drawing.Size(460, 144)
        Me.pnlLogo.TabIndex = 0

        ' picLogo
        Me.picLogo.Location = New System.Drawing.Point(18, 18)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(142, 92)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 0
        Me.picLogo.TabStop = False

        ' lblLogo
        Me.lblLogo.AutoSize = True
        Me.lblLogo.Font = New System.Drawing.Font("Segoe UI Emoji", 30.0!)
        Me.lblLogo.ForeColor = System.Drawing.Color.FromArgb(155, 188, 232)
        Me.lblLogo.Location = New System.Drawing.Point(30, 24)
        Me.lblLogo.Name = "lblLogo"
        Me.lblLogo.Text = "K"

        ' lblAppName
        Me.lblAppName.AutoSize = True
        Me.lblAppName.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblAppName.ForeColor = System.Drawing.Color.White
        Me.lblAppName.Location = New System.Drawing.Point(176, 34)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Text = "KUMO"

        ' lblAppSub
        Me.lblAppSub.AutoSize = True
        Me.lblAppSub.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAppSub.ForeColor = System.Drawing.Color.FromArgb(155, 188, 232)
        Me.lblAppSub.Location = New System.Drawing.Point(178, 76)
        Me.lblAppSub.Name = "lblAppSub"
        Me.lblAppSub.Text = ""

        ' ── pnlLinea (acento terracota) ───────────
        Me.pnlLinea.BackColor = System.Drawing.Color.FromArgb(155, 188, 232)
        Me.pnlLinea.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLinea.Location = New System.Drawing.Point(0, 144)
        Me.pnlLinea.Name = "pnlLinea"
        Me.pnlLinea.Size = New System.Drawing.Size(460, 4)

        ' ── gbCredenciales ────────────────────────
        Me.gbCredenciales.BackColor = System.Drawing.Color.White
        Me.gbCredenciales.Controls.Add(Me.lblUsuario)
        Me.gbCredenciales.Controls.Add(Me.txtUsuario)
        Me.gbCredenciales.Controls.Add(Me.lblPassword)
        Me.gbCredenciales.Controls.Add(Me.txtPassword)
        Me.gbCredenciales.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gbCredenciales.ForeColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.gbCredenciales.Location = New System.Drawing.Point(24, 160)
        Me.gbCredenciales.Name = "gbCredenciales"
        Me.gbCredenciales.Padding = New System.Windows.Forms.Padding(12)
        Me.gbCredenciales.Size = New System.Drawing.Size(412, 148)
        Me.gbCredenciales.TabIndex = 1
        Me.gbCredenciales.TabStop = False
        Me.gbCredenciales.Text = "Acceso"

        ' lblUsuario
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblUsuario.Location = New System.Drawing.Point(14, 28)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Text = "USUARIO"
        Me.lblUsuario.BackColor = System.Drawing.Color.Transparent

        ' txtUsuario
        Me.txtUsuario.BackColor = System.Drawing.Color.FromArgb(249, 251, 255)
        Me.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsuario.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.txtUsuario.Location = New System.Drawing.Point(14, 48)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(382, 28)
        Me.txtUsuario.TabIndex = 0

        ' lblPassword
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblPassword.Location = New System.Drawing.Point(14, 88)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Text = "CONTRASEÑA"
        Me.lblPassword.BackColor = System.Drawing.Color.Transparent

        ' txtPassword
        Me.txtPassword.BackColor = System.Drawing.Color.FromArgb(249, 251, 255)
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPassword.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.txtPassword.Location = New System.Drawing.Point(14, 108)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(382, 28)
        Me.txtPassword.TabIndex = 1

        ' ── btnEntrar (café primario) ─────────────
        Me.btnEntrar.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEntrar.FlatAppearance.BorderSize = 0
        Me.btnEntrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(86, 125, 183)
        Me.btnEntrar.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnEntrar.ForeColor = System.Drawing.Color.White
        Me.btnEntrar.Location = New System.Drawing.Point(24, 328)
        Me.btnEntrar.Name = "btnEntrar"
        Me.btnEntrar.Size = New System.Drawing.Size(193, 40)
        Me.btnEntrar.TabIndex = 2
        Me.btnEntrar.Text = "Ingresar al sistema"
        Me.btnEntrar.UseVisualStyleBackColor = False
        Me.btnEntrar.Cursor = Cursors.Hand

        ' ── btnCancelar (crema secundario) ────────
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(232, 239, 248)
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(155, 188, 232)
        Me.btnCancelar.FlatAppearance.BorderSize = 1
        Me.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(220, 233, 248)
        Me.btnCancelar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.btnCancelar.Location = New System.Drawing.Point(243, 328)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(193, 40)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "Cerrar"
        Me.btnCancelar.Cursor = Cursors.Hand

        ' ── StatusStrip ───────────────────────────
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbInfo, Me.sbVersion})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 388)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(6, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(460, 26)
        Me.StatusStrip1.SizingGrip = False

        Me.sbInfo.Font = New System.Drawing.Font("Segoe UI", 8.0F)
        Me.sbInfo.ForeColor = System.Drawing.Color.White
        Me.sbInfo.Name = "sbInfo"
        Me.sbInfo.Spring = True
        Me.sbInfo.Text = "  Listo para iniciar sesion."
        Me.sbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.sbVersion.Font = New System.Drawing.Font("Segoe UI", 8.0F)
        Me.sbVersion.ForeColor = System.Drawing.Color.FromArgb(155, 188, 232)
        Me.sbVersion.Name = "sbVersion"
        Me.sbVersion.Text = "v1.0.0  -  Acceso"
        Me.sbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight

        ' ── Form1 ─────────────────────────────────
        Me.AcceptButton = Me.btnEntrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(249, 251, 255)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(460, 414)
        Me.Controls.Add(Me.pnlLinea)
        Me.Controls.Add(Me.pnlLogo)
        Me.Controls.Add(Me.gbCredenciales)
        Me.Controls.Add(Me.btnEntrar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0F)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KUMO | Acceso de caja"

        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLogo.ResumeLayout(False)
        Me.pnlLogo.PerformLayout()
        Me.gbCredenciales.ResumeLayout(False)
        Me.gbCredenciales.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
        ' Runtime premium design snapshot. Keep this block aligned with the executable view.
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.ClientSize = New System.Drawing.Size(920, 560)
        Me.BackColor = System.Drawing.Color.FromArgb(245, 247, 250)
        Me.Text = "KUMO | Acceso de caja"
        Me.btnCancelar.SetBounds(466, 435, 244, 48)
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(232, 239, 248)
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.btnCancelar.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnCancelar.Text = "Cerrar"
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.FlatAppearance.BorderSize = 1
        Me.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(155, 188, 232)
        Me.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(220, 233, 248)
        Me.btnCancelar.UseVisualStyleBackColor = False
        Me.btnEntrar.SetBounds(210, 435, 244, 48)
        Me.btnEntrar.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.btnEntrar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.btnEntrar.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnEntrar.Text = "Ingresar al sistema"
        Me.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEntrar.FlatAppearance.BorderSize = 0
        Me.btnEntrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 226, 241)
        Me.btnEntrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(86, 125, 183)
        Me.btnEntrar.UseVisualStyleBackColor = False
        Me.gbCredenciales.SetBounds(210, 213, 500, 204)
        Me.gbCredenciales.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.gbCredenciales.ForeColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.gbCredenciales.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.gbCredenciales.Text = "Acceso"
        Me.lblAppName.SetBounds(166, 36, 99, 44)
        Me.lblAppName.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblAppName.ForeColor = System.Drawing.Color.FromArgb(37, 67, 109)
        Me.lblAppName.Font = New System.Drawing.Font("Segoe UI", 21!, System.Drawing.FontStyle.Bold)
        Me.lblAppName.Text = "KUMO"
        Me.lblAppName.AutoSize = True
        Me.lblAppName.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblAppSub.SetBounds(168, 74, 0, 22)
        Me.lblAppSub.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblAppSub.ForeColor = System.Drawing.Color.FromArgb(111, 132, 162)
        Me.lblAppSub.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular)
        Me.lblAppSub.Text = ""
        Me.lblAppSub.AutoSize = True
        Me.lblAppSub.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblLogo.SetBounds(30, 24, 11, 20)
        Me.lblLogo.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblLogo.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblLogo.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblLogo.Text = "K"
        Me.lblLogo.AutoSize = True
        Me.lblLogo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPassword.SetBounds(30, 112, 87, 21)
        Me.lblPassword.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblPassword.Text = "CONTRASEÑA"
        Me.lblPassword.AutoSize = True
        Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblUsuario.SetBounds(30, 38, 59, 21)
        Me.lblUsuario.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblUsuario.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.lblUsuario.Text = "USUARIO"
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.picLogo.SetBounds(24, 18, 124, 92)
        Me.picLogo.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.picLogo.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.picLogo.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pnlLinea.SetBounds(210, 190, 500, 5)
        Me.pnlLinea.BackColor = System.Drawing.Color.FromArgb(155, 188, 232)
        Me.pnlLinea.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.pnlLinea.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.pnlLogo.SetBounds(210, 44, 500, 132)
        Me.pnlLogo.BackColor = System.Drawing.Color.FromArgb(236, 243, 252)
        Me.pnlLogo.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.pnlLogo.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.StatusStrip1.SetBounds(0, 538, 920, 22)
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.StatusStrip1.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.StatusStrip1.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(113, 152, 209)
        Me.txtPassword.SetBounds(30, 141, 440, 27)
        Me.txtPassword.BackColor = System.Drawing.Color.FromArgb(249, 251, 255)
        Me.txtPassword.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 11!, System.Drawing.FontStyle.Regular)
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtUsuario.SetBounds(30, 67, 440, 27)
        Me.txtUsuario.BackColor = System.Drawing.Color.FromArgb(249, 251, 255)
        Me.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.txtUsuario.Font = New System.Drawing.Font("Segoe UI", 11!, System.Drawing.FontStyle.Regular)
        Me.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left

    End Sub

    Friend WithEvents pnlLogo As System.Windows.Forms.Panel
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblLogo As System.Windows.Forms.Label
    Friend WithEvents lblAppName As System.Windows.Forms.Label
    Friend WithEvents lblAppSub As System.Windows.Forms.Label
    Friend WithEvents pnlLinea As System.Windows.Forms.Panel
    Friend WithEvents gbCredenciales As System.Windows.Forms.GroupBox
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnEntrar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sbInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sbVersion As System.Windows.Forms.ToolStripStatusLabel
End Class






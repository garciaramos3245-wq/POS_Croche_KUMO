Partial Class Form6
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.pnlLinea = New System.Windows.Forms.Panel()
        Me.gbPreview = New System.Windows.Forms.GroupBox()
        Me.rtb = New System.Windows.Forms.RichTextBox()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.gbPreview.SuspendLayout()
        Me.SuspendLayout()

        Dim clrH As System.Drawing.Color = System.Drawing.Color.FromArgb(113, 152, 209)
        Dim clrA As System.Drawing.Color = System.Drawing.Color.FromArgb(155, 188, 232)
        Dim clrA2 As System.Drawing.Color = System.Drawing.Color.FromArgb(86, 125, 183)
        Dim clrS As System.Drawing.Color = System.Drawing.Color.FromArgb(232, 239, 248)
        Dim clrBG As System.Drawing.Color = System.Drawing.Color.FromArgb(249, 251, 255)
        Dim clrT As System.Drawing.Color = System.Drawing.Color.FromArgb(52, 79, 118)
        Dim clrHov As System.Drawing.Color = System.Drawing.Color.FromArgb(220, 233, 248)
        Dim clrPale As System.Drawing.Color = System.Drawing.Color.White

        Me.pnlHeader.BackColor = clrH : Me.pnlHeader.Controls.Add(Me.lblTitulo) : Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top : Me.pnlHeader.Location = New System.Drawing.Point(0, 0) : Me.pnlHeader.Name = "pnlHeader" : Me.pnlHeader.Size = New System.Drawing.Size(370, 50)
        Me.lblTitulo.AutoSize = True : Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold) : Me.lblTitulo.ForeColor = clrA : Me.lblTitulo.Location = New System.Drawing.Point(14, 14) : Me.lblTitulo.Name = "lblTitulo" : Me.lblTitulo.Text = "KUMO | Ticket premium" : Me.lblTitulo.BackColor = System.Drawing.Color.Transparent

        Me.pnlLinea.BackColor = clrA : Me.pnlLinea.Dock = System.Windows.Forms.DockStyle.Top : Me.pnlLinea.Location = New System.Drawing.Point(0, 50) : Me.pnlLinea.Name = "pnlLinea" : Me.pnlLinea.Size = New System.Drawing.Size(370, 4)

        Me.gbPreview.BackColor = System.Drawing.Color.White : Me.gbPreview.Controls.Add(Me.rtb) : Me.gbPreview.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold) : Me.gbPreview.ForeColor = clrH : Me.gbPreview.Location = New System.Drawing.Point(12, 66) : Me.gbPreview.Name = "gbPreview" : Me.gbPreview.Padding = New System.Windows.Forms.Padding(4) : Me.gbPreview.Size = New System.Drawing.Size(346, 430) : Me.gbPreview.TabStop = False : Me.gbPreview.Text = "Vista previa del ticket"

        Me.rtb.BackColor = clrBG : Me.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None : Me.rtb.Font = New System.Drawing.Font("Courier New", 8.5F) : Me.rtb.ForeColor = clrT : Me.rtb.Location = New System.Drawing.Point(8, 22) : Me.rtb.Name = "rtb" : Me.rtb.ReadOnly = True : Me.rtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical : Me.rtb.Size = New System.Drawing.Size(330, 400)

        Me.btnImprimir.BackColor = clrH : Me.btnImprimir.Cursor = Cursors.Hand : Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnImprimir.FlatAppearance.BorderSize = 0 : Me.btnImprimir.FlatAppearance.MouseOverBackColor = clrA2 : Me.btnImprimir.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold) : Me.btnImprimir.ForeColor = clrPale : Me.btnImprimir.Location = New System.Drawing.Point(12, 506) : Me.btnImprimir.Name = "btnImprimir" : Me.btnImprimir.Size = New System.Drawing.Size(160, 36) : Me.btnImprimir.Text = "Vista de impresion"

        Me.btnCerrar.BackColor = clrS : Me.btnCerrar.Cursor = Cursors.Hand : Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnCerrar.FlatAppearance.BorderColor = clrA : Me.btnCerrar.FlatAppearance.MouseOverBackColor = clrHov : Me.btnCerrar.Font = New System.Drawing.Font("Segoe UI", 9.0F) : Me.btnCerrar.ForeColor = clrT : Me.btnCerrar.Location = New System.Drawing.Point(198, 506) : Me.btnCerrar.Name = "btnCerrar" : Me.btnCerrar.Size = New System.Drawing.Size(160, 36) : Me.btnCerrar.Text = "Cerrar ticket"

        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!) : Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font : Me.BackColor = clrBG : Me.ClientSize = New System.Drawing.Size(370, 554)
        Me.Controls.Add(Me.gbPreview) : Me.Controls.Add(Me.btnImprimir) : Me.Controls.Add(Me.btnCerrar) : Me.Controls.Add(Me.pnlLinea) : Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0F) : Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog : Me.MaximizeBox = False : Me.Name = "Form6" : Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent : Me.Text = "Ticket de Venta"

        Me.pnlHeader.ResumeLayout(False) : Me.pnlHeader.PerformLayout() : Me.gbPreview.ResumeLayout(False) : Me.ResumeLayout(False)
    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents pnlLinea As System.Windows.Forms.Panel
    Friend WithEvents gbPreview As System.Windows.Forms.GroupBox
    Friend WithEvents rtb As System.Windows.Forms.RichTextBox
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
End Class


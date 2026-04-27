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
        Me.pnlMeta = New System.Windows.Forms.Panel()
        Me.lblTicketNumeroCaption = New System.Windows.Forms.Label()
        Me.lblTicketNumero = New System.Windows.Forms.Label()
        Me.lblTicketFechaCaption = New System.Windows.Forms.Label()
        Me.lblTicketFecha = New System.Windows.Forms.Label()
        Me.lblTicketTotalCaption = New System.Windows.Forms.Label()
        Me.lblTicketTotal = New System.Windows.Forms.Label()
        Me.rtb = New System.Windows.Forms.RichTextBox()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.gbPreview.SuspendLayout()
        Me.pnlMeta.SuspendLayout()
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

        Me.gbPreview.BackColor = System.Drawing.Color.White : Me.gbPreview.Controls.Add(Me.pnlMeta) : Me.gbPreview.Controls.Add(Me.rtb) : Me.gbPreview.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold) : Me.gbPreview.ForeColor = clrH : Me.gbPreview.Location = New System.Drawing.Point(12, 66) : Me.gbPreview.Name = "gbPreview" : Me.gbPreview.Padding = New System.Windows.Forms.Padding(4) : Me.gbPreview.Size = New System.Drawing.Size(346, 430) : Me.gbPreview.TabStop = False : Me.gbPreview.Text = "Vista previa del ticket"

        Me.pnlMeta.BackColor = clrBG : Me.pnlMeta.Controls.Add(Me.lblTicketNumeroCaption) : Me.pnlMeta.Controls.Add(Me.lblTicketNumero) : Me.pnlMeta.Controls.Add(Me.lblTicketFechaCaption) : Me.pnlMeta.Controls.Add(Me.lblTicketFecha) : Me.pnlMeta.Controls.Add(Me.lblTicketTotalCaption) : Me.pnlMeta.Controls.Add(Me.lblTicketTotal) : Me.pnlMeta.Location = New System.Drawing.Point(12, 28) : Me.pnlMeta.Name = "pnlMeta" : Me.pnlMeta.Size = New System.Drawing.Size(322, 74)

        Me.lblTicketNumeroCaption.AutoSize = True : Me.lblTicketNumeroCaption.Font = New System.Drawing.Font("Segoe UI", 7.0F, System.Drawing.FontStyle.Bold) : Me.lblTicketNumeroCaption.ForeColor = clrA2 : Me.lblTicketNumeroCaption.Location = New System.Drawing.Point(12, 11) : Me.lblTicketNumeroCaption.Name = "lblTicketNumeroCaption" : Me.lblTicketNumeroCaption.Text = "FOLIO"
        Me.lblTicketNumero.AutoSize = True : Me.lblTicketNumero.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold) : Me.lblTicketNumero.ForeColor = clrT : Me.lblTicketNumero.Location = New System.Drawing.Point(12, 27) : Me.lblTicketNumero.Name = "lblTicketNumero" : Me.lblTicketNumero.Text = "V-000"

        Me.lblTicketFechaCaption.AutoSize = True : Me.lblTicketFechaCaption.Font = New System.Drawing.Font("Segoe UI", 7.0F, System.Drawing.FontStyle.Bold) : Me.lblTicketFechaCaption.ForeColor = clrA2 : Me.lblTicketFechaCaption.Location = New System.Drawing.Point(110, 11) : Me.lblTicketFechaCaption.Name = "lblTicketFechaCaption" : Me.lblTicketFechaCaption.Text = "FECHA"
        Me.lblTicketFecha.AutoSize = True : Me.lblTicketFecha.Font = New System.Drawing.Font("Segoe UI", 8.5F) : Me.lblTicketFecha.ForeColor = clrT : Me.lblTicketFecha.Location = New System.Drawing.Point(110, 31) : Me.lblTicketFecha.Name = "lblTicketFecha" : Me.lblTicketFecha.Text = "00/00/0000"

        Me.lblTicketTotalCaption.AutoSize = True : Me.lblTicketTotalCaption.Font = New System.Drawing.Font("Segoe UI", 7.0F, System.Drawing.FontStyle.Bold) : Me.lblTicketTotalCaption.ForeColor = clrA2 : Me.lblTicketTotalCaption.Location = New System.Drawing.Point(232, 11) : Me.lblTicketTotalCaption.Name = "lblTicketTotalCaption" : Me.lblTicketTotalCaption.Text = "TOTAL"
        Me.lblTicketTotal.AutoSize = True : Me.lblTicketTotal.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold) : Me.lblTicketTotal.ForeColor = clrA2 : Me.lblTicketTotal.Location = New System.Drawing.Point(232, 27) : Me.lblTicketTotal.Name = "lblTicketTotal" : Me.lblTicketTotal.Text = "$0.00"

        Me.rtb.BackColor = clrBG : Me.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None : Me.rtb.Font = New System.Drawing.Font("Consolas", 8.75F) : Me.rtb.ForeColor = clrT : Me.rtb.Location = New System.Drawing.Point(12, 112) : Me.rtb.Name = "rtb" : Me.rtb.ReadOnly = True : Me.rtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical : Me.rtb.Size = New System.Drawing.Size(322, 310)

        Me.btnImprimir.BackColor = clrH : Me.btnImprimir.Cursor = Cursors.Hand : Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnImprimir.FlatAppearance.BorderSize = 0 : Me.btnImprimir.FlatAppearance.MouseOverBackColor = clrA2 : Me.btnImprimir.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold) : Me.btnImprimir.ForeColor = clrPale : Me.btnImprimir.Location = New System.Drawing.Point(12, 506) : Me.btnImprimir.Name = "btnImprimir" : Me.btnImprimir.Size = New System.Drawing.Size(160, 36) : Me.btnImprimir.Text = "Vista de impresion"

        Me.btnCerrar.BackColor = clrS : Me.btnCerrar.Cursor = Cursors.Hand : Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnCerrar.FlatAppearance.BorderColor = clrA : Me.btnCerrar.FlatAppearance.MouseOverBackColor = clrHov : Me.btnCerrar.Font = New System.Drawing.Font("Segoe UI", 9.0F) : Me.btnCerrar.ForeColor = clrT : Me.btnCerrar.Location = New System.Drawing.Point(198, 506) : Me.btnCerrar.Name = "btnCerrar" : Me.btnCerrar.Size = New System.Drawing.Size(160, 36) : Me.btnCerrar.Text = "Cerrar ticket"

        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!) : Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font : Me.BackColor = clrBG : Me.ClientSize = New System.Drawing.Size(370, 554)
        Me.Controls.Add(Me.gbPreview) : Me.Controls.Add(Me.btnImprimir) : Me.Controls.Add(Me.btnCerrar) : Me.Controls.Add(Me.pnlLinea) : Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0F) : Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog : Me.MaximizeBox = False : Me.Name = "Form6" : Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent : Me.Text = "Ticket de Venta"

        Me.pnlHeader.ResumeLayout(False) : Me.pnlHeader.PerformLayout() : Me.gbPreview.ResumeLayout(False) : Me.pnlMeta.ResumeLayout(False) : Me.pnlMeta.PerformLayout() : Me.ResumeLayout(False)
    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents pnlLinea As System.Windows.Forms.Panel
    Friend WithEvents gbPreview As System.Windows.Forms.GroupBox
    Friend WithEvents pnlMeta As System.Windows.Forms.Panel
    Friend WithEvents lblTicketNumeroCaption As System.Windows.Forms.Label
    Friend WithEvents lblTicketNumero As System.Windows.Forms.Label
    Friend WithEvents lblTicketFechaCaption As System.Windows.Forms.Label
    Friend WithEvents lblTicketFecha As System.Windows.Forms.Label
    Friend WithEvents lblTicketTotalCaption As System.Windows.Forms.Label
    Friend WithEvents lblTicketTotal As System.Windows.Forms.Label
    Friend WithEvents rtb As System.Windows.Forms.RichTextBox
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
End Class


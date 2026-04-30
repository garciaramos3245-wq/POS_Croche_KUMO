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
        ' Runtime premium design snapshot. Keep this block aligned with the executable view.
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.ClientSize = New System.Drawing.Size(1536, 864)
        Me.BackColor = System.Drawing.Color.FromArgb(244, 240, 234)
        Me.Text = "Ticket de Venta - V-000"
        Me.btnCerrar.SetBounds(776, 750, 190, 42)
        Me.btnCerrar.BackColor = System.Drawing.Color.FromArgb(247, 241, 232)
        Me.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.btnCerrar.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.btnCerrar.Text = "Cerrar ticket"
        Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCerrar.FlatAppearance.BorderSize = 1
        Me.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 189, 150)
        Me.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 235, 224)
        Me.btnCerrar.UseVisualStyleBackColor = False
        Me.btnImprimir.SetBounds(570, 750, 190, 42)
        Me.btnImprimir.BackColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.btnImprimir.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255)
        Me.btnImprimir.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.btnImprimir.Text = "Vista de impresion"
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.FlatAppearance.BorderSize = 0
        Me.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(214, 226, 241)
        Me.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(67, 74, 84)
        Me.btnImprimir.UseVisualStyleBackColor = False
        Me.gbPreview.SetBounds(458, 82, 620, 650)
        Me.gbPreview.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.gbPreview.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.gbPreview.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.gbPreview.Text = "Vista previa del ticket"
        Me.lblTicketFecha.SetBounds(222, 29, 64, 20)
        Me.lblTicketFecha.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblTicketFecha.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblTicketFecha.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblTicketFecha.Text = "00/00/0000"
        Me.lblTicketFecha.AutoSize = True
        Me.lblTicketFecha.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTicketFechaCaption.SetBounds(222, 10, 39, 20)
        Me.lblTicketFechaCaption.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblTicketFechaCaption.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblTicketFechaCaption.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblTicketFechaCaption.Text = "FECHA"
        Me.lblTicketFechaCaption.AutoSize = True
        Me.lblTicketFechaCaption.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTicketNumero.SetBounds(14, 26, 35, 20)
        Me.lblTicketNumero.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblTicketNumero.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblTicketNumero.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblTicketNumero.Text = "V-000"
        Me.lblTicketNumero.AutoSize = True
        Me.lblTicketNumero.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTicketNumeroCaption.SetBounds(14, 10, 36, 20)
        Me.lblTicketNumeroCaption.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblTicketNumeroCaption.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblTicketNumeroCaption.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblTicketNumeroCaption.Text = "FOLIO"
        Me.lblTicketNumeroCaption.AutoSize = True
        Me.lblTicketNumeroCaption.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTicketTotal.SetBounds(474, 26, 32, 20)
        Me.lblTicketTotal.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblTicketTotal.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblTicketTotal.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblTicketTotal.Text = "$0.00"
        Me.lblTicketTotal.AutoSize = True
        Me.lblTicketTotal.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTicketTotalCaption.SetBounds(474, 10, 38, 20)
        Me.lblTicketTotalCaption.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblTicketTotalCaption.ForeColor = System.Drawing.Color.FromArgb(116, 141, 175)
        Me.lblTicketTotalCaption.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblTicketTotalCaption.Text = "TOTAL"
        Me.lblTicketTotalCaption.AutoSize = True
        Me.lblTicketTotalCaption.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTitulo.SetBounds(92, 14, 128, 20)
        Me.lblTitulo.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(46, 52, 60)
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
        Me.lblTitulo.Text = "KUMO | Ticket premium"
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.pnlHeader.SetBounds(0, 0, 1536, 50)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(247, 241, 232)
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.pnlHeader.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.pnlLinea.SetBounds(0, 50, 1536, 4)
        Me.pnlLinea.BackColor = System.Drawing.Color.FromArgb(214, 189, 150)
        Me.pnlLinea.ForeColor = System.Drawing.Color.FromArgb(52, 79, 118)
        Me.pnlLinea.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular)
        Me.pnlMeta.SetBounds(18, 34, 584, 76)
        Me.pnlMeta.BackColor = System.Drawing.Color.FromArgb(250, 246, 240)
        Me.pnlMeta.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.pnlMeta.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold)
        Me.rtb.SetBounds(18, 126, 584, 506)
        Me.rtb.BackColor = System.Drawing.Color.FromArgb(255, 252, 247)
        Me.rtb.ForeColor = System.Drawing.Color.FromArgb(76, 66, 55)
        Me.rtb.Font = New System.Drawing.Font("Consolas", 9.25!, System.Drawing.FontStyle.Regular)

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





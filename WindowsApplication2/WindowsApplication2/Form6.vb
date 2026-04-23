Imports System.Data.SqlClient

Public Class Form6

    Private idVenta As Integer
    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)

    Public Sub New(Optional id As Integer = 0)
        InitializeComponent()
        idVenta = id
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                ModEstilo.EstilarControles(Me)
                ModEstilo.EstilarBotonPrimario(btnImprimir)
                ModEstilo.EstilarBotonSecundario(btnCerrar)

                Me.BackColor = CLR_BG_PREMIUM
                pnlHeader.BackColor = CLR_PANEL_PREMIUM
                InsertarLogoHeader()
                lblTitulo.ForeColor = CLR_DARK_PREMIUM
                lblTitulo.Text = "KUMO | Ticket premium"
                pnlLinea.BackColor = Color.FromArgb(214, 189, 150)
                gbPreview.BackColor = CLR_SURFACE_PREMIUM
                gbPreview.ForeColor = CLR_TEXT_PREMIUM
                gbPreview.Text = "Vista previa del ticket"
                rtb.BackColor = CLR_SURFACE_PREMIUM
                rtb.ForeColor = CLR_TEXT_PREMIUM
                rtb.BorderStyle = BorderStyle.None
                btnImprimir.Text = "Vista de impresion"
                btnCerrar.Text = "Cerrar ticket"
                btnImprimir.BackColor = CLR_DARK_PREMIUM
                btnImprimir.ForeColor = Color.White
                btnImprimir.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)
                btnCerrar.BackColor = CLR_PANEL_PREMIUM
                btnCerrar.ForeColor = CLR_TEXT_PREMIUM
                btnCerrar.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
                btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)
                Me.Text = "Ticket de Venta - V-000"
                rtb.Text = "Vista previa del ticket KUMO"
                ConfigurarLayoutTicket()
            End Sub)
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarBotonPrimario(btnImprimir)
        ModEstilo.EstilarBotonSecundario(btnCerrar)

        Me.BackColor = CLR_BG_PREMIUM
        pnlHeader.BackColor = CLR_PANEL_PREMIUM
        InsertarLogoHeader()
        lblTitulo.ForeColor = CLR_DARK_PREMIUM
        lblTitulo.Text = "KUMO | Ticket premium"
        pnlLinea.BackColor = Color.FromArgb(214, 189, 150)
        gbPreview.BackColor = CLR_SURFACE_PREMIUM
        gbPreview.ForeColor = CLR_TEXT_PREMIUM
        gbPreview.Text = "Vista previa del ticket"
        rtb.BackColor = CLR_SURFACE_PREMIUM
        rtb.ForeColor = CLR_TEXT_PREMIUM
        rtb.BorderStyle = BorderStyle.None
        btnImprimir.Text = "Vista de impresion"
        btnCerrar.Text = "Cerrar ticket"
        btnImprimir.BackColor = CLR_DARK_PREMIUM
        btnImprimir.ForeColor = Color.White
        btnImprimir.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)
        btnCerrar.BackColor = CLR_PANEL_PREMIUM
        btnCerrar.ForeColor = CLR_TEXT_PREMIUM
        btnCerrar.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        Me.Text = "Ticket de Venta - V-" & idVenta.ToString("000")
        GenerarTicket()
        ConfigurarLayoutTicket()
    End Sub

    Private Sub InsertarLogoHeader()
        Dim pic = TryCast(pnlHeader.Controls("picTicketLogo"), PictureBox)
        If pic Is Nothing Then
            pic = New PictureBox()
            pic.Name = "picTicketLogo"
            pic.Location = New Point(10, 5)
            pic.Size = New Size(76, 38)
            pnlHeader.Controls.Add(pic)
        End If

        ModEstilo.CargarLogo(pic)
        lblTitulo.Left = 92
    End Sub

    Private Sub GenerarTicket()
        Try
            rtb.Text = ObtenerTextoTicket(idVenta)
        Catch ex As Exception
            MsgBox("Error al generar ticket: " & ex.Message)
        End Try
    End Sub

    Public Shared Function ObtenerTextoTicket(idVenta As Integer) As String
        Dim venta = ObtenerTabla(
            "SELECT Fecha, Total FROM PEDIDOS WHERE Id_Pedido = @id",
            New SqlParameter("@id", idVenta))

        If venta.Rows.Count = 0 Then
            Return "No se encontro la venta."
        End If

        Dim fecha As String = ModEstilo.FormatoFechaHora24(CDate(venta.Rows(0)("Fecha")))
        Dim total As Decimal = CDec(venta.Rows(0)("Total"))

        Dim dt = ObtenerTabla(
            "SELECT p.NombrePr AS Nombre, d.Cantidad, " &
            "(d.Cantidad * d.PrecioVentaMomento) AS Subtotal " &
            "FROM DET_PEDIDOS d " &
            "INNER JOIN PRODUCTO p ON p.Id_Producto = d.Id_Producto " &
            "WHERE d.Id_Pedido = @id",
            New SqlParameter("@id", idVenta))

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("================================")
        sb.AppendLine("             KUMO               ")
        sb.AppendLine("            tu ticket           ")
        sb.AppendLine("================================")
        sb.AppendLine("Ticket No: V-" & idVenta.ToString("000"))
        sb.AppendLine("Fecha   : " & fecha)
        sb.AppendLine("--------------------------------")
        sb.AppendLine("PRODUCTOS")
        sb.AppendLine("--------------------------------")

        For Each row As DataRow In dt.Rows
            Dim nom As String = row("Nombre").ToString()
            If nom.Length > 16 Then nom = nom.Substring(0, 16)
            sb.AppendLine(nom.PadRight(18) & "x" & CInt(row("Cantidad")).ToString().PadLeft(2) &
                          "  $" & CDec(row("Subtotal")).ToString("N2").PadLeft(8))
        Next

        sb.AppendLine("--------------------------------")
        sb.AppendLine("TOTAL:".PadRight(20) & "$" & total.ToString("N2").PadLeft(9))
        sb.AppendLine("================================")
        sb.AppendLine("      gracias por tu compra")
        sb.AppendLine("        vuelve muy pronto")
        Return sb.ToString()
    End Function

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim pd As New Printing.PrintDocument()
        AddHandler pd.PrintPage,
            Sub(s, ev)
                ev.Graphics.DrawString(
                    rtb.Text,
                    New System.Drawing.Font("Courier New", 8),
                    System.Drawing.Brushes.Black, 10, 10)
            End Sub

        Dim preview As New PrintPreviewDialog()
        preview.Document = pd
        preview.ShowDialog()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub ConfigurarLayoutTicket()
        Dim margen As Integer = 24
        Dim anchoCard As Integer = Math.Min(620, Me.ClientSize.Width - (margen * 2))
        Dim altoCard As Integer = Me.ClientSize.Height - pnlHeader.Height - pnlLinea.Height - 160
        Dim xCard As Integer = (Me.ClientSize.Width - anchoCard) \ 2
        Dim yCard As Integer = pnlLinea.Bottom + 28

        gbPreview.SetBounds(xCard, yCard, anchoCard, altoCard)
        rtb.SetBounds(16, 30, gbPreview.Width - 32, gbPreview.Height - 46)

        Dim anchoBoton As Integer = 190
        Dim sep As Integer = 16
        Dim xBotones As Integer = (Me.ClientSize.Width - ((anchoBoton * 2) + sep)) \ 2
        btnImprimir.SetBounds(xBotones, gbPreview.Bottom + 18, anchoBoton, 42)
        btnCerrar.SetBounds(btnImprimir.Right + sep, gbPreview.Bottom + 18, anchoBoton, 42)

        gbPreview.Anchor = AnchorStyles.None
        btnImprimir.Anchor = AnchorStyles.None
        btnCerrar.Anchor = AnchorStyles.None
    End Sub

    Private Sub Form6_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutTicket()
    End Sub

End Class

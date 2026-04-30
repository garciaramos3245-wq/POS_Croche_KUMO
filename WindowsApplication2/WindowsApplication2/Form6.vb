Imports System.Data.SqlClient

Public Class Form6

    Private idVenta As Integer
    Private Class TicketDetalle
        Public Property Nombre As String
        Public Property Cantidad As Integer
        Public Property Subtotal As Decimal
    End Class

    Private Class TicketData
        Public Property Numero As String
        Public Property Fecha As String
        Public Property Total As Decimal
        Public Property Detalles As List(Of TicketDetalle)
    End Class

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)
    Private ReadOnly CLR_ACCENT_PREMIUM As Color = Color.FromArgb(181, 138, 92)

    Public Sub New(Optional id As Integer = 0)
        InitializeComponent()
        idVenta = id
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                If ModEstilo.EstaEnModoDisenio(Me) Then
                    ModEstilo.PrepararVentana(Me)
                End If
                AplicarDisenoTicket("Ticket de Venta - V-000")
                rtb.Text = "Vista previa del ticket KUMO"
            End Sub)
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AplicarDisenoTicket("Ticket de Venta - V-" & idVenta.ToString("000"))
        GenerarTicket()
    End Sub

    Private Sub AplicarDisenoTicket(tituloVentana As String)
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
        pnlMeta.BackColor = Color.FromArgb(250, 246, 240)
        rtb.BackColor = CLR_SURFACE_PREMIUM
        rtb.ForeColor = CLR_TEXT_PREMIUM
        rtb.BorderStyle = BorderStyle.None
        rtb.Font = New Font("Consolas", 9.25F)
        btnImprimir.Text = "Vista de impresion"
        btnCerrar.Text = "Cerrar ticket"
        btnImprimir.BackColor = CLR_DARK_PREMIUM
        btnImprimir.ForeColor = Color.White
        btnImprimir.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)
        btnCerrar.BackColor = CLR_PANEL_PREMIUM
        btnCerrar.ForeColor = CLR_TEXT_PREMIUM
        btnCerrar.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)
        Me.Text = tituloVentana
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
            Dim datos = ObtenerDatosTicket(idVenta)
            PintarMetaTicket(datos)
            RenderizarTicket(datos)
        Catch ex As Exception
            MsgBox("Error al generar ticket: " & ex.Message)
        End Try
    End Sub

    Private Shared Function ObtenerDatosTicket(idVenta As Integer) As TicketData
        Dim venta = ObtenerTabla(
            "SELECT Fecha, Total FROM PEDIDOS WHERE Id_Pedido = @id",
            New SqlParameter("@id", idVenta))

        If venta.Rows.Count = 0 Then
            Return New TicketData With {
                .Numero = "V-" & idVenta.ToString("000"),
                .Fecha = "Sin registro",
                .Total = 0D,
                .Detalles = New List(Of TicketDetalle)()
            }
        End If

        Dim datos As New TicketData With {
            .Numero = "V-" & idVenta.ToString("000"),
            .Fecha = ModEstilo.FormatoFechaHora24(CDate(venta.Rows(0)("Fecha"))),
            .Total = CDec(venta.Rows(0)("Total")),
            .Detalles = New List(Of TicketDetalle)()
        }

        Dim dt = ObtenerTabla(
            "SELECT p.NombrePr AS Nombre, d.Cantidad, " &
            "(d.Cantidad * d.PrecioVentaMomento) AS Subtotal " &
            "FROM DET_PEDIDOS d " &
            "INNER JOIN PRODUCTO p ON p.Id_Producto = d.Id_Producto " &
            "WHERE d.Id_Pedido = @id",
            New SqlParameter("@id", idVenta))

        For Each row As DataRow In dt.Rows
            datos.Detalles.Add(New TicketDetalle With {
                .Nombre = row("Nombre").ToString(),
                .Cantidad = CInt(row("Cantidad")),
                .Subtotal = CDec(row("Subtotal"))
            })
        Next

        Return datos
    End Function

    Public Shared Function ObtenerTextoTicket(idVenta As Integer) As String
        Dim datos = ObtenerDatosTicket(idVenta)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("================================")
        sb.AppendLine("             KUMO               ")
        sb.AppendLine("        TICKET DE VENTA         ")
        sb.AppendLine("================================")
        sb.AppendLine("Ticket : " & datos.Numero)
        sb.AppendLine("Fecha  : " & datos.Fecha)
        sb.AppendLine("--------------------------------")
        sb.AppendLine("PRODUCTOS")
        sb.AppendLine("--------------------------------")

        For Each item In datos.Detalles
            Dim nom As String = item.Nombre
            If nom.Length > 18 Then nom = nom.Substring(0, 18)
            sb.AppendLine(nom.PadRight(20) & "x" & item.Cantidad.ToString().PadLeft(2) &
                          " $" & item.Subtotal.ToString("N2").PadLeft(8))
        Next

        sb.AppendLine("--------------------------------")
        sb.AppendLine("TOTAL:".PadRight(24) & "$" & datos.Total.ToString("N2").PadLeft(8))
        sb.AppendLine("================================")
        sb.AppendLine("      gracias por tu compra")
        sb.AppendLine("        vuelve muy pronto")
        Return sb.ToString()
    End Function

    Private Sub PintarMetaTicket(datos As TicketData)
        lblTicketNumero.Text = datos.Numero
        lblTicketFecha.Text = datos.Fecha
        lblTicketTotal.Text = "$" & datos.Total.ToString("N2")
    End Sub

    Private Sub RenderizarTicket(datos As TicketData)
        rtb.Clear()
        rtb.SuspendLayout()

        AppendTexto("KUMO" & Environment.NewLine, New Font("Segoe UI", 17.0F, FontStyle.Bold), CLR_DARK_PREMIUM, HorizontalAlignment.Center)
        AppendTexto("Ticket de venta" & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Regular), CLR_ACCENT_PREMIUM, HorizontalAlignment.Center)
        AppendTexto(Environment.NewLine, rtb.Font, CLR_TEXT_PREMIUM)

        AppendTexto("Resumen" & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Bold), CLR_DARK_PREMIUM)
        AppendTexto("Folio: " & datos.Numero & Environment.NewLine, New Font("Segoe UI", 9.0F), CLR_TEXT_PREMIUM)
        AppendTexto("Fecha: " & datos.Fecha & Environment.NewLine & Environment.NewLine, New Font("Segoe UI", 9.0F), CLR_TEXT_PREMIUM)

        AppendTexto("Productos" & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Bold), CLR_DARK_PREMIUM)

        If datos.Detalles.Count = 0 Then
            AppendTexto("No hay articulos registrados para esta venta." & Environment.NewLine & Environment.NewLine,
                        New Font("Segoe UI", 9.0F, FontStyle.Italic),
                        CLR_TEXT_PREMIUM)
        Else
            For Each item In datos.Detalles
                Dim nombre As String = item.Nombre
                If nombre.Length > 22 Then nombre = nombre.Substring(0, 22) & "..."

                AppendTexto(nombre & Environment.NewLine,
                            New Font("Segoe UI", 9.5F, FontStyle.Bold),
                            CLR_TEXT_PREMIUM)
                AppendTexto("Cantidad " & item.Cantidad.ToString() & "    Importe $" & item.Subtotal.ToString("N2") & Environment.NewLine & Environment.NewLine,
                            New Font("Consolas", 8.75F, FontStyle.Regular),
                            Color.FromArgb(122, 108, 92))
            Next
        End If

        AppendTexto("Total cobrado" & Environment.NewLine, New Font("Segoe UI", 8.5F, FontStyle.Bold), CLR_ACCENT_PREMIUM)
        AppendTexto("$" & datos.Total.ToString("N2") & Environment.NewLine & Environment.NewLine,
                    New Font("Segoe UI", 16.0F, FontStyle.Bold),
                    CLR_DARK_PREMIUM)

        AppendTexto("Gracias por tu compra." & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Bold), CLR_DARK_PREMIUM, HorizontalAlignment.Center)
        AppendTexto("Te esperamos pronto en KUMO.", New Font("Segoe UI", 8.5F), CLR_ACCENT_PREMIUM, HorizontalAlignment.Center)

        rtb.SelectionStart = 0
        rtb.SelectionLength = 0
        rtb.ResumeLayout()
    End Sub

    Private Sub AppendTexto(texto As String,
                            fuente As Font,
                            color As Color,
                            Optional alineacion As HorizontalAlignment = HorizontalAlignment.Left)
        rtb.SelectionStart = rtb.TextLength
        rtb.SelectionLength = 0
        rtb.SelectionFont = fuente
        rtb.SelectionColor = color
        rtb.SelectionAlignment = alineacion
        rtb.AppendText(texto)
    End Sub

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
        pnlMeta.SetBounds(18, 34, gbPreview.Width - 36, 76)
        lblTicketNumeroCaption.Location = New Point(14, 10)
        lblTicketNumero.Location = New Point(14, 26)
        lblTicketFechaCaption.Location = New Point(Math.Max(110, (pnlMeta.Width \ 2) - 70), 10)
        lblTicketFecha.Location = New Point(lblTicketFechaCaption.Left, 29)
        lblTicketTotalCaption.Location = New Point(Math.Max(pnlMeta.Width - 110, 220), 10)
        lblTicketTotal.Location = New Point(lblTicketTotalCaption.Left, 26)
        rtb.SetBounds(18, pnlMeta.Bottom + 16, gbPreview.Width - 36, gbPreview.Height - pnlMeta.Bottom - 34)

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

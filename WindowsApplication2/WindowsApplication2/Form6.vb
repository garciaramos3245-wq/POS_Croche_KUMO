' Genera, muestra e imprime tickets de venta.

Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class Form6

    ' Modelos internos, folio activo y paleta visual del ticket.

    Private idVenta As Integer
    Private statusTicket As StatusStrip
    Private sbTicketInfo As ToolStripStatusLabel
    ' Modelo de una linea del ticket con producto, cantidad e importe.
    Private Class TicketDetalle
        ' Nombre del producto mostrado en el ticket.
        Public Property Nombre As String
        ' Cantidad de piezas vendidas en la linea del ticket.
        Public Property Cantidad As Integer
        ' Subtotal calculado antes de descuentos o como importe de linea.
        Public Property Subtotal As Decimal
    End Class

    ' Modelo completo del ticket con folio, fecha, totales, pago y detalle.
    Private Class TicketData
        ' Folio visible del ticket.
        Public Property Numero As String
        ' Fecha y hora en texto para mostrar o imprimir.
        Public Property Fecha As String
        ' Subtotal calculado antes de descuentos o como importe de linea.
        Public Property Subtotal As Decimal
        ' Descuento total aplicado a la venta.
        Public Property Descuento As Decimal
        ' Importe usado para calcular impuestos.
        Public Property BaseGravable As Decimal
        ' Monto de IVA calculado o guardado.
        Public Property IVA As Decimal
        ' Porcentaje de IVA usado en la venta.
        Public Property TasaIVA As Decimal
        ' Total final cobrado al cliente.
        Public Property Total As Decimal
        ' Forma de pago usada en la venta.
        Public Property MetodoPago As String
        ' Monto recibido o registrado como pago.
        Public Property PagoCon As Decimal
        ' Cambio entregado al cliente.
        Public Property Cambio As Decimal
        ' Indica si la venta fue cancelada despues de registrarse.
        Public Property Cancelada As Boolean
        ' Fecha de cancelacion en texto si existe.
        Public Property FechaCancelacion As String
        ' Lista de productos incluidos en el ticket.
        Public Property Detalles As List(Of TicketDetalle)
    End Class

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)
    Private ReadOnly CLR_ACCENT_PREMIUM As Color = Color.FromArgb(181, 138, 92)

    ' Inicializa el formulario y aplica configuracion visual inicial.
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

    ' Prepara la ventana de ticket y genera la vista previa.
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AplicarDisenoTicket("Ticket de Venta - V-" & idVenta.ToString("000"))
        GenerarTicket()
    End Sub

    ' Aplica estilo visual a encabezado, vista previa, botones y layout.
    Private Sub AplicarDisenoTicket(tituloVentana As String)
        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarBotonPrimario(btnImprimir)
        ModEstilo.EstilarBotonSecundario(btnCerrar)

        Me.BackColor = CLR_BG_PREMIUM
        pnlHeader.BackColor = CLR_PANEL_PREMIUM
        InsertarLogoHeader()
        lblTitulo.ForeColor = CLR_DARK_PREMIUM
        lblTitulo.Text = "KUMO | Ticket"
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
        ConfigurarBarraInferiorTicket()
        Me.Text = tituloVentana
        ConfigurarLayoutTicket()
    End Sub

    ' Crea la barra inferior del ticket y le agrega fecha y hora actual.
    Private Sub ConfigurarBarraInferiorTicket()
        If statusTicket Is Nothing Then
            statusTicket = New StatusStrip() With {
                .Name = "StatusStripTicket",
                .SizingGrip = False,
                .Dock = DockStyle.Bottom
            }
            sbTicketInfo = New ToolStripStatusLabel() With {
                .Name = "sbTicketInfo",
                .Spring = True,
                .Text = "  Ticket"
            }
            statusTicket.Items.Add(sbTicketInfo)
            Me.Controls.Add(statusTicket)
        End If

        statusTicket.BackColor = CLR_DARK_PREMIUM
        sbTicketInfo.ForeColor = Color.White
        sbTicketInfo.Font = New Font("Segoe UI", 8.0F)
        ModEstilo.ConfigurarRelojStatusStrip(Me, statusTicket)
    End Sub

    ' Crea o reutiliza el logo del encabezado y lo carga desde Assets.
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

    ' Obtiene los datos del ticket y los pinta en pantalla.
    Private Sub GenerarTicket()
        Try
            Dim datos = ObtenerDatosTicket(idVenta)
            PintarMetaTicket(datos)
            RenderizarTicket(datos)
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Ticket no disponible", CrearMensajeErrorDatos("cargar el ticket", ex), ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    ' Consulta venta y detalle, corrige valores faltantes y arma el modelo del ticket.
    Private Shared Function ObtenerDatosTicket(idVenta As Integer) As TicketData
        AsegurarColumnasPagoPedido()

        Dim venta = ObtenerTabla(
            "SELECT Fecha, " &
            "ISNULL(Subtotal, Total) AS Subtotal, " &
            "ISNULL(Descuento, 0) AS Descuento, " &
            "ISNULL(BaseGravable, ISNULL(Subtotal, Total) - ISNULL(Descuento, 0)) AS BaseGravable, " &
            "ISNULL(IVA, 0) AS IVA, " &
            "ISNULL(TasaIVA, 0) AS TasaIVA, " &
            "Total, " &
            "ISNULL(MetodoPago, 'Efectivo') AS MetodoPago, " &
            "ISNULL(PagoCon, Total) AS PagoCon, " &
            "ISNULL(Cambio, 0) AS Cambio, " &
            "ISNULL(Cancelada, 0) AS Cancelada, " &
            "FechaCancelacion " &
            "FROM PEDIDOS WHERE Id_Pedido = @id",
            New SqlParameter("@id", idVenta))

        If venta.Rows.Count = 0 Then
            Return New TicketData With {
                .Numero = "V-" & idVenta.ToString("000"),
                .Fecha = "Sin registro",
                .Subtotal = 0D,
                .Descuento = 0D,
                .BaseGravable = 0D,
                .IVA = 0D,
                .TasaIVA = 0D,
                .Total = 0D,
                .MetodoPago = "Efectivo",
                .PagoCon = 0D,
                .Cambio = 0D,
                .Cancelada = False,
                .FechaCancelacion = "",
                .Detalles = New List(Of TicketDetalle)()
            }
        End If

        Dim fechaCancelacion As String = ""
        If Not IsDBNull(venta.Rows(0)("FechaCancelacion")) Then
            fechaCancelacion = ModEstilo.FormatoDiaFechaHora(CDate(venta.Rows(0)("FechaCancelacion")))
        End If

        Dim datos As New TicketData With {
            .Numero = "V-" & idVenta.ToString("000"),
            .Fecha = ModEstilo.FormatoDiaFechaHora(CDate(venta.Rows(0)("Fecha"))),
            .Subtotal = CDec(venta.Rows(0)("Subtotal")),
            .Descuento = CDec(venta.Rows(0)("Descuento")),
            .BaseGravable = CDec(venta.Rows(0)("BaseGravable")),
            .IVA = CDec(venta.Rows(0)("IVA")),
            .TasaIVA = CDec(venta.Rows(0)("TasaIVA")),
            .Total = CDec(venta.Rows(0)("Total")),
            .MetodoPago = venta.Rows(0)("MetodoPago").ToString(),
            .PagoCon = CDec(venta.Rows(0)("PagoCon")),
            .Cambio = CDec(venta.Rows(0)("Cambio")),
            .Cancelada = CBool(venta.Rows(0)("Cancelada")),
            .FechaCancelacion = fechaCancelacion,
            .Detalles = New List(Of TicketDetalle)()
        }

        Dim dt = ObtenerTabla(
            "SELECT p.NombrePr AS Nombre, d.Cantidad, " &
            "(d.Cantidad * d.PrecioVentaMomento) AS Subtotal " &
            "FROM DET_PEDIDOS d " &
            "INNER JOIN PRODUCTO p ON p.Id_Producto = d.Id_Producto " &
            "WHERE d.Id_Pedido = @id",
            New SqlParameter("@id", idVenta))

        Dim sumaDetalles As Decimal = 0D
        For Each row As DataRow In dt.Rows
            Dim subtotalDetalle As Decimal = CDec(row("Subtotal"))
            sumaDetalles += subtotalDetalle
            datos.Detalles.Add(New TicketDetalle With {
                .Nombre = row("Nombre").ToString(),
                .Cantidad = CInt(row("Cantidad")),
                .Subtotal = subtotalDetalle
            })
        Next

        If datos.Subtotal <= 0D AndAlso sumaDetalles > 0D Then datos.Subtotal = sumaDetalles
        If datos.BaseGravable <= 0D Then datos.BaseGravable = Math.Max(0D, datos.Subtotal - datos.Descuento)
        If datos.IVA < 0D Then datos.IVA = 0D
        If datos.TasaIVA <= 0D AndAlso datos.IVA > 0D AndAlso datos.BaseGravable > 0D Then
            datos.TasaIVA = Math.Round((datos.IVA / datos.BaseGravable) * 100D, 2)
        End If
        If datos.Descuento <= 0D Then datos.Descuento = Math.Max(0D, datos.Subtotal - datos.BaseGravable)
        If datos.PagoCon <= 0D Then datos.PagoCon = datos.Total
        If datos.Cambio < 0D Then datos.Cambio = 0D

        Return datos
    End Function

    ' Construye el texto plano del ticket para imprimirlo.
    Public Shared Function ObtenerTextoTicket(idVenta As Integer) As String
        Dim datos = ObtenerDatosTicket(idVenta)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("================================")
        sb.AppendLine("             KUMO               ")
        sb.AppendLine("        TICKET DE VENTA         ")
        sb.AppendLine("================================")
        sb.AppendLine("Ticket : " & datos.Numero)
        sb.AppendLine("Fecha  : " & datos.Fecha)
        If datos.Cancelada Then
            sb.AppendLine("Estado : CANCELADA")
            If datos.FechaCancelacion <> "" Then sb.AppendLine("Cancel.: " & datos.FechaCancelacion)
        End If
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
        sb.AppendLine(FormatearLineaTicket("SUBTOTAL:", datos.Subtotal))
        sb.AppendLine(FormatearLineaTicket("DESCUENTO:", datos.Descuento, "-$"))
        sb.AppendLine(FormatearLineaTicket("BASE IVA:", datos.BaseGravable))
        sb.AppendLine("--------------------------------")
        sb.AppendLine("TRASLACION DE IMPUESTOS")
        sb.AppendLine(FormatearLineaTicket("IVA " & datos.TasaIVA.ToString("N0") & "%:", datos.IVA))
        sb.AppendLine("--------------------------------")
        sb.AppendLine(FormatearLineaTicket("TOTAL A PAGAR:", datos.Total))
        sb.AppendLine("METODO:".PadRight(20) & datos.MetodoPago.PadLeft(12))
        sb.AppendLine(FormatearLineaTicket("PAGO CON:", datos.PagoCon))
        sb.AppendLine(FormatearLineaTicket("CAMBIO:", datos.Cambio))
        sb.AppendLine("================================")
        sb.AppendLine("      gracias por tu compra")
        sb.AppendLine("        vuelve muy pronto")
        Return sb.ToString()
    End Function

    ' Alinea etiqueta e importe dentro del ancho del ticket.
    Private Shared Function FormatearLineaTicket(etiqueta As String, monto As Decimal, Optional prefijo As String = "$") As String
        Dim textoMonto As String = prefijo & monto.ToString("N2")
        Return etiqueta.PadRight(20) & textoMonto.PadLeft(12)
    End Function

    ' Valida impresora, arma el PrintDocument y abre vista previa.
    Public Shared Function MostrarVistaPreviaTicket(texto As String,
                                                    owner As IWin32Window,
                                                    Optional titulo As String = "Ticket de venta") As Boolean
        If String.IsNullOrWhiteSpace(texto) Then
            ModMensajes.Mostrar(owner, "Ticket vacio", "No hay informacion para imprimir en el ticket.", ModMensajes.TipoAviso.Advertencia)
            Return False
        End If

        Try
            If PrinterSettings.InstalledPrinters.Count = 0 Then
                ModMensajes.Mostrar(owner, "Impresora no disponible", "Windows no tiene impresoras instaladas. Agrega una impresora o selecciona Microsoft Print to PDF como predeterminada.", ModMensajes.TipoAviso.Advertencia)
                Return False
            End If

            Using pd As New PrintDocument()
                pd.DocumentName = titulo

                Dim impresoraValida As Boolean = pd.PrinterSettings IsNot Nothing AndAlso pd.PrinterSettings.IsValid
                If Not impresoraValida Then
                    For Each impresora As String In PrinterSettings.InstalledPrinters
                        pd.PrinterSettings.PrinterName = impresora
                        If pd.PrinterSettings.IsValid Then
                            impresoraValida = True
                            Exit For
                        End If
                    Next
                End If

                If Not impresoraValida Then
                    ModMensajes.Mostrar(owner, "Impresora no disponible", "La impresora predeterminada no esta disponible. Revisa que este encendida o cambia la impresora predeterminada en Windows.", ModMensajes.TipoAviso.Advertencia)
                    Return False
                End If

                pd.DefaultPageSettings.Margins = New Margins(10, 10, 10, 10)

                AddHandler pd.PrintPage,
                    Sub(s, ev)
                        Using fuente As New Font("Courier New", 8)
                            ev.Graphics.DrawString(texto, fuente, Brushes.Black, ev.MarginBounds.Left, ev.MarginBounds.Top)
                        End Using
                    End Sub

                Using preview As New PrintPreviewDialog()
                    preview.Document = pd
                    preview.Text = titulo
                    preview.StartPosition = FormStartPosition.CenterParent
                    preview.Width = 900
                    preview.Height = 700

                    If owner Is Nothing Then
                        preview.ShowDialog()
                    Else
                        preview.ShowDialog(owner)
                    End If
                End Using
            End Using

            Return True
        Catch ex As InvalidPrinterException
            ModMensajes.Mostrar(owner, "Impresora no disponible", "No se encontro una impresora valida. Revisa la impresora predeterminada de Windows." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
        Catch ex As Exception
            ModMensajes.Mostrar(owner, "Error de impresion", "No se pudo preparar la impresion del ticket." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
        End Try

        Return False
    End Function

    ' Muestra folio, fecha y total en la cabecera de la vista previa.
    Private Sub PintarMetaTicket(datos As TicketData)
        lblTicketNumero.Text = datos.Numero
        lblTicketFecha.Text = datos.Fecha
        lblTicketTotal.Text = "$" & datos.Total.ToString("N2")
    End Sub

    ' Dibuja el ticket con formato enriquecido dentro del RichTextBox.
    Private Sub RenderizarTicket(datos As TicketData)
        rtb.Clear()
        rtb.SuspendLayout()

        AppendTexto("KUMO" & Environment.NewLine, New Font("Segoe UI", 17.0F, FontStyle.Bold), CLR_DARK_PREMIUM, HorizontalAlignment.Center)
        AppendTexto("Ticket de venta" & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Regular), CLR_ACCENT_PREMIUM, HorizontalAlignment.Center)
        AppendTexto(Environment.NewLine, rtb.Font, CLR_TEXT_PREMIUM)

        AppendTexto("Resumen" & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Bold), CLR_DARK_PREMIUM)
        AppendTexto("Folio: " & datos.Numero & Environment.NewLine, New Font("Segoe UI", 9.0F), CLR_TEXT_PREMIUM)
        If datos.Cancelada Then
            AppendTexto("Estado: Cancelada" & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Bold), Color.FromArgb(154, 73, 64))
            If datos.FechaCancelacion <> "" Then
                AppendTexto("Cancelada: " & datos.FechaCancelacion & Environment.NewLine, New Font("Segoe UI", 9.0F), Color.FromArgb(154, 73, 64))
            End If
        End If
        AppendTexto("Metodo: " & datos.MetodoPago & Environment.NewLine, New Font("Segoe UI", 9.0F), CLR_TEXT_PREMIUM)
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

        AppendTexto("Totales" & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Bold), CLR_DARK_PREMIUM)
        AppendTexto("Subtotal $" & datos.Subtotal.ToString("N2") & Environment.NewLine &
                    "Descuento -$" & datos.Descuento.ToString("N2") & Environment.NewLine &
                    "Base gravable $" & datos.BaseGravable.ToString("N2") & Environment.NewLine,
                    New Font("Consolas", 8.75F, FontStyle.Regular),
                    Color.FromArgb(122, 108, 92))

        AppendTexto("Traslacion de impuestos" & Environment.NewLine, New Font("Segoe UI", 8.5F, FontStyle.Bold), CLR_ACCENT_PREMIUM)
        AppendTexto("IVA " & datos.TasaIVA.ToString("N0") & "% $" & datos.IVA.ToString("N2") & Environment.NewLine & Environment.NewLine,
                    New Font("Consolas", 8.75F, FontStyle.Bold),
                    Color.FromArgb(74, 133, 95))

        AppendTexto("Total cobrado" & Environment.NewLine, New Font("Segoe UI", 8.5F, FontStyle.Bold), CLR_ACCENT_PREMIUM)
        AppendTexto("$" & datos.Total.ToString("N2") & Environment.NewLine & Environment.NewLine,
                    New Font("Segoe UI", 16.0F, FontStyle.Bold),
                    CLR_DARK_PREMIUM)

        AppendTexto("Metodo " & datos.MetodoPago & "    Pago $" & datos.PagoCon.ToString("N2") & "    Cambio $" & datos.Cambio.ToString("N2") & Environment.NewLine & Environment.NewLine,
                    New Font("Consolas", 8.75F, FontStyle.Bold),
                    Color.FromArgb(122, 108, 92))

        AppendTexto("Gracias por tu compra." & Environment.NewLine, New Font("Segoe UI", 9.0F, FontStyle.Bold), CLR_DARK_PREMIUM, HorizontalAlignment.Center)
        AppendTexto("Te esperamos pronto en KUMO.", New Font("Segoe UI", 8.5F), CLR_ACCENT_PREMIUM, HorizontalAlignment.Center)

        rtb.SelectionStart = 0
        rtb.SelectionLength = 0
        rtb.ResumeLayout()
    End Sub

    ' Agrega texto al RichTextBox con fuente, color y alineacion indicados.
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

    ' Abre la vista previa de impresion del ticket actual.
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim texto As String = rtb.Text
        If idVenta > 0 Then texto = ObtenerTextoTicket(idVenta)

        MostrarVistaPreviaTicket(texto, Me, "Ticket de venta V-" & idVenta.ToString("000"))
    End Sub

    ' Cierra la ventana del ticket.
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    ' Acomoda tarjeta de vista previa y botones del ticket.
    Private Sub ConfigurarLayoutTicket()
        Dim margen As Integer = 24
        Dim anchoCard As Integer = Math.Min(620, Me.ClientSize.Width - (margen * 2))
        Dim altoStatus As Integer = If(statusTicket Is Nothing, 0, statusTicket.Height)
        Dim altoCard As Integer = Me.ClientSize.Height - pnlHeader.Height - pnlLinea.Height - altoStatus - 160
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

    ' Reacomoda la vista del ticket al cambiar tamano.
    Private Sub Form6_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutTicket()
    End Sub

End Class

' Administra pedidos especiales y su informacion de cliente, entrega, saldo y estado.

Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class Form5

    ' Paleta visual y folio seleccionado para editar pedidos.

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_MUTED_PREMIUM As Color = Color.FromArgb(136, 118, 94)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)

    ' Inicializa el formulario y aplica configuracion visual inicial.
    Public Sub New()
        InitializeComponent()
        ConfigurarSelectorEntrega()
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                If ModEstilo.EstaEnModoDisenio(Me) Then
                    ModEstilo.PrepararVentana(Me)
                End If
                If cbEstado.Items.Count > 0 Then
                    cbEstado.SelectedIndex = 0
                End If
                AplicarDisenoPedidos()
            End Sub)
    End Sub

    Private idSeleccionado As Integer = 0
    Private lblHoraTxt As Label
    Private dtpHoraEntrega As DateTimePicker

    ' Prepara la ventana, escucha cambios de pedidos y carga la agenda.
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AddHandler ModActualizaciones.PedidosActualizados, AddressOf RefrescarPedidos
        ConfigurarSelectorEntrega()
        cbEstado.SelectedIndex = 0
        CargarPedidos()
        AplicarDisenoPedidos()
    End Sub

    ' Aplica estilos y layout al formulario de pedidos.
    Private Sub AplicarDisenoPedidos()
        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarStatusStrip(StatusStrip1)
        ModEstilo.EstilarBotonPrimario(btnGuardar)
        ModEstilo.EstilarBotonPeligro(btnEliminar)
        ModEstilo.EstilarBotonSecundario(btnNuevo)
        ModEstilo.EstilarBotonSecundario(btnCargar)
        ModEstilo.EstilarBotonPeligro(btnRegresar)
        ModEstilo.ConfigurarRelojStatusStrip(Me, StatusStrip1)
        AplicarEstiloPedidosPremium()
        ConfigurarLayoutPedidos()
    End Sub

    ' Configura textos, colores, campos, tabla y botones de pedidos.
    Private Sub AplicarEstiloPedidosPremium()
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Pedidos"

        gbForm.BackColor = CLR_SURFACE_PREMIUM
        gbForm.ForeColor = CLR_TEXT_PREMIUM
        gbForm.Text = "Pedido especial"

        gbLista.BackColor = CLR_PANEL_PREMIUM
        gbLista.ForeColor = CLR_TEXT_PREMIUM
        gbLista.Text = "Agenda de pedidos"

        ConfigurarSelectorEntrega()

        For Each lbl As Label In New Label() {lblNombreTxt, lblTelTxt, lblDescTxt, lblColTxt, lblMedTxt, lblNotasTxt, lblPrecioTxt, lblAnticTxt, lblSaldoTxt, lblFechaTxt, lblHoraTxt, lblEstadoTxt}
            lbl.ForeColor = CLR_MUTED_PREMIUM
            lbl.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Next

        lblNombreTxt.Text = "Cliente"
        lblTelTxt.Text = "Telefono"
        lblDescTxt.Text = "Descripcion"
        lblColTxt.Text = "Paleta"
        lblMedTxt.Text = "Medidas"
        lblNotasTxt.Text = "Notas"
        lblPrecioTxt.Text = "Precio final"
        lblAnticTxt.Text = "Anticipo"
        lblSaldoTxt.Text = "Saldo"
        lblFechaTxt.Text = "Dia de entrega"
        lblHoraTxt.Text = "Hora"
        lblEstadoTxt.Text = "Estado"

        For Each tb As TextBox In New TextBox() {txtNombre, txtTel, txtDesc, txtColores, txtMedidas, txtNotas, txtPrecio, txtAnticipo, txtSaldo}
            tb.BackColor = CLR_SURFACE_PREMIUM
            tb.ForeColor = CLR_TEXT_PREMIUM
            tb.BorderStyle = BorderStyle.FixedSingle
            tb.Font = New Font("Segoe UI", 10.0F)
        Next

        cbEstado.BackColor = CLR_SURFACE_PREMIUM
        cbEstado.ForeColor = CLR_TEXT_PREMIUM
        cbEstado.FlatStyle = FlatStyle.Flat
        cbEstado.Font = New Font("Segoe UI", 9.5F)

        dtpEntrega.Font = New Font("Segoe UI", 9.5F)
        dtpEntrega.CalendarMonthBackground = CLR_SURFACE_PREMIUM
        dtpHoraEntrega.Font = New Font("Segoe UI", 9.5F)
        dtpHoraEntrega.CalendarMonthBackground = CLR_SURFACE_PREMIUM

        dgv.BackgroundColor = CLR_SURFACE_PREMIUM
        dgv.BorderStyle = BorderStyle.None
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = Color.FromArgb(229, 217, 201)
        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersDefaultCellStyle.BackColor = CLR_DARK_PREMIUM
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = CLR_DARK_PREMIUM
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
        dgv.DefaultCellStyle.BackColor = CLR_SURFACE_PREMIUM
        dgv.DefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgv.DefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 248, 242)
        dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgv.RowTemplate.Height = 32

        btnGuardar.Text = "Guardar pedido"
        btnNuevo.Text = "+ Nuevo"
        btnEliminar.Text = "Eliminar"
        btnCargar.Text = "Cargar seleccionado"
        btnRegresar.Text = "Cerrar"

        btnGuardar.BackColor = Color.FromArgb(74, 133, 95)
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 111, 78)

        btnNuevo.BackColor = CLR_PANEL_PREMIUM
        btnNuevo.ForeColor = CLR_TEXT_PREMIUM
        btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(214, 189, 150)
        btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnCargar.BackColor = CLR_DARK_PREMIUM
        btnCargar.ForeColor = Color.White
        btnCargar.FlatAppearance.BorderColor = CLR_DARK_PREMIUM
        btnCargar.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)

        btnEliminar.BackColor = Color.FromArgb(154, 73, 64)
        btnEliminar.FlatAppearance.MouseOverBackColor = Color.FromArgb(133, 61, 53)

        btnRegresar.BackColor = CLR_DARK_PREMIUM
        btnRegresar.ForeColor = Color.FromArgb(244, 226, 193)
        btnRegresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 64, 73)
    End Sub

    ' Separa la captura de dia y hora sin cambiar el campo guardado.
    Private Sub ConfigurarSelectorEntrega()
        If lblHoraTxt Is Nothing Then
            lblHoraTxt = New Label() With {
                .Name = "lblHoraTxt",
                .AutoSize = True,
                .BackColor = Color.Transparent,
                .Text = "Hora"
            }
            gbForm.Controls.Add(lblHoraTxt)
        End If

        If dtpHoraEntrega Is Nothing Then
            dtpHoraEntrega = New DateTimePicker() With {
                .Name = "dtpHoraEntrega",
                .ShowUpDown = True
            }
            gbForm.Controls.Add(dtpHoraEntrega)
        End If

        dtpEntrega.Format = DateTimePickerFormat.Custom
        dtpEntrega.CustomFormat = "dddd dd/MM/yyyy"
        dtpHoraEntrega.Format = DateTimePickerFormat.Custom
        dtpHoraEntrega.CustomFormat = "h:mm tt"
    End Sub

    ' Acomoda formulario de pedido y lista segun el tamano disponible.
    Private Sub ConfigurarLayoutPedidos()
        Dim margen As Integer = 18
        Dim top As Integer = 14
        Dim altoBoton As Integer = 42
        Dim panelDerecho As Integer = Math.Max(420, Math.Min(520, CInt(Me.ClientSize.Width * 0.33)))
        Dim anchoIzquierdo As Integer = Me.ClientSize.Width - panelDerecho - (margen * 3)
        Dim yBloques As Integer = top + altoBoton + 14
        Dim altoDisponible As Integer = Me.ClientSize.Height - StatusStrip1.Height - yBloques - margen

        btnGuardar.SetBounds(margen, top, 128, altoBoton)
        btnNuevo.SetBounds(btnGuardar.Right + 12, top, 110, altoBoton)
        btnEliminar.SetBounds(btnNuevo.Right + 12, top, 128, altoBoton)
        btnRegresar.SetBounds(Me.ClientSize.Width - margen - 122, top, 122, altoBoton)

        gbForm.SetBounds(margen, yBloques, anchoIzquierdo, altoDisponible)
        gbLista.SetBounds(gbForm.Right + margen, yBloques, panelDerecho, altoDisponible)

        Dim pad As Integer = 18
        Dim espacioCol As Integer = 18
        Dim anchoMitad As Integer = (gbForm.Width - (pad * 2) - espacioCol) \ 2
        Dim anchoTercio As Integer = (gbForm.Width - (pad * 2) - (espacioCol * 2)) \ 3
        Dim y As Integer = 38

        lblNombreTxt.Location = New Point(pad, y)
        lblTelTxt.Location = New Point(pad + anchoMitad + espacioCol, y)
        txtNombre.SetBounds(pad, y + 24, anchoMitad, 34)
        txtTel.SetBounds(pad + anchoMitad + espacioCol, y + 24, anchoMitad, 34)

        y += 72
        lblDescTxt.Location = New Point(pad, y)
        txtDesc.SetBounds(pad, y + 24, gbForm.Width - (pad * 2), 34)

        y += 72
        lblColTxt.Location = New Point(pad, y)
        lblMedTxt.Location = New Point(pad + anchoMitad + espacioCol, y)
        txtColores.SetBounds(pad, y + 24, anchoMitad, 34)
        txtMedidas.SetBounds(pad + anchoMitad + espacioCol, y + 24, anchoMitad, 34)

        y += 72
        lblNotasTxt.Location = New Point(pad, y)
        txtNotas.SetBounds(pad, y + 24, gbForm.Width - (pad * 2), 34)

        y += 72
        lblPrecioTxt.Location = New Point(pad, y)
        lblAnticTxt.Location = New Point(pad + anchoTercio + espacioCol, y)
        lblSaldoTxt.Location = New Point(pad + (anchoTercio * 2) + (espacioCol * 2), y)
        txtPrecio.SetBounds(pad, y + 24, anchoTercio, 34)
        txtAnticipo.SetBounds(pad + anchoTercio + espacioCol, y + 24, anchoTercio, 34)
        txtSaldo.SetBounds(pad + (anchoTercio * 2) + (espacioCol * 2), y + 24, anchoTercio, 34)

        y += 78
        lblFechaTxt.Location = New Point(pad, y)
        lblHoraTxt.Location = New Point(pad + anchoTercio + espacioCol, y)
        lblEstadoTxt.Location = New Point(pad + (anchoTercio * 2) + (espacioCol * 2), y)
        dtpEntrega.SetBounds(pad, y + 24, anchoTercio, 34)
        dtpHoraEntrega.SetBounds(pad + anchoTercio + espacioCol, y + 24, anchoTercio, 34)
        cbEstado.SetBounds(pad + (anchoTercio * 2) + (espacioCol * 2), y + 24, anchoTercio, 34)

        dgv.SetBounds(14, 32, gbLista.Width - 28, gbLista.Height - 96)
        btnCargar.SetBounds(14, gbLista.Height - 48, gbLista.Width - 28, 34)

        gbForm.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbLista.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        dgv.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnCargar.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        btnRegresar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    ' Actualiza el saldo cuando cambia el precio final.
    Private Sub txtPrecio_TextChanged(sender As Object, e As EventArgs) Handles txtPrecio.TextChanged
        CalcSaldo()
    End Sub

    ' Actualiza el saldo cuando cambia el anticipo.
    Private Sub txtAnticipo_TextChanged(sender As Object, e As EventArgs) Handles txtAnticipo.TextChanged
        CalcSaldo()
    End Sub

    ' Calcula saldo pendiente restando anticipo al precio.
    Private Sub CalcSaldo()
        Dim p As Decimal = LeerDecimal(txtPrecio.Text)
        Dim a As Decimal = LeerDecimal(txtAnticipo.Text)
        txtSaldo.Text = (p - a).ToString("N2")
    End Sub

    ' Carga la agenda de pedidos desde PEDIDOS y CLIENTES.
    Private Sub CargarPedidos()
        Try
            AsegurarColumnasDetallePedido()

            Dim dt = ObtenerTabla(
                "SELECT p.Id_Pedido AS ID_Pedido, " &
                "RTRIM(c.Nombres_cl + ' ' + ISNULL(c.Apellidos,'')) AS Cliente, " &
                "LOWER(FORMAT(p.Fecha, 'dddd', 'es-MX')) AS Dia, " &
                "CONVERT(varchar, p.Fecha, 103) AS Entrega, " &
                "LOWER(REPLACE(REPLACE(FORMAT(p.Fecha, 'h:mm tt', 'en-US'), 'AM', 'a.m.'), 'PM', 'p.m.')) AS Hora " &
                "FROM PEDIDOS p " &
                "INNER JOIN CLIENTES c ON c.ID_CLIENTE = p.ID_CLIENTE " &
                "WHERE NOT EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido) " &
                "ORDER BY p.Fecha DESC")

            dgv.DataSource = dt
            If dgv.Columns.Contains("ID_Pedido") Then dgv.Columns("ID_Pedido").Visible = False
            FormatearColumnasPedidos()
            sbInfo.Text = "  " & dt.Rows.Count & " pedidos registrados"
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Pedidos no disponibles", "No se pudieron cargar los pedidos." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    ' Ajusta formato de importes y tamanos utiles en la agenda.
    Private Sub FormatearColumnasPedidos()
        If dgv.Columns.Contains("Cliente") Then dgv.Columns("Cliente").MinimumWidth = 150
        If dgv.Columns.Contains("Dia") Then dgv.Columns("Dia").MinimumWidth = 105
        If dgv.Columns.Contains("Entrega") Then dgv.Columns("Entrega").MinimumWidth = 90
        If dgv.Columns.Contains("Hora") Then dgv.Columns("Hora").MinimumWidth = 90
    End Sub

    ' Carga el detalle del pedido con doble clic en la tabla.
    Private Sub dgv_DoubleClick(sender As Object, e As EventArgs) Handles dgv.DoubleClick
        CargarDetalle()
    End Sub

    ' Carga el detalle del pedido seleccionado.
    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        CargarDetalle()
    End Sub

    ' Consulta el detalle de productos de la venta seleccionada.
    Private Sub CargarDetalle()
        If dgv.CurrentRow Is Nothing Then Return
        idSeleccionado = CInt(dgv.CurrentRow.Cells("ID_Pedido").Value)

        Try
            AsegurarColumnasDetallePedido()

            Dim dt = ObtenerTabla(
                "SELECT p.*, c.Nombres_cl, c.Apellidos, c.Telefono " &
                "FROM PEDIDOS p " &
                "INNER JOIN CLIENTES c ON c.ID_CLIENTE = p.ID_CLIENTE " &
                "WHERE p.Id_Pedido = @id " &
                "AND NOT EXISTS (SELECT 1 FROM DET_PEDIDOS d WHERE d.Id_Pedido = p.Id_Pedido)",
                New SqlParameter("@id", idSeleccionado))

            If dt.Rows.Count = 0 Then Return

            Dim row = dt.Rows(0)
            Dim total As Decimal = ValorDecimal(row, "Total")
            Dim anticipo As Decimal = ValorDecimal(row, "Anticipo")
            Dim saldo As Decimal = ValorDecimal(row, "Saldo")
            If saldo = 0D AndAlso total <> 0D Then saldo = total - anticipo

            txtNombre.Text = (row("Nombres_cl").ToString() & " " & row("Apellidos").ToString()).Trim()
            txtTel.Text = row("Telefono").ToString()
            txtDesc.Text = TextoColumna(row, "DescripcionPedido")
            txtColores.Text = TextoColumna(row, "Colores")
            txtMedidas.Text = TextoColumna(row, "Medidas")
            txtNotas.Text = TextoColumna(row, "Notas")
            txtPrecio.Text = total.ToString("N2")
            txtAnticipo.Text = anticipo.ToString("N2")
            txtSaldo.Text = saldo.ToString("N2")

            If Not IsDBNull(row("Fecha")) Then
                AplicarFechaHoraEntrega(CDate(row("Fecha")))
            End If

            Dim estado As String = If(IsDBNull(row("MetodoPago")), "Pendiente", row("MetodoPago").ToString())
            Dim idx As Integer = cbEstado.Items.IndexOf(estado)
            If idx >= 0 Then
                cbEstado.SelectedIndex = idx
            Else
                cbEstado.SelectedIndex = 0
            End If

        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Detalle no disponible", "No se pudo cargar el detalle del pedido." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
        End Try
    End Sub

    ' Lee texto opcional de una columna de pedido sin mostrar valores nulos.
    Private Function TextoColumna(row As DataRow, columna As String) As String
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columna) OrElse IsDBNull(row(columna)) Then Return ""
        Return row(columna).ToString()
    End Function

    ' Lee importes opcionales de una columna de pedido.
    Private Function ValorDecimal(row As DataRow, columna As String) As Decimal
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columna) OrElse IsDBNull(row(columna)) Then Return 0D
        Return Convert.ToDecimal(row(columna))
    End Function

    ' Interpreta importes capturados aunque usen simbolos o separadores locales.
    Private Function LeerDecimal(texto As String) As Decimal
        Dim valor As Decimal
        Dim limpio As String = If(texto, "").Trim()

        If Decimal.TryParse(limpio, NumberStyles.Currency, CultureInfo.CurrentCulture, valor) Then Return valor
        If Decimal.TryParse(limpio, NumberStyles.Currency, CultureInfo.InvariantCulture, valor) Then Return valor

        Return 0D
    End Function

    ' Combina el dia y la hora separados en el valor unico que usa la base.
    Private Function ObtenerFechaHoraEntrega() As DateTime
        Dim horaBase As DateTime = If(dtpHoraEntrega Is Nothing, dtpEntrega.Value, dtpHoraEntrega.Value)
        Return dtpEntrega.Value.Date.Add(New TimeSpan(horaBase.Hour, horaBase.Minute, 0))
    End Function

    ' Al cargar un pedido reparte el valor guardado entre los dos controles.
    Private Sub AplicarFechaHoraEntrega(fechaHora As DateTime)
        dtpEntrega.Value = fechaHora
        If dtpHoraEntrega IsNot Nothing Then dtpHoraEntrega.Value = fechaHora
    End Sub

    ' Limpia la ficha para capturar un producto nuevo.
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        idSeleccionado = 0
        txtNombre.Clear()
        txtTel.Clear()
        txtDesc.Clear()
        txtColores.Clear()
        txtMedidas.Clear()
        txtNotas.Clear()
        txtPrecio.Clear()
        txtAnticipo.Clear()
        txtSaldo.Clear()
        dtpEntrega.Value = DateTime.Now
        If dtpHoraEntrega IsNot Nothing Then dtpHoraEntrega.Value = DateTime.Now
        cbEstado.SelectedIndex = 0
        dgv.ClearSelection()
        txtNombre.Focus()
    End Sub

    ' Normaliza el estado o metodo guardado para el pedido.
    Private Function ObtenerMetodoPedido() As String
        Dim metodo As String = cbEstado.Text.Trim()
        If metodo = "" Then metodo = "Pendiente"
        If metodo.Length > 20 Then metodo = metodo.Substring(0, 20)
        Return metodo
    End Function

    ' Agrega texto largo de pedido y permite limpiar el dato guardando nulo.
    Private Sub AgregarParametroTexto(cmd As SqlCommand, nombre As String, valor As String)
        Dim limpio As String = If(valor, "").Trim()
        Dim parametro = cmd.Parameters.Add(nombre, SqlDbType.NVarChar, -1)
        parametro.Value = If(limpio = "", CType(DBNull.Value, Object), limpio)
    End Sub

    ' Agrega importes con precision consistente para SQL Server.
    Private Sub AgregarParametroDecimal(cmd As SqlCommand, nombre As String, valor As Decimal)
        Dim parametro = cmd.Parameters.Add(nombre, SqlDbType.Decimal)
        parametro.Precision = 10
        parametro.Scale = 2
        parametro.Value = valor
    End Sub

    ' Valida y guarda un producto nuevo o actualiza uno existente junto con su stock.
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txtNombre.Text.Trim() = "" Then
            ModMensajes.Mostrar(Me, "Dato faltante", "Escribe el nombre del cliente antes de guardar.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        Dim total As Decimal = LeerDecimal(txtPrecio.Text)
        Dim anticipo As Decimal = LeerDecimal(txtAnticipo.Text)
        Dim saldo As Decimal = total - anticipo
        txtSaldo.Text = saldo.ToString("N2")

        Try
            AsegurarColumnasDetallePedido()
        Catch ex As Exception
            ModMensajes.Mostrar(Me, "Base no disponible", "No se pudieron preparar los campos del pedido." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
            Return
        End Try

        Using cn = ObtenerConexion()
            cn.Open()
            Dim trans = cn.BeginTransaction()

            Try
                Dim idCliente = ObtenerIdCliente(txtNombre.Text.Trim(), txtTel.Text.Trim(), trans)
                Dim metodo = ObtenerMetodoPedido()

                If idSeleccionado = 0 Then
                    Using cmd As New SqlCommand(
                        "INSERT INTO PEDIDOS (ID_CLIENTE, Fecha, Total, MetodoPago, DescripcionPedido, Colores, Medidas, Notas, Anticipo, Saldo) " &
                        "VALUES (@idCliente, @fecha, @total, @metodo, @descripcion, @colores, @medidas, @notas, @anticipo, @saldo)",
                        cn,
                        trans)
                        cmd.Parameters.AddWithValue("@idCliente", idCliente)
                        cmd.Parameters.AddWithValue("@fecha", ObtenerFechaHoraEntrega())
                        AgregarParametroDecimal(cmd, "@total", total)
                        cmd.Parameters.AddWithValue("@metodo", metodo)
                        AgregarParametroTexto(cmd, "@descripcion", txtDesc.Text)
                        AgregarParametroTexto(cmd, "@colores", txtColores.Text)
                        AgregarParametroTexto(cmd, "@medidas", txtMedidas.Text)
                        AgregarParametroTexto(cmd, "@notas", txtNotas.Text)
                        AgregarParametroDecimal(cmd, "@anticipo", anticipo)
                        AgregarParametroDecimal(cmd, "@saldo", saldo)
                        cmd.ExecuteNonQuery()
                    End Using
                Else
                    Using cmd As New SqlCommand(
                        "UPDATE PEDIDOS SET ID_CLIENTE = @idCliente, Fecha = @fecha, " &
                        "Total = @total, MetodoPago = @metodo, DescripcionPedido = @descripcion, " &
                        "Colores = @colores, Medidas = @medidas, Notas = @notas, Anticipo = @anticipo, Saldo = @saldo " &
                        "WHERE Id_Pedido = @idPedido",
                        cn,
                        trans)
                        cmd.Parameters.AddWithValue("@idCliente", idCliente)
                        cmd.Parameters.AddWithValue("@fecha", ObtenerFechaHoraEntrega())
                        AgregarParametroDecimal(cmd, "@total", total)
                        cmd.Parameters.AddWithValue("@metodo", metodo)
                        AgregarParametroTexto(cmd, "@descripcion", txtDesc.Text)
                        AgregarParametroTexto(cmd, "@colores", txtColores.Text)
                        AgregarParametroTexto(cmd, "@medidas", txtMedidas.Text)
                        AgregarParametroTexto(cmd, "@notas", txtNotas.Text)
                        AgregarParametroDecimal(cmd, "@anticipo", anticipo)
                        AgregarParametroDecimal(cmd, "@saldo", saldo)
                        cmd.Parameters.AddWithValue("@idPedido", idSeleccionado)
                        cmd.ExecuteNonQuery()
                    End Using
                End If

                trans.Commit()
                ModMensajes.Mostrar(Me, "Pedido guardado", If(idSeleccionado = 0, "Pedido guardado correctamente.", "Pedido actualizado correctamente."), ModMensajes.TipoAviso.Exito)
                ModActualizaciones.NotificarPedidosActualizados()

            Catch ex As Exception
                trans.Rollback()
                ModMensajes.Mostrar(Me, "No se pudo guardar", "No se guardo el pedido." & vbCrLf & "Detalle: " & ex.Message, ModMensajes.TipoAviso.Error)
            End Try
        End Using

        CargarPedidos()
        btnNuevo_Click(Nothing, Nothing)
    End Sub

    ' Confirma y elimina el producto seleccionado junto con su inventario.
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If idSeleccionado = 0 Then
            ModMensajes.Mostrar(Me, "Selecciona un pedido", "Elige un pedido de la lista antes de eliminarlo.", ModMensajes.TipoAviso.Advertencia)
            Return
        End If

        If Not ModMensajes.Confirmar(Me, "Eliminar pedido", "Deseas eliminar el pedido de " & txtNombre.Text & "?", "Eliminar", "Cancelar", ModMensajes.TipoAviso.Advertencia) Then Return

        Using cn = ObtenerConexion()
            cn.Open()
            Using cmdDet As New SqlCommand("DELETE FROM DET_PEDIDOS WHERE Id_Pedido = @id", cn)
                cmdDet.Parameters.AddWithValue("@id", idSeleccionado)
                cmdDet.ExecuteNonQuery()
            End Using
            Using cmd As New SqlCommand("DELETE FROM PEDIDOS WHERE Id_Pedido = @id", cn)
                cmd.Parameters.AddWithValue("@id", idSeleccionado)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ModMensajes.Mostrar(Me, "Pedido eliminado", "El pedido se elimino correctamente.", ModMensajes.TipoAviso.Exito)
        ModActualizaciones.NotificarPedidosActualizados()
        CargarPedidos()
        btnNuevo_Click(Nothing, Nothing)
    End Sub

    ' Cierra el formulario actual.
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Close()
    End Sub

    ' Recarga pedidos despues de cambios externos.
    Private Sub RefrescarPedidos()
        If Me.IsDisposed Then Return
        CargarPedidos()
    End Sub

    ' Quita la suscripcion al evento de pedidos al cerrar.
    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.PedidosActualizados, AddressOf RefrescarPedidos
    End Sub

    ' Reacomoda pedidos al cambiar el tamano.
    Private Sub Form5_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutPedidos()
    End Sub
End Class

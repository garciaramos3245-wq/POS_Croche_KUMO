Imports System.Runtime.InteropServices
Imports System.Data.SqlClient

Public Class Form3

    Private ReadOnly CLR_BG_PREMIUM As Color = Color.FromArgb(244, 240, 234)
    Private ReadOnly CLR_SURFACE_PREMIUM As Color = Color.FromArgb(255, 252, 247)
    Private ReadOnly CLR_PANEL_PREMIUM As Color = Color.FromArgb(247, 241, 232)
    Private ReadOnly CLR_TEXT_PREMIUM As Color = Color.FromArgb(76, 66, 55)
    Private ReadOnly CLR_MUTED_PREMIUM As Color = Color.FromArgb(136, 118, 94)
    Private ReadOnly CLR_DARK_PREMIUM As Color = Color.FromArgb(46, 52, 60)
    Private ReadOnly CLR_GOLD_PREMIUM As Color = Color.FromArgb(214, 189, 150)

    Public Sub New()
        InitializeComponent()
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                AplicarTemaInventario()
            End Sub)
    End Sub

    Private dtProductos As New DataTable
    Private idSeleccionado As Integer = 0
    Private _temaAplicado As Boolean = False

    Private Function TablaCategorias() As String
        Return "[CATEGOR" & ChrW(205) & "A]"
    End Function

    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(
        ByVal nLeftRect As Integer,
        ByVal nTopRect As Integer,
        ByVal nRightRect As Integer,
        ByVal nBottomRect As Integer,
        ByVal nWidthEllipse As Integer,
        ByVal nHeightEllipse As Integer
    ) As IntPtr
    End Function

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me)
        AddHandler ModActualizaciones.InventarioActualizado, AddressOf RefrescarInventario
        AplicarTemaInventario()
        CargarCategorias()
        CargarProductos()
    End Sub

    Private Sub AplicarTemaInventario()
        If _temaAplicado Then
            ConfigurarLayoutInventario()
            Return
        End If

        ModEstilo.EstilarControles(Me)
        ModEstilo.EstilarStatusStrip(StatusStrip1)
        ModEstilo.EstilarBotonPrimario(btnGuardar)
        ModEstilo.EstilarBotonPeligro(btnEliminar)
        ModEstilo.EstilarBotonSecundario(btnNuevo)
        ModEstilo.EstilarBotonSecundario(btnActualizar)
        ModEstilo.EstilarBotonSecundario(btnFiltrar)
        ModEstilo.EstilarBotonPeligro(btnRegresar)
        AplicarEstiloInventarioPremium()
        ConfigurarLayoutInventario()
        _temaAplicado = True
    End Sub

    Private Sub AplicarEstiloInventarioPremium()
        Me.BackColor = CLR_BG_PREMIUM
        Me.Text = "KUMO | Inventario premium"

        gbFiltro.BackColor = CLR_PANEL_PREMIUM
        gbFiltro.ForeColor = CLR_TEXT_PREMIUM
        gbFiltro.Text = "Filtro comercial"
        gbFiltro.Region = Nothing

        gbTabla.BackColor = CLR_SURFACE_PREMIUM
        gbTabla.ForeColor = CLR_TEXT_PREMIUM
        gbTabla.Text = "Catalogo en piso"

        gbDetalle.BackColor = CLR_SURFACE_PREMIUM
        gbDetalle.ForeColor = CLR_TEXT_PREMIUM
        gbDetalle.Text = "Ficha del producto"

        lblBuscar.Text = "Busqueda rapida"
        lblCatTxt.Text = "Coleccion"
        lblNombreTxt.Text = "Nombre comercial"
        lblPrecioTxt.Text = "Precio de venta"
        lblStockTxt.Text = "Stock disponible"
        lblCatDetTxt.Text = "Categoria"
        lblInfo.ForeColor = CLR_MUTED_PREMIUM

        btnNuevo.Text = "+ Nuevo"
        btnGuardar.Text = "Guardar producto"
        btnEliminar.Text = "Eliminar"
        btnActualizar.Text = "Recargar"
        btnFiltrar.Text = "Filtrar"
        btnRegresar.Text = "Cerrar"

        gbFiltro.Padding = New Padding(18, 24, 18, 16)
        gbTabla.Padding = New Padding(8)
        gbDetalle.Padding = New Padding(14, 28, 14, 14)

        For Each lbl As Label In New Label() {lblBuscar, lblCatTxt, lblNombreTxt, lblPrecioTxt, lblStockTxt, lblCatDetTxt}
            lbl.ForeColor = CLR_MUTED_PREMIUM
            lbl.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Next

        For Each tb As TextBox In New TextBox() {txtBuscar, txtNombre, txtPrecio, txtStock}
            tb.BackColor = CLR_SURFACE_PREMIUM
            tb.ForeColor = CLR_TEXT_PREMIUM
            tb.BorderStyle = BorderStyle.FixedSingle
            tb.Font = New Font("Segoe UI", 10.0F)
        Next

        cbCategoria.BackColor = CLR_SURFACE_PREMIUM
        cbCategoria.ForeColor = CLR_TEXT_PREMIUM
        cbCategoria.FlatStyle = FlatStyle.Flat
        cbCategoria.Font = New Font("Segoe UI", 9.5F)

        cbCatDetalle.BackColor = CLR_SURFACE_PREMIUM
        cbCatDetalle.ForeColor = CLR_TEXT_PREMIUM
        cbCatDetalle.FlatStyle = FlatStyle.Flat
        cbCatDetalle.Font = New Font("Segoe UI", 9.5F)

        dgv.BackgroundColor = CLR_SURFACE_PREMIUM
        dgv.BorderStyle = BorderStyle.None
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = Color.FromArgb(229, 217, 201)
        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersDefaultCellStyle.BackColor = CLR_DARK_PREMIUM
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = CLR_DARK_PREMIUM
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.75F, FontStyle.Bold)
        dgv.DefaultCellStyle.BackColor = CLR_SURFACE_PREMIUM
        dgv.DefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgv.DefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 248, 242)
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = CLR_TEXT_PREMIUM
        dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 236, 223)
        dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = CLR_TEXT_PREMIUM
        dgv.RowTemplate.Height = 32

        btnGuardar.BackColor = Color.FromArgb(74, 133, 95)
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 111, 78)
        btnGuardar.TextAlign = ContentAlignment.MiddleCenter

        btnNuevo.BackColor = CLR_PANEL_PREMIUM
        btnNuevo.ForeColor = CLR_TEXT_PREMIUM
        btnNuevo.FlatAppearance.BorderColor = CLR_GOLD_PREMIUM
        btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnActualizar.BackColor = CLR_SURFACE_PREMIUM
        btnActualizar.ForeColor = CLR_TEXT_PREMIUM
        btnActualizar.FlatAppearance.BorderColor = CLR_GOLD_PREMIUM
        btnActualizar.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 235, 224)

        btnFiltrar.BackColor = CLR_DARK_PREMIUM
        btnFiltrar.ForeColor = Color.White
        btnFiltrar.FlatAppearance.BorderColor = CLR_DARK_PREMIUM
        btnFiltrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 74, 84)

        btnEliminar.BackColor = Color.FromArgb(154, 73, 64)
        btnEliminar.FlatAppearance.MouseOverBackColor = Color.FromArgb(133, 61, 53)

        btnRegresar.BackColor = CLR_DARK_PREMIUM
        btnRegresar.ForeColor = Color.FromArgb(244, 226, 193)
        btnRegresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 64, 73)
    End Sub

    Private Sub ConfigurarLayoutInventario()
        Dim margen As Integer = 18
        Dim altoBoton As Integer = 40
        Dim top As Integer = 14
        Dim panelDerecho As Integer = Math.Max(420, Math.Min(500, CInt(Me.ClientSize.Width * 0.31)))
        Dim anchoIzquierdo As Integer = Me.ClientSize.Width - panelDerecho - (margen * 3)
        Dim altoDisponible As Integer = Me.ClientSize.Height - StatusStrip1.Height - (margen * 2)
        Dim yBloques As Integer = top + altoBoton + 28
        Dim altoDetalle As Integer = altoDisponible - altoBoton - 14

        btnNuevo.SetBounds(margen, top, 120, altoBoton)
        btnGuardar.SetBounds(btnNuevo.Right + 12, top, 120, altoBoton)
        btnEliminar.SetBounds(btnGuardar.Right + 12, top, 120, altoBoton)
        btnActualizar.SetBounds(btnEliminar.Right + 12, top, 138, altoBoton)
        btnRegresar.SetBounds(Me.ClientSize.Width - margen - 118, top, 118, altoBoton)

        Dim altoFiltro As Integer = 118

        gbFiltro.SetBounds(margen, yBloques, anchoIzquierdo, altoFiltro)
        gbTabla.SetBounds(margen, gbFiltro.Bottom + 14, anchoIzquierdo, altoDisponible - gbFiltro.Height - altoBoton - 28)
        gbDetalle.SetBounds(gbFiltro.Right + margen, yBloques, panelDerecho, altoDetalle)
        Dim margenInternoIzq As Integer = 18
        Dim margenInternoDer As Integer = 18
        Dim anchoCliente As Integer = gbFiltro.ClientSize.Width
        Dim anchoBotonFiltrar As Integer = 96
        Dim anchoCategoria As Integer = Math.Max(180, Math.Min(210, CInt(anchoCliente * 0.24)))
        Dim separacion As Integer = 16
        Dim xBotonFiltrar As Integer = anchoCliente - margenInternoDer - anchoBotonFiltrar
        Dim xCategoria As Integer = xBotonFiltrar - separacion - anchoCategoria
        Dim anchoBusqueda As Integer = Math.Max(260, xCategoria - margenInternoIzq - separacion)
        Dim yEtiqueta As Integer = 30
        Dim yControl As Integer = 54

        lblBuscar.Location = New Point(margenInternoIzq, yEtiqueta)
        txtBuscar.SetBounds(margenInternoIzq, yControl, anchoBusqueda, 34)
        lblCatTxt.Location = New Point(xCategoria, yEtiqueta)
        cbCategoria.SetBounds(xCategoria, yControl, anchoCategoria, 34)
        btnFiltrar.SetBounds(xBotonFiltrar, yControl, anchoBotonFiltrar, 36)
        btnFiltrar.BringToFront()

        dgv.SetBounds(14, 30, gbTabla.Width - 28, gbTabla.Height - 72)
        lblInfo.Location = New Point(16, gbTabla.Height - 34)

        Dim pad As Integer = 18
        Dim anchoCampo As Integer = gbDetalle.Width - (pad * 2)
        Dim y As Integer = 42
        Dim espVertical As Integer = 18
        Dim altoCampo As Integer = 36
        Dim anchoMedio As Integer = (anchoCampo - 12) \ 2

        lblNombreTxt.Location = New Point(pad, y)
        txtNombre.SetBounds(pad, y + 24, anchoCampo, altoCampo)

        y += 24 + altoCampo + espVertical
        lblPrecioTxt.Location = New Point(pad, y)
        lblStockTxt.Location = New Point(pad + anchoMedio + 12, y)
        txtPrecio.SetBounds(pad, y + 24, anchoMedio, altoCampo)
        txtStock.SetBounds(pad + anchoMedio + 12, y + 24, anchoMedio, altoCampo)

        y += 24 + altoCampo + espVertical
        lblCatDetTxt.Location = New Point(pad, y)
        cbCatDetalle.SetBounds(pad, y + 24, anchoCampo, altoCampo)

        gbFiltro.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        gbTabla.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbDetalle.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        dgv.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnRegresar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    Private Sub CargarCategorias()
        Dim tabla = ObtenerTabla("SELECT NombreCat FROM " & TablaCategorias() & " ORDER BY NombreCat")
        cbCategoria.Items.Clear()
        cbCatDetalle.Items.Clear()
        cbCategoria.Items.Add("(Todas)")

        For Each row As DataRow In tabla.Rows
            cbCategoria.Items.Add(row("NombreCat").ToString())
            cbCatDetalle.Items.Add(row("NombreCat").ToString())
        Next

        cbCategoria.SelectedIndex = 0
        If cbCatDetalle.Items.Count > 0 Then cbCatDetalle.SelectedIndex = 0
    End Sub

    Private Sub CargarProductos()
        Try
            dtProductos = ObtenerTabla(
                "SELECT p.Id_Producto AS ID_Producto, p.NombrePr AS Nombre, p.Precio, " &
                "ISNULL(i.cant_disp,0) AS Stock, c.NombreCat AS Categoria " &
                "FROM PRODUCTO p " &
                "LEFT JOIN INVENTARIO i ON i.Id_Producto = p.Id_Producto " &
                "LEFT JOIN " & TablaCategorias() & " c ON c.Id_Categoria = p.Id_Categoria " &
                "ORDER BY c.NombreCat, p.NombrePr")
            dgv.DataSource = dtProductos
            If dgv.Columns.Contains("ID_Producto") Then dgv.Columns("ID_Producto").Visible = False
            lblInfo.Text = dtProductos.Rows.Count & " productos registrados"
            sbInfo.Text = "  " & lblInfo.Text
        Catch ex As Exception
            MsgBox("Error al cargar productos: " & ex.Message)
        End Try
    End Sub

    Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
        Dim t As String = txtBuscar.Text.Trim().Replace("'", "''")
        Dim c As String = cbCategoria.Text.Replace("'", "''")
        Dim filtros As New List(Of String)

        If t <> "" Then filtros.Add("Nombre LIKE '%" & t & "%'")
        If c <> "(Todas)" Then filtros.Add("Categoria = '" & c & "'")

        dtProductos.DefaultView.RowFilter = String.Join(" AND ", filtros.ToArray())
    End Sub

    Private Sub dgv_SelectionChanged(sender As Object, e As EventArgs) Handles dgv.SelectionChanged
        If dgv.CurrentRow Is Nothing Then Return
        txtNombre.Text = dgv.CurrentRow.Cells("Nombre").Value.ToString()
        txtPrecio.Text = dgv.CurrentRow.Cells("Precio").Value.ToString()
        txtStock.Text = dgv.CurrentRow.Cells("Stock").Value.ToString()
        idSeleccionado = CInt(dgv.CurrentRow.Cells("ID_Producto").Value)
        Dim idx As Integer = cbCatDetalle.Items.IndexOf(dgv.CurrentRow.Cells("Categoria").Value.ToString())
        If idx >= 0 Then cbCatDetalle.SelectedIndex = idx
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        idSeleccionado = 0
        txtNombre.Clear()
        txtPrecio.Clear()
        txtStock.Clear()
        If cbCatDetalle.Items.Count > 0 Then
            cbCatDetalle.SelectedIndex = 0
        Else
            cbCatDetalle.Text = ""
        End If
        dgv.ClearSelection()
        txtNombre.Focus()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txtNombre.Text.Trim() = "" Then MsgBox("Escribe el nombre del producto.") : Return
        If txtPrecio.Text.Trim() = "" Then MsgBox("Escribe el precio.") : Return
        If txtStock.Text.Trim() = "" Then MsgBox("Escribe el stock.") : Return

        Dim nombre As String = txtNombre.Text.Trim()
        If nombre.Length > 30 Then
            nombre = nombre.Substring(0, 30)
        End If

        Dim precio As Decimal
        Dim stock As Integer

        If Not Decimal.TryParse(txtPrecio.Text, precio) OrElse precio <= 0D Then
            MsgBox("Escribe un precio valido.")
            Return
        End If

        If Not Integer.TryParse(txtStock.Text, stock) OrElse stock < 0 Then
            MsgBox("Escribe un stock valido.")
            Return
        End If

        If cbCatDetalle.Items.Count = 0 OrElse cbCatDetalle.Text.Trim() = "" Then
            MsgBox("Selecciona una categoria valida antes de guardar.")
            cbCatDetalle.Focus()
            Return
        End If

        Using cn = ObtenerConexion()
            cn.Open()
            Dim trans = cn.BeginTransaction()

            Try
                Dim idCategoria As Integer
                Using cmdCat As New SqlCommand(
                    "SELECT Id_Categoria FROM " & TablaCategorias() & " WHERE NombreCat = @nombre",
                    cn,
                    trans)
                    cmdCat.Parameters.AddWithValue("@nombre", cbCatDetalle.Text)
                    Dim categoria = cmdCat.ExecuteScalar()
                    If categoria Is Nothing OrElse IsDBNull(categoria) Then
                        Throw New Exception("La categoria seleccionada no existe. Selecciona una categoria valida.")
                    End If
                    idCategoria = CInt(categoria)
                End Using

                If idSeleccionado = 0 Then
                    Dim nuevoId As Integer
                    Using cmd As New SqlCommand(
                        "INSERT INTO PRODUCTO (NombrePr, Precio, Id_Categoria) VALUES (@nombre, @precio, @idCategoria); " &
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        cn,
                        trans)
                        cmd.Parameters.AddWithValue("@nombre", nombre)
                        cmd.Parameters.AddWithValue("@precio", precio)
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria)
                        nuevoId = CInt(cmd.ExecuteScalar())
                    End Using

                    Using cmdInv As New SqlCommand(
                        "INSERT INTO INVENTARIO (cant_disp, Id_Producto) VALUES (@stock, @idProducto)",
                        cn,
                        trans)
                        cmdInv.Parameters.AddWithValue("@stock", stock)
                        cmdInv.Parameters.AddWithValue("@idProducto", nuevoId)
                        cmdInv.ExecuteNonQuery()
                    End Using
                Else
                    Using cmd As New SqlCommand(
                        "UPDATE PRODUCTO SET NombrePr = @nombre, Precio = @precio, Id_Categoria = @idCategoria " &
                        "WHERE Id_Producto = @idProducto",
                        cn,
                        trans)
                        cmd.Parameters.AddWithValue("@nombre", nombre)
                        cmd.Parameters.AddWithValue("@precio", precio)
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria)
                        cmd.Parameters.AddWithValue("@idProducto", idSeleccionado)
                        cmd.ExecuteNonQuery()
                    End Using

                    Using cmdInv As New SqlCommand(
                        "UPDATE INVENTARIO SET cant_disp = @stock WHERE Id_Producto = @idProducto",
                        cn,
                        trans)
                        cmdInv.Parameters.AddWithValue("@stock", stock)
                        cmdInv.Parameters.AddWithValue("@idProducto", idSeleccionado)
                        cmdInv.ExecuteNonQuery()
                    End Using
                End If

                trans.Commit()
                MsgBox(If(idSeleccionado = 0, "Producto agregado correctamente.", "Producto actualizado correctamente."))
                ModActualizaciones.NotificarInventarioActualizado()

            Catch ex As Exception
                trans.Rollback()
                MsgBox("Error al guardar: " & ex.Message)
            End Try
        End Using

        CargarProductos()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If idSeleccionado = 0 Then MsgBox("Selecciona un producto de la lista.") : Return
        If MsgBox("Eliminar " & txtNombre.Text & "?", MsgBoxStyle.YesNo, "Confirmar") = MsgBoxResult.Yes Then
            Using cn = ObtenerConexion()
                cn.Open()
                Dim trans = cn.BeginTransaction()

                Try
                    Using cmdInv As New SqlCommand("DELETE FROM INVENTARIO WHERE Id_Producto = @idProducto", cn, trans)
                        cmdInv.Parameters.AddWithValue("@idProducto", idSeleccionado)
                        cmdInv.ExecuteNonQuery()
                    End Using

                    Using cmd As New SqlCommand("DELETE FROM PRODUCTO WHERE Id_Producto = @idProducto", cn, trans)
                        cmd.Parameters.AddWithValue("@idProducto", idSeleccionado)
                        cmd.ExecuteNonQuery()
                    End Using

                    trans.Commit()
                    MsgBox("Producto eliminado.")
                    ModActualizaciones.NotificarInventarioActualizado()

                Catch ex As Exception
                    trans.Rollback()
                    MsgBox("Error al eliminar: " & ex.Message)
                End Try
            End Using

            CargarProductos()
            btnNuevo_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        CargarProductos()
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Close()
    End Sub

    Private Sub RefrescarInventario()
        If Me.IsDisposed Then Return
        CargarCategorias()
        CargarProductos()
    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler ModActualizaciones.InventarioActualizado, AddressOf RefrescarInventario
    End Sub

    Private Sub Form3_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutInventario()
    End Sub
End Class

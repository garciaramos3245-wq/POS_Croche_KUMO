Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Windows.Forms

Module ModEstilo

    Public ReadOnly CLR_BG As Color = Color.FromArgb(245, 247, 250)
    Public ReadOnly CLR_SURFACE As Color = Color.FromArgb(232, 239, 248)
    Public ReadOnly CLR_CARD As Color = Color.White
    Public ReadOnly CLR_HEADER As Color = Color.FromArgb(113, 152, 209)
    Public ReadOnly CLR_ACCENT As Color = Color.FromArgb(155, 188, 232)
    Public ReadOnly CLR_ACCENT2 As Color = Color.FromArgb(86, 125, 183)
    Public ReadOnly CLR_TEXT As Color = Color.FromArgb(52, 79, 118)
    Public ReadOnly CLR_MUTED As Color = Color.FromArgb(116, 141, 175)
    Public ReadOnly CLR_BORDER As Color = Color.FromArgb(214, 226, 241)
    Public ReadOnly CLR_INPUT As Color = Color.FromArgb(249, 251, 255)
    Public ReadOnly CLR_RED As Color = Color.FromArgb(201, 89, 89)
    Public ReadOnly CLR_RED_SOFT As Color = Color.FromArgb(255, 242, 242)
    Public ReadOnly CLR_RED_HOT As Color = Color.FromArgb(224, 108, 108)
    Public ReadOnly CLR_GREEN As Color = Color.FromArgb(74, 151, 107)
    Public ReadOnly CLR_GREEN_SOFT As Color = Color.FromArgb(236, 249, 241)
    Private _logoCache As Image
    Private _iconoCache As Icon
    Private _rutaLogo As String = ""
    Private _rutaIcono As String = ""
    Private _rutaLogoEvaluada As Boolean = False
    Private _rutaIconoEvaluada As Boolean = False

    Public Function FormatoFechaHora24(valor As DateTime) As String
        Dim sufijo As String = If(valor.Hour < 12, "a.m.", "p.m.")
        Return valor.ToString("dd/MM/yyyy") & "  " & valor.ToString("h:mm") & " " & sufijo
    End Function

    Public Function EstaEnModoDisenio(ctrl As Control) As Boolean
        If LicenseManager.UsageMode = LicenseUsageMode.Designtime Then
            Return True
        End If

        Dim proceso = System.Diagnostics.Process.GetCurrentProcess().ProcessName
        If String.Equals(proceso, "devenv", StringComparison.OrdinalIgnoreCase) OrElse
           String.Equals(proceso, "DesignToolsServer", StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        Dim actual As Control = ctrl
        While actual IsNot Nothing
            If actual.Site IsNot Nothing AndAlso actual.Site.DesignMode Then
                Return True
            End If
            actual = actual.Parent
        End While

        Return False
    End Function

    Public Sub AplicarTemaConsistente(frm As Form, accion As Action)
        If frm Is Nothing OrElse accion Is Nothing Then Return
        Try
            frm.SuspendLayout()
            accion()
        Catch
            If Not EstaEnModoDisenio(frm) Then Throw
            ' Evita que el diseñador falle si algun control o recurso aun no esta listo.
        Finally
            frm.ResumeLayout(True)
        End Try
    End Sub

    Public Sub AplicarVistaPreviaDisenio(frm As Form, accion As Action)
        AplicarTemaConsistente(frm, accion)
    End Sub

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = 2

    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Function CreateRoundRectRgn(
        ByVal nLeftRect As Integer,
        ByVal nTopRect As Integer,
        ByVal nRightRect As Integer,
        ByVal nBottomRect As Integer,
        ByVal nWidthEllipse As Integer,
        ByVal nHeightEllipse As Integer
    ) As IntPtr
    End Function

    <DllImport("gdi32.dll")>
    Private Function DeleteObject(hObject As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, lParam As Integer) As IntPtr
    End Function

    Public Sub PrepararVentana(frm As Form, Optional radius As Integer = 0, Optional pantallaCompleta As Boolean = True)
        frm.FormBorderStyle = FormBorderStyle.None
        frm.BackColor = CLR_BG
        AplicarIcono(frm)

        If pantallaCompleta Then
            ExpandirFormulario(frm)
        Else
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.WindowState = FormWindowState.Normal
        End If

        AplicarRegionFormulario(frm, radius)

        AddHandler frm.Shown,
            Sub(sender, e)
                If pantallaCompleta Then
                    ExpandirFormulario(frm)
                End If
                AplicarRegionFormulario(frm, radius)
                ReubicarMarca(frm)
            End Sub

        AddHandler frm.SizeChanged,
            Sub(sender, e)
                AplicarRegionFormulario(frm, radius)
                ReubicarMarca(frm)
            End Sub
    End Sub

    Public Sub ExpandirFormulario(frm As Form)
        Dim area = Screen.FromControl(frm).Bounds
        frm.StartPosition = FormStartPosition.Manual
        frm.WindowState = FormWindowState.Normal
        frm.Bounds = area
    End Sub

    Public Sub EstilarControles(frm As Form)
        frm.BackColor = CLR_BG
        frm.ForeColor = CLR_TEXT
        frm.Font = New Font("Segoe UI", 9.0F)
        AplicarIcono(frm)

        RemoveHandler frm.Paint, AddressOf PintarFondoKumo
        AddHandler frm.Paint, AddressOf PintarFondoKumo

        For Each ctrl As Control In ObtenerTodos(frm)
            Select Case ctrl.GetType().Name

                Case "GroupBox"
                    EstilarGroupBox(CType(ctrl, GroupBox))

                Case "Panel"
                    EstilarPanel(CType(ctrl, Panel))

                Case "Label"
                    EstilarLabel(CType(ctrl, Label))

                Case "TextBox"
                    EstilarTextBox(CType(ctrl, TextBox))

                Case "ComboBox"
                    EstilarCombo(CType(ctrl, ComboBox))

                Case "Button"
                    EstilarBotonBase(CType(ctrl, Button))

                Case "DataGridView"
                    EstilarDGV(CType(ctrl, DataGridView))

                Case "CheckBox"
                    ctrl.BackColor = Color.Transparent
                    ctrl.ForeColor = CLR_TEXT
                    ctrl.Font = New Font("Segoe UI", 9.0F)

                Case "DateTimePicker"
                    Dim dtp As DateTimePicker = CType(ctrl, DateTimePicker)
                    dtp.CalendarForeColor = CLR_TEXT
                    dtp.CalendarMonthBackground = CLR_CARD
                    dtp.CalendarTitleBackColor = CLR_HEADER
                    dtp.CalendarTitleForeColor = Color.White

                Case "RichTextBox"
                    Dim rtb As RichTextBox = CType(ctrl, RichTextBox)
                    rtb.BackColor = CLR_CARD
                    rtb.ForeColor = CLR_TEXT

                Case "PictureBox"
                    ctrl.BackColor = Color.Transparent

            End Select
        Next

        EstilarEtiquetasEspeciales(frm)
        AsegurarMarcaFormulario(frm)
    End Sub

    Public Sub EstilarBotonPrimario(btn As Button)
        btn.BackColor = CLR_HEADER
        btn.ForeColor = Color.White
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.FlatAppearance.MouseOverBackColor = CLR_ACCENT2
        btn.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        btn.UseVisualStyleBackColor = False
    End Sub

    Public Sub EstilarBotonPeligro(btn As Button)
        btn.BackColor = CLR_RED
        btn.ForeColor = Color.White
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.FlatAppearance.MouseOverBackColor = CLR_RED_HOT
        btn.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        btn.UseVisualStyleBackColor = False
    End Sub

    Public Sub EstilarBotonSecundario(btn As Button)
        btn.BackColor = CLR_SURFACE
        btn.ForeColor = CLR_TEXT
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderColor = CLR_ACCENT
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 233, 248)
        btn.Font = New Font("Segoe UI", 9.0F)
        btn.Cursor = Cursors.Hand
        btn.UseVisualStyleBackColor = False
    End Sub

    Public Sub EstilarBotonCobrar(btn As Button)
        btn.BackColor = CLR_ACCENT2
        btn.ForeColor = Color.White
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.FlatAppearance.MouseOverBackColor = CLR_HEADER
        btn.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        btn.UseVisualStyleBackColor = False
    End Sub

    Public Sub EstilarStatusStrip(ss As StatusStrip)
        ss.BackColor = CLR_HEADER
        ss.SizingGrip = False
        For Each item As ToolStripItem In ss.Items
            item.ForeColor = Color.White
            item.Font = New Font("Segoe UI", 8.0F)
        Next
    End Sub

    Public Sub EstilarMenuStrip(ms As MenuStrip)
        ms.BackColor = CLR_HEADER
        ms.ForeColor = Color.White
        ms.Font = New Font("Segoe UI", 9.0F)

        For Each item As ToolStripItem In ms.Items
            item.BackColor = CLR_HEADER
            If item.Name = "mnuCancelarVenta" Then
                item.ForeColor = Color.FromArgb(255, 236, 236)
            Else
                item.ForeColor = Color.White
            End If

            Dim menu = TryCast(item, ToolStripMenuItem)
            If menu IsNot Nothing Then
                menu.DropDown.BackColor = CLR_CARD
                For Each subItem As ToolStripItem In menu.DropDownItems
                    subItem.BackColor = CLR_CARD
                    subItem.ForeColor = CLR_TEXT
                Next
            End If
        Next
    End Sub

    Public Sub EstilarDGV(dgv As DataGridView)
        dgv.BackgroundColor = CLR_CARD
        dgv.BorderStyle = BorderStyle.FixedSingle
        dgv.GridColor = CLR_BORDER
        dgv.RowHeadersVisible = False
        dgv.EnableHeadersVisualStyles = False
        dgv.AllowUserToResizeRows = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.Font = New Font("Segoe UI", 9.0F)

        dgv.ColumnHeadersDefaultCellStyle.BackColor = CLR_HEADER
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = CLR_HEADER
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        dgv.ColumnHeadersHeight = 30

        dgv.DefaultCellStyle.BackColor = CLR_CARD
        dgv.DefaultCellStyle.ForeColor = CLR_TEXT
        dgv.DefaultCellStyle.SelectionBackColor = CLR_SURFACE
        dgv.DefaultCellStyle.SelectionForeColor = CLR_TEXT
        dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)
        dgv.RowTemplate.Height = 28

        dgv.AlternatingRowsDefaultCellStyle.BackColor = CLR_BG
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = CLR_TEXT
        dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = CLR_SURFACE
        dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = CLR_TEXT
    End Sub

    Public Sub CargarLogo(pb As PictureBox)
        If pb Is Nothing Then Return
        Dim logoPath = ObtenerRutaLogo()
        pb.BackColor = Color.Transparent
        pb.SizeMode = PictureBoxSizeMode.Zoom

        If logoPath = "" Then
            pb.Image = Nothing
            Return
        End If

        Try
            If _logoCache Is Nothing Then
                Using fs As New FileStream(logoPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Using img = Image.FromStream(fs)
                        _logoCache = New Bitmap(img)
                    End Using
                End Using
            End If

            Dim anterior = pb.Image
            pb.Image = CType(_logoCache.Clone(), Image)
            If anterior IsNot Nothing Then
                anterior.Dispose()
            End If
        Catch
            pb.Image = Nothing
        End Try
    End Sub

    Private Sub EstilarGroupBox(gb As GroupBox)
        If gb.Name = "gbCancelar" Then
            gb.BackColor = CLR_RED_SOFT
            gb.ForeColor = CLR_RED
        ElseIf gb.BackColor = Color.White OrElse gb.BackColor = CLR_SURFACE OrElse gb.BackColor = CLR_BG Then
            gb.BackColor = CLR_CARD
            gb.ForeColor = CLR_HEADER
        End If

        gb.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        AplicarEsquinasRedondeadas(gb, 18)
    End Sub

    Private Sub EstilarPanel(pnl As Panel)
        Dim esMetrica = pnl.Name.StartsWith("pnlIngresos") OrElse
                        pnl.Name.StartsWith("pnlVentas") OrElse
                        pnl.Name.StartsWith("pnlPromedio") OrElse
                        pnl.Name.StartsWith("pnlArticulos")

        If esMetrica Then
            pnl.BackColor = ObtenerColorPanel(pnl.Name)
        ElseIf pnl.BackColor = Color.White OrElse pnl.BackColor = Color.FromArgb(245, 246, 250) Then
            pnl.BackColor = CLR_CARD
        End If

        AplicarEsquinasRedondeadas(pnl, 18)
    End Sub

    Private Sub EstilarLabel(lbl As Label)
        lbl.BackColor = Color.Transparent
        lbl.ForeColor = CLR_MUTED
        lbl.Font = New Font("Segoe UI", 8.5F)

        If lbl.Name.EndsWith("Val") Then
            lbl.ForeColor = CLR_HEADER
            lbl.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        ElseIf lbl.Name.EndsWith("Title") Then
            lbl.ForeColor = CLR_MUTED
            lbl.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        ElseIf lbl.Name.EndsWith("Sub") Then
            lbl.ForeColor = CLR_MUTED
            lbl.Font = New Font("Segoe UI", 7.5F)
        ElseIf lbl.Name = "lblTotal" Then
            lbl.ForeColor = CLR_ACCENT2
            lbl.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        ElseIf lbl.Name = "lblSubtotal" Then
            lbl.ForeColor = CLR_TEXT
            lbl.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        ElseIf lbl.Name = "lblDescuento" Then
            lbl.ForeColor = CLR_RED
            lbl.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        ElseIf lbl.Name = "lblNumVenta" Then
            lbl.ForeColor = CLR_ACCENT2
            lbl.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        ElseIf lbl.Name = "lblLinea" Then
            lbl.BackColor = CLR_BORDER
        End If
    End Sub

    Private Sub EstilarTextBox(tb As TextBox)
        tb.Font = New Font("Segoe UI", 9.5F)
        tb.BorderStyle = BorderStyle.FixedSingle
        tb.ForeColor = CLR_TEXT
        If tb.ReadOnly Then
            tb.BackColor = CLR_SURFACE
        Else
            tb.BackColor = CLR_INPUT
        End If
    End Sub

    Private Sub EstilarCombo(cb As ComboBox)
        cb.Font = New Font("Segoe UI", 9.0F)
        cb.FlatStyle = FlatStyle.Flat
        cb.BackColor = CLR_INPUT
        cb.ForeColor = CLR_TEXT
    End Sub

    Private Sub EstilarBotonBase(btn As Button)
        btn.Font = New Font("Segoe UI", 9.0F)
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderColor = CLR_BORDER
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.MouseOverBackColor = CLR_SURFACE
        btn.BackColor = CLR_CARD
        btn.ForeColor = CLR_TEXT
        btn.Cursor = Cursors.Hand
        btn.UseVisualStyleBackColor = False
    End Sub

    Private Sub EstilarEtiquetasEspeciales(frm As Form)
        For Each ctrl As Control In ObtenerTodos(frm)
            Dim lbl = TryCast(ctrl, Label)
            If lbl Is Nothing Then Continue For

            If lbl.Name = "lblIngresosVal" Then lbl.ForeColor = CLR_GREEN
            If lbl.Name = "lblArticulosVal" Then lbl.ForeColor = CLR_ACCENT2
            If lbl.Name = "lblVentasVal" Then lbl.ForeColor = CLR_HEADER
            If lbl.Name = "lblPromedioVal" Then lbl.ForeColor = CLR_ACCENT2
        Next
    End Sub

    Private Sub AsegurarMarcaFormulario(frm As Form)
        If frm.Name = "Form1" OrElse frm.Name = "Form2" OrElse frm.Name = "Form5" OrElse frm.Name = "Form6" Then Return

        Dim pnl = TryCast(frm.Controls("pnlKumoBrand"), Panel)
        If pnl Is Nothing Then
            pnl = New Panel()
            pnl.Name = "pnlKumoBrand"
            pnl.Size = New Size(314, 64)
            pnl.BackColor = CLR_CARD

            Dim pic As New PictureBox()
            pic.Name = "picKumoBrand"
            pic.Location = New Point(10, 10)
            pic.Size = New Size(62, 42)
            CargarLogo(pic)

            Dim lblMarca As New Label()
            lblMarca.Name = "lblKumoBrand"
            lblMarca.AutoSize = True
            lblMarca.Location = New Point(82, 11)
            lblMarca.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
            lblMarca.ForeColor = CLR_HEADER
            lblMarca.Text = "KUMO"

            Dim lblSeccion As New Label()
            lblSeccion.Name = "lblKumoSection"
            lblSeccion.AutoSize = True
            lblSeccion.Location = New Point(82, 34)
            lblSeccion.Font = New Font("Segoe UI", 8.5F)
            lblSeccion.ForeColor = CLR_MUTED
            lblSeccion.Text = ObtenerSubtituloMarca(frm.Name)

            pnl.Controls.Add(pic)
            pnl.Controls.Add(lblMarca)
            pnl.Controls.Add(lblSeccion)
            frm.Controls.Add(pnl)
            pnl.BringToFront()
        Else
            pnl.Size = New Size(314, 64)
            Dim lblSeccion = TryCast(pnl.Controls("lblKumoSection"), Label)
            If lblSeccion IsNot Nothing Then
                lblSeccion.Text = ObtenerSubtituloMarca(frm.Name)
            End If
        End If

        ReubicarMarca(frm)
        AplicarEsquinasRedondeadas(pnl, 18)
    End Sub

    Private Sub ReubicarMarca(frm As Form)
        Dim pnl = TryCast(frm.Controls("pnlKumoBrand"), Panel)
        If pnl Is Nothing Then Return

        Dim y As Integer = 12
        Dim x As Integer = frm.ClientSize.Width - pnl.Width - 14

        Dim btnAncla = BuscarControlSuperior(frm, "btnRegresar")
        If btnAncla Is Nothing Then btnAncla = BuscarControlSuperior(frm, "btnSalida")

        If btnAncla IsNot Nothing AndAlso btnAncla.Left > pnl.Width + 24 Then
            x = btnAncla.Left - pnl.Width - 12
            y = btnAncla.Top
        End If

        If x < 12 Then x = 12
        pnl.Location = New Point(x, y)
        pnl.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnl.BringToFront()
    End Sub

    Private Function BuscarControlSuperior(frm As Form, nombre As String) As Control
        For Each ctrl As Control In frm.Controls
            If ctrl.Name = nombre Then Return ctrl
        Next
        Return Nothing
    End Function

    Private Sub AplicarIcono(frm As Form)
        Dim ruta = ObtenerRutaIcono()
        If ruta = "" Then Return

        If _iconoCache Is Nothing Then
            Using fs As New FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using ico As New Icon(fs)
                    _iconoCache = CType(ico.Clone(), Icon)
                End Using
            End Using
        End If

        Dim anterior = frm.Icon
        frm.Icon = CType(_iconoCache.Clone(), Icon)
        If anterior IsNot Nothing Then
            anterior.Dispose()
        End If
    End Sub

    Private Function ObtenerColorPanel(nombre As String) As Color
        If nombre.Contains("Ingresos") Then Return CLR_GREEN_SOFT
        If nombre.Contains("Ventas") Then Return Color.FromArgb(239, 246, 255)
        If nombre.Contains("Promedio") Then Return Color.FromArgb(236, 242, 255)
        Return Color.FromArgb(240, 247, 255)
    End Function

    Private Function ObtenerSubtituloMarca(nombreFormulario As String) As String
        Select Case nombreFormulario
            Case "Form3"
                Return "Inventario y control de stock"
            Case "Form4"
                Return "Historial de ventas del dia"
            Case "Form5"
                Return "Pedidos y seguimiento"
            Case "Form7"
                Return "Resumen visual de reportes"
            Case "Form8"
                Return "Cancelaciones con restauracion"
            Case Else
                Return "Operacion diaria Kumo"
        End Select
    End Function

    Private Sub PintarFondoKumo(sender As Object, e As PaintEventArgs)
        Dim frm = TryCast(sender, Form)
        If frm Is Nothing Then Return

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Using brSoft As New SolidBrush(Color.FromArgb(75, CLR_ACCENT))
            e.Graphics.FillEllipse(brSoft, New Rectangle(-60, -40, 220, 150))
            e.Graphics.FillEllipse(brSoft, New Rectangle(frm.ClientSize.Width - 180, frm.ClientSize.Height - 120, 240, 170))
        End Using

        Using brCard As New SolidBrush(Color.FromArgb(40, Color.White))
            e.Graphics.FillEllipse(brCard, New Rectangle(frm.ClientSize.Width - 290, 40, 130, 70))
            e.Graphics.FillEllipse(brCard, New Rectangle(frm.ClientSize.Width - 240, 20, 110, 85))
            e.Graphics.FillEllipse(brCard, New Rectangle(frm.ClientSize.Width - 190, 42, 120, 68))
        End Using
    End Sub

    Private Function ObtenerTodos(parent As Control) As List(Of Control)
        Dim lista As New List(Of Control)
        For Each ctrl As Control In parent.Controls
            lista.Add(ctrl)
            If ctrl.Controls.Count > 0 Then
                lista.AddRange(ObtenerTodos(ctrl))
            End If
        Next
        Return lista
    End Function

    Private Function ObtenerRutaLogo() As String
        If _rutaLogoEvaluada Then Return _rutaLogo

        Dim candidatos = {
            Path.Combine(Application.StartupPath, "Assets", "Logo.jpeg"),
            Path.Combine(Application.StartupPath, "..", "..", "Assets", "Logo.jpeg"),
            Path.Combine(Application.StartupPath, "..", "..", "..", "Assets", "Logo.jpeg")
        }

        For Each ruta In candidatos
            Dim absoluta = System.IO.Path.GetFullPath(ruta)
            If File.Exists(absoluta) Then
                _rutaLogoEvaluada = True
                _rutaLogo = absoluta
                Return absoluta
            End If
        Next

        _rutaLogoEvaluada = True
        _rutaLogo = ""
        Return ""
    End Function

    Private Function ObtenerRutaIcono() As String
        If _rutaIconoEvaluada Then Return _rutaIcono

        Dim candidatos = {
            Path.Combine(Application.StartupPath, "Assets", "Logo.ico"),
            Path.Combine(Application.StartupPath, "..", "..", "Assets", "Logo.ico"),
            Path.Combine(Application.StartupPath, "..", "..", "..", "Assets", "Logo.ico")
        }

        For Each ruta In candidatos
            Dim absoluta = System.IO.Path.GetFullPath(ruta)
            If File.Exists(absoluta) Then
                _rutaIconoEvaluada = True
                _rutaIcono = absoluta
                Return absoluta
            End If
        Next

        _rutaIconoEvaluada = True
        _rutaIcono = ""
        Return ""
    End Function

    Private Sub AplicarRegionFormulario(frm As Form, radius As Integer)
        If radius <= 0 Then
            frm.Region = Nothing
            Return
        End If

        AplicarEsquinasRedondeadas(frm, radius)
    End Sub

    Private Sub AplicarEsquinasRedondeadas(ctrl As Control, radius As Integer)
        If ctrl.Width <= 0 OrElse ctrl.Height <= 0 Then Return

        Dim hrgn = CreateRoundRectRgn(0, 0, ctrl.Width + 1, ctrl.Height + 1, radius, radius)
        Try
            ctrl.Region = Region.FromHrgn(hrgn)
        Finally
            DeleteObject(hrgn)
        End Try
    End Sub

    Private Sub HabilitarArrastre(frm As Form)
        RegistrarArrastre(frm, frm)

        For Each ctrl As Control In ObtenerTodos(frm)
            If PermiteArrastre(ctrl) Then
                RegistrarArrastre(ctrl, frm)
            End If
        Next
    End Sub

    Private Function PermiteArrastre(ctrl As Control) As Boolean
        Return TypeOf ctrl Is Panel OrElse
               TypeOf ctrl Is Label OrElse
               TypeOf ctrl Is GroupBox OrElse
               TypeOf ctrl Is StatusStrip OrElse
               TypeOf ctrl Is MenuStrip
    End Function

    Private Sub RegistrarArrastre(ctrl As Control, frm As Form)
        AddHandler ctrl.MouseDown,
            Sub(sender, e)
                If e.Button <> MouseButtons.Left Then Return
                ReleaseCapture()
                SendMessage(frm.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
            End Sub
    End Sub

End Module

' Archivo: ModMensajes.vb.
' Muestra dialogos personalizados de informacion, confirmacion, advertencia y error.

Imports System.Runtime.InteropServices

Module ModMensajes

    ' Documentacion: Tipos y funciones que construyen dialogos modales personalizados.

    ' Documentacion: Tipos de aviso que determinan color, subtitulo e intencion del dialogo.
    Public Enum TipoAviso
        Info
        Exito
        Advertencia
        [Error]
    End Enum

    ' Documentacion: Importa la funcion de Windows que crea regiones con esquinas redondeadas.
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

    ' Documentacion: Libera objetos GDI creados al aplicar regiones redondeadas.
    <DllImport("gdi32.dll")>
    Private Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

    ' Documentacion: Muestra un aviso de un solo boton al usuario.
    Public Sub Mostrar(owner As IWin32Window,
                       titulo As String,
                       mensaje As String,
                       Optional tipo As TipoAviso = TipoAviso.Info,
                       Optional textoBoton As String = "Entendido")
        MostrarDialogo(owner, titulo, mensaje, textoBoton, "", tipo)
    End Sub

    ' Documentacion: Muestra una confirmacion con boton principal y secundario, y devuelve la decision.
    Public Function Confirmar(owner As IWin32Window,
                              titulo As String,
                              mensaje As String,
                              Optional textoPrimario As String = "Confirmar",
                              Optional textoSecundario As String = "Cancelar",
                              Optional tipo As TipoAviso = TipoAviso.Advertencia) As Boolean
        Return MostrarDialogo(owner, titulo, mensaje, textoPrimario, textoSecundario, tipo)
    End Function

    ' Documentacion: Construye el formulario modal personalizado y devuelve si se acepto la accion.
    Private Function MostrarDialogo(owner As IWin32Window,
                                    titulo As String,
                                    mensaje As String,
                                    textoPrimario As String,
                                    textoSecundario As String,
                                    tipo As TipoAviso) As Boolean
        Dim clrDark As Color = Color.FromArgb(46, 52, 60)
        Dim clrSurface As Color = Color.FromArgb(255, 252, 247)
        Dim clrGold As Color = Color.FromArgb(244, 212, 141)
        Dim clrText As Color = Color.FromArgb(76, 66, 55)
        Dim clrMuted As Color = Color.FromArgb(120, 104, 85)
        Dim clrAccent As Color = ColorTipo(tipo)
        Dim haySecundario As Boolean = textoSecundario.Trim() <> ""

        Using dlg As New Form()
            dlg.Text = titulo
            dlg.FormBorderStyle = FormBorderStyle.None
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.ClientSize = New Size(560, 340)
            dlg.BackColor = Color.FromArgb(244, 240, 234)
            dlg.ShowInTaskbar = False
            dlg.KeyPreview = True

            Dim pnlHeader As New Panel() With {
                .BackColor = clrDark,
                .Dock = DockStyle.Top,
                .Height = 86
            }

            Dim lblTitulo As New Label() With {
                .Text = titulo,
                .ForeColor = Color.FromArgb(255, 248, 239),
                .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
                .Bounds = New Rectangle(24, 14, 420, 32),
                .TextAlign = ContentAlignment.MiddleLeft,
                .AutoEllipsis = True
            }

            Dim lblSubtitulo As New Label() With {
                .Text = SubtituloTipo(tipo),
                .ForeColor = If(tipo = TipoAviso.Error, Color.FromArgb(255, 204, 194), clrGold),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Bounds = New Rectangle(26, 50, 410, 22),
                .TextAlign = ContentAlignment.MiddleLeft,
                .AutoEllipsis = True
            }

            Dim btnCerrar As New Button() With {
                .Text = "X",
                .Bounds = New Rectangle(502, 24, 34, 34),
                .FlatStyle = FlatStyle.Flat,
                .BackColor = Color.FromArgb(57, 64, 73),
                .ForeColor = Color.FromArgb(244, 226, 193),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand,
                .DialogResult = DialogResult.Cancel
            }
            btnCerrar.FlatAppearance.BorderColor = Color.FromArgb(96, 87, 72)
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 78, 88)

            pnlHeader.Controls.Add(lblTitulo)
            pnlHeader.Controls.Add(lblSubtitulo)
            pnlHeader.Controls.Add(btnCerrar)
            dlg.Controls.Add(pnlHeader)

            Dim pnlCard As New Panel() With {
                .BackColor = clrSurface,
                .Bounds = New Rectangle(28, 110, 504, 138)
            }

            Dim barra As New Panel() With {
                .BackColor = clrAccent,
                .Bounds = New Rectangle(18, 24, 6, 90)
            }

            Dim lblMensaje As New Label() With {
                .Text = mensaje,
                .ForeColor = clrText,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .Bounds = New Rectangle(40, 20, 438, 98),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            pnlCard.Controls.Add(barra)
            pnlCard.Controls.Add(lblMensaje)
            dlg.Controls.Add(pnlCard)

            Dim btnPrimario As New Button() With {
                .Text = textoPrimario,
                .Bounds = If(haySecundario, New Rectangle(292, 276, 240, 40), New Rectangle(180, 276, 200, 40)),
                .DialogResult = DialogResult.OK
            }
            EstilarBoton(btnPrimario, If(tipo = TipoAviso.Error, clrDark, clrAccent), Color.White, If(tipo = TipoAviso.Error, clrDark, clrAccent), If(tipo = TipoAviso.Error, Color.FromArgb(57, 64, 73), Oscurecer(clrAccent)))
            dlg.Controls.Add(btnPrimario)

            If haySecundario Then
                Dim btnSecundario As New Button() With {
                    .Text = textoSecundario,
                    .Bounds = New Rectangle(28, 276, 240, 40),
                    .DialogResult = DialogResult.Cancel
                }
                EstilarBoton(btnSecundario, Color.FromArgb(249, 243, 234), Color.FromArgb(98, 84, 69), Color.FromArgb(216, 198, 172), Color.FromArgb(243, 235, 224))
                RedondearControl(btnSecundario, 16)
                dlg.Controls.Add(btnSecundario)
                dlg.CancelButton = btnSecundario
            Else
                dlg.CancelButton = btnPrimario
            End If

            dlg.AcceptButton = btnPrimario
            RedondearControl(dlg, 26)
            RedondearControl(pnlHeader, 22)
            RedondearControl(pnlCard, 20)
            RedondearControl(btnCerrar, 16)
            RedondearControl(btnPrimario, 16)

            If owner Is Nothing Then
                Return dlg.ShowDialog() = DialogResult.OK
            End If

            Return dlg.ShowDialog(owner) = DialogResult.OK
        End Using
    End Function

    ' Documentacion: Devuelve el color de acento segun el tipo de aviso.
    Private Function ColorTipo(tipo As TipoAviso) As Color
        Select Case tipo
            Case TipoAviso.Exito
                Return Color.FromArgb(74, 133, 95)
            Case TipoAviso.Advertencia
                Return Color.FromArgb(181, 138, 92)
            Case TipoAviso.Error
                Return Color.FromArgb(146, 79, 67)
            Case Else
                Return Color.FromArgb(58, 68, 80)
        End Select
    End Function

    ' Documentacion: Devuelve el texto secundario segun el tipo de aviso.
    Private Function SubtituloTipo(tipo As TipoAviso) As String
        Select Case tipo
            Case TipoAviso.Exito
                Return "Operacion completada"
            Case TipoAviso.Advertencia
                Return "Confirma antes de continuar"
            Case TipoAviso.Error
                Return "Revisa el detalle antes de continuar"
            Case Else
                Return "Informacion del sistema"
        End Select
    End Function

    ' Documentacion: Genera una variante mas oscura de un color para estados de hover.
    Private Function Oscurecer(color As Color) As Color
        Return Color.FromArgb(Math.Max(0, color.R - 18), Math.Max(0, color.G - 18), Math.Max(0, color.B - 18))
    End Function

    ' Documentacion: Aplica colores, borde, fuente y cursor a botones de dialogo.
    Private Sub EstilarBoton(btn As Button, colorFondo As Color, colorTexto As Color, colorBorde As Color, colorHover As Color)
        btn.BackColor = colorFondo
        btn.ForeColor = colorTexto
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.BorderColor = colorBorde
        btn.FlatAppearance.MouseOverBackColor = colorHover
        btn.FlatAppearance.MouseDownBackColor = colorHover
        btn.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        btn.UseVisualStyleBackColor = False
    End Sub

    ' Documentacion: Redondea controles del dialogo y limpia la region si falla el recurso nativo.
    Private Sub RedondearControl(ctrl As Control, radius As Integer)
        If ctrl Is Nothing OrElse ctrl.Width <= 0 OrElse ctrl.Height <= 0 Then Return

        Try
            Dim hrgn = CreateRoundRectRgn(0, 0, ctrl.Width + 1, ctrl.Height + 1, radius, radius)
            Try
                ctrl.Region = Region.FromHrgn(hrgn)
            Finally
                DeleteObject(hrgn)
            End Try
        Catch
            ctrl.Region = Nothing
        End Try
    End Sub

End Module

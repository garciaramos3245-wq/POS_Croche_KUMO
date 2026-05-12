' Archivo: Form1.vb.
' Administra la pantalla de acceso al sistema KUMO.

Public Class Form1

    ' Documentacion: Controles creados por codigo para complementar el login.

    Private pnlIntro As Panel
    Private lblIntroEyebrow As Label
    Private lblIntroTitle As Label
    Private lblIntroSub As Label
    Private lblIntroFoot As Label

    ' Documentacion: Inicializa el formulario y aplica configuracion visual inicial.
    Public Sub New()
        InitializeComponent()
        ModEstilo.AplicarTemaConsistente(Me,
            Sub()
                If ModEstilo.EstaEnModoDisenio(Me) Then
                    ModEstilo.PrepararVentana(Me, 28, False)
                End If
                AplicarDisenoAcceso()
            End Sub)
    End Sub

    ' Documentacion: Prepara la ventana de login, aplica el diseno y la centra en pantalla.
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModEstilo.PrepararVentana(Me, 28, False)
        AplicarDisenoAcceso()
        CentrarVentanaLogin()
    End Sub

    ' Documentacion: Crea el bloque visual de acceso, aplica estilos y fija el tamano del login.
    Private Sub AplicarDisenoAcceso()
        CrearPanelInformativo()
        AplicarEstilo()
        Me.ClientSize = New Size(920, 560)
        Me.MinimumSize = New Size(920, 560)
        Me.MaximumSize = New Size(920, 560)
        ConfigurarLayoutAcceso()
    End Sub

    ' Documentacion: Calcula la posicion para mostrar el login al centro del area util.
    Private Sub CentrarVentanaLogin()
        Dim area = Screen.FromControl(Me).WorkingArea
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(
            area.Left + ((area.Width - Me.Width) \ 2),
            area.Top + ((area.Height - Me.Height) \ 2))
    End Sub

    ' Documentacion: Crea el panel informativo lateral usado por el diseno de acceso.
    Private Sub CrearPanelInformativo()
        If pnlIntro IsNot Nothing Then Return

        pnlIntro = New Panel()
        pnlIntro.Name = "pnlIntro"

        lblIntroEyebrow = New Label()
        lblIntroEyebrow.Name = "lblIntroEyebrow"
        lblIntroEyebrow.AutoSize = True
        lblIntroEyebrow.Text = "TERMINAL DE CAJA"

        lblIntroTitle = New Label()
        lblIntroTitle.Name = "lblIntroTitle"
        lblIntroTitle.AutoSize = False
        lblIntroTitle.Text = "Un acceso mas claro para arrancar tu punto de venta."

        lblIntroSub = New Label()
        lblIntroSub.Name = "lblIntroSub"
        lblIntroSub.AutoSize = False
        lblIntroSub.Text = "Inicia sesion desde una ventana compacta, con identidad visual y una experiencia mas cercana a una caja profesional."

        lblIntroFoot = New Label()
        lblIntroFoot.Name = "lblIntroFoot"
        lblIntroFoot.AutoSize = False
        lblIntroFoot.Text = "Acceso demo: admin / 1234"

        pnlIntro.Controls.Add(lblIntroEyebrow)
        pnlIntro.Controls.Add(lblIntroTitle)
        pnlIntro.Controls.Add(lblIntroSub)
        pnlIntro.Controls.Add(lblIntroFoot)
        Me.Controls.Add(pnlIntro)
        pnlIntro.BringToFront()
    End Sub

    ' Documentacion: Aplica colores, fuentes, textos, logo y botones de la pantalla de acceso.
    Private Sub AplicarEstilo()
        ModEstilo.EstilarControles(Me)
        Me.BackColor = ModEstilo.CLR_BG
        Me.Font = New Font("Segoe UI", 9.0F)
        Me.Text = "KUMO | Acceso de caja"

        pnlIntro.BackColor = Color.FromArgb(31, 51, 79)
        pnlIntro.Visible = False
        pnlLogo.BackColor = Color.FromArgb(236, 243, 252)
        ModEstilo.CargarLogo(picLogo)
        lblLogo.Visible = False
        lblAppName.ForeColor = Color.FromArgb(37, 67, 109)
        lblAppName.Text = "KUMO"
        lblAppSub.ForeColor = Color.FromArgb(111, 132, 162)
        lblAppSub.Text = ""

        lblIntroEyebrow.Text = ""
        lblIntroTitle.Text = ""
        lblIntroSub.Text = ""
        lblIntroFoot.Text = ""

        gbCredenciales.BackColor = ModEstilo.CLR_CARD
        gbCredenciales.ForeColor = ModEstilo.CLR_HEADER
        gbCredenciales.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        gbCredenciales.Text = "Acceso"

        For Each tb As TextBox In {txtUsuario, txtPassword}
            tb.BackColor = ModEstilo.CLR_INPUT
            tb.ForeColor = ModEstilo.CLR_TEXT
            tb.BorderStyle = BorderStyle.FixedSingle
            tb.Font = New Font("Segoe UI", 11.0F)
        Next

        For Each lbl As Label In {lblUsuario, lblPassword}
            lbl.ForeColor = ModEstilo.CLR_MUTED
            lbl.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            lbl.BackColor = Color.Transparent
        Next

        ModEstilo.EstilarBotonPrimario(btnEntrar)
        btnEntrar.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        btnEntrar.Text = "Ingresar al sistema"

        ModEstilo.EstilarBotonSecundario(btnCancelar)
        btnCancelar.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnCancelar.Text = "Cerrar"

        ModEstilo.EstilarStatusStrip(StatusStrip1)
        ModEstilo.ConfigurarRelojStatusStrip(Me, StatusStrip1)
        sbInfo.Font = New Font("Segoe UI", 8.0F)
        sbVersion.ForeColor = ModEstilo.CLR_SURFACE
        sbVersion.Font = New Font("Segoe UI", 8.0F)
        sbInfo.Text = "  Listo para iniciar sesion."
        sbVersion.Text = "v1.0.0  -  Acceso"

        pnlLinea.BackColor = ModEstilo.CLR_ACCENT
    End Sub

    ' Documentacion: Acomoda logo, credenciales y botones dentro de la ventana fija del login.
    Private Sub ConfigurarLayoutAcceso()
        If pnlIntro Is Nothing Then
            CrearPanelInformativo()
        End If

        If pnlIntro Is Nothing OrElse
           lblIntroEyebrow Is Nothing OrElse
           lblIntroTitle Is Nothing OrElse
           lblIntroSub Is Nothing OrElse
           lblIntroFoot Is Nothing Then
            Return
        End If

        Dim altoBloque As Integer = 438
        Dim anchoPanelDerecho As Integer = 500
        Dim yBase As Integer = 44
        Dim xPanelDerecho As Integer = (Me.ClientSize.Width - anchoPanelDerecho) \ 2

        pnlIntro.SetBounds(0, 0, 0, 0)

        pnlLogo.SetBounds(xPanelDerecho, yBase, anchoPanelDerecho, 132)
        picLogo.SetBounds(24, 18, 124, 92)
        lblAppName.Location = New Point(166, 36)
        lblAppName.Font = New Font("Segoe UI", 21.0F, FontStyle.Bold)
        lblAppSub.Location = New Point(168, 74)
        lblAppSub.Font = New Font("Segoe UI", 10.5F)

        pnlLinea.SetBounds(xPanelDerecho, pnlLogo.Bottom + 14, anchoPanelDerecho, 5)
        gbCredenciales.SetBounds(xPanelDerecho, pnlLinea.Bottom + 18, anchoPanelDerecho, 204)

        Dim interiorX As Integer = 30
        Dim interiorW As Integer = gbCredenciales.Width - (interiorX * 2)

        lblUsuario.Location = New Point(interiorX, 38)
        txtUsuario.SetBounds(interiorX, lblUsuario.Bottom + 8, interiorW, 36)
        lblPassword.Location = New Point(interiorX, txtUsuario.Bottom + 18)
        txtPassword.SetBounds(interiorX, lblPassword.Bottom + 8, interiorW, 36)

        Dim anchoBoton As Integer = (anchoPanelDerecho - 12) \ 2
        btnEntrar.SetBounds(xPanelDerecho, gbCredenciales.Bottom + 18, anchoBoton, 48)
        btnCancelar.SetBounds(btnEntrar.Right + 12, gbCredenciales.Bottom + 18, anchoBoton, 48)

        pnlIntro.Anchor = AnchorStyles.None
        pnlLogo.Anchor = AnchorStyles.None
        gbCredenciales.Anchor = AnchorStyles.None
        btnEntrar.Anchor = AnchorStyles.None
        btnCancelar.Anchor = AnchorStyles.None
        pnlLinea.Anchor = AnchorStyles.None
    End Sub

    ' Documentacion: Reacomoda el login cuando la ventana cambia de tamano.
    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not Me.Visible Then Return
        If Me.WindowState = FormWindowState.Minimized Then Return
        ConfigurarLayoutAcceso()
    End Sub

    ' Documentacion: Valida usuario y contrasena; si son correctos abre el punto de venta.
    Private Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        If txtUsuario.Text.Trim() = "admin" AndAlso txtPassword.Text = "1234" Then
            Me.Hide()
            Using principal As New Form2()
                principal.ShowDialog()
                If principal.InicioCorrecto Then
                    Me.Close()
                    Return
                End If
            End Using
            Me.Show()
            Me.Activate()
            sbInfo.Text = "  El POS no pudo abrirse correctamente. Revisa el mensaje y vuelve a intentar."
        Else
            sbInfo.Text = "  Usuario o contrasena incorrectos."
            sbInfo.ForeColor = Color.FromArgb(255, 230, 230)
            txtPassword.Clear()
            txtPassword.Focus()
        End If
    End Sub

    ' Documentacion: Confirma cancelacion, restaura stock y elimina venta y detalle en transaccion.
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

End Class

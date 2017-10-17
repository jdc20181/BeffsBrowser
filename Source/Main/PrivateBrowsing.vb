Imports System.Net

Imports System.IO
Imports System.Deployment.Application
Imports Gecko
Imports System.Windows.Forms
Imports System.Reflection

Public Class PrivateBrowsing
    Dim int As Integer = 0


    Private Sub Loading(ByVal sender As Object, ByVal e As Gecko.GeckoProgressEventArgs)
        TabControl1.SelectedTab.Text = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).DocumentTitle
        If TabControl1.SelectedTab.Text = Nothing Then
            TabControl1.SelectedTab.Text = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString
        End If
        ToolStripProgressBar1.Maximum = e.MaximumProgress
        ToolStripProgressBar1.Value = e.MaximumProgress
        Me.Cursor = Cursors.AppStarting

    End Sub

    Private Sub Done(ByVal sender As Object, ByVal e As Gecko.Events.GeckoDocumentCompletedEventArgs)

        Me.Cursor = Cursors.Default
        ToolStripTextBox1.Text = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString

        If CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url = New Uri("https://jdc20181.github.io/StartPage/Startpage2.html") Then
            Dim nameIndex, urlIndex As Integer

            Dim names() As String = {My.Settings.StartPageName1, My.Settings.StartPageName2, My.Settings.StartPageName3, My.Settings.StartPageName4, My.Settings.StartPageName5, My.Settings.StartPageName6, My.Settings.StartPageName7, My.Settings.StartPageName8, My.Settings.StartPageName9, My.Settings.StartPageName10}
            Dim urls() As String = {My.Settings.StartPage1, My.Settings.StartPage2, My.Settings.StartPage3, My.Settings.StartPage4, My.Settings.StartPage5, My.Settings.StartPage6, My.Settings.StartPage7, My.Settings.StartPage8, My.Settings.StartPage9, My.Settings.StartPage10}
            Dim wb As GeckoWebBrowser = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser)

            Dim siteNodes = wb.Document.GetElementsByClassName("site")
            For Each n As GeckoHtmlElement In siteNodes
                'n.TextContent = "Site"
                n.TextContent = names(nameIndex)
                nameIndex += 1
            Next

            Dim urlNodes = wb.Document.GetElementsByClassName("url")
            For Each n As GeckoHtmlElement In urlNodes
                n.TextContent = urls(urlIndex)
                n.SetAttribute("href", urls(urlIndex))
                urlIndex += 1
            Next
        End If




    End Sub


    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click



        'Dim brws As New GeckoWebBrowser

        'AddHandler brws.ProgressChanged, AddressOf Loading
        'AddHandler brws.DocumentCompleted, AddressOf Done
        'int = int + 0.5
        'CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(ToolStripTextBox1.Text)
        Dim s As String = ToolStripTextBox1.Text
        Dim fHasSpace As Boolean = s.Contains(" ")

        If fHasSpace = True Then
            search()
        ElseIf fHasSpace = False Then
            UrlNavigate()
        End If

    End Sub
#Region "Test2"
    Public Sub UrlNavigate()
        If CheckURL(ToolStripTextBox1.Text) = True Then
            Dim brws As New GeckoWebBrowser

            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1

            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(ToolStripTextBox1.Text)


        Else
            search()

        End If
    End Sub
    Public Sub search()
        Dim brws As New GeckoWebBrowser
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 0.5
        If My.Settings.securesearch = "Yes" Then
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate("https://duckduckgo.com/?q=" & ToolStripTextBox1.Text)
        ElseIf My.Settings.securesearch = "No" Then
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(My.Settings.SearchP & ToolStripTextBox1.Text)


        End If
    End Sub
    Public Function CheckURL(ByVal urltocheck As String)
        Dim url As New System.Uri("http://" & urltocheck)
        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)
        Dim resp As System.Net.WebResponse
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            Return True
        Catch ex As Exception
            req = Nothing
            Return False
        End Try
    End Function

#End Region
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click


        Dim brws As New GeckoWebBrowser
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GoBack()

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        Dim brws As New GeckoWebBrowser
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GoForward()

    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        MsgBox("Welcome to Private Browser 2.0, many improvments happened, check github for more information! (Released in Version 2.2)")
    End Sub



    Private Sub PrivateBrowsing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ExitFullScreen.Visible = False
        ToolStripDropDownButton1.Visible = False
        coloring()

        Dim tab As New TabPage
            Dim brws As New GeckoWebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.TabControl1.TabPages.Add(tab)
            Me.TabControl1.SelectedTab = tab
            brws.Navigate(My.Settings.HomeSpace)
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            Dim field = GetType(GeckoWebBrowser).GetField("WebBrowser", BindingFlags.Instance Or BindingFlags.NonPublic)
            Dim nsIWebBrowser As nsIWebBrowser = DirectCast(field.GetValue(brws), nsIWebBrowser)
            'this might be null if called right before initialization of browser
            Xpcom.QueryInterface(Of nsILoadContext)(nsIWebBrowser).SetPrivateBrowsing(True)



    End Sub


    Private Sub PrivateBrowsing_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click

        Dim brws As New GeckoWebBrowser

            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Reload()

    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click, NewTabToolStripMenuItem.Click

        Dim tab As New TabPage
        Dim brws As New GeckoWebBrowser
        brws.Dock = DockStyle.Fill
        tab.Text = " New Tab"
        tab.Controls.Add(brws)
        Me.TabControl1.TabPages.Add(tab)
        Me.TabControl1.SelectedTab = tab
        brws.Navigate(My.Settings.HomeSpace)
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 1
        Dim field = GetType(GeckoWebBrowser).GetField("WebBrowser", BindingFlags.Instance Or BindingFlags.NonPublic)
        Dim nsIWebBrowser As nsIWebBrowser = DirectCast(field.GetValue(brws), nsIWebBrowser)
        'this might be null if called right before initialization of browser
        Xpcom.QueryInterface(Of nsILoadContext)(nsIWebBrowser).SetPrivateBrowsing(True)

    End Sub

    Private Sub Fullscreen_Click(sender As Object, e As EventArgs) Handles Fullscreen.Click, FullScreenToolStripMenuItem.Click
        MakeFullScreen()
        Fullscreen.Visible = False
        ExitFullScreen.Visible = True
    End Sub
    Public Sub MakeFullScreen()
        Me.SetVisibleCore(False)
        FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        Me.SetVisibleCore(True)
    End Sub

    Private Sub ExitFullScreen_Click(sender As Object, e As EventArgs) Handles ExitFullScreen.Click, ExitFullscreenToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Maximized
        Me.Size = New Drawing.Size(3200, 3200)  ' MaximumSize
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Fullscreen.Visible = True
        ControlBox = True
        Me.TopMost = False
        ExitFullScreen.Visible = False
    End Sub
    Public Sub coloring()
        ToolStripTextBox1.BackColor = ColorTranslator.FromHtml(My.Settings.Color)
        'This is for button colors
        ToolStripButton1.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton2.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton3.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton7.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton10.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        Fullscreen.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ExitFullScreen.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStrip1.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton5.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
    End Sub
    Private Sub ToolStripTextBox1_MouseHover(sender As Object, e As EventArgs) Handles ToolStripTextBox1.MouseHover
        'ToolStripTextBox1.Size = New Size(625, 25)

        ToolStripTextBox1.TextBox.Width = 700
    End Sub

    Private Sub ToolStripTextBox1_MouseLeave(sender As Object, e As EventArgs) Handles ToolStripTextBox1.MouseLeave
        'ToolStripTextBox1.Size = New Size(580, 25)
        ToolStripTextBox1.TextBox.Width = 650
    End Sub

End Class

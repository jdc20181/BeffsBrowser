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


        Try
            Dim brws As New GeckoWebBrowser

            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(ToolStripTextBox1.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click

        Try
            Dim brws As New GeckoWebBrowser
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GoBack()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            Dim brws As New GeckoWebBrowser
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GoForward()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        MsgBox("Welcome to Private Browser 2.0, many improvments happened, check github for more information! (Released in Version 2.2)")
    End Sub



    Private Sub PrivateBrowsing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ExitFullScreen.Visible = False
        Try

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
        Catch ex As Exception

        End Try


    End Sub


    Private Sub PrivateBrowsing_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Try
            Dim brws As New GeckoWebBrowser

            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Reload()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        Try

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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Fullscreen_Click(sender As Object, e As EventArgs) Handles Fullscreen.Click
        MakeFullScreen()
        Fullscreen.Visible = False
        ExitFullScreen.Visible = True
    End Sub
    Public Sub MakeFullScreen()
        Me.SetVisibleCore(False)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        Me.SetVisibleCore(True)
    End Sub

    Private Sub ExitFullScreen_Click(sender As Object, e As EventArgs) Handles ExitFullScreen.Click
        Me.WindowState = FormWindowState.Normal
        Me.Size = New Drawing.Size(1446, 772)  ' MaximumSize
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Fullscreen.Visible = True
        ControlBox = True
        Me.TopMost = False
        ExitFullScreen.Visible = False
    End Sub
End Class

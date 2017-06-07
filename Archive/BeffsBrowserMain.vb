**Archive**
Imports mshtml
Imports BeffsBrowser.My.Resources
Imports System.Net
Imports System.IO
Imports System.Deployment.Application
Public Class BBMain

#Region "load items"
    Dim int As Integer = 0


    Private Sub Loading(ByVal sender As Object, ByVal e As Windows.Forms.WebBrowserProgressChangedEventArgs)

        ToolStripProgressBar1.Maximum = e.MaximumProgress
        ToolStripProgressBar1.Value = e.MaximumProgress
    End Sub

    Private Sub Done(ByVal sender As Object, ByVal e As Windows.Forms.WebBrowserDocumentCompletedEventArgs)
        Tabcontrol1.SelectedTab.Text = CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).DocumentTitle
        ToolStripTextBox1.Text = CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).Url.ToString
        History.ListBox1.Items.Add(ToolStripTextBox1.Text)
        Save_History()
  

    End Sub
    Public Sub LoadUpSettings()
        RestartComputerToolStripMenuItem.Visible = False
        CheckBox1.Visible = False
        Timer1.Start()
        ToolStripLabel1.Text = Today
        ToolStripTextBox1.ShortcutsEnabled = False
        Try
            Dim tab As New TabPage
            Dim brws As New WebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate(My.Settings.HomeSpace)
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
        Catch ex As Exception
        End Try
    End Sub

    Public Sub favload()
        Try
            int = int + 0.5
            Dim tab As New TabPage
            Dim brws As New WebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate(ToolStripTextBox1.Text)
            brws.ScriptErrorsSuppressed = True
        Catch ex As Exception
        End Try

    End Sub
    Private Sub BBMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WebBrowserHelper.FixBrowserVersion()
        AutoScaleMode = My.Settings.Visual
        CheckBox1.Visible = False
        If CheckBox1.Checked = True Then favload()
        CheckBox2.Visible = False
        ToolStripButton6.Visible = False
        Dim statuslabel As New Label
        statuslabel.Anchor = AnchorStyles.Bottom
        If Today = "04/06/2016" Then
            MsgBox("You should check for news or updates today!")
            Try
                int = int + 0.5
                Dim tab As New TabPage
                Dim brws As New WebBrowser
                brws.Dock = DockStyle.Fill
                tab.Text = " New Tab"
                tab.Controls.Add(brws)
                Me.Tabcontrol1.TabPages.Add(tab)
                Me.Tabcontrol1.SelectedTab = tab
                brws.Navigate("http://beffsbrowser.codeplex.com")
                brws.ScriptErrorsSuppressed = True
            Catch ex As Exception
            End Try
        End If
        LoadUpSettings()
       

    End Sub
#End Region
#Region "navigation controls"
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click, RefreshToolStripMenuItem.Click
        Try
            Dim brws As New WebBrowser
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).Refresh()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            Dim brws As New WebBrowser
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).Navigate(ToolStripTextBox1.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ForwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForwardToolStripMenuItem.Click
        Try
            Dim brws As New WebBrowser
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).GoForward()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click

        Try
            Dim brws As New WebBrowser
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).GoBack()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTabToolStripMenuItem.Click
        Try

            Dim tab As New TabPage
            Dim brws As New WebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate(My.Settings.HomeSpace)
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1
        Catch ex As Exception

        End Try

    End Sub

    Private Sub NewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewWindowToolStripMenuItem.Click
        Dim window As New BBMain
        window.Show()
    End Sub



    Private Sub CloseWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseWindowToolStripMenuItem.Click
        Close()
    End Sub
#End Region
#Region "full screen and form close"
    Private Sub BeffsBrowserMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Dim Result As DialogResult
        If Tabcontrol1.TabCount = 2 Then
            Result = MessageBox.Show("You have 2 or more Tabs open. Are you sure you wanna exit?", "BeffsBrowser is shutting down", MessageBoxButtons.YesNo)

            'if user clicked no, cancel form closing
        ElseIf Result = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If

    End Sub

#End Region
#Region "Others"

    Dim tabcontrol As New BeffsBrowser.beffscustomtabcontrol
#End Region
#Region "Utility"
 
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Try
            Dim brws As New WebBrowser
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).Navigate(My.Settings.SearchP & ToolStripTextBox1.Text)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click, StopToolStripMenuItem.Click
        Try
            Dim brws As New WebBrowser
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int - 0.5
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).Stop()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label1.Text = TimeOfDay
    End Sub
    Private Sub ToolStripLabel1_textchanged(sender As Object, e As EventArgs) Handles ToolStripLabel1.TextChanged
        ToolStripLabel1.Text = Today
    End Sub
    Private Sub WhatsNewToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        MsgBox("Added some new improvements removed christmas items.  Copyright 2015-2016 BeffsBrowser ")
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        MsgBox("The current time is:" + vbNewLine + Label1.Text)
    End Sub
    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs)
        MsgBox("BeffsEasy Keyboard is coming soon!")
    End Sub
    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click
        MsgBox("Todays date is:" + vbNewLine + ToolStripLabel1.Text)
    End Sub
    Private Sub RestartComputerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartComputerToolStripMenuItem.Click
        'Dim window As New rebootcomputercap
        'window.Show()
        MessageBox.Show("This feature has been temp. removed. See the change log for version 1.6.0 for more information.")
    End Sub
    Private Sub DaysTillXmasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DaysTillXmasToolStripMenuItem.Click
        MsgBox("Days until Xmas: " & DateDiff("d", Now, "December 25, 2016"))
    End Sub

#Region "MoreItems mostly random and newer"
    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Try
            Dim tab As New TabPage
            Dim brws As New WebBrowser
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate(My.Settings.HomeSpace) 'change it to your browser control name
            My.Settings.Save()
            My.Settings.Reload()
        Catch ex As Exception

        End Try


    End Sub
    Private Sub SettingsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem1.Click
        Dim window As New BBSettingsBasics
        window.Show()
    End Sub
    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim brws As New WebBrowser
        Try
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).ShowPrintDialog()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Beep()
        MsgBox(" BeffsBrowser (c) 2015-2017")

    End Sub
    Private Sub PrintToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem1.Click
        Dim brws As New WebBrowser
        Try
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).ShowPrintDialog()
        Catch ex As Exception
        End Try
    End Sub
#End Region
#Region "Lastly items"
    Public Event Navigated As WebBrowserNavigatedEventHandler
    Private Sub toolStripTextBox1_KeyDown( _
    ByVal sender As Object, ByVal e As KeyEventArgs) _
    Handles ToolStripTextBox1.KeyDown

        Try
            If (e.KeyCode = Keys.Enter) Then
                Dim brws As New WebBrowser
                brws.ScriptErrorsSuppressed = True
                AddHandler brws.ProgressChanged, AddressOf Loading
                AddHandler brws.DocumentCompleted, AddressOf Done
                int = int + 0.5
                CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).Navigate(ToolStripTextBox1.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SaveFileAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveFileAsToolStripMenuItem.Click
        CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).ShowSaveAsDialog()
    End Sub
    Private Sub toolstripTextBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ToolStripTextBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If My.Computer.Clipboard.ContainsText Then
                ToolStripTextBox1.Paste()
                CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).Navigate(ToolStripTextBox1.Text)
                If Not ToolStripTextBox1.Text = "http://" & "https://" Then
                    MsgBox("sorry for shortcuts you will need the http:// or https://")
                    CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).GoBack()
                End If
            End If
        End If
    End Sub
    Public Sub Save_History()
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(My.Settings.History, True)
        For Each Str As String In History.ListBox1.Items
            file.WriteLine(Str)
        Next
        file.Close()
    End Sub

    Private Sub PrintPreviewToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem1.Click
        CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).ShowPrintPreviewDialog()
    End Sub

'FullScreen
    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click, FullScreenToolStripMenuItem.Click
        ToolStripButton5.Visible = False
        ControlBox = False
        Me.WindowState = FormWindowState.Maximized
        Me.Size = SystemInformation.PrimaryMonitorSize
        Me.WindowState = 2
        Me.Location = New Point(0, 0)
        Me.TopMost = True
        Me.FormBorderStyle = 0
        Me.TopMost = True
        ToolStripButton6.Visible = True
    End Sub
'Exit FullScreen.
    Private Sub ToolStripButton6_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton6.Click, ExitFullScreenToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Normal
        Me.Size = MaximumSize
        Me.FormBorderStyle = FormBorderStyle.Sizable
        ToolStripButton5.Visible = True
        ControlBox = True
        Me.TopMost = False
        ToolStripButton6.Visible = False
    End Sub

    Public Sub favorites()
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(My.Settings.Favlist, True)
        For Each Str As String In NewFavs.ListBox1.Items
            file.WriteLine(Str)
        Next
        file.Close()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            favload()
        ElseIf CheckBox1.Checked = False Then
            'do nothing
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Try
                int = int + 0.5
                Dim tab As New TabPage
                Dim brws As New WebBrowser
                brws.Dock = DockStyle.Fill
                tab.Text = " New Tab"
                tab.Controls.Add(brws)
                Me.Tabcontrol1.TabPages.Add(tab)
                Me.Tabcontrol1.SelectedTab = tab
                brws.Navigate(ToolStripTextBox1.Text)
                brws.ScriptErrorsSuppressed = True
            Catch ex As Exception
            End Try
        Else
            'do nothing 

        End If
    End Sub
    Private Sub ToolStripLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripLabel2.Click
        CheckForUpdates.Show()
    End Sub
    Public Sub updates()
        Try
            Dim tab As New TabPage
            Dim brws As New WebBrowser
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate("http://" + CheckForUpdates.TextBox1.Text) 'change it to your browser control name
            My.Settings.Save()
            My.Settings.Reload()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub YouTubeConverterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YouTubeConverterToolStripMenuItem.Click
        Dim a As New BeffsYouTubeDownloader
        a.Show()
    End Sub

    Private Sub AddToFavoritesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToFavoritesToolStripMenuItem.Click
        NewFavs.ListBox1.Items.Add(ToolStripTextBox1.Text)
        favorites()
    End Sub


End Class

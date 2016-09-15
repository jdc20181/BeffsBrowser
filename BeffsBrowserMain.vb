'This is the updated code and is a little messy as it isn't ready for release quite yet so you may get errors!
'Some of the unneeded code I removed it was code to open for example BeffsEasy notes, it isn't needed until you wish to add that to your
'project and prevents errors from occuring.


Imports mshtml
Imports BeffsBrowser.My.Resources

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
    End Sub
    Private Sub BBMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Today = "02/15/2016" Then
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

        Timer1.Start()
        ToolStripLabel1.Text = Today

    End Sub

    Private Sub beffsbrowsermain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ToolStripTextBox1.ShortcutsEnabled = False
        Try
            Dim tab As New TabPage
            Dim brws As New WebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate("C:\Users\y\Pictures\Camera Roll\Version1.2.7Splashscreen.png")
            brws.ScriptErrorsSuppressed = True
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
        Catch ex As Exception
        End Try
    End Sub
#End Region
#Region "navigation controls"
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
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

    Private Sub CloseTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseTabToolStripMenuItem.Click
        Tabcontrol1.Controls.Remove(Tabcontrol1.SelectedTab)
    End Sub

    Private Sub CloseWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseWindowToolStripMenuItem.Click
        Close()
    End Sub
#End Region
#Region "full screen and form close"
    Private Sub BeffsBrowserMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'display message on form closing
        Dim Result As DialogResult
        Result = MessageBox.Show("Are you sure you wanna exit?", "BeffsBrowser is shutting down", MessageBoxButtons.YesNo)

        'if user clicked no, cancel form closing
        If Result = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

#End Region
#Region "Others"




    Private Sub HideAddressBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideaddressbarToolStripMenuItem.Click
        If HideaddressbarToolStripMenuItem.Checked = True Then
            ToolStrip1.Visible = False
            HideaddressbarToolStripMenuItem.Checked = False
        ElseIf HideaddressbarToolStripMenuItem.Checked = False Then
            ToolStrip1.Visible = True
            HideaddressbarToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub FullScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullScreenToolStripMenuItem.Click
        If FullScreenToolStripMenuItem.Checked = False Then
            ToolStrip1.Visible = False

            FullScreenToolStripMenuItem.Checked = False
            Me.WindowState = FormWindowState.Maximized
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            FullScreenToolStripMenuItem.Checked = True
        ElseIf FullScreenToolStripMenuItem.Checked = True Then
            ToolStrip1.Visible = True

            FullScreenToolStripMenuItem.Checked = True
            Me.WindowState = FormWindowState.Normal
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            FullScreenToolStripMenuItem.Checked = False
        End If
    End Sub
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
    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
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


#End Region
#Region "Random"
    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label1.Text = TimeOfDay
    End Sub

#End Region
#Region "other items"
    Private Sub CalendarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalendarToolStripMenuItem.Click
        Dim window As New Form1
        window.Show()
    End Sub
    Private Sub ToolStripLabel1_textchanged(sender As Object, e As EventArgs) Handles ToolStripLabel1.TextChanged
        ToolStripLabel1.Text = Today
    End Sub


 
#End Region
#Region "More items misc utility mixed"
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        MsgBox("The current time is:" + vbNewLine + Label1.Text)
    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click
        MsgBox("Todays date is:" + vbNewLine + ToolStripLabel1.Text)
    End Sub
    
    Private Sub DaysTillXmasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DaysTillXmasToolStripMenuItem.Click
        MsgBox("Days until Xmas: " & DateDiff("d", Now, "December 25, 2016"))
    End Sub
#End Region
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
    
    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim brws As New WebBrowser
        Try
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).ShowPrintDialog()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RestartBeffsBrowserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartBeffsBrowserToolStripMenuItem.Click
        Application.Restart()
    End Sub
    Private Sub ExitBeffsBrowserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitBeffsBrowserToolStripMenuItem.Click
        Application.Exit()
    End Sub
    Private Sub FilterBrowsingBetaV02ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilterBrowsingBetaV02ToolStripMenuItem.Click
        Dim window As New FilterBrowsingBeta
        window.Show()
    End Sub
    
    Private Sub PrintToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem1.Click
        Dim brws As New WebBrowser
        Try
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).ShowPrintDialog()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PrintPreviewToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem1.Click
        Try
        Catch ex As Exception
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).ShowPrintPreviewDialog()
        End Try
    End Sub

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
                    MsgBox("You need a actual website search bar is coming soon")
                    CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).GoBack()
                End If
            End If
        End If
    End Sub


End Class


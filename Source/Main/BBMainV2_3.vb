Imports BeffsBrowser.My.Resources
Imports System.Net
Imports System.IO
Imports System.Deployment.Application
Imports Gecko
Imports System.Windows.Forms

Public Class BBMain

#Region "load items"
    Dim int As Integer = 0


    Private Sub Loading(ByVal sender As Object, ByVal e As Gecko.GeckoProgressEventArgs)
        Tabcontrol1.SelectedTab.Text = CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).DocumentTitle
        If Tabcontrol1.SelectedTab.Text = Nothing Then
            Tabcontrol1.SelectedTab.Text = CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString
        End If
        ToolStripProgressBar1.Maximum = e.MaximumProgress
        ToolStripProgressBar1.Value = e.MaximumProgress
        Me.Cursor = Cursors.AppStarting

    End Sub

    Private Sub Done(ByVal sender As Object, ByVal e As Gecko.Events.GeckoDocumentCompletedEventArgs)

        Me.Cursor = Cursors.Default
        ToolStripTextBox1.Text = CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString
        Save_History()


        If CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url = New Uri("https://jdc20181.github.io/StartPage/Startpage2.html") Then
            Dim nameIndex, urlIndex As Integer

            Dim names() As String = {My.Settings.StartPageName1, My.Settings.StartPageName2, My.Settings.StartPageName3, My.Settings.StartPageName4, My.Settings.StartPageName5, My.Settings.StartPageName6, My.Settings.StartPageName7, My.Settings.StartPageName8, My.Settings.StartPageName9, My.Settings.StartPageName10}
            Dim urls() As String = {My.Settings.StartPage1, My.Settings.StartPage2, My.Settings.StartPage3, My.Settings.StartPage4, My.Settings.StartPage5, My.Settings.StartPage6, My.Settings.StartPage7, My.Settings.StartPage8, My.Settings.StartPage9, My.Settings.StartPage10}
            Dim wb As GeckoWebBrowser = CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser)

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


    Public Sub LoadUpSettings()
        CheckBox1.Visible = False


        ToolStripTextBox1.ShortcutsEnabled = False
        Try
            Dim tab As New TabPage
            Dim brws As New GeckoWebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate(My.Settings.HomeSpace)

            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done

         
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Favload()

        int = int + 0.5
            Dim tab As New TabPage
            Dim brws As New GeckoWebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate(ToolStripTextBox1.Text)



    End Sub
    Public Sub RestoreStartUp()
        Dim HistoryPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) &
                         "\.\BeffsBrowserData" &
                         My.Settings.Restore
        Dim itm As String
        For Each itm In File.ReadAllLines(HistoryPath)



            Dim tab As New TabPage
                Dim brws As New GeckoWebBrowser
                brws.Dock = DockStyle.Fill
                tab.Text = " New Tab"
                tab.Controls.Add(brws)
                Me.Tabcontrol1.TabPages.Add(tab)
                Me.Tabcontrol1.SelectedTab = tab
                brws.Navigate(itm)
                AddHandler brws.ProgressChanged, AddressOf Loading
                AddHandler brws.DocumentCompleted, AddressOf Done
                int = int + 1

            System.IO.File.WriteAllText(HistoryPath, "")

        Next

    End Sub
    Private Sub BBMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Visual Settings
        'This is for URLbox color
        ToolStripTextBox1.BackColor = ColorTranslator.FromHtml(My.Settings.Color)
        'This is for button colors
        ToolStripButton1.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton2.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton3.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton4.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton7.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton8.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton9.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton10.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripButton11.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripLabel2.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        Fullscreen.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ExitFullScreen.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStripDropDownButton2.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)
        ToolStrip1.BackColor = ColorTranslator.FromHtml(My.Settings.ButtonColor)


        'End Visual Settings
        RestoreStartUp()
        ToolStripTextBox1.Control.ContextMenuStrip = ContextMenuStrip1
        Tabcontrol1.ContextMenuStrip = ContextMenuStrip2
        ToolStripDropDownButton2.Alignment =
            System.Windows.Forms.ToolStripItemAlignment.Right
        'Label1.Visible = False
        ToolStripDropDownButton1.Visible = False

        WebBrowserHelper.FixBrowserVersion()
        AutoScaleMode = My.Settings.Visual
        CheckBox1.Visible = False
        If CheckBox1.Checked = True Then Favload()
        CheckBox2.Visible = False
        ExitFullScreen.Visible = False
        Dim statuslabel As New Label
        statuslabel.Anchor = AnchorStyles.Bottom
        LoadUpSettings()

    End Sub
#End Region


#Region "navigation controls"
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click, RefreshToolStripMenuItem.Click

        Dim brws As New GeckoWebBrowser

            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Reload()

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click

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

            CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(ToolStripTextBox1.Text)
            RestoreSave()


        ElseIf CheckURL(ToolStripTextBox1.Text) = False Then
            search()

        End If
    End Sub
    Public Sub search()
        Dim brws As New GeckoWebBrowser
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 0.5
        CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(My.Settings.SearchP & ToolStripTextBox1.Text)
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

    Private Sub ForwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForwardToolStripMenuItem.Click, ToolStripButton11.Click

        Dim brws As New GeckoWebBrowser
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GoForward()


    End Sub

    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click, ToolStripButton9.Click


        Dim brws As New GeckoWebBrowser
        AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GoBack()

    End Sub

    Private Sub NewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTabToolStripMenuItem.Click, ToolStripButton10.Click


        Dim tab As New TabPage
            Dim brws As New GeckoWebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate(My.Settings.HomeSpace)
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1


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
    Public Sub RestoreSave()
        Dim HistoryPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) &
                      "\.\BeffsBrowserData" &
                      My.Settings.Restore
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(HistoryPath, True)

        Dim page As TabPage
        For Each page In Me.Tabcontrol1.TabPages

            file.WriteLine(CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString)

        Next



        file.Close()

    End Sub
    Private Sub BeffsBrowserMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim shouldSave = (My.Settings.RestoreSetting = "Enabled")
        Dim hastabs2 = (Tabcontrol1.TabCount >= 2)
        If shouldSave AndAlso hastabs2 Then
            RestoreSave()
        Else

        End If
        Dim shouldWarn = (My.Settings.TabCloseWarning = "Yes")
        Dim hasTabs = (Tabcontrol1.TabCount >= 2)

        If shouldWarn AndAlso hasTabs Then
            Dim shouldCloseResult = MessageBox.Show("You have 2 or more tabs open. Are you sure you wanna exit?" & vbNewLine & "A Total of" & " " & Tabcontrol1.TabCount & " " & "Tabs will be closed", "Closing Multi-Tabbed Window", MessageBoxButtons.YesNo)

            If shouldCloseResult = DialogResult.No Then
                e.Cancel = True
            End If
        End If

    End Sub

#End Region
#Region "Others"

    Private Sub ReportBugsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim frmmanage As New Security
        frmmanage.ShowDialog()
    End Sub





    Dim tabcontrol As New BeffsBrowser.beffscustomtabcontrol
#End Region
#Region "Utility"
    Private Sub CalculatorToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim window As New BeffsCalculator
        window.Show()
    End Sub
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Try
            Dim brws As New GeckoWebBrowser
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 0.5
            CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(My.Settings.SearchP & ToolStripTextBox1.Text)
        Catch ex As Exception
        End Try
    End Sub
#End Region
#Region "Random"

    Private Sub ManageFavoritesToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Dim window As New Favorites
        window.Show()
    End Sub
#End Region

#Region "More items misc utility mixed"



    Private Sub DaysTillXmasToolStripMenuItem_Click(sender As Object, e As EventArgs)
        MsgBox("Days until Xmas: " & DateDiff("d", Now, "December 25, 2017"))
    End Sub
#End Region
#Region "MoreItems mostly random and newer"
    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        ' Try
        Dim tab As New TabPage
        Dim brws As New GeckoWebBrowser
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
    End Sub


    Private Sub FavoritesToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Dim window As New Favorites
        window.Show()
    End Sub



#End Region
#Region "Lastly items"
    Public Event Navigated As WebBrowserNavigatedEventHandler
    Private Sub ToolStripTextBox1_KeyDown(
    ByVal sender As Object, ByVal e As KeyEventArgs) _
    Handles ToolStripTextBox1.KeyDown

        If (e.KeyCode = Keys.Enter) Then
   
            Dim s As String = ToolStripTextBox1.Text
            Dim fHasSpace As Boolean = s.Contains(" ")

            If fHasSpace = True Then
                search()
            ElseIf fHasSpace = False Then
                UrlNavigate()
            End If
        End If
  
    End Sub




    Public Sub Save_History()
        Dim HistoryPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) &
                         "\.\BeffsBrowserData" &
                         My.Settings.History
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(HistoryPath, True)
   
        file.WriteLine(CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString())

        file.Close()
    End Sub


#End Region
#Region "NewItems"
    Private Sub MoreToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        Dim b As New BBM
        b.Show()

    End Sub

    Private Sub BeffsEasyCaptureToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim window As New beffseasycapture
        window.Show()
    End Sub

    Private Sub BeffsEasyNotesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim window As New BeffsEasyNotes
        window.Show()
    End Sub



    Private Sub CalculatorToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Dim window As New BeffsCalculator
        window.Show()
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles Fullscreen.Click, FullScreenToolStripMenuItem.Click
        MakeFullScreen()
        Fullscreen.Visible = False
        ExitFullScreen.Visible = True
    End Sub

    Private Sub ToolStripButton6_Click_1(sender As Object, e As EventArgs) Handles ExitFullScreen.Click, ExitFullScreenToolStripMenuItem.Click
        'NormalMode()
        'ToolStripButton6.Visible = False
        ' ToolStripButton5.Visible = True
        Me.WindowState = FormWindowState.Normal
        Me.Size = New Drawing.Size(1446, 772)  ' MaximumSize
        FormBorderStyle = FormBorderStyle.Sizable
        Fullscreen.Visible = True
        ControlBox = True
        Me.TopMost = False
        ExitFullScreen.Visible = False
    End Sub
    Public Sub Favorites()
        Dim FavPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) &
                         "\.\BeffsBrowserData" &
                         My.Settings.Favlist
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(FavPath, True)
     
        file.WriteLine(CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString())


        file.Close()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Favload()
        ElseIf CheckBox1.Checked = False Then
            'do nothing
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Try
                int = int + 0.5
                Dim tab As New TabPage
                Dim brws As New GeckoWebBrowser
                brws.Dock = DockStyle.Fill
                tab.Text = " New Tab"
                tab.Controls.Add(brws)
                Me.Tabcontrol1.TabPages.Add(tab)
                Me.Tabcontrol1.SelectedTab = tab
                brws.Navigate(ToolStripTextBox1.Text)
            Catch ex As Exception
            End Try
        Else
            'do nothing 

        End If
    End Sub
    Private Sub ToolStripLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripLabel2.Click
        CheckForUpdates.Show()
    End Sub
    Public Sub Updates()
        Try
            Dim tab As New TabPage
            Dim brws As New GeckoWebBrowser
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
#End Region
#Region "Misc Items"

    Public Sub MakeFullScreen()
        Me.SetVisibleCore(False)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        Me.SetVisibleCore(True)
    End Sub





  



    Private Sub AddToFavoritesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddToFavoritesToolStripMenuItem1.Click, ToolStripButton8.Click
        favorites()
    End Sub





#End Region

  

 
  



#Region "StartPagev2 "
    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        ' Try

        Dim tab As New TabPage
            Dim brws As New GeckoWebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.Tabcontrol1.TabPages.Add(tab)
            Me.Tabcontrol1.SelectedTab = tab
            brws.Navigate("https://jdc20181.github.io/StartPage/Startpage2.html")
            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1
        ' Catch ex As Exception

        '    End Try
    End Sub
   
#End Region
#Region "MenuStrip URL Box"
 



    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If ToolStripTextBox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(ToolStripTextBox1.SelectedText)

        Else


        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If ToolStripTextBox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(ToolStripTextBox1.SelectedText)

        End If
        ToolStripTextBox1.SelectedText = ""
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If My.Computer.Clipboard.ContainsText Then
            ToolStripTextBox1.Paste()
        End If
    End Sub

    Private Sub PasteAndGoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteAndGoToolStripMenuItem.Click
        If My.Computer.Clipboard.ContainsText Then
            ToolStripTextBox1.Paste()
            Try
                Dim brws As New GeckoWebBrowser

                AddHandler brws.ProgressChanged, AddressOf Loading
                AddHandler brws.DocumentCompleted, AddressOf Done
                int = int + 1
                CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(ToolStripTextBox1.Text)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub PasteAndSearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteAndSearchToolStripMenuItem.Click
        If My.Computer.Clipboard.ContainsText Then
            ToolStripTextBox1.Paste()
            Try
                Dim brws As New GeckoWebBrowser
                AddHandler brws.ProgressChanged, AddressOf Loading
                AddHandler brws.DocumentCompleted, AddressOf Done
                int = int + 0.5
                CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(My.Settings.SearchP & ToolStripTextBox1.Text)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        ToolStripTextBox1.SelectAll()

    End Sub
#End Region

    Private Sub SavePageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SavePageToolStripMenuItem.Click
        Dim sfd = New SaveFileDialog()
        sfd.Filter = " Html File | *.html"
        If sfd.ShowDialog() = DialogResult.OK Then



            CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).SaveDocument(sfd.FileName)
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click


        CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate("javascript:print()")


    End Sub
#Region "Test - Close tabs to right"
    Private Sub RemoveTabsToRight(tabControl As TabControl, tabPage As TabPage)
        Dim index = tabControl.TabPages.IndexOf(tabPage)
        Dim i As Integer
        For i = tabControl.TabCount - 1 To index + 1 Step -1
            tabControl.TabPages.RemoveAt(i)
        Next
    End Sub

    Private Sub CloseTabsToRightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseTabsToRightToolStripMenuItem.Click
        RemoveTabsToRight(Me.Tabcontrol1, Me.Tabcontrol1.SelectedTab)

    End Sub

    Private Sub ToolStripTextBox1_MouseHover(sender As Object, e As EventArgs) Handles ToolStripTextBox1.MouseHover
        ToolStripTextBox1.TextBox.Width = 700
    End Sub

    Private Sub ToolStripTextBox1_MouseLeave(sender As Object, e As EventArgs) Handles ToolStripTextBox1.MouseLeave
        ToolStripTextBox1.TextBox.Width = 650
    End Sub



#End Region

End Class
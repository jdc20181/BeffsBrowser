'This is the coding needed for the main form please see the buildingBeffsBrowser documentation before continuing'
Public Class BeffsBrowserMain
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim tab As New TabPage
            Dim brws As New WebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.TabControl1.TabPages.Add(tab)
            Me.TabControl1.SelectedTab = tab
            brws.Navigate("")
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Refresh()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Navigate(ToolStripTextBox1.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ForwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForwardToolStripMenuItem.Click
        CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).GoForward()
    End Sub

    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).GoBack()
    End Sub

    Private Sub NewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTabToolStripMenuItem.Click
        Try
            Dim tab As New TabPage
            Dim brws As New WebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = "New Tab"
            tab.Controls.Add(brws)
            Me.TabControl1.TabPages.Add(tab)
            Me.TabControl1.SelectedTab = tab
            brws.Navigate("")


        Catch ex As Exception

        End Try
    End Sub

    Private Sub NewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewWindowToolStripMenuItem.Click
        Dim window As New BeffsBrowserMain
        window.Show()
    End Sub

    Private Sub CloseTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseTabToolStripMenuItem.Click
        TabControl1.Controls.Remove(TabControl1.SelectedTab)
    End Sub

    Private Sub CloseWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseWindowToolStripMenuItem.Click
        Close()
    End Sub
    
   ' Some code has been removed because you dont need it just yet, see the extending the basics for more info'

Imports System.IO

Public Class Favorites
    Dim FavsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) &
                             "\.\BeffsBrowserData" &
                             My.Settings.Favlist

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.AddRange(File.ReadAllLines(FavsPath))
        ListBox1.BackColor = ColorTranslator.FromHtml("#3985f6")
        ListBox1.ForeColor = ColorTranslator.FromHtml("#FFF")
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)
        File.WriteAllLines(FavsPath, ListBox1.Items.Cast(Of String)())
    End Sub

    Private Sub NavigateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NavigateToolStripMenuItem.Click
        BBMain.ToolStripTextBox1.Text = ListBox1.SelectedItem

        BBMain.CheckBox1.Checked = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub CopyURLMenuitem_Click(sender As Object, e As EventArgs) Handles CopyURLMenuitem.Click
        My.Computer.Clipboard.Clear()

        My.Computer.Clipboard.SetText(ListBox1.SelectedItem)

    End Sub
#Region "Allow Movement!"
    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown

        If e.Button = MouseButtons.Left Or MouseButtons.Right Then
            IsFormBeingDragged = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp

        If e.Button = MouseButtons.Left Or MouseButtons.Right Then
            IsFormBeingDragged = False
        End If
    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove

        If IsFormBeingDragged Then
            Dim temp As Point = New Point()

            temp.X = Me.Location.X + (e.X - MouseDownX)
            temp.Y = Me.Location.Y + (e.Y - MouseDownY)
            Me.Location = temp
            temp = Nothing
        End If
    End Sub
#End Region
End Class

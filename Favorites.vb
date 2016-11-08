

Imports System.IO

Public Class NewFavs

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.AddRange(File.ReadAllLines(My.Settings.Favlist))

    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)
        File.WriteAllLines(My.Settings.Favlist, ListBox1.Items.Cast(Of String)())
    End Sub

    Private Sub NavigateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NavigateToolStripMenuItem.Click
        BBMain.ToolStripTextBox1.Text = ListBox1.SelectedItem
        BBMain.checkbox1.checked = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class

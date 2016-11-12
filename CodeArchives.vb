'Not offical project code but a directory of code snippets'

'New Code for fullscreen needs to be edited.
Public Class frmMainForm
    Private Sub cmdCloseForm_Click(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles cmdCloseForm.Click

        Close()

    End Sub
    Private Sub cmdChange_Click(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles cmdChange.Click

        If cmdChange.Text = "Full" Then

            cmdChange.Text = "Normal"
            cmdMake.Text = "Normal"

            FullScreen(chkTaskbar.Checked)

            If chkTaskbar.Checked Then
                Me.FullScreen(True)
            End If
        Else
            cmdChange.Text = "Full"
            cmdMake.Text = "Full"
            NormalMode()
        End If

    End Sub
    Private Sub cmdMsgBox_Click(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles cmdMsgBox.Click
        MessageBox.Show("Hello")
    End Sub
    Private Sub cmdShowChildForm_Click(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles cmdShowChildForm.Click

        Dim f As New frmChildForm
        Dim TopMostSetting As Boolean = Me.TopMost

        Try
            Me.TopMost = False
            f.ShowDialog()
        Finally
            f.Dispose()
            Me.TopMost = TopMostSetting
        End Try
    End Sub
    Public Sub MakeFullScreen()
        Me.SetVisibleCore(False)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        Me.SetVisibleCore(True)
    End Sub
    Private Sub cmdMake_Click(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles cmdMake.Click

        If cmdMake.Text = "Full" Then
            cmdMake.Text = "Normal"
            cmdChange.Text = "Normal"
            MakeFullScreen()
        Else
            cmdMake.Text = "Full"
            cmdChange.Text = "Full"
            NormalMode()
        End If

    End Sub
    Private Sub cmdDetect_Click(sender As Object, e As EventArgs) Handles cmdDetect.Click
        MessageBox.Show(Me.WindowState.ToString)
    End Sub
    Private Sub frmMainForm_StyleChanged(
        sender As Object,
        e As EventArgs) Handles Me.StyleChanged

        ListBox1.Items.Add(Me.WindowState.ToString)
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        NormalMode()

    End Sub
End Class








______________________________
'Attention this code is archived!
'Enter fullscreen
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
'Exit Fullscreen
'This code is the same but is being added to archive so it isnt confusing.
  Me.WindowState = FormWindowState.Normal
        Me.Size = MaximumSize
        Me.FormBorderStyle = FormBorderStyle.Sizable
        ToolStripButton5.Visible = True
        ControlBox = True
        Me.TopMost = False
        ToolStripButton6.Visible = False
________________________________
Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            ComboBox1.Text = "https://www.google.com/?gws_rd=ssl#q="
            CheckBox2.Enabled = False
        ElseIf CheckBox1.Checked = False Then
            CheckBox2.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            ComboBox1.Text = "http://www.bing.com/search?q="
            CheckBox1.Enabled = False
        ElseIf CheckBox2.Checked = False Then
            CheckBox1.Enabled = True
        End If
    End Sub
___________________

Imports System.IO
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Net.Mail
Public Class BugReport

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim mail As New MailMessage




        ' Set your Gmail email address
        mail.From = New MailAddress("bugs@beffschromeproject.tk")

        ' Add recipient email address
        mail.To.Add("bugs@beffschromeproject.tk")

        mail.Subject = ComboBox1.Text

        ' Set email body

        mail.Body = message.Text
          Dim SMTP As New SmtpClient("smtp@gmail.com")
        SMTP.EnableSsl = True
        SMTP.Credentials = New System.Net.NetworkCredential("jdc20181@gmail.com", "*****************************")
        SMTP.Port = "587"

        SMTP.Send(mail)


    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        MsgBox("I appreciate any bugs reported or problems or even questions, but no warranty is given to BeffsBrowser as stated in license." + vbNewLine + "Your Email will not be shared", vbInformation, "Disclaimer")
    End Sub

    Private Sub BugReport_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'display message on form closing
        Dim Result As DialogResult
        Result = MessageBox.Show("Are you sure you wanna exit? Your Message will not be sent.", "Warning", MessageBoxButtons.YesNo)

        'if user clicked no, cancel form closing
        If Result = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

  

   
  
 
    Private Sub message_MouseClick(sender As Object, e As MouseEventArgs) Handles message.MouseClick
        message.Text = ""
    End Sub

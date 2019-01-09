Imports System.IO
Imports System.Speech.Synthesis
Public Class BeffsEasyNotes
    Private currentFile As String
    Private checkPrint As Integer
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If TextBox1.Modified Then

            Dim answer As Integer
            answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If answer = Windows.Forms.DialogResult.Yes Then
                TextBox1.Clear()
            Else
                Exit Sub
            End If
        Else
            TextBox1.Clear()
        End If


        currentFile = ""
        Me.Text = "Editor: New Document"
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        'Check if there's text added to the textbox
        If TextBox1.Modified Then
            'If the text of notepad changed, the program will ask the user if they want to save the changes
            Dim ask As MsgBoxResult
            ask = MsgBox("Do you want to save the changes", MsgBoxStyle.YesNoCancel, "Open Document")
            If ask = MsgBoxResult.No Then
                OpenFileDialog1.ShowDialog()
                TextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
            ElseIf ask = MsgBoxResult.Cancel Then
            ElseIf ask = MsgBoxResult.Yes Then
                SaveFileDialog1.ShowDialog()
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                TextBox1.Clear()
            End If
        Else
            
            OpenFileDialog1.ShowDialog()
            Try
                TextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click   
        SaveFile(currentFile)
    End Sub
    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        'check if textbox can undo
        If TextBox1.CanUndo Then
            TextBox1.Undo()
        Else
        End If
    End Sub
    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        'check if textbox can undo
        If TextBox1.CanRedo Then
            TextBox1.Redo()
        Else
        End If
    End Sub
    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If TextBox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(TextBox1.SelectedText)
        End If
        TextBox1.SelectedText = ""
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If TextBox1.SelectionLength > 0 Then
        Else
            My.Computer.Clipboard.SetText(TextBox1.SelectedText)
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If My.Computer.Clipboard.ContainsText Then
            TextBox1.Paste()
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        TextBox1.SelectAll()
    End Sub

    Private Sub SearchToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem1.Click
        Dim a As String
        Dim b As String
        a = InputBox("Enter text to be found")
        b = InStr(TextBox1.Text, a)
        If b Then
            TextBox1.Focus()
            TextBox1.SelectionStart = b - 1
            TextBox1.SelectionLength = Len(a)
        Else
            MsgBox("Text not found.")
        End If
    End Sub
    Private Sub LeftToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeftToolStripMenuItem.Click
        TextBox1.SelectionAlignment = HorizontalAlignment.Left
        LeftToolStripMenuItem.Checked = True
        CenterToolStripMenuItem.Checked = False
        RightToolStripMenuItem.Checked = False
    End Sub

    Private Sub CenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CenterToolStripMenuItem.Click
        TextBox1.SelectionAlignment = HorizontalAlignment.Center
        LeftToolStripMenuItem.Checked = False
        CenterToolStripMenuItem.Checked = True
        RightToolStripMenuItem.Checked = False

    End Sub

    Private Sub RightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RightToolStripMenuItem.Click
        TextBox1.SelectionAlignment = HorizontalAlignment.Right
        LeftToolStripMenuItem.Checked = False
        CenterToolStripMenuItem.Checked = True
        RightToolStripMenuItem.Checked = False

    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer2.Interval = My.Settings.AutoSaveInterval

        If My.Settings.AutoSave = "Enabled" Then
            Timer1.Start()
        ElseIf My.Settings.AutoSave = "Disabled" Then
            Timer1.Stop()
        End If
 



        'calling the below function to start the context ment (when the user right click the richtextbox
        InitializeMyContextMenu()

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub InitializeMyContextMenu()
        ' Create the contextMenu and the MenuItem to add.
        Dim contextMenu1 As New ContextMenu()
        Dim menuItem1 As New MenuItem("C&ut")
        AddHandler menuItem1.Click, AddressOf CutToolStripButton_Click
        Dim menuItem2 As New MenuItem("&Copy")
        AddHandler menuItem2.Click, AddressOf CopyToolStripButton_Click
        Dim menuItem3 As New MenuItem("&Paste")
        AddHandler menuItem3.Click, AddressOf PasteToolStripButton_Click
        ' Use the MenuItems property to call the Add method
        ' to add the MenuItem to the MainMenu menu item collection.
        contextMenu1.MenuItems.Add(menuItem1)
        contextMenu1.MenuItems.Add(menuItem2)
        contextMenu1.MenuItems.Add(menuItem3)
        ' Assign mainMenu1 to the rich text box.
        textbox1.ContextMenu = contextMenu1
    End Sub








    Private Sub IncreaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles IncreaseToolStripMenuItem1.Click
        Try
            textbox1.SelectionFont = New Font(textbox1.SelectionFont.FontFamily, Int(textbox1.SelectionFont.SizeInPoints + 5))
        Catch ex As Exception
        End Try
        textbox1.Focus()
    End Sub

    Private Sub DecreaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DecreaseToolStripMenuItem1.Click
        Try
            textbox1.SelectionFont = New Font(textbox1.SelectionFont.FontFamily, Int(textbox1.SelectionFont.SizeInPoints - 5))
        Catch ex As Exception
        End Try
        textbox1.Focus()

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If textbox1.SelectionFont.Bold = True Then
            If textbox1.SelectionFont.Italic = True Then
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Regular + FontStyle.Italic)
            Else
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Regular)
            End If

        ElseIf textbox1.SelectionFont.Bold = False Then
            If textbox1.SelectionFont.Italic = True Then
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Bold + FontStyle.Italic)
            Else
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Bold)
            End If
        End If
        textbox1.Focus()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If textbox1.SelectionFont.Italic = True Then
            If textbox1.SelectionFont.Bold = True Then
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Regular + FontStyle.Bold)
            Else
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Regular)
            End If

        ElseIf textbox1.SelectionFont.Italic = False Then
            If textbox1.SelectionFont.Bold = True Then
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Italic + FontStyle.Bold)
            Else
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Italic)
            End If
        End If
        textbox1.Focus()
    End Sub

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles NewToolStripButton.Click
        If textbox1.Modified Then

            Dim answer As Integer
            answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If answer = Windows.Forms.DialogResult.Yes Then
                textbox1.Clear()
            Else
                Exit Sub
            End If

        Else

            textbox1.Clear()

        End If

        currentFile = ""
        Me.Text = "Editor: New Document"

    End Sub

    Private Sub OpenFile()

        OpenFileDialog1.Title = "RTE - Open File"
        OpenFileDialog1.DefaultExt = "rtf"
        OpenFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.ShowDialog()

        If OpenFileDialog1.FileName = "" Then Exit Sub

        Dim strExt As String
        strExt = System.IO.Path.GetExtension(OpenFileDialog1.FileName)
        strExt = strExt.ToUpper()

        Select Case strExt
            Case ".RTF"
                textbox1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.RichText)
            Case Else
                Dim txtReader As System.IO.StreamReader
                txtReader = New System.IO.StreamReader(OpenFileDialog1.FileName)
                textbox1.Text = txtReader.ReadToEnd
                txtReader.Close()
                txtReader = Nothing
                textbox1.SelectionStart = 0
                textbox1.SelectionLength = 0
        End Select

        currentFile = OpenFileDialog1.FileName
        textbox1.Modified = False
        Me.Text = "Editor: " & currentFile.ToString()

    End Sub
    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        If textbox1.Modified Then

            Dim answer As Integer
            answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If answer = Windows.Forms.DialogResult.No Then
                Exit Sub

            Else
                OpenFile()
            End If
        Else
            OpenFile()

        End If
    End Sub
    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        SaveFile(currentFile)


    End Sub
    Private Sub CutToolStripButton_Click(sender As Object, e As EventArgs) Handles CutToolStripButton.Click
        My.Computer.Clipboard.Clear()
        If textbox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(textbox1.SelectedText)

        End If
        textbox1.SelectedText = ""
    End Sub

    Private Sub CopyToolStripButton_Click(sender As Object, e As EventArgs) Handles CopyToolStripButton.Click
        My.Computer.Clipboard.Clear()
        If textbox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(textbox1.SelectedText)

        Else


        End If
    End Sub

    Private Sub PasteToolStripButton_Click(sender As Object, e As EventArgs) Handles PasteToolStripButton.Click
        If My.Computer.Clipboard.ContainsText Then
            textbox1.Paste()
        End If
    End Sub

    Private Sub FontColorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontColorsToolStripMenuItem.Click
        ColorDialog1.ShowDialog()
        textbox1.ForeColor = ColorDialog1.Color
    End Sub

    Private Sub FontsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FontsToolStripMenuItem1.Click
        FontDialog1.ShowDialog()
        textbox1.Font = FontDialog1.Font
    End Sub

    Private Sub BoldToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BoldToolStripMenuItem.Click
        If textbox1.SelectionFont.Bold = True Then
            If textbox1.SelectionFont.Italic = True Then
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Regular + FontStyle.Italic)
            Else
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Regular)
            End If

        ElseIf textbox1.SelectionFont.Bold = False Then
            If textbox1.SelectionFont.Italic = True Then
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Bold + FontStyle.Italic)
            Else
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Bold)
            End If
        End If
        textbox1.Focus()
    End Sub

    Private Sub ItalicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItalicsToolStripMenuItem.Click
        If textbox1.SelectionFont.Italic = True Then
            If textbox1.SelectionFont.Bold = True Then
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Regular + FontStyle.Bold)
            Else
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Regular)
            End If

        ElseIf textbox1.SelectionFont.Italic = False Then
            If textbox1.SelectionFont.Bold = True Then
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Italic + FontStyle.Bold)
            Else
                textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Italic)
            End If
        End If
        textbox1.Focus()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveFileDialog1.Title = "BeffsEasyNotes - Save File"
        SaveFileDialog1.DefaultExt = "rtf"
        SaveFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName = "" Then Exit Sub

        Dim strExt As String
        strExt = System.IO.Path.GetExtension(SaveFileDialog1.FileName)
        strExt = strExt.ToUpper()

        Select Case strExt
            Case ".RTF"
                textbox1.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText)
            Case Else
                Dim txtWriter As System.IO.StreamWriter
                txtWriter = New System.IO.StreamWriter(SaveFileDialog1.FileName)
                txtWriter.Write(textbox1.Text)
                txtWriter.Close()
                txtWriter = Nothing
                textbox1.SelectionStart = 0
                textbox1.SelectionLength = 0
        End Select

        currentFile = SaveFileDialog1.FileName
        textbox1.Modified = False
        Me.Text = "Editor: " & currentFile.ToString()
    End Sub

 

    Private Sub BeffsEasyNotes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'display message on form closing
        Dim Result As DialogResult
        Result = MessageBox.Show("Are you sure you wanna exit Any unsaved Progresss will be lost?", "Save your work before leaving!", MessageBoxButtons.YesNo)

        'if user clicked no, cancel form closing
        If Result = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        Dim dialog As New PrintDialog
        dialog.ShowDialog()
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()

    End Sub


    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        PrintDialog1.Document = PrintDocument1

        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        textbox1.SelectionIndent = 20
        textbox1.BulletIndent = 10
        textbox1.SelectionBullet = True
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs)
        Dim strInput As String
        strInput = textbox1.Text
        Dim strSplit() As String
        strSplit = strInput.Split(CChar(" "))
        MsgBox("Number of words: " & strSplit.Length)

    End Sub
    Public Sub charater_count()

        ToolStripStatusLabel2.Text = "Total Characters" & " " & textbox1.Text.Length.ToString()


        If textbox1.Text = "" Then
            ToolStripStatusLabel2.Text = "Total Characters" & " " & "0"
        End If
    End Sub
    Public Sub wordcount()
        charater_count()

        Dim strInput As String
        strInput = textbox1.Text
        Dim strSplit() As String
        strSplit = strInput.Split(CChar(" "))
        ToolStripStatusLabel1.Text = "Total Words " & strSplit.Length
        If textbox1.Text = "" Then
            ToolStripStatusLabel1.Text = "Total Words" & " " & "0"
        Else
            ' do nothing
        End If
    End Sub

    Private Sub BeffsEasyListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BeffsEasyListToolStripMenuItem.Click
        Dim window As New BeffsEasyLists
        window.Show()
    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        textbox1.BulletIndent = 10
        textbox1.SelectionBullet = True
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        textbox1.SelectionBullet = False
    End Sub

    Private Sub UnderLineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnderLineToolStripMenuItem.Click

        If Not textbox1.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = textbox1.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle

            If textbox1.SelectionFont.Underline = True Then
                newFontStyle = FontStyle.Regular
            Else
                newFontStyle = FontStyle.Underline
            End If

            textbox1.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If
    End Sub

    Private Sub NoneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoneToolStripMenuItem.Click
        textbox1.SelectionIndent = 0
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        textbox1.SelectionIndent = 5
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        textbox1.SelectionIndent = 10
    End Sub

    Private Sub ToolStripButton4_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        If Not textbox1.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = textbox1.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle

            If textbox1.SelectionFont.Underline = True Then
                newFontStyle = FontStyle.Regular
            Else
                newFontStyle = FontStyle.Underline
            End If

            textbox1.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If
    End Sub
    Private Sub NormalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalToolStripMenuItem.Click
        If Not textbox1.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = textbox1.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle
            newFontStyle = FontStyle.Regular

            textbox1.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If
    End Sub

    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        checkPrint = 0
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        checkPrint = textbox1.Print(checkPrint, textbox1.TextLength, e)


        If checkPrint < textbox1.TextLength Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub
    Private Sub textbox1_TextChanged_1(sender As Object, e As EventArgs) Handles textbox1.TextChanged
        wordcount()
    End Sub
    Private Sub PictureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PictureToolStripMenuItem.Click
        InsertPicture()
    End Sub
    Public Sub InsertPicture()
        Try
            Dim GetPicture As New OpenFileDialog
            GetPicture.Filter = "PNGs (*.png), Bitmaps (*.bmp), GIFs (*.gif), JPEGs (*.jpg)|*.bmp;*.gif;*.jpg;*.png|PNGs (*.png)|*.png|Bitmaps (*.bmp)|*.bmp|GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg"
            GetPicture.FilterIndex = 1
            GetPicture.InitialDirectory = "C:\"
            If GetPicture.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim SelectedPicture As String = GetPicture.FileName
                Dim Picture As Bitmap = New Bitmap(SelectedPicture)
                Dim cboard As Object = Clipboard.GetData(System.Windows.Forms.DataFormats.Text)
                Clipboard.SetImage(Picture)
                Dim PictureFormat As DataFormats.Format = DataFormats.GetFormat(DataFormats.Bitmap)
                If textbox1.CanPaste(PictureFormat) Then
                    textbox1.Paste(PictureFormat)
                End If
                Clipboard.Clear()
                Clipboard.SetText(cboard)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Property DetectUrls As Boolean
    Private Sub textbox1_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles textbox1.LinkClicked
        BBMain.CheckBox2.Checked = True
        BBMain.ToolStripTextBox1.Text = e.LinkText
    End Sub
    Private Sub Link_Clicked(sender As Object, e As System.Windows.Forms.LinkClickedEventArgs)
        System.Diagnostics.Process.Start(e.LinkText)
        BBMain.CheckBox2.Checked = True
        BBMain.ToolStripTextBox1.Text = e.LinkText
    End Sub
    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        textbox1.SelectionFont = New Font(Me.textbox1.SelectionFont, FontStyle.Strikeout)
        textbox1.Focus()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        EasyNoteSettings.Show()

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub SaveFile(ByVal strFileName As String)

        If strFileName = String.Empty Then
            'strFileName = "C:\Documents\" & Date.Now.ToString("MM-dd-yyyy HH\hmm\minss\s") & ".rtf"
            strFileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & Date.Now.ToString("MM-dd-yyyy HH\hmm\minss\s") & My.Settings.EasyNoteExt

        End If

        Dim strExt As String = System.IO.Path.GetExtension(strFileName).ToUpper()

        Select Case strExt
            Case ".RTF"
                textbox1.SaveFile(strFileName)
            Case Else
                ' to save as plain text
                Dim txtWriter As System.IO.StreamWriter
                txtWriter = New System.IO.StreamWriter(strFileName)
                txtWriter.Write(textbox1.Text)
                txtWriter.Close()
                txtWriter = Nothing
                textbox1.SelectionStart = 0
                textbox1.SelectionLength = 0
                textbox1.Modified = False
        End Select

        Me.Text = "Editor: " & strFileName

    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        SaveFile(currentFile)
        Timer1.Stop()
        Timer2.Start()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        SaveFile(currentFile)
    End Sub
End Class

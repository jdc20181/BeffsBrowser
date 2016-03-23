'Some changes updated from the new upcoming release All code is shown, this hasn't been organized so its a pretty messy. 
'A Custom Rich Text Box control enables you to be able to print from the richtextbox, I named my RTB Textbox1. 
'IMPORTANT YOU MUST HAVE THE CUSTOMIZED RICH TEXT CONTROL FOR SOME OF THE FOLLOWING TO WORK PLEASE SEE Richtextboxprintctrl.vb for more info


Imports System.IO

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
            'If textbox's text is still the same, notepad will show the OpenFileDialog
            OpenFileDialog1.ShowDialog()
            Try
                TextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        ' Create a SaveFileDialog to request a path and file name to save to.
        Dim saveFile1 As New SaveFileDialog()

        ' Initialize the SaveFileDialog to specify the RTF extension for the file.
        saveFile1.DefaultExt = "*.rtf"
        saveFile1.Filter = "RTF Files|*.rtf"

        ' Determine if the user selected a file name from the saveFileDialog.
        If (saveFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) _
            And (saveFile1.FileName.Length) > 0 Then

            ' Save the contents of the RichTextBox into the file.
            TextBox1.SaveFile(saveFile1.FileName, _
                RichTextBoxStreamType.PlainText)
        End If
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

        ToolStripLabel1.Enabled = False
        'calling the below function to start the context ment (when the user right click the richtextbox
        InitializeMyContextMenu()
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
        TextBox1.ContextMenu = contextMenu1
    End Sub
   







    Private Sub IncreaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles IncreaseToolStripMenuItem1.Click
        Try
            TextBox1.SelectionFont = New Font(TextBox1.SelectionFont.FontFamily, Int(TextBox1.SelectionFont.SizeInPoints + 5))
        Catch ex As Exception
        End Try
        TextBox1.Focus()
    End Sub

    Private Sub DecreaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DecreaseToolStripMenuItem1.Click
        Try
            TextBox1.SelectionFont = New Font(TextBox1.SelectionFont.FontFamily, Int(TextBox1.SelectionFont.SizeInPoints - 5))
        Catch ex As Exception
        End Try
        TextBox1.Focus()

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If TextBox1.SelectionFont.Bold = True Then
            If TextBox1.SelectionFont.Italic = True Then
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Regular + FontStyle.Italic)
            Else
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Regular)
            End If

        ElseIf TextBox1.SelectionFont.Bold = False Then
            If TextBox1.SelectionFont.Italic = True Then
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Bold + FontStyle.Italic)
            Else
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Bold)
            End If
        End If
        TextBox1.Focus()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If TextBox1.SelectionFont.Italic = True Then
            If TextBox1.SelectionFont.Bold = True Then
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Regular + FontStyle.Bold)
            Else
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Regular)
            End If

        ElseIf TextBox1.SelectionFont.Italic = False Then
            If TextBox1.SelectionFont.Bold = True Then
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Italic + FontStyle.Bold)
            Else
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Italic)
            End If
        End If
        TextBox1.Focus()
    End Sub

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles NewToolStripButton.Click
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
                TextBox1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.RichText)
            Case Else
                Dim txtReader As System.IO.StreamReader
                txtReader = New System.IO.StreamReader(OpenFileDialog1.FileName)
                TextBox1.Text = txtReader.ReadToEnd
                txtReader.Close()
                txtReader = Nothing
                TextBox1.SelectionStart = 0
                TextBox1.SelectionLength = 0
        End Select

        currentFile = OpenFileDialog1.FileName
        TextBox1.Modified = False
        Me.Text = "Editor: " & currentFile.ToString()

    End Sub
    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        If TextBox1.Modified Then

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
    Dim saveFile As New SaveFileDialog()
    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        If currentFile = "" Then
            SaveAsToolStripMenuItem_Click(Me, e)
            Exit Sub
        End If

        Dim strExt As String
        strExt = System.IO.Path.GetExtension(currentFile)
        strExt = strExt.ToUpper()

        Select Case strExt
            Case ".RTF"
                TextBox1.SaveFile(currentFile)
            Case Else
                ' to save as plain text
                Dim txtWriter As System.IO.StreamWriter
                txtWriter = New System.IO.StreamWriter(currentFile)
                txtWriter.Write(TextBox1.Text)
                txtWriter.Close()
                txtWriter = Nothing
                TextBox1.SelectionStart = 0
                TextBox1.SelectionLength = 0
                TextBox1.Modified = False
        End Select

        Me.Text = "Editor: " & currentFile.ToString()

    End Sub
    Private Sub CutToolStripButton_Click(sender As Object, e As EventArgs) Handles CutToolStripButton.Click
        My.Computer.Clipboard.Clear()
        If TextBox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(TextBox1.SelectedText)

        End If
        TextBox1.SelectedText = ""
    End Sub

    Private Sub CopyToolStripButton_Click(sender As Object, e As EventArgs) Handles CopyToolStripButton.Click
        My.Computer.Clipboard.Clear()
        If TextBox1.SelectionLength > 0 Then
        Else
            My.Computer.Clipboard.SetText(TextBox1.SelectedText)

        End If
    End Sub

    Private Sub PasteToolStripButton_Click(sender As Object, e As EventArgs) Handles PasteToolStripButton.Click
        If My.Computer.Clipboard.ContainsText Then
            TextBox1.Paste()
        End If
    End Sub

    Private Sub FontColorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontColorsToolStripMenuItem.Click
        ColorDialog1.ShowDialog()
        TextBox1.ForeColor = ColorDialog1.Color
    End Sub

    Private Sub FontsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FontsToolStripMenuItem1.Click
        FontDialog1.ShowDialog()
        TextBox1.Font = FontDialog1.Font
    End Sub

    Private Sub BoldToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BoldToolStripMenuItem.Click
        If TextBox1.SelectionFont.Bold = True Then
            If TextBox1.SelectionFont.Italic = True Then
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Regular + FontStyle.Italic)
            Else
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Regular)
            End If

        ElseIf TextBox1.SelectionFont.Bold = False Then
            If TextBox1.SelectionFont.Italic = True Then
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Bold + FontStyle.Italic)
            Else
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Bold)
            End If
        End If
        TextBox1.Focus()
    End Sub

    Private Sub ItalicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItalicsToolStripMenuItem.Click
        If TextBox1.SelectionFont.Italic = True Then
            If TextBox1.SelectionFont.Bold = True Then
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Regular + FontStyle.Bold)
            Else
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Regular)
            End If

        ElseIf TextBox1.SelectionFont.Italic = False Then
            If TextBox1.SelectionFont.Bold = True Then
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Italic + FontStyle.Bold)
            Else
                TextBox1.SelectionFont = New Font(Me.TextBox1.SelectionFont, FontStyle.Italic)
            End If
        End If
        TextBox1.Focus()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveFileDialog1.Title = "RTE - Save File"
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
                TextBox1.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText)
            Case Else
                Dim txtWriter As System.IO.StreamWriter
                txtWriter = New System.IO.StreamWriter(SaveFileDialog1.FileName)
                txtWriter.Write(TextBox1.Text)
                txtWriter.Close()
                txtWriter = Nothing
                TextBox1.SelectionStart = 0
                TextBox1.SelectionLength = 0
        End Select

        currentFile = SaveFileDialog1.FileName
        TextBox1.Modified = False
        Me.Text = "Editor: " & currentFile.ToString()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("This is built from a richtextbox for quality retro style documents that still are used today! This is a easy to use utility of BeffsBrowser! Version 0.1 Beta")
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
        TextBox1.SelectionIndent = 20
        TextBox1.BulletIndent = 10
        TextBox1.SelectionBullet = True
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs)
        Dim strInput As String
        strInput = TextBox1.Text
        Dim strSplit() As String
        strSplit = strInput.Split(CChar(" "))
        MsgBox("Number of words: " & strSplit.Length)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        Dim strInput As String
        strInput = textbox1.Text
        Dim strSplit() As String
        strSplit = strInput.Split(CChar(" "))
        ToolStripLabel1.Text = "Total Words " & strSplit.Length
    End Sub

    Private Sub BeffsEasyListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BeffsEasyListToolStripMenuItem.Click
        Dim window As New BeffsEasyLists
        window.Show()
    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        TextBox1.BulletIndent = 10
        TextBox1.SelectionBullet = True
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        TextBox1.SelectionBullet = False
    End Sub

    Private Sub UnderLineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnderLineToolStripMenuItem.Click

        If Not TextBox1.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = TextBox1.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle

            If TextBox1.SelectionFont.Underline = True Then
                newFontStyle = FontStyle.Regular
            Else
                newFontStyle = FontStyle.Underline
            End If

            TextBox1.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If
    End Sub

    Private Sub NoneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoneToolStripMenuItem.Click
        TextBox1.SelectionIndent = 0
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        TextBox1.SelectionIndent = 5
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        TextBox1.SelectionIndent = 10
    End Sub

    Private Sub ToolStripButton4_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        If Not TextBox1.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = TextBox1.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle

            If TextBox1.SelectionFont.Underline = True Then
                newFontStyle = FontStyle.Regular
            Else
                newFontStyle = FontStyle.Underline
            End If

            TextBox1.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If
    End Sub

    Private Sub NormalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalToolStripMenuItem.Click
        If Not TextBox1.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = TextBox1.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle
            newFontStyle = FontStyle.Regular

            TextBox1.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If
    End Sub

    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint

        ' Adapted from Microsoft's example for extended richtextbox control
        '
        checkPrint = 0

    End Sub


    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        ' Adapted from Microsoft's example for extended richtextbox control
        '
        ' Print the content of the RichTextBox. Store the last character printed.
        checkPrint = TextBox1.Print(checkPrint, TextBox1.TextLength, e)

        ' Look for more pages
        If checkPrint < TextBox1.TextLength Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    
End Class





  


Public Class Hangman
    Dim Word As String                      ' will hold the word to be guessed
    Dim WordLength As Integer               ' will hold the length of the word
    Dim Char1 As String                     ' will hold the first letter of the word
    Dim Char2 As String                     ' will hold the second letter of the word
    Dim Char3 As String                     ' will hold the third letter of the word
    Dim Char4 As String                     ' will hold the fourth letter of the word
    Dim Char5 As String                     ' will hold the fifth letter of the word
    Dim Char6 As String ' will hold the sixth letter of the word
    Dim char7 As String
    Dim char8 As String
    Dim char9 As String
    Dim char10 As String
    Dim char11 As String
    Dim char12 As String
    Dim GameStarted As Boolean = False      ' will switch to True when the user click on Play Button
    Dim RightTrys As Integer                ' will hold the number of the right guessed letters
    Dim WrongTrys As Integer                ' will hold the number of the wrong guessed letters
    Private Sub PlayButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayButton.Click
        Char1 = ""
        Char2 = ""
        Char3 = ""
        Char4 = ""
        Char5 = ""
        Char6 = ""
        char7 = ""
        char8 = ""
        char9 = ""
        char10 = ""
        char11 = ""
        char12 = ""
        DrawHanger()
        Dim number As Integer
        number = Int(Rnd() * ListBox1.Items.Count - 1) + 1
        Word = ListBox1.Items(number)
        WordLength = Word.Length
        ShowLength(Word.Length)
        clearLetters()
        AssignLetters()
        TextBox1.Focus()
        RightTrys = 0
        WrongTrys = 0
        WrongGuessesLabel.Text = "Wrong Guesses: "
        GameStarted = True
        PlayButton.Visible = False
    End Sub
    Public Sub ShowLength(ByVal VisibleLines As Integer)
        hideCharachterLength()
        If VisibleLines = 1 Then
            Line1.Visible = True
        ElseIf VisibleLines = 2 Then
            Line1.Visible = True
            Line2.Visible = True
        ElseIf VisibleLines = 3 Then
            Line1.Visible = True
            Line2.Visible = True
            Line3.Visible = True
        ElseIf VisibleLines = 4 Then
            Line1.Visible = True
            Line2.Visible = True
            Line3.Visible = True
            Line4.Visible = True
        ElseIf VisibleLines = 5 Then
            Line1.Visible = True
            Line2.Visible = True
            Line3.Visible = True
            Line4.Visible = True
            Line5.Visible = True
        ElseIf VisibleLines = 6 Then
            Line1.Visible = True
            Line2.Visible = True
            Line3.Visible = True
            Line4.Visible = True
            Line5.Visible = True
            Line6.Visible = True
        End If
    End Sub

    Public Sub AssignLetters()
        If WordLength = 2 Then
            Char1 = Word.Chars(0).ToString.ToUpper
            Char2 = Word.Chars(1).ToString.ToUpper
        ElseIf WordLength = 3 Then
            Char1 = Word.Chars(0).ToString.ToUpper
            Char2 = Word.Chars(1).ToString.ToUpper
            Char3 = Word.Chars(2).ToString.ToUpper
        ElseIf WordLength = 4 Then
            Char1 = Word.Chars(0).ToString.ToUpper
            Char2 = Word.Chars(1).ToString.ToUpper
            Char3 = Word.Chars(2).ToString.ToUpper
            Char4 = Word.Chars(3).ToString.ToUpper
        ElseIf WordLength = 5 Then
            Char1 = Word.Chars(0).ToString.ToUpper
            Char2 = Word.Chars(1).ToString.ToUpper
            Char3 = Word.Chars(2).ToString.ToUpper
            Char4 = Word.Chars(3).ToString.ToUpper
            Char5 = Word.Chars(4).ToString.ToUpper
        ElseIf WordLength = 6 Then
            Char1 = Word.Chars(0).ToString.ToUpper
            Char2 = Word.Chars(1).ToString.ToUpper
            Char3 = Word.Chars(2).ToString.ToUpper
            Char4 = Word.Chars(3).ToString.ToUpper
            Char5 = Word.Chars(4).ToString.ToUpper
            Char6 = Word.Chars(5).ToString.ToUpper
        End If
    End Sub
    Public Sub hideCharachterLength()
        Line1.Visible = False
        Line2.Visible = False
        Line3.Visible = False
        Line4.Visible = False
        Line5.Visible = False
        Line6.Visible = False
    End Sub
    Public Sub DrawHanger()
        Dim pnt As Graphics
        pnt = Me.CreateGraphics
        Dim pencil As New Pen(Color.Black, 2)
        pnt.DrawLine(pencil, 46, 434, 115, 394)
        pnt.DrawLine(pencil, 115, 394, 184, 434)
        pnt.DrawLine(pencil, 115, 394, 115, 220)
        pnt.DrawLine(pencil, 115, 220, 250, 220)
        pnt.DrawLine(pencil, 250, 220, 250, 250)
    End Sub
    Public Sub StartDrawingWhenLosing(ByVal trys As Integer)
        Dim pnt As Graphics
        pnt = Me.CreateGraphics
        Dim pencil As New Pen(Color.Black, 2)
        If trys = 1 Then
            pnt.DrawEllipse(pencil, 228, 250, 40, 40)
        ElseIf trys = 2 Then
            pnt.DrawLine(pencil, 248, 290, 248, 370)
        ElseIf trys = 3 Then
            pnt.DrawLine(pencil, 248, 315, 213, 293)
        ElseIf trys = 4 Then
            pnt.DrawLine(pencil, 248, 315, 283, 293)
        ElseIf trys = 5 Then
            pnt.DrawLine(pencil, 248, 369, 213, 391)
        ElseIf trys = 6 Then
            pnt.DrawLine(pencil, 248, 369, 283, 391)
            TextBox1.Clear()
            Try
                Letter1.Text = Char1
                Letter2.Text = Char2
                Letter3.Text = Char3
                Letter4.Text = Char4
                Letter5.Text = Char5
                Letter6.Text = Char6
            Catch ex As Exception
            End Try
            MsgBox("You Lose")
            resetGame()
        End If
    End Sub
    Public Sub resetGame()
        WrongGuessesLabel.Text = ""
        Dim pnt As Graphics
        pnt = Me.CreateGraphics
        pnt.Clear(Color.FromArgb(0, 192, 0))
        PlayButton.Visible = True
    End Sub
    Public Sub clearLetters()
        Letter1.Text = ""
        Letter2.Text = ""
        Letter3.Text = ""
        Letter4.Text = ""
        Letter5.Text = ""
        Letter6.Text = ""
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MsgBox("Retired! Hangman is being discontinued. Sorry.")
        Me.Close()
        clearLetters()
        hideCharachterLength()
    End Sub
    Public Sub CheckForWinning()
        If RightTrys = WordLength Then
            MsgBox("You Won")
            resetGame()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If GameStarted = True Then
            If TextBox1.Text = "" Then
            Else
                If TextBox1.Text.ToUpper.Contains(Char1) And Letter1.Text = "" And Char1 <> "" Then
                    Letter1.Text = Char1
                    RightTrys += 1
                    CheckForWinning()
                ElseIf TextBox1.Text.ToUpper.Contains(Char2) And Letter2.Text = "" And Char2 <> "" Then
                    Letter2.Text = Char2
                    RightTrys += 1
                    CheckForWinning()
                ElseIf TextBox1.Text.ToUpper.Contains(Char3) And Letter3.Text = "" And Char3 <> "" Then
                    Letter3.Text = Char3
                    RightTrys += 1
                    CheckForWinning()
                ElseIf TextBox1.Text.ToUpper.Contains(Char4) And Letter4.Text = "" And Char4 <> "" Then
                    Letter4.Text = Char4
                    RightTrys += 1
                    CheckForWinning()
                ElseIf TextBox1.Text.ToUpper.Contains(Char5) And Letter5.Text = "" And Char5 <> "" Then
                    Letter5.Text = Char5
                    RightTrys += 1
                    CheckForWinning()
                ElseIf TextBox1.Text.ToUpper.Contains(Char6) And Letter6.Text = "" And Char6 <> "" Then
                    Letter6.Text = Char6
                    RightTrys += 1
                    CheckForWinning()
                Else
                    WrongTrys += 1
                    StartDrawingWhenLosing(WrongTrys)
                    WrongGuessesLabel.Text = WrongGuessesLabel.Text & " " & TextBox1.Text
                End If
            End If
        Else
        End If
        TextBox1.Text = ""
    End Sub
End Class

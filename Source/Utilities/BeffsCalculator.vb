Public Class BeffsCalculator
    Dim value1 As String = ""
    Dim value2 As String = ""
    Dim value3 As Double = 0.0

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        value1 = ""
        value2 = ""
        value3 = 0.0
        Button1.Text = 0.0
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Me.Close()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If value1 > "" And value2 = "+" Then
            Button1.Text = Val(value1) + Val(Button1.Text)
            value3 = Button1.Text
        ElseIf value2 > "" And value2 = "-" Then
            Button1.Text = Val(value1) - Val(Button1.Text)
            value3 = Button1.Text
        ElseIf value2 > "" And value2 = "*" Then
            Button1.Text = Val(value1) * Val(Button1.Text)
            value3 = Button1.Text
        ElseIf value2 > "" And value2 = "/" Then
            Button1.Text = Val(value1) / Val(Button1.Text)
            value3 = Button1.Text
        ElseIf value2 > "" And value2 = "^" Then
            Button1.Text = Val(value1) ^ Val(Button1.Text)
            value3 = Button1.Text

        End If

    End Sub
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click, Button13.Click, Button9.Click, Button5.Click, Button20.Click
        value2 = sender.text
        value1 = Button1.Text
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If Button1.Text = value1 Then
            Button1.Text = "0."
        ElseIf Button1.Text = "0." Then
            Button1.Text = "0."
        ElseIf Button1.Text = "0" Then
            Button1.Text = "0."
        ElseIf Button1.Text = value1 Then
            Button1.Text = "0."
        Else : Button1.Text = Button1.Text & "0"
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If Button1.Text = "0." Then
            Button1.Text = "."
        ElseIf Button1.Text = value3 Then
            Button1.Text = "."
        ElseIf Button1.Text = value1 Then
            Button1.Text = "."
        Else
            If Button1.Text.Contains(".") Then
            Else
                Button1.Text = Button1.Text & "."
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click, Button7.Click, Button6.Click, Button4.Click, Button3.Click, Button2.Click, Button12.Click, Button11.Click, Button10.Click
        If Button1.Text = value1 Then
            Button1.Text = sender.text
        ElseIf Button1.Text = "0." Then
            Button1.Text = sender.text
        ElseIf Button1.Text = value3 Then
            Button1.Text = sender.text
        Else
            Button1.Text = Button1.Text & sender.text
        End If
    End Sub

    Private Sub BeffsCalculator_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.calculations = Button1.Text
    End Sub

    Private Sub BeffsCalculator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Text = My.Settings.Calculations
    End Sub

    Private Sub Button1_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyCode = Keys.C) Then
            My.Computer.Clipboard.Clear()
            My.Computer.Clipboard.SetText(Button1.Text)
        End If

    End Sub




    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Button1.Focus()
        SendKeys.Send("{BACKSPACE}")
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Me.Close()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        SquareRootCalculator.Show()
    End Sub
    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown

        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp

        If e.Button = MouseButtons.Left Then
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

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        RoundNumbers.Show()

    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        My.Computer.Clipboard.Clear()

        My.Computer.Clipboard.SetText(Button1.Text)


    End Sub
End Class

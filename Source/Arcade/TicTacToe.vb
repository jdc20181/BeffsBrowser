Public Class TicTacToe
    Dim random As String = 1

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        random += 1
        If random = 10 Then
            random = 1
        End If

    End Sub
    Public Sub PC()

        If random = 1 And Button1.Enabled = True Then
            Button1.Text = "O"
            Button1.Enabled = False
        End If
        If random = 2 And Button2.Enabled = True Then
            Button2.Text = "O"
            Button2.Enabled = False
        End If
        If random = 3 And Button3.Enabled = True Then
            Button3.Text = "O"
            Button3.Enabled = False
        End If
        If random = 4 And Button4.Enabled = True Then
            Button4.Text = "O"
            Button4.Enabled = False
        End If
        If random = 5 And Button5.Enabled = True Then
            Button5.Text = "O"
            Button5.Enabled = False
        End If
        If random = 6 And Button6.Enabled = True Then
            Button6.Text = "O"
            Button6.Enabled = False
        End If
        If random = 7 And Button7.Enabled = True Then
            Button7.Text = "O"
            Button7.Enabled = False
        End If
        If random = 8 And Button8.Enabled = True Then
            Button8.Text = "O"
            Button8.Enabled = False
        End If
        If random = 9 And Button9.Enabled = True Then
            Button9.Text = "O"
            Button9.Enabled = False
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Timer1.Stop()
        Button1.Text = "O"
        Label1.Text = 1

        Button1.Enabled = False
        Button10.Enabled = False

    End Sub

    Public Sub Options()

        'If user hits 1
        If Label1.Text = 1 Then
            If Button2.Text = "X" And Button3.Enabled = True Then
                With Button3
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button3.Text = "X" And Button2.Enabled = True Then
                With Button2
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button5.Text = "X" And Button9.Enabled = True Then
                With Button9
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button9.Text = "X" And Button5.Enabled = True Then
                With Button5
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button4.Text = "X" And Button7.Enabled = True Then
                With Button7
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button7.Text = "X" And Button4.Enabled = True Then
                With Button4
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button8.Text = "X" Or Button6.Text = "X" Then
                hit()
            End If


        End If

        'If user hits 2
        If Label1.Text = 2 Then
            If Button1.Text = "X" And Button3.Enabled = True Then
                With Button3
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button1.Text = "X" And Button1.Enabled = True Then
                With Button1
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button5.Text = "X" And Button8.Enabled = True Then
                With Button8
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button8.Text = "X" And Button5.Enabled = True Then
                With Button5
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button6.Text = "X" Or Button4.Text = "X" Or Button7.Text = "X" Or Button9.Text = "X" Then
                hit()
            End If

        End If

        'If user hits 3
        If Label1.Text = 3 Then
            If Button1.Text = "X" And Button2.Enabled = True Then
                With Button2
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button2.Text = "X" And Button1.Enabled = True Then
                With Button1
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button6.Text = "X" And Button9.Enabled = True Then
                With Button9
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button9.Text = "X" And Button6.Enabled = True Then
                With Button6
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button5.Text = "X" And Button7.Enabled = True Then
                With Button7
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button7.Text = "X" And Button5.Enabled = True Then
                With Button5
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button8.Text = "X" Or Button4.Text = "X" Then
                hit()
            End If

        End If

        'If user hits 4
        If Label1.Text = 4 Then
            If Button1.Text = "X" And Button7.Enabled = True Then
                With Button7
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button7.Text = "X" And Button1.Enabled = True Then
                With Button1
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button5.Text = "X" And Button6.Enabled = True Then
                With Button6
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button6.Text = "X" And Button5.Enabled = True Then
                With Button5
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button2.Text = "X" Or Button3.Text = "X" Or Button8.Text = "X" Or Button9.Text = "X" Then
                hit()
            End If

        End If

        'If user hits 5
        If Label1.Text = 5 Then
            If Button1.Text = "X" And Button9.Enabled = True Then
                With Button9
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button9.Text = "X" And Button1.Enabled = True Then
                With Button1
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button2.Text = "X" And Button8.Enabled = True Then
                With Button8
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button8.Text = "X" And Button2.Enabled = True Then
                With Button2
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button3.Text = "X" And Button7.Enabled = True Then
                With Button7
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button7.Text = "X" And Button3.Enabled = True Then
                With Button3
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button6.Text = "X" And Button4.Enabled = True Then
                With Button4
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button4.Text = "X" And Button6.Enabled = True Then
                With Button6
                    .Text = "O"
                    .Enabled = False
                End With
            End If

        End If


        'If user hits 6
        If Label1.Text = 6 Then
            If Button3.Text = "X" And Button9.Enabled = True Then
                With Button9
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button9.Text = "X" And Button3.Enabled = True Then
                With Button3
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button5.Text = "X" And Button4.Enabled = True Then
                With Button4
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button4.Text = "X" And Button5.Enabled = True Then
                With Button5
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button1.Text = "X" Or Button2.Text = "X" Or Button7.Text = "X" Or Button8.Text = "X" Then
                hit()
            End If

        End If


        'If user hits 7
        If Label1.Text = 7 Then
            If Button1.Text = "X" And Button4.Enabled = True Then
                With Button4
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button4.Text = "X" And Button1.Enabled = True Then
                With Button1
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button5.Text = "X" And Button3.Enabled = True Then
                With Button3
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button3.Text = "X" And Button5.Enabled = True Then
                With Button5
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button8.Text = "X" And Button9.Enabled = True Then
                With Button9
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button9.Text = "X" And Button8.Enabled = True Then
                With Button8
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button6.Text = "X" Or Button2.Text = "X" Then
                hit()
            End If

        End If


        'If user hits 8
        If Label1.Text = 8 Then
            If Button2.Text = "X" And Button5.Enabled = True Then
                With Button5
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button5.Text = "X" And Button2.Enabled = True Then
                With Button2
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button9.Text = "X" And Button7.Enabled = True Then
                With Button7
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button7.Text = "X" And Button9.Enabled = True Then
                With Button9
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button6.Text = "X" Or Button3.Text = "X" Or Button1.Text = "X" Or Button4.Text = "X" Then
                hit()
            End If

        End If

        'If user hits 9
        If Label1.Text = 9 Then
            If Button6.Text = "X" And Button3.Enabled = True Then
                With Button3
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button3.Text = "X" And Button6.Enabled = True Then
                With Button6
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button5.Text = "X" And Button1.Enabled = True Then
                With Button1
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button1.Text = "X" And Button5.Enabled = True Then
                With Button5
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button8.Text = "X" And Button7.Enabled = True Then
                With Button7
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button7.Text = "X" And Button8.Enabled = True Then
                With Button8
                    .Text = "O"
                    .Enabled = False
                End With
            ElseIf Button2.Text = "X" Or Button4.Text = "X" Then
                hit()
            End If

        End If



    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Button9.Click, Button8.Click, Button7.Click, Button6.Click, _
    Button5.Click, Button4.Click, Button3.Click, Button2.Click, Button1.Click

        sender.Text = "X"
        sender.Enabled = False
        Timer1.Stop()
        Label1.Text = sender.name
        Label1.Text = Label1.Text.Replace("Button", "")
        If random = Label1.Text Then
            random = random + 1
        End If
        If random > 0 Then
            PC()
            random = 0
        ElseIf random = 0 Then
            Options()
        End If
        Label2.Text = random
        CheckIfComputerWins()
    End Sub

    Public Sub hit()
        For Each ctl As Control In Me.Controls
            If Label1.Text < 9 Then
                If (ctl.Name.StartsWith("Button" & Label1.Text + 1)) Then
                    Dim btn As Button = DirectCast(ctl, Button)
                    If btn.Enabled = True Then
                        btn.Text = "O"
                        btn.Enabled = False
                    Else

                        Label1.Text = Label1.Text + 1
                    End If

                End If
            Else
                If (ctl.Name.StartsWith("Button1")) Then
                    Dim btn As Button = DirectCast(ctl, Button)
                    If btn.Enabled = True Then
                        btn.Text = "O"
                        btn.Enabled = False
                    Else

                        Label1.Text = Label1.Text + 1
                    End If

                End If
            End If

        Next

    End Sub

    Public Sub CheckIfComputerWins()
        If Button1.Text = "O" And Button2.Text = "O" And Button3.Text = "O" _
         Or Button4.Text = "O" And Button5.Text = "O" And Button6.Text = "O" _
        Or Button7.Text = "O" And Button8.Text = "O" And Button9.Text = "O" _
        Or Button1.Text = "O" And Button4.Text = "O" And Button7.Text = "O" _
        Or Button2.Text = "O" And Button5.Text = "O" And Button8.Text = "O" _
        Or Button3.Text = "O" And Button6.Text = "O" And Button9.Text = "O" _
        Or Button1.Text = "O" And Button5.Text = "O" And Button9.Text = "O" _
        Or Button7.Text = "O" And Button5.Text = "O" And Button3.Text = "O" Then
            MsgBox("Computer Wins")
            Button1.Text = ""
            Button1.Enabled = True
            Button2.Text = ""
            Button2.Enabled = True
            Button3.Text = ""
            Button3.Enabled = True
            Button4.Text = ""
            Button4.Enabled = True
            Button5.Text = ""
            Button5.Enabled = True
            Button6.Text = ""
            Button6.Enabled = True
            Button7.Text = ""
            Button7.Enabled = True
            Button8.Text = ""
            Button8.Enabled = True
            Button9.Text = ""
            Button9.Enabled = True

        ElseIf Button1.Text = "X" And Button2.Text = "X" And Button3.Text = "X" _
     Or Button4.Text = "X" And Button5.Text = "X" And Button6.Text = "X" _
    Or Button7.Text = "X" And Button8.Text = "X" And Button9.Text = "X" _
    Or Button1.Text = "X" And Button4.Text = "X" And Button7.Text = "X" _
    Or Button2.Text = "X" And Button5.Text = "X" And Button8.Text = "X" _
    Or Button3.Text = "X" And Button6.Text = "X" And Button9.Text = "X" _
    Or Button1.Text = "X" And Button5.Text = "X" And Button9.Text = "X" _
    Or Button7.Text = "X" And Button5.Text = "X" And Button3.Text = "X" Then
            MsgBox("Congratulations, You Win")
            Button1.Text = ""
            Button1.Enabled = True
            Button2.Text = ""
            Button2.Enabled = True
            Button3.Text = ""
            Button3.Enabled = True
            Button4.Text = ""
            Button4.Enabled = True
            Button5.Text = ""
            Button5.Enabled = True
            Button6.Text = ""
            Button6.Enabled = True
            Button7.Text = ""
            Button7.Enabled = True
            Button8.Text = ""
            Button8.Enabled = True
            Button9.Text = ""
            Button9.Enabled = True
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Button1.Text = ""
        Button1.Enabled = True
        Button2.Text = ""
        Button2.Enabled = True
        Button3.Text = ""
        Button3.Enabled = True
        Button4.Text = ""
        Button4.Enabled = True
        Button5.Text = ""
        Button5.Enabled = True
        Button6.Text = ""
        Button6.Enabled = True
        Button7.Text = ""
        Button7.Enabled = True
        Button8.Text = ""
        Button8.Enabled = True
        Button9.Text = ""
        Button9.Enabled = True
        Button10.Enabled = True
        Timer1.Start()

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim d As New TicTac
        d.Show()
        Me.Dispose()

    End Sub
End Class

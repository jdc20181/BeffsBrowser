Public Class Slots


    Dim myMoney As Long = 1000000, myBid As Long = 5000, mybank As Long = 0
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox5.Text = myMoney
        TextBox4.Text = myBid
        TextBox1.Text = My.Settings.jpbank
        validateshorts()

        Status.Text = "Please Make Your Bid and press the Spin button to begin. "
        OptionsToolStripMenuItem.Alignment = _
           System.Windows.Forms.ToolStripItemAlignment.Right
    End Sub

    Private Sub Spin_Click(sender As Object, e As EventArgs) Handles Spin.Click


        If (Long.Parse(TextBox4.Text) >= Long.Parse(TextBox5.Text)) Then
            MsgBox("You can not bid more money than you have!")
        ElseIf (Long.Parse(TextBox5.Text) <= 0) Then
            NewGame()

        Else

            myBid = Long.Parse(TextBox4.Text)
            updateSlots()
            checkSlots()
            TextBox5.Text = myMoney
            TextBox1.Text = mybank
            validateshorts()
        End If
    End Sub
    Private Sub updateSlots()
        'Dim rand As Random = New Random()
        Dim rand As Random = New Random(DateTime.Now.Millisecond * DateTime.Now.Second * DateTime.Now.Minute * DateTime.Now.Hour)

        Dim slots As New List(Of TextBox)
        slots.Add(Slot1)
        slots.Add(Slot2)
        slots.Add(Slot3)
        For i As Long = 0 To 2
            Dim r As Long = rand.Next(9)
            slots(i).Text = r
            r = Nothing
        Next
    End Sub
    Private Sub checkSlots()
        Dim t1 As Long = Slot1.Text, t2 As Long = Slot2.Text, t3 As Long = Slot3.Text
        If ((t1 = t2) OrElse (t1 = t3) OrElse (t2 = t1) OrElse (t2 = t3) OrElse (t3 = t1) OrElse (t3 = t2)) Then
            Status.Text = "7X Money Big Win!!!"
            myMoney += (myBid * 7)
            myMoney += (myBid * 7)
        ElseIf ((t1 = t2) AndAlso (t1 = t3) AndAlso (t2 = t3)) Then
            Status.Text = " 70X Money! EPIC WIN!"

            myMoney += (myBid * 70)
            mybank += (myBid * 14)
        ElseIf ((t1 = 7) AndAlso (t1 = 7) AndAlso (t2 = 7)) Then
            Status.Text = "700X Money JackPot Winner! "
            myMoney += CInt(myBid * 700) + CInt(mybank * 700)
            mybank = "0"
        Else
            Status.Text = "Bid Lost."
            myMoney -= myBid
            mybank += (myBid * 2)

        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Slots_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MsgBox("Final score" + vbNewLine + "$" + TextBox5.Text)
        My.Settings.jpbank = mybank
        My.Settings.Save()
    End Sub

    Private Sub NewGameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewGameToolStripMenuItem.Click
        NewGame()
    End Sub
    Private Sub NewGame()
        Me.Close()
        Dim d As New Slots
        d.Show()

    End Sub
  
    
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("Casino Slots 'Win big or go home with nothing'")
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        TextBox4.Clear()
        TextBox4.Text = 100
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        TextBox4.Clear()
        TextBox4.Text = 500

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        TextBox4.Clear()
        TextBox4.Text = 1000
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        TextBox4.Clear()
        TextBox4.Text = 2500

    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        TextBox4.Clear()
        TextBox4.Text = 5000
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        TextBox4.Clear()
        TextBox4.Text = 10000
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        TextBox4.Clear()
        TextBox4.Text = 25000
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        TextBox4.Clear()
        TextBox4.Text = 250000
    End Sub
    Public Sub centertext()
  
    End Sub

  
    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        'ValidateShorts()

    End Sub
    Public Sub validateshorts()
        If TextBox5.Text <= 100 Then
            ToolStripButton1.Enabled = False
            ToolStripButton2.Enabled = False
            ToolStripButton3.Enabled = False
            ToolStripButton4.Enabled = False
            ToolStripButton5.Enabled = False
            ToolStripButton6.Enabled = False
            ToolStripButton7.Enabled = False
            ToolStripButton8.Enabled = False
        ElseIf TextBox5.Text <= 500 Then
            ToolStripButton1.Enabled = True

            ToolStripButton2.Enabled = False
            ToolStripButton3.Enabled = False
            ToolStripButton4.Enabled = False
            ToolStripButton5.Enabled = False
            ToolStripButton6.Enabled = False
            ToolStripButton7.Enabled = False
            ToolStripButton8.Enabled = False
        ElseIf TextBox5.Text <= 1000 Then
            ToolStripButton1.Enabled = True
            ToolStripButton2.Enabled = True

            ToolStripButton3.Enabled = False
            ToolStripButton4.Enabled = False
            ToolStripButton5.Enabled = False
            ToolStripButton6.Enabled = False
            ToolStripButton7.Enabled = False
            ToolStripButton8.Enabled = False
        ElseIf TextBox5.Text <= 2500 Then
            ToolStripButton1.Enabled = True
            ToolStripButton2.Enabled = True
            ToolStripButton3.Enabled = True
            ToolStripButton4.Enabled = False
            ToolStripButton5.Enabled = False
            ToolStripButton6.Enabled = False
            ToolStripButton7.Enabled = False
            ToolStripButton8.Enabled = False
        ElseIf TextBox5.Text <= 5000 Then
            ToolStripButton1.Enabled = True
            ToolStripButton2.Enabled = True
            ToolStripButton3.Enabled = True
            ToolStripButton4.Enabled = True
            ToolStripButton5.Enabled = False
            ToolStripButton6.Enabled = False
            ToolStripButton7.Enabled = False
            ToolStripButton8.Enabled = False
        ElseIf TextBox5.Text <= 10000 Then
            ToolStripButton1.Enabled = True
            ToolStripButton2.Enabled = True
            ToolStripButton3.Enabled = True
            ToolStripButton4.Enabled = True
            ToolStripButton5.Enabled = True
            ToolStripButton6.Enabled = False
            ToolStripButton7.Enabled = False
            ToolStripButton8.Enabled = False
        ElseIf TextBox5.Text <= 25000 Then
            ToolStripButton1.Enabled = True
            ToolStripButton2.Enabled = True
            ToolStripButton3.Enabled = True
            ToolStripButton4.Enabled = True
            ToolStripButton5.Enabled = True
            ToolStripButton6.Enabled = True
            ToolStripButton7.Enabled = False
            ToolStripButton8.Enabled = False
        ElseIf TextBox5.Text <= 250000 Then
            ToolStripButton1.Enabled = True
            ToolStripButton2.Enabled = True
            ToolStripButton3.Enabled = True
            ToolStripButton4.Enabled = True
            ToolStripButton5.Enabled = True
            ToolStripButton6.Enabled = True
            ToolStripButton7.Enabled = True
            ToolStripButton8.Enabled = False
        End If
    End Sub
End Class

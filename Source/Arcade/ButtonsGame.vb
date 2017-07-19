Public Class ButtonsGame




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim rand As New Random
        Dim x As Integer = rand.Next(0, Me.ClientSize.Width - _
           Button1.Width)
        Dim y As Integer = rand.Next(0, Me.ClientSize.Height - _
           Button1.Height)
        Button1.Location = New Point(x, y)

        Button1.Text = Button1.Text
        Button1.Size = Button1.Size
        Button1.BackColor = Button1.BackColor


        AddHandler Button1.Click, AddressOf Button1_Click

        Static hits As Integer

        hits += 1 * 100

        Label1.Text = hits.ToString("n0")
        If Label1.Text = 1 Then Label4.Text = 1
        If Label1.Text = 170 Then Label4.Text = 2
        If Label1.Text = 230 Then Label4.Text = 3
        If Label1.Text = 260 Then Label4.Text = 4
        If Label1.Text = 275 Then Label4.Text = 5
        If Label1.Text = 280 Then Label4.Text = 6
        If Label1.Text = 580 Then Label4.Text = 7
        If Label1.Text = 1000 Then Label4.Text = 8
        If Label1.Text = 3550 Then Label4.Text = 9
        If Label1.Text = 5000 Then Label4.Text = 10
        If Label1.Text = 55000 Then Label4.Text = 11
        If Label1.Text = 60000 Then Label4.Text = 11
        If Label1.Text = 70000 Then Label4.Text = 12
        If Label1.Text = 75000 Then Label4.Text = 13
        If Label1.Text = 81000 Then Label4.Text = 14
        If Label1.Text = 100000 Then Label4.Text = 15
        If Label1.Text = 125000 Then Label4.Text = 16
        If Label1.Text = 137000 Then Label4.Text = 17
        If Label1.Text = 178000 Then Label4.Text = "You may wanna be careful what you click!!!!"
        If Label1.Text = 178000 Then Label4.Text = 18
        If Label1.Text = 180000 Then Label4.Text = 19
        If Label1.Text = 193000 Then Label4.Text = "Bonus Round Points go double here!"
        If Label1.Text = 194000 Then Button1.Location = New Point(x, y)
        If Label1.Text = 194000 Then Label4.Text = "B20"
        If Label1.Text = 210000 Then Button1.Location = New Point(x, y)
        If Label1.Text = 210000 Then Label4.Text = 21
        If Label1.Text = 211000 Then Label4.Text = "It is gonna get harder!"
        If Label1.Text = 300000 Then Label4.Text = 22
        If Label1.Text = 310000 Then Label4.Text = 23
        If Label1.Text = 327000 Then Label4.Text = 24
        If Label1.Text = 600000 Then Label4.Text = 25
        If Label1.Text = 1000000 Then Label4.Text = 26
        If Label1.Text = 1250000 Then Label4.Text = 27
        If Label1.Text = 1600000 Then Label4.Text = 28
        If Label1.Text = 2000000 Then Label4.Text = 29
        If Label1.Text = 2500000 Then Label4.Text = 30
        If Label1.Text = 2510000 Then Label4.Text = "Take the score x10 now"
        If Label1.Text = 2999999 Then Label4.Text = "Almost a Champ!"
        If Label1.Text = 3010000 Then Label4.Text = 31
        If Label1.Text = 3255555 Then Label4.Text = 32
        If Label1.Text = 3333333 Then Label4.Text = 35
        If Label1.Text = 3444444 Then Label4.Text = 36
        If Label1.Text = 3500000 Then Me.Close()

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        MsgBox("Annoying Button Adiction! How to play well click on the button and your score will increase! Enjoy it now because It is gonna get harder! ")
    End Sub


    Private Sub BBBBBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Begin!"
        Label1.Text = "1"
        Label4.Text = "1"

    End Sub

    Private Sub ButtonsG_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MessageBox.Show("Final score  " + Label1.Text + ", Level  " + Label4.Text)
    End Sub

    
  
End Class

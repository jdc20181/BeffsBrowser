Public Class MathOfQuiz

    ' Create a Random object called randomizer 
    ' to generate random numbers.
    Private randomizer As New Random()

    ' These integer variables store the numbers 
    ' for the addition problem. 
    Private addend1 As Integer
    Private addend2 As Integer

    ' These integer variables store the numbers 
    ' for the subtraction problem. 
    Private minuend As Integer
    Private subtrahend As Integer

    ' These integer variables store the numbers 
    ' for the multiplication problem. 
    Private multiplicand As Integer
    Private multiplier As Integer

    ' These integer variables store the numbers 
    ' for the division problem. 
    Private dividend As Integer
    Private divisor As Integer

    ' This integer variable keeps track of the 
    ' remaining time.
    Private timeLeft As Integer

 

    ''' <summary> 
    ''' Call the StartTheQuiz() method and enable
    ''' the Start button. 
    ''' </summary> 
    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        StartTheQuiz()
        startButton.Enabled = False
    End Sub

    ''' <summary> 
    ''' Start the quiz by filling in all of the problem 
    ''' values and starting the timer. 
    ''' </summary> 
    Public Sub StartTheQuiz()
        ' Fill in the addition problem.
        ' Generate two random numbers to add.
        ' Store the values in the variables 'addend1' and 'addend2'.
        addend1 = randomizer.[Next](51)
        addend2 = randomizer.[Next](51)

        ' Convert the two randomly generated numbers
        ' into strings so that they can be displayed
        ' in the label controls.
        plusLeftLabel.Text = addend1.ToString()
        plusRightLabel.Text = addend2.ToString()

        ' 'sum' is the name of the NumericUpDown control.
        ' This step makes sure its value is zero before
        ' adding any values to it.
        sum.Value = 0

        ' Fill in the subtraction problem.
        minuend = randomizer.[Next](1, 101)
        subtrahend = randomizer.[Next](1, minuend)
        minusLeftLabel.Text = minuend.ToString()
        minusRightLabel.Text = subtrahend.ToString()
        difference.Value = 0

        ' Fill in the multiplication problem.
        multiplicand = randomizer.[Next](2, 11)
        multiplier = randomizer.[Next](2, 11)
        timesLeftLabel.Text = multiplicand.ToString()
        timesRightLabel.Text = multiplier.ToString()
        product.Value = 0

        ' Fill in the division problem.
        divisor = randomizer.[Next](2, 11)
        Dim temporaryQuotient As Integer = randomizer.[Next](2, 11)
        dividend = divisor * temporaryQuotient
        dividedLeftLabel.Text = dividend.ToString()
        dividedRightLabel.Text = divisor.ToString()
        quotient.Value = 0

        ' Start the timer.
        timeLeft = 30
        timeLabel.Text = "30 seconds"
        timer1.Start()
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs) Handles timer1.Tick
        If CheckTheAnswer() Then
            ' If CheckTheAnswer() returns true, then the user 
            ' got the answer right. Stop the timer  
            ' and show a MessageBox.
            timer1.[Stop]()
            MessageBox.Show("You got all the answers right!", "Congratulations!")
            startButton.Enabled = True
        ElseIf timeLeft > 0 Then
            ' If CheckTheAnswer() return false, keep counting
            ' down. Decrease the time left by one second and 
            ' display the new time left by updating the 
            ' Time Left label.
            timeLeft -= 1
            timeLabel.Text = timeLeft & " seconds"
        Else
            ' If the user ran out of time, stop the timer, show 
            ' a MessageBox, and fill in the answers.
            timer1.[Stop]()
            timeLabel.Text = "Time's up!"
            MessageBox.Show("You didn't finish in time.", "Sorry!")
            sum.Value = addend1 + addend2
            difference.Value = minuend - subtrahend
            product.Value = multiplicand * multiplier
            quotient.Value = dividend \ divisor
            startButton.Enabled = True
        End If
    End Sub

    ''' <summary> 
    ''' Check the answers to see if the user got everything right. 
    ''' </summary> 
    ''' <returns>True if the answers are correct, false otherwise.</returns> 
    Private Function CheckTheAnswer() As Boolean
        If (addend1 + addend2 = sum.Value) AndAlso (minuend - subtrahend = difference.Value) AndAlso (multiplicand * multiplier = product.Value) AndAlso (dividend \ divisor = quotient.Value) Then
            Return True
        Else
            Return False
        End If
    End Function



    ''' <summary> 
    ''' Modify the behavior of the NumericUpDown control
    ''' to make it easier to enter numeric values for
    ''' the quiz.
    ''' </summary> 
    Private Sub answer_Enter(sender As Object, e As EventArgs) Handles sum.Enter, quotient.Enter, product.Enter, difference.Enter
        ' Select the whole answer in the NumericUpDown control.
        Dim answerBox As NumericUpDown = TryCast(sender, NumericUpDown)

        If answerBox IsNot Nothing Then
            Dim lengthOfAnswer As Integer = answerBox.Value.ToString().Length
            answerBox.[Select](0, lengthOfAnswer)
        End If
    End Sub

    Private Sub MathOfQuiz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MsgBox("Discontinued.")
        Me.Close()

    End Sub
End Class

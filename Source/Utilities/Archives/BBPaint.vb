Public Class BBPaint
    'The lines that have been drawn but not saved.
    Private lines As New List(Of Line)

    'The start point of the line currently being drawn.
    Private start As Point

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Place a blank image in the PictureBox control.
        Me.PictureBox1.Image = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        'Remember the point at which the line started.
        Me.start = e.Location
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        'Remember the point at which the line ended.
        Dim [end] As Point = e.Location

        'Add the new line to the list.
        Me.lines.Add(New Line(Me.start, [end]))

        Dim area As New Rectangle(Math.Min(Me.start.X, [end].X), _
                                  Math.Min(Me.start.Y, [end].Y), _
                                  Math.Abs(Me.start.X - [end].X), _
                                  Math.Abs(Me.start.Y - [end].Y))

        'Inflate the rectangle by 1 pixel in each direction to ensure every changed pixel will be redrawn.
        area.Inflate(1, 1)

        'Force the control to repaint so the new line is drawn.
        Me.PictureBox1.Invalidate(area)
        Me.PictureBox1.Update()
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        'Draw each line on the control.
        Me.DrawLines(e.Graphics)
    End Sub

    Private Sub Save()
        'Create a Graphics object from the Image in the PictureBox.
        Using g As Graphics = Graphics.FromImage(Me.PictureBox1.Image)
            'Draw each line on the image to make them permanent.
            Me.DrawLines(g)
        End Using

        'Clear the temporary lines that were just saved.
        Me.Clear()
    End Sub

    Private Sub Clear()
        'Clear all unsaved lines.
        Me.lines.Clear()

        'Force the control to repaint so the lines are removed.
        Me.PictureBox1.Refresh()
    End Sub

    Private Sub DrawLines(ByVal g As Graphics)
        For Each line As Line In Me.lines
            g.DrawLine(Pens.Black, line.Start, line.End)
        Next line
    End Sub

End Class

Public Class Line

    'The line's start point.
    Private _start As Point

    'The line's end point.
    Private _end As Point

    'The line's start point.
    Public Property Start() As Point
        Get
            Return Me._start
        End Get
        Set(ByVal value As Point)
            Me._start = value
        End Set
    End Property

    'The line's end point.
    Public Property [End]() As Point
        Get
            Return Me._end
        End Get
        Set(ByVal value As Point)
            Me._end = value
        End Set
    End Property

    Public Sub New()
        Me.New(Point.Empty, Point.Empty)
    End Sub

    Public Sub New(ByVal start As Point, ByVal [end] As Point)
        Me._start = start
        Me._end = [end]
    End Sub
End Class

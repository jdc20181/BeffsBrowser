Public Class beffseasycapture
    
   Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click, CaptureToolStripMenuItem.Click
        Me.Opacity = 0
        Dim area As Rectangle
        Dim capture As System.Drawing.Bitmap
        Dim graph As Graphics
        area = Screen.PrimaryScreen.WorkingArea
        capture = New System.Drawing.Bitmap(area.Width, area.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        graph = Graphics.FromImage(capture)
        graph.CopyFromScreen(area.X, area.Y, 0, 0, area.Size, CopyPixelOperation.SourceCopy)
        PictureBox1.Image = capture
        information()
        AutoSave()

    End Sub
    Public Sub information()
        Me.Opacity = 1


    End Sub
    
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click, SaveToolStripMenuItem.Click
        Dim save As New SaveFileDialog
        Try
            save.Title = "Save File"
            save.FileName = "Screenshot"
            save.Filter = "png|*.png"
            If save.ShowDialog = Windows.Forms.DialogResult.OK Then
                PictureBox1.Image.Save(save.FileName, Drawing.Imaging.ImageFormat.Png)
            End If

        Catch ex As Exception

        End Try
    End Sub
    
    
    Public Sub AutoSave()
Me.PictureBox1.Image.Save(IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyPictures, "ScreenShot-" & Now.ToString("ddd_dd_MM_yyyy_hh_mm_ss")))
        End Sub




    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()

        My.Computer.Clipboard.SetImage(PictureBox1.Image)

       
    End Sub

  
    Private Sub beffseasycapture_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolStripDropDownButton1.Visible = False
    End Sub
End Class

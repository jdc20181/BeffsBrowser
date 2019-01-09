Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization
Public Class EasyCaptImgur
    Public Shared Property TaskbarManager As Object
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
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
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Dim save As New SaveFileDialog
        Try
            save.Title = "Save File"
            save.FileName = "Screenshot"
            '  save.Filter = "png|*.png"
            save.Filter = "Bitmap Image (.bmp)|*.bmp|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff"
            If save.ShowDialog = Windows.Forms.DialogResult.OK Then
                PictureBox1.Image.Save(save.FileName, Drawing.Imaging.ImageFormat.Png)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub AutoSave()
        'Dim SavePath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) & "ScreenShot-" & Now.ToString("ddd_dd_MM_yyyy_hh_mm_ss")

        Dim strfilename = My.Computer.FileSystem.SpecialDirectories.MyPictures & "/BeffsEasyCapture/" & "ScreenShot-" & Now.ToString("ddd_dd_MM_yyyy_hh_mm_ss") & ".png"
        'PictureBox1.Image.Save(SavePath, Drawing.Imaging.ImageFormat.Png)

        ' Me.PictureBox1.Image.Save(IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyPictures & "/BeffsEasyCapture/", "ScreenShot-" & Now.ToString("ddd_dd_MM_yyyy_hh_mm_ss" & ".Png")))
        Me.PictureBox1.Image.Save(IO.Path.Combine(strfilename))
        Me.Text = "AutoSaved To: " & strfilename
        PostToImgur(strfilename, "Abc1234")

    End Sub


    Public Class ImgurImageData
        Public Property link() As String
    End Class
    Public Class ImgurAPI
        Public Property data() As ImgurImageData
    End Class

    Public Shared Function PostToImgur(ByVal imagFilePath As String, ByVal apiKey As String) As String
        Dim imageData As Byte()
        Dim fileStream As FileStream = File.OpenRead(imagFilePath)
        imageData = New Byte(fileStream.Length - 1) {}
        fileStream.Read(imageData, 0, imageData.Length)
        fileStream.Close()
        Const MAX_URI_LENGTH As Integer = 32766
        Dim base64img As String = System.Convert.ToBase64String(imageData)
        Dim sb As StringBuilder = New StringBuilder()
        Dim i As Integer = 0

        While i < base64img.Length
            sb.Append(Uri.EscapeDataString(base64img.Substring(i, Math.Min(MAX_URI_LENGTH, base64img.Length - i))))
            i += MAX_URI_LENGTH
        End While

        Dim uploadRequestString As String = "key=" & apiKey & "&title=" & "imageTitle" & "&caption=" & "img" & "&image=" & sb.ToString()
        Dim request As HttpWebRequest = CType(WebRequest.Create("https://api.imgur.com/3/image"), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        request.ServicePoint.Expect100Continue = False
        request.Headers("Authorization") = "Client-ID Abc1234"
        Dim streamWriter As StreamWriter = New StreamWriter(request.GetRequestStream())
        streamWriter.Write(uploadRequestString)
        streamWriter.Close()
        Dim response As WebResponse = request.GetResponse()
        Dim responseStream As Stream = response.GetResponseStream()
        Dim responseReader As StreamReader = New StreamReader(responseStream)

        Dim jss As New JavaScriptSerializer()
        Dim image As ImgurAPI = jss.Deserialize(Of ImgurAPI)(responseReader.ReadToEnd())

        Dim url As String = image.data.link

        Clipboard.Clear()
        Clipboard.SetText(url)
        'TextBox1.Text = url
    End Function
End Class

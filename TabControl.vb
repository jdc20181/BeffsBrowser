Imports System.Runtime.InteropServices
Public Class beffscustomtabcontrol
    Inherits System.Windows.Forms.TabControl

    Private _hotTabIndex As Int32 = -1

    Public Sub New()
        MyBase.New()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
    End Sub

#Region " Properties "

    Private ReadOnly Property CloseButtonHeight As Int32
        Get
            Return FontHeight
        End Get
    End Property

    Private Property HotTabIndex As Int32
        Get
            Return _hotTabIndex
        End Get
        Set(ByVal value As Int32)
            If _hotTabIndex <> value Then
                _hotTabIndex = value
                Me.Invalidate()
            End If
        End Set
    End Property

#End Region

#Region " Overridden Methods"
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        Me.OnFontChanged(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
        MyBase.OnFontChanged(e)
        Dim hFont As IntPtr = Me.Font.ToHfont()
        SendMessage(Me.Handle, WM_SETFONT, hFont, New IntPtr(-1))
        SendMessage(Me.Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero)
        Me.UpdateStyles()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim HTI As New TCHITTESTINFO(e.X, e.Y)
        HotTabIndex = SendMessage(Me.Handle, TCM_HITTEST, IntPtr.Zero, HTI)
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        HotTabIndex = -1
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaintBackground(pevent)
        For id As Int32 = 0 To Me.TabCount - 1
            DrawTabBackground(pevent.Graphics, id)
        Next
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        For id As Int32 = 0 To Me.TabCount - 1
            DrawTabContent(e.Graphics, id)
        Next
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = TCM_SETPADDING Then
            m.LParam = MAKELPARAM(Me.Padding.X + CloseButtonHeight \ 2, Me.Padding.Y)
        End If
        If m.Msg = WM_MOUSEDOWN AndAlso Not Me.DesignMode Then
            Dim pt As Point = Me.PointToClient(Cursor.Position)
            Dim closeRect As Rectangle = GetCloseButtonRect(HotTabIndex)
            If closeRect.Contains(pt) Then
                TabPages.RemoveAt(HotTabIndex)
                m.Msg = WM_NULL
            End If
        End If
        MyBase.WndProc(m)
    End Sub

#End Region

#Region " Private Methods "

    Private Function MAKELPARAM(ByVal lo As Integer, ByVal hi As Integer) As IntPtr
        Return New IntPtr((hi << 16) Or (lo And &HFFFF))
    End Function

    Private Sub DrawTabBackground(ByVal graphics As Graphics, ByVal id As Integer)
        If id = SelectedIndex Then
            graphics.FillRectangle(Brushes.DarkGray, GetTabRect(id))
        ElseIf id = HotTabIndex Then
            Dim rc As Rectangle = GetTabRect(id)
            rc.Width -= 1
            rc.Height -= 1
            graphics.DrawRectangle(Pens.DarkGray, rc)
        End If
    End Sub

    Private Sub DrawTabContent(ByVal graphics As Graphics, ByVal id As Integer)
        Dim selectedOrHot As Boolean = id = Me.SelectedIndex OrElse id = Me.HotTabIndex
        Dim vertical As Boolean = Me.Alignment >= 2

        Dim tabImage As Image = Nothing

        If (Me.ImageList IsNot Nothing) Then
            Dim page As TabPage = Me.TabPages(id)
            If page.ImageIndex > -1 AndAlso page.ImageIndex < Me.ImageList.Images.Count Then
                tabImage = Me.ImageList.Images(page.ImageIndex)
            End If
            If page.ImageKey.Length > 0 AndAlso Me.ImageList.Images.ContainsKey(page.ImageKey) Then
                tabImage = Me.ImageList.Images(page.ImageKey)
            End If
        End If

        Dim tabRect As Rectangle = GetTabRect(id)
        Dim contentRect As Rectangle = If(vertical, New Rectangle(0, 0, tabRect.Height, tabRect.Width), New Rectangle(Point.Empty, tabRect.Size))
        Dim textrect As Rectangle = contentRect
        textrect.Width -= FontHeight

        If tabImage IsNot Nothing Then
            textrect.Width -= tabImage.Width
            textrect.X += tabImage.Width
        End If

        Dim frColor As Color = If(id = SelectedIndex, Color.White, Me.ForeColor)
        Dim bkColor As Color = If(id = SelectedIndex, Color.DarkGray, Me.BackColor)
        Using bm As Bitmap = New Bitmap(contentRect.Width, contentRect.Height)
            Using bmGraphics As Graphics = graphics.FromImage(bm)
                TextRenderer.DrawText(bmGraphics, Me.TabPages(id).Text, Me.Font, textrect, frColor, bkColor)
                If selectedOrHot Then
                    Dim closeRect As Rectangle = New Rectangle(contentRect.Right - CloseButtonHeight, 0, CloseButtonHeight, CloseButtonHeight)
                    closeRect.Offset(-2, (contentRect.Height - closeRect.Height) \ 2)
                    DrawCloseButton(bmGraphics, closeRect)
                End If
                If (tabImage IsNot Nothing) Then
                    Dim imageRect As New Rectangle(Padding.X, 0, tabImage.Width, tabImage.Height)
                    imageRect.Offset(0, (contentRect.Height - imageRect.Height) \ 2)
                    bmGraphics.DrawImage(tabImage, imageRect)
                End If
            End Using
            If vertical Then
                If Me.Alignment = TabAlignment.Left Then
                    bm.RotateFlip(RotateFlipType.Rotate270FlipNone)
                Else
                    bm.RotateFlip(RotateFlipType.Rotate90FlipNone)
                End If
            End If
            graphics.DrawImage(bm, tabRect)
        End Using

    End Sub

    Private Sub DrawCloseButton(ByVal graphics As Graphics, ByVal bounds As Rectangle)
        graphics.FillRectangle(Brushes.Red, bounds)
        Using closeFont As Font = New Font("Arial", Font.Size, FontStyle.Bold)
            TextRenderer.DrawText(graphics, "X", closeFont, bounds, Color.White, Color.Red, TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or TextFormatFlags.VerticalCenter)
        End Using
    End Sub

    Private Function GetCloseButtonRect(ByVal id As Integer) As Rectangle

        Dim tabRect As Rectangle = GetTabRect(id)
        Dim closeRect As Rectangle = New Rectangle(tabRect.Left, tabRect.Top, CloseButtonHeight, CloseButtonHeight)

        Select Case Alignment
            Case TabAlignment.Left
                closeRect.Offset((tabRect.Width - closeRect.Width) \ 2, 0)
            Case TabAlignment.Right
                closeRect.Offset((tabRect.Width - closeRect.Width) \ 2, tabRect.Height - closeRect.Height)
            Case Else
                closeRect.Offset(tabRect.Width - closeRect.Width, (tabRect.Height - closeRect.Height) \ 2)
        End Select

        Return closeRect

    End Function

#End Region

#Region " Interop "

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Int32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal msg As Int32, ByVal wParam As IntPtr, ByRef lParam As TCHITTESTINFO) As Int32
    End Function

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Private Structure TCHITTESTINFO
        Public pt As Point
        Public flags As TCHITTESTFLAGS
        Public Sub New(ByVal x As Int32, ByVal y As Int32)
            pt = New Point(x, y)
        End Sub
    End Structure

    <Flags()> _
    Private Enum TCHITTESTFLAGS
        TCHT_NOWHERE = 1
        TCHT_ONITEMICON = 2
        TCHT_ONITEMLABEL = 4
        TCHT_ONITEM = TCHT_ONITEMICON Or TCHT_ONITEMLABEL
    End Enum

    Private Const WM_NULL As Int32 = &H0
    Private Const WM_SETFONT = &H30
    Private Const WM_FONTCHANGE = &H1D
    Private Const WM_MOUSEDOWN As Int32 = &H201

    Private Const TCM_FIRST = &H1300
    Private Const TCM_HITTEST = TCM_FIRST + 13
    Private Const TCM_SETPADDING = TCM_FIRST + 43

#End Region

End Class

Module Module1
    Function getgraphicsobject(ByRef picturebox1 As PictureBox) As Graphics
        Dim dx As Integer = 100
        Dim dy As Integer = 100

        If picturebox1.Width > dx Then
            dx = picturebox1.Width
        End If
        If picturebox1.Height > dy Then
            dy = picturebox1.Height
        End If
        Dim bmp As Bitmap
        bmp = New Bitmap(dx, dy)
        picturebox1.Image = bmp
        Dim G As Graphics
        G = Graphics.fromimage(bmp)
        Return G
    End Function
End Module

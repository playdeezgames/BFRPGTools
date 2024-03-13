Public Class PixelBuffer(Of TPixel As Structure)
    Inherits BasePixelSink(Of TPixel)
    Implements IPixelBuffer(Of TPixel)

    Private ReadOnly pixels As TPixel()

    Sub New(columns As Integer, rows As Integer, Optional pixels As TPixel() = Nothing)
        Me.Size = (columns, rows)
        Me.pixels = If(pixels, Enumerable.Range(0, columns * rows).Select(Function(x) CType(Nothing, TPixel)).ToArray)
    End Sub

    Public Overrides ReadOnly Property Size As (Columns As Integer, Rows As Integer)

    Public Overrides Sub Write(column As Integer, row As Integer, pixel As TPixel) Implements IPixelBuffer(Of TPixel).Write
        If column >= 0 AndAlso row >= 0 AndAlso column < Size.Columns AndAlso row < Size.Rows Then
            pixels(column + row * Size.Columns) = pixel
        End If
    End Sub

    Public Sub WriteAll(pixel As TPixel) Implements IPixelBuffer(Of TPixel).WriteAll
        For Each index In Enumerable.Range(0, pixels.Length)
            pixels(index) = pixel
        Next
    End Sub

    Public Function Read(column As Integer, row As Integer) As TPixel Implements IPixelBuffer(Of TPixel).Read
        If column >= 0 AndAlso row >= 0 AndAlso column < Size.Columns AndAlso row < Size.Rows Then
            Return pixels(column + row * Size.Columns)
        End If
    End Function
End Class

Public Class PixelBuffer(Of TPixel As Structure)
    Inherits BasePixelSink(Of TPixel)
    Implements IPixelBuffer(Of TPixel)

    Private ReadOnly pixels As TPixel()

    Sub New(columns As Integer, rows As Integer, Optional pixels As TPixel() = Nothing)
        Me.Columns = columns
        Me.Rows = rows
        Me.pixels = If(pixels, Enumerable.Range(0, columns * rows).Select(Function(x) CType(Nothing, TPixel)).ToArray)
    End Sub

    Public ReadOnly Property Columns As Integer Implements IPixelBuffer(Of TPixel).Columns

    Public ReadOnly Property Rows As Integer Implements IPixelBuffer(Of TPixel).Rows

    Public Overrides Sub Write(column As Integer, row As Integer, pixel As TPixel) Implements IPixelBuffer(Of TPixel).Write
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Throw New ArgumentOutOfRangeException()
        End If
        pixels(column + row * Columns) = pixel
    End Sub

    Public Sub WriteAll(pixel As TPixel) Implements IPixelBuffer(Of TPixel).WriteAll
        For Each index In Enumerable.Range(0, pixels.Length)
            pixels(index) = pixel
        Next
    End Sub

    Public Function Read(column As Integer, row As Integer) As TPixel Implements IPixelBuffer(Of TPixel).Read
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Throw New ArgumentOutOfRangeException()
        End If
        Return pixels(column + row * Columns)
    End Function
End Class

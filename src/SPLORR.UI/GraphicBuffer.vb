Public Class GraphicBuffer(Of TPixel As Structure)
    Implements IGraphicBuffer(Of TPixel)

    Private ReadOnly pixels As TPixel()

    Sub New(columns As Integer, rows As Integer)
        Me.Columns = columns
        Me.Rows = rows
        Dim pixel As TPixel = Nothing
        Me.pixels = Enumerable.Range(0, columns * rows).Select(Function(x) pixel).ToArray
    End Sub

    Public ReadOnly Property Columns As Integer Implements IGraphicBuffer(Of TPixel).Columns

    Public ReadOnly Property Rows As Integer Implements IGraphicBuffer(Of TPixel).Rows

    Public Sub Write(column As Integer, row As Integer, pixel As TPixel) Implements IGraphicBuffer(Of TPixel).Write
        pixels(column + row * Columns) = pixel
    End Sub

    Public Function Read(column As Integer, row As Integer) As TPixel Implements IGraphicBuffer(Of TPixel).Read
        Return pixels(column + row * Columns)
    End Function
End Class

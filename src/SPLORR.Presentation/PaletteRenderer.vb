Public Class PaletteRenderer(Of TPixelFrom As Structure, TPixelTo As Structure)
    Implements IRenderer(Of TPixelFrom, TPixelTo)
    Private ReadOnly palette As IReadOnlyDictionary(Of TPixelFrom, TPixelTo)
    Sub New(palette As IReadOnlyDictionary(Of TPixelFrom, TPixelTo))
        Me.palette = palette
    End Sub
    Public Sub Render(bufferFrom As IPixelBuffer(Of TPixelFrom), bufferTo As IPixelBuffer(Of TPixelTo)) Implements IRenderer(Of TPixelFrom, TPixelTo).Render
        For Each y In Enumerable.Range(0, bufferFrom.Size.Rows)
            For Each x In Enumerable.Range(0, bufferFrom.Size.Columns)
                bufferTo.Write(x, y, palette(bufferFrom.Read(x, y)))
            Next
        Next
    End Sub
End Class

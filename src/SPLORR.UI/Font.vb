Public Class Font
    Private ReadOnly glyphs As New Dictionary(Of Char, GlyphBuffer)
    Public ReadOnly Height As Integer
    Public Sub New(fontData As FontData)
        Height = fontData.Height
        For Each glyph In fontData.Glyphs.Keys
            glyphs(glyph) = New GlyphBuffer(fontData, glyph)
        Next
    End Sub
    Public Sub WriteText(Of TPixel As Structure)(sink As IPixelSink(Of TPixel), position As (column As Integer, row As Integer), text As String, pixel As TPixel)
        For Each character In text
            Dim buffer = glyphs(character)
            buffer.CopyTo(sink, position, pixel)
            position = (position.column + buffer.Width, position.row)
        Next
    End Sub
    Public Function TextWidth(text As String) As Integer
        Return text.Sum(Function(x) glyphs(x).Width)
    End Function
End Class

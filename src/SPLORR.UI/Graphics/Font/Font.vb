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
    Public Sub WriteLeftText(Of TPixel As Structure)(sink As IPixelSink(Of TPixel), row As Integer, text As String, pixel As TPixel)
        WriteText(sink, (0, row), text, pixel)
    End Sub
    Public Sub WriteRightText(Of TPixel As Structure)(sink As IPixelSink(Of TPixel), row As Integer, text As String, pixel As TPixel)
        WriteText(sink, (sink.Size.Columns - TextWidth(text), row), text, pixel)
    End Sub
    Public Sub WriteCenterText(Of TPixel As Structure)(sink As IPixelSink(Of TPixel), row As Integer, text As String, pixel As TPixel)
        WriteText(sink, ((sink.Size.Columns - TextWidth(text)) \ 2, row), text, pixel)
    End Sub
    Public Function TextWidth(text As String) As Integer
        Return text.Sum(Function(x) glyphs(x).Width)
    End Function
End Class

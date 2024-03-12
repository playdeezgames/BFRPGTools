Public Class GlyphBuffer
    Inherits PixelBuffer(Of Boolean)
    Public ReadOnly Property Width As Integer
    Public ReadOnly Property Height As Integer
    Public Sub New(font As FontData, glyph As Char)
        MyBase.New(font.Glyphs(glyph).Width, font.Height)
        Height = font.Height
        Width = font.Glyphs(glyph).Width
        For Each row In font.Glyphs(glyph).Lines
            For Each column In row.Value
                Write(column, row.Key, True)
            Next
        Next
    End Sub
    Public Sub CopyTo(Of TPixel As Structure)(
                                           sink As IPixelSink(Of TPixel),
                                           position As (Integer, Integer),
                                           hue As TPixel)
        sink.ColorizeWrite(Me, (0, 0), position, (Width, Height), Function(x)
                                                                      If x Then
                                                                          Return hue
                                                                      Else
                                                                          Return Nothing
                                                                      End If
                                                                  End Function)
    End Sub
End Class

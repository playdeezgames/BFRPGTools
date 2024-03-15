Imports System.Drawing
Imports System.IO
Imports System.Text.Json
Imports SPLORR.UI

<System.Runtime.Versioning.SupportedOSPlatform("windows")>
Module Program
    Sub Main(args As String())
        Dim inputFilename = args(0)
        Dim cellWidth = CInt(args(1))
        Dim cellHeight = CInt(args(2))
        Dim outputFilename = args(3)

        Dim bmp = New Bitmap(inputFilename)
        Dim glyph = ChrW(0)
        Dim fontData As New FontData With {
            .Height = cellHeight,
            .Glyphs = New Dictionary(Of Char, GlyphData)
        }

        For Each row In Enumerable.Range(0, (bmp.Height + 1) \ (cellHeight + 1))
            For Each column In Enumerable.Range(0, (bmp.Width + 1) \ (cellWidth + 1))
                Dim glyphData As New GlyphData With
                    {
                        .Width = cellWidth,
                        .Lines = New Dictionary(Of Integer, IEnumerable(Of Integer))
                    }
                fontData.Glyphs(glyph) = glyphData
                glyph = ChrW(AscW(glyph) + 1)
                Console.WriteLine(AscW(glyph))

                For Each y In Enumerable.Range(0, cellHeight)
                    Dim line As New List(Of Integer)

                    For Each x In Enumerable.Range(0, cellWidth)
                        Dim color = bmp.GetPixel(column * (cellWidth + 1) + x, row * (cellHeight + 1) + y)
                        If color.R = 0 AndAlso color.G = 0 AndAlso color.B = 0 Then
                            Console.Write(" ")
                        Else
                            Console.Write("#")
                            line.Add(x)
                        End If
                    Next

                    If line.Count <> 0 Then
                        glyphData.Lines(y) = line
                    End If
                    Console.WriteLine()
                Next
            Next
        Next
        File.WriteAllText(OutputFilename, JsonSerializer.Serialize(fontData))
    End Sub
End Module

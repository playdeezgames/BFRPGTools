Imports Microsoft.Xna.Framework

Public Class Renderer
    Implements IRenderer(Of Hue, Color)
    Private ReadOnly palette As IReadOnlyDictionary(Of Hue, Color) =
        New Dictionary(Of Hue, Color) From
        {
            {Hue.Black, New Color(0, 0, 0)},
            {Hue.Blue, New Color(0, 0, 170)},
            {Hue.Green, New Color(0, 170, 0)},
            {Hue.Cyan, New Color(0, 170, 170)},
            {Hue.Red, New Color(170, 0, 0)},
            {Hue.Magenta, New Color(170, 0, 170)},
            {Hue.Orange, New Color(170, 85, 0)},
            {Hue.LightGray, New Color(170, 170, 170)},
            {Hue.DarkGray, New Color(85, 85, 85)},
            {Hue.LightBlue, New Color(85, 85, 255)},
            {Hue.LightGreen, New Color(85, 255, 85)},
            {Hue.LightCyan, New Color(85, 255, 255)},
            {Hue.LightRed, New Color(255, 85, 85)},
            {Hue.LightMagenta, New Color(255, 85, 255)},
            {Hue.Yellow, New Color(255, 255, 85)},
            {Hue.White, New Color(255, 255, 255)}
        }

    Public Sub Render(bufferFrom As IPixelBuffer(Of Hue), bufferTo As IPixelBuffer(Of Color)) Implements IRenderer(Of Hue, Color).Render
        For Each y In Enumerable.Range(0, bufferFrom.Rows)
            For Each x In Enumerable.Range(0, bufferFrom.Columns)
                bufferTo.Write(x, y, palette(bufferFrom.Read(x, y)))
            Next
        Next
    End Sub
End Class

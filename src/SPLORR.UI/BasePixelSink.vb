Public MustInherit Class BasePixelSink(Of TPixel As Structure)
    Implements IPixelSink(Of TPixel)

    Public MustOverride Sub Write(
                                 column As Integer,
                                 row As Integer,
                                 pixel As TPixel) Implements IPixelSink(Of TPixel).Write

    Public Sub StretchWrite(
                           source As IPixelSource(Of TPixel),
                           fromLocation As (column As Integer, row As Integer),
                           toLocation As (column As Integer, row As Integer),
                           size As (columns As Integer, rows As Integer),
                           scale As (x As Integer, y As Integer),
                           filter As Func(Of TPixel, Boolean)) Implements IPixelSink(Of TPixel).StretchWrite
        For Each y In Enumerable.Range(0, size.rows)
            For Each x In Enumerable.Range(0, size.columns)
                Dim pixel = source.Read(x + fromLocation.column, y + fromLocation.row)
                If filter(pixel) Then
                    WriteFill((x * scale.x + toLocation.column, y * scale.y + toLocation.row), scale, pixel)
                End If
            Next
        Next
    End Sub

    Public Sub ColorizeWrite(Of TSourcePixel As Structure)(
                                                          source As IPixelSource(Of TSourcePixel),
                                                          fromLocation As (column As Integer, row As Integer),
                                                          toLocation As (column As Integer, row As Integer),
                                                          size As (columns As Integer, rows As Integer),
                                                          xform As Func(Of TSourcePixel, TPixel?)) Implements IPixelSink(Of TPixel).ColorizeWrite
        For Each y In Enumerable.Range(0, size.rows)
            For Each x In Enumerable.Range(0, size.columns)
                Dim destinationPixel As TPixel? = xform(source.Read(x + fromLocation.column, y + fromLocation.row))
                If destinationPixel IsNot Nothing Then
                    Write(x + toLocation.column, y + toLocation.row, destinationPixel.Value)
                End If
            Next
        Next
    End Sub

    Public Sub WriteFill(location As (column As Integer, row As Integer), size As (columns As Integer, rows As Integer), pixel As TPixel) Implements IPixelSink(Of TPixel).WriteFill
        For Each y In Enumerable.Range(0, size.rows)
            For Each x In Enumerable.Range(0, size.columns)
                Write(x, y, pixel)
            Next
        Next
    End Sub

    Public Sub WriteFrame(location As (column As Integer, row As Integer), size As (columns As Integer, rows As Integer), pixel As TPixel) Implements IPixelSink(Of TPixel).WriteFrame
        For Each x In Enumerable.Range(location.column, size.columns)
            Write(x, location.row, pixel)
            Write(x, location.row + size.rows, pixel)
        Next
        For Each y In Enumerable.Range(location.row + 1, size.rows - 2)
            Write(location.column, y, pixel)
            Write(location.column + size.columns - 1, y, pixel)
        Next
    End Sub
End Class

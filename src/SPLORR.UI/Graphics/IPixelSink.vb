Public Interface IPixelSink(Of TPixel As Structure)
    ReadOnly Property Size As (Columns As Integer, Rows As Integer)
    Sub Write(column As Integer, row As Integer, pixel As TPixel)
    Sub StretchWrite(
                    source As IPixelSource(Of TPixel),
                    fromLocation As (column As Integer, row As Integer),
                    toLocation As (column As Integer, row As Integer),
                    size As (columns As Integer, rows As Integer),
                    scale As (x As Integer, y As Integer),
                    filter As Func(Of TPixel, Boolean))
    Sub ColorizeWrite(Of TSourcePixel As Structure)(
                     source As IPixelSource(Of TSourcePixel),
                     fromLocation As (column As Integer, row As Integer),
                     toLocation As (column As Integer, row As Integer),
                     size As (columns As Integer, rows As Integer),
                     xform As Func(Of TSourcePixel, TPixel?))
    Sub WriteFill(
                 location As (column As Integer, row As Integer),
                 size As (columns As Integer, rows As Integer),
                 pixel As TPixel)
    Sub WriteFrame(
                  location As (column As Integer, row As Integer),
                  size As (columns As Integer, rows As Integer),
                  pixel As TPixel)
End Interface

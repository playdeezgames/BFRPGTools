Public Interface IPixelSource(Of TPixel As Structure)
    Function Read(column As Integer, row As Integer) As TPixel
End Interface

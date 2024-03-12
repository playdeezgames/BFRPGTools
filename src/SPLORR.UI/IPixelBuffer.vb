Imports System.IO

Public Interface IPixelBuffer(Of TPixel As Structure)
    Inherits IPixelSource(Of TPixel), IPixelSink(Of TPixel)
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Sub WriteAll(pixel As TPixel)
End Interface

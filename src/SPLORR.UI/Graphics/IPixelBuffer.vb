Imports System.IO

Public Interface IPixelBuffer(Of TPixel As Structure)
    Inherits IPixelSource(Of TPixel), IPixelSink(Of TPixel)
    Sub WriteAll(pixel As TPixel)
End Interface

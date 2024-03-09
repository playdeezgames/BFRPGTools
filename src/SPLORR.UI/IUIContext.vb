Public Interface IUIContext(Of TPixel As Structure)
    ReadOnly Property Display As IPixelBuffer(Of TPixel)
End Interface

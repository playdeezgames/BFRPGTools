Public Interface IRenderer(Of TPixelFrom As Structure, TPixelTo As Structure)
    Sub Render(bufferFrom As IPixelBuffer(Of TPixelFrom), bufferTo As IPixelBuffer(Of TPixelTo))
End Interface

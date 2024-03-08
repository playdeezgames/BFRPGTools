Public Interface IGraphicBuffer(Of TPixel As Structure)
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Function Read(column As Integer, row As Integer) As TPixel
    Sub Write(column As Integer, row As Integer, pixel As TPixel)
End Interface

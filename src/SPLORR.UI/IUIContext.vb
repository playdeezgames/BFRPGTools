Public Interface IUIContext(Of TPixel As Structure, TCommand, TSfx, TModel)
    ReadOnly Property Model As TModel
    ReadOnly Property Display As IPixelBuffer(Of TPixel)
    ReadOnly Property Command As ICommandBuffer(Of TCommand)
    ReadOnly Property Config As IHostConfig
    Sub Play(sfx As TSfx)
End Interface

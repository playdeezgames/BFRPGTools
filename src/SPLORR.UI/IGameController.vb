Public Interface IGameController(Of TPixel As Structure, TCommand, TSfx)
    ReadOnly Property Display As IPixelBuffer(Of TPixel)
    ReadOnly Property Command As ICommandBuffer(Of TCommand)
    ReadOnly Property ViewWidth As Integer
    ReadOnly Property ViewHeight As Integer
    ReadOnly Property FrameWidth As Integer
    ReadOnly Property FrameHeight As Integer
    ReadOnly Property WindowTitle As String
    ReadOnly Property IsFullScreen As Boolean
    ReadOnly Property IsQuitRequested As Boolean
    ReadOnly Property Volume As Single
    ReadOnly Property IsMuted As Boolean
    Sub Update()
    ReadOnly Property QueuedSfx As IEnumerable(Of TSfx)
End Interface

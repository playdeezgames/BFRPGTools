Public Interface IGameController(Of TPixel As Structure, TCommand, TSfx, TModel, TAssets)
    Inherits IUIContext(Of TPixel, TCommand, TSfx, TModel, TAssets)
    ReadOnly Property ViewWidth As Integer
    ReadOnly Property ViewHeight As Integer
    ReadOnly Property FrameWidth As Integer
    ReadOnly Property FrameHeight As Integer
    ReadOnly Property WindowTitle As String
    ReadOnly Property IsFullScreen As Boolean
    ReadOnly Property IsQuitRequested As Boolean
    ReadOnly Property Volume As Single
    ReadOnly Property IsMuted As Boolean
    Sub Update(elapsedTime As TimeSpan)
    ReadOnly Property QueuedSfx As IEnumerable(Of TSfx)
End Interface

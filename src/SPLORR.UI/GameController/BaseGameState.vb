Public MustInherit Class BaseGameState(Of TState, TPixel As Structure, TCommand, TSfx, TModel, TAssets)
    MustOverride Function Update(context As IUIContext(Of TPixel, TCommand, TSfx, TModel, TAssets), elapsedTime As TimeSpan) As TState
End Class

Friend Class EmbarkState
    Inherits BaseGameState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        Throw New NotImplementedException()
    End Function
End Class

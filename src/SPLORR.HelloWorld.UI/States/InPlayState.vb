Friend Class InPlayState
    Inherits BaseGameState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        While context.Command.HasCommand
            Select Case context.Command.ReadCommand()
                Case Command.B
                    Return GameState.GameMenu
            End Select
        End While
        context.Display.WriteAll(Hue.Black)
        context.Assets.Font.WriteLeftText(context.Display, 0, "In Play", Hue.White)
        Return GameState.InPlay
    End Function
End Class

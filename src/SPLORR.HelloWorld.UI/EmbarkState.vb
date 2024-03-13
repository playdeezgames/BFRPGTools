Friend Class EmbarkState
    Inherits BaseGameState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        While context.Command.HasCommand
            Select Case context.Command.ReadCommand()
                Case Command.B
                    Return GameState.MainMenu
            End Select
        End While
        Dim display = context.Display
        Dim font = context.Assets.Font
        display.WriteAll(Hue.Black)
        font.WriteText(display, (0, 0), "Embark!", Hue.White)
        Return GameState.Embark
    End Function
End Class

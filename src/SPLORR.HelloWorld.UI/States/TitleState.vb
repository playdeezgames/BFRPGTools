Friend Class TitleState
    Inherits BaseGameState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Private ReadOnly hues As New List(Of Hue) From
        {
            Hue.LightBlue,
            Hue.LightGreen,
            Hue.LightCyan,
            Hue.LightRed,
            Hue.LightMagenta,
            Hue.Yellow
        }
    Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, Model.HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        While context.Command.HasCommand
            Select Case context.Command.ReadCommand()
                Case Command.Start
                    Return GameState.MainMenu
            End Select
        End While
        Dim font = context.Assets.Font
        Dim display = context.Display
        display.WriteAll(Hue.Black)
        font.WriteCenterText(
            display,
            (display.Size.Rows - font.Height) \ 2,
            "SPLORR!!",
            RNG.FromList(hues))
        font.WriteCenterText(
            display,
            display.Size.Rows - font.Height,
            "Press <START>/<Enter>",
            Hue.DarkGray)
        Return GameState.Title
    End Function
End Class

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
            context.Command.ReadCommand()
        End While
        Dim font = context.Assets.Font
        Dim display = context.Display
        Dim text = "Hello, World!"
        display.WriteAll(Hue.Black)
        font.WriteText(
            display,
            ((display.Columns - font.TextWidth(text)) \ 2, (display.Rows - font.Height) \ 2),
            text,
            RNG.FromList(hues))
        Return GameState.Title
    End Function
End Class

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
        Dim text = "SPLORR!!"
        display.WriteAll(Hue.Black)
        font.WriteText(
            display,
            ((display.Size.Columns - font.TextWidth(text)) \ 2, (display.Size.Rows - font.Height) \ 2),
            text,
            RNG.FromList(hues))
        text = "Press <START>/<Enter>"
        font.WriteText(
            display,
            ((display.Size.Columns - font.TextWidth(text)) \ 2, display.Size.Rows - font.Height),
            text,
            Hue.DarkGray)
        Return GameState.Title
    End Function
End Class

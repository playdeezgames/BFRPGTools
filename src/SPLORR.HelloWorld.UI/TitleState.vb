Friend Module TitleState
    Function Update(context As IUIContext(Of Hue, Command, Sfx, Model.Model, Assets), elapsedTime As TimeSpan) As GameState
        While context.Command.HasCommand
            context.Command.ReadCommand()
        End While
        Dim font = context.Assets.Font
        Dim display = context.Display
        Dim text = "Hello, World!"
        font.WriteText(
            display,
            ((display.Columns - font.TextWidth(text)) \ 2, (display.Rows - font.Height) \ 2),
            text,
            Hue.LightGray)
        Return GameState.Title
    End Function
End Module

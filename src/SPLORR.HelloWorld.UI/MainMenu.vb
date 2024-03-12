Friend Class MainMenu
    Inherits BaseGameState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Const EmbarkText = "Embark!"
    Const ScumLoadText = "Scum Load"
    Const LoadText = "Load..."
    Const OptionsText = "Options..."
    Const AboutText = "About..."
    Const QuitText = "Quit"
    Private ReadOnly menuItems As String() = {
        EmbarkText,
        ScumLoadText,
        LoadText,
        OptionsText,
        AboutText,
        QuitText}
    Private menuItemIndex As Integer = 0

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        While context.Command.HasCommand
            Select Case context.Command.ReadCommand()
                Case Command.Down, Command.Select
                    menuItemIndex = (menuItemIndex + 1) Mod menuItems.Length
                Case Command.Up
                    menuItemIndex = (menuItemIndex + menuItems.Length - 1) Mod menuItems.Length
                Case Command.Start, Command.A
                    Return HandleMenuItem(menuItems(menuItemIndex))
            End Select
        End While
        Dim display = context.Display
        Dim font = context.Assets.Font
        display.WriteAll(Hue.Black)
        Dim text = "Main Menu"
        font.WriteText(display, ((display.Columns - font.TextWidth(text)) \ 2, 0), text, Hue.Orange)

        display.WriteFill((0, (display.Rows - font.Height) \ 2), (display.Columns, font.Height), Hue.LightBlue)
        Dim y = (display.Rows - font.Height) \ 2 - menuItemIndex * font.Height
        For Each index In Enumerable.Range(0, menuItems.Length)
            text = menuItems(index)
            font.WriteText(display, ((display.Columns - font.TextWidth(text)) \ 2, y), text, If(index = menuItemIndex, Hue.Black, Hue.LightBlue))
            y += font.Height
        Next
        Return GameState.MainMenu
    End Function

    Private Function HandleMenuItem(menuItem As String) As GameState
        Select Case menuItem
            Case QuitText
                Return GameState.Quit
        End Select
        Return GameState.MainMenu
    End Function
End Class

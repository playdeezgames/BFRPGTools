Public MustInherit Class BaseMenuState(Of TState As Structure, TPixel As Structure, TModel)
    Inherits BaseGameState(Of TState, TPixel, Command, Sfx, TModel, HWAssets)
    Private ReadOnly menuItems As String()
    Private ReadOnly title As String
    Private ReadOnly state As TState
    Private menuItemIndex As Integer = 0
    Private ReadOnly backgroundHue As TPixel
    Private ReadOnly headerHue As TPixel
    Private ReadOnly hiliteHue As TPixel
    Private ReadOnly footerHue As TPixel
    Protected Sub SetMenuItemIndex(index As Integer)
        menuItemIndex = Math.Clamp(index, 0, menuItems.Length - 1)
    End Sub

    Sub New(
           title As String,
           menuItems As String(),
           state As TState,
           backgroundHue As TPixel,
           headerHue As TPixel,
           hiliteHue As TPixel,
           footerHue As TPixel)
        Me.menuItems = menuItems
        Me.title = title
        Me.state = state
        Me.backgroundHue = backgroundHue
        Me.headerHue = headerHue
        Me.hiliteHue = hiliteHue
    End Sub

    Public Overrides Function Update(context As IUIContext(Of TPixel, Command, Sfx, TModel, HWAssets), elapsedTime As TimeSpan) As TState
        While context.Command.HasCommand
            Select Case context.Command.ReadCommand()
                Case Command.Down, Command.Select
                    menuItemIndex = (menuItemIndex + 1) Mod menuItems.Length
                Case Command.Up
                    menuItemIndex = (menuItemIndex + menuItems.Length - 1) Mod menuItems.Length
                Case Command.Start, Command.A
                    Return HandleMenuItem(menuItems(menuItemIndex))
                Case Command.B
                    Dim backState = HandleGoBack()
                    If backState.HasValue Then
                        Return backState.Value
                    End If
            End Select
        End While
        Dim display = context.Display
        Dim font = context.Assets.Font
        display.WriteAll(backgroundHue)
        Dim text = title
        font.WriteCenterText(display, 0, text, headerHue)

        display.WriteFill((0, (display.Size.Rows - font.Height) \ 2), (display.Size.Columns, font.Height), hiliteHue)
        Dim y = (display.Size.Rows - font.Height) \ 2 - menuItemIndex * font.Height
        For Each index In Enumerable.Range(0, menuItems.Length)
            text = menuItems(index)
            font.WriteCenterText(display, y, text, If(index = menuItemIndex, backgroundHue, hiliteHue))
            y += font.Height
        Next

        display.WriteFill((0, display.Size.Rows - font.Height), (display.Size.Columns, font.Height), footerHue)
        font.WriteCenterText(display, display.Size.Rows - font.Height, "Up/Down/Select | A/Start/Space | B/Esc", backgroundHue)
        Return state
    End Function

    Protected MustOverride Function HandleMenuItem(menuItem As String) As TState
    Protected MustOverride Function HandleGoBack() As TState?
End Class

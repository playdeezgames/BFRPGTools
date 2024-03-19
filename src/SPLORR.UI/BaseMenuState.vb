Public MustInherit Class BaseMenuState(Of TState As Structure, TPixel As Structure, TCommand, TSfx, TModel, TAssets)
    Inherits BaseGameState(Of TState, TPixel, TCommand, TSfx, TModel, TAssets)
    Private ReadOnly menuItems As String()
    Private ReadOnly title As String
    Private ReadOnly state As TState
    Private menuItemIndex As Integer = 0
    Private ReadOnly menuConfig As MenuStateConfig(Of TPixel, TCommand, TAssets)
    Protected Sub SetMenuItemIndex(index As Integer)
        menuItemIndex = Math.Clamp(index, 0, menuItems.Length - 1)
    End Sub

    Sub New(
           title As String,
           menuItems As String(),
           state As TState,
           config As MenuStateConfig(Of TPixel, TCommand, TAssets))
        Me.menuItems = menuItems
        Me.title = title
        Me.state = state
        Me.menuConfig = config
    End Sub

    Public Overrides Function Update(context As IUIContext(Of TPixel, TCommand, TSfx, TModel, TAssets), elapsedTime As TimeSpan) As TState
        While context.Command.HasCommand
            Dim command = context.Command.ReadCommand()
            If menuConfig.NextItemCommand(command) Then
                menuItemIndex = (menuItemIndex + 1) Mod menuItems.Length
            ElseIf menuconfig.previousItemCommand(command) Then
                menuItemIndex = (menuItemIndex + menuItems.Length - 1) Mod menuItems.Length
            ElseIf menuconfig.chooseCommand(command) Then
                Return HandleMenuItem(menuItems(menuItemIndex))
            ElseIf menuconfig.cancelCommand(command) Then
                Dim backState = HandleGoBack()
                If backState.HasValue Then
                    Return backState.Value
                End If
            End If
        End While
        Dim display = context.Display
        Dim font = menuConfig.GetFont(context.Assets)
        display.WriteAll(menuConfig.BackgroundHue)
        Dim text = title
        font.WriteCenterText(display, 0, text, menuConfig.HeaderHue)

        display.WriteFill((0, (display.Size.Rows - font.Height) \ 2), (display.Size.Columns, font.Height), menuConfig.HiliteHue)
        Dim y = (display.Size.Rows - font.Height) \ 2 - menuItemIndex * font.Height
        For Each index In Enumerable.Range(0, menuItems.Length)
            text = menuItems(index)
            font.WriteCenterText(display, y, text, If(index = menuItemIndex, menuConfig.BackgroundHue, menuConfig.HiliteHue))
            y += font.Height
        Next

        display.WriteFill((0, display.Size.Rows - font.Height), (display.Size.Columns, font.Height), menuConfig.FooterHue)
        font.WriteCenterText(display, display.Size.Rows - font.Height, menuConfig.FooterText, menuConfig.BackgroundHue)
        Return state
    End Function

    Protected MustOverride Function HandleMenuItem(menuItem As String) As TState
    Protected MustOverride Function HandleGoBack() As TState?
End Class

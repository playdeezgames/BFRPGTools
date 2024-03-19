Public MustInherit Class BaseMenuState(Of TState As Structure, TPixel As Structure, TCommand, TSfx, TModel, TAssets)
    Inherits BaseGameState(Of TState, TPixel, TCommand, TSfx, TModel, TAssets)
    Private ReadOnly menuItems As String()
    Private ReadOnly title As String
    Private ReadOnly state As TState
    Private menuItemIndex As Integer = 0
    Private ReadOnly backgroundHue As TPixel
    Private ReadOnly headerHue As TPixel
    Private ReadOnly hiliteHue As TPixel
    Private ReadOnly footerHue As TPixel
    Private ReadOnly getFont As Func(Of TAssets, Font)
    Private ReadOnly footerText As String
    Private ReadOnly nextItemCommand As Func(Of TCommand, Boolean)
    Private ReadOnly previousItemCommand As Func(Of TCommand, Boolean)
    Private ReadOnly chooseCommand As Func(Of TCommand, Boolean)
    Private ReadOnly cancelCommand As Func(Of TCommand, Boolean)
    Private ReadOnly config As MenuStateConfig(Of TPixel, TCommand, TAssets)
    Protected Sub SetMenuItemIndex(index As Integer)
        menuItemIndex = Math.Clamp(index, 0, menuItems.Length - 1)
    End Sub

    Sub New(
           title As String,
           menuItems As String(),
           state As TState,
           config As MenuStateConfig(Of TPixel, TCommand, TAssets),
           backgroundHue As TPixel,
           headerHue As TPixel,
           hiliteHue As TPixel,
           footerHue As TPixel,
           getFont As Func(Of TAssets, Font),
           footerText As String,
           nextItemCommand As Func(Of TCommand, Boolean),
           previousItemCommand As Func(Of TCommand, Boolean),
           chooseCommand As Func(Of TCommand, Boolean),
           cancelCommand As Func(Of TCommand, Boolean))
        Me.menuItems = menuItems
        Me.title = title
        Me.state = state
        Me.config = config
        Me.backgroundHue = backgroundHue
        Me.headerHue = headerHue
        Me.hiliteHue = hiliteHue
        Me.footerHue = footerHue
        Me.getFont = getFont
        Me.footerText = footerText
        Me.nextItemCommand = nextItemCommand
        Me.previousItemCommand = previousItemCommand
        Me.chooseCommand = chooseCommand
        Me.cancelCommand = cancelCommand
    End Sub

    Public Overrides Function Update(context As IUIContext(Of TPixel, TCommand, TSfx, TModel, TAssets), elapsedTime As TimeSpan) As TState
        While context.Command.HasCommand
            Dim command = context.Command.ReadCommand()
            If nextItemCommand(command) Then
                menuItemIndex = (menuItemIndex + 1) Mod menuItems.Length
            ElseIf previousItemCommand(command) Then
                menuItemIndex = (menuItemIndex + menuItems.Length - 1) Mod menuItems.Length
            ElseIf chooseCommand(command) Then
                Return HandleMenuItem(menuItems(menuItemIndex))
            ElseIf cancelCommand(command) Then
                Dim backState = HandleGoBack()
                If backState.HasValue Then
                    Return backState.Value
                End If
            End If
        End While
        Dim display = context.Display
        Dim font = getFont(context.Assets)
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
        font.WriteCenterText(display, display.Size.Rows - font.Height, footerText, backgroundHue)
        Return state
    End Function

    Protected MustOverride Function HandleMenuItem(menuItem As String) As TState
    Protected MustOverride Function HandleGoBack() As TState?
End Class

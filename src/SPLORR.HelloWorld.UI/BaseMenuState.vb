﻿Public MustInherit Class BaseMenuState
    Inherits BaseGameState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Private ReadOnly menuItems As String()
    Private ReadOnly title As String
    Private ReadOnly state As GameState
    Private menuItemIndex As Integer = 0
    Protected Sub SetMenuItemIndex(index As Integer)
        menuItemIndex = Math.Clamp(index, 0, menuItems.Length - 1)
    End Sub

    Sub New(title As String, menuItems As String(), state As GameState)
        Me.menuItems = menuItems
        Me.title = title
        Me.state = state
    End Sub

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
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
        display.WriteAll(Hue.Black)
        Dim text = title
        font.WriteCenterText(display, 0, text, Hue.Orange)

        display.WriteFill((0, (display.Size.Rows - font.Height) \ 2), (display.Size.Columns, font.Height), Hue.LightBlue)
        Dim y = (display.Size.Rows - font.Height) \ 2 - menuItemIndex * font.Height
        For Each index In Enumerable.Range(0, menuItems.Length)
            text = menuItems(index)
            font.WriteCenterText(display, y, text, If(index = menuItemIndex, Hue.Black, Hue.LightBlue))
            y += font.Height
        Next

        display.WriteFill((0, display.Size.Rows - font.Height), (display.Size.Columns, font.Height), Hue.LightGray)
        font.WriteCenterText(display, display.Size.Rows - font.Height, "Up/Down/Select | A/Start/Space | B/Esc", Hue.Black)
        Return state
    End Function

    Protected MustOverride Function HandleMenuItem(menuItem As String) As GameState
    Protected MustOverride Function HandleGoBack() As GameState?
End Class

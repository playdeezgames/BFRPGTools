﻿Friend Class GameMenuState
    Inherits BaseMenuState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Const AbandonGameText = "Abandon Game"

    Public Sub New(config As MenuStateConfig(Of Hue, Command, HWAssets))
        MyBase.New("Game Menu", {AbandonGameText}, GameState.GameMenu, config, Hue.Black, Hue.Orange, Hue.LightBlue, Hue.DarkGray, Function(a) a.Font,
            "Up/Down/Select | A/Start/Space | B/Esc",
            Function(cmd) cmd = Command.Down OrElse cmd = Command.Select,
            Function(cmd) cmd = Command.Up,
            Function(cmd) cmd = Command.A OrElse cmd = Command.Start,
            Function(cmd) cmd = Command.B)
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        Select Case menuItem
            Case AbandonGameText
                Return GameState.ConfirmAbandon
        End Select
        Return GameState.GameMenu
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        Return GameState.Neutral
    End Function
End Class

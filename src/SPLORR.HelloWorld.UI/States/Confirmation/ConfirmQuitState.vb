﻿Public Class ConfirmQuitState
    Inherits BaseMenuState(Of GameState, Hue)
    Const NoText = "No"
    Const YesText = "Yes"

    Public Sub New()
        MyBase.New("Are you sure you want to quit?", {NoText, YesText}, GameState.ConfirmQuit, Hue.Black, Hue.Orange, Hue.LightBlue, Hue.DarkGray)
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        If menuItem = YesText Then
            Return GameState.Quit
        End If
        Return GameState.MainMenu
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        Return GameState.MainMenu
    End Function
End Class

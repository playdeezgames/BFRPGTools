﻿Public Class ConfirmQuitState
    Inherits BaseMenuState
    Const NoText = "No"
    Const YesText = "Yes"

    Public Sub New()
        MyBase.New("Are you sure you want to quit?", {NoText, YesText}, GameState.ConfirmQuit, GameState.MainMenu)
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        If menuItem = YesText Then
            Return GameState.Quit
        End If
        Return GameState.MainMenu
    End Function
End Class

﻿Friend Class GameMenuState
    Inherits BaseMenuState
    Const AbandonGameText = "Abandon Game"

    Public Sub New()
        MyBase.New("Game Menu", {AbandonGameText}, GameState.GameMenu)
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

﻿Friend Class ConfirmAbandonState
    Inherits BaseMenuState
    Const NoText = "No"
    Const YesText = "Yes"
    Const PromptText = "Are you sure you want to abandon the game?"
    Private model As HWModel = Nothing

    Public Sub New()
        MyBase.New(PromptText, {NoText, YesText}, GameState.ConfirmAbandon)
    End Sub

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        If model Is Nothing Then
            model = context.Model
        End If
        Return MyBase.Update(context, elapsedTime)
    End Function

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        If menuItem = YesText Then
            model.AbandonWorld()
            Return GameState.MainMenu
        End If
        Return GameState.GameMenu
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        Return GameState.GameMenu
    End Function
End Class
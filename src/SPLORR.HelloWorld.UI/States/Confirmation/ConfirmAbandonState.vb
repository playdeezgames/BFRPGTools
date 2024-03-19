Friend Class ConfirmAbandonState
    Inherits BaseMenuState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Const NoText = "No"
    Const YesText = "Yes"
    Const PromptText = "Are you sure you want to abandon the game?"
    Private model As HWModel = Nothing

    Public Sub New(config As MenuStateConfig(Of Hue, Command, HWAssets))
        MyBase.New(
            PromptText,
            {NoText, YesText},
            GameState.ConfirmAbandon,
            config,
            Hue.Black,
            Hue.Orange,
            Hue.LightBlue,
            Hue.DarkGray,
            Function(a) a.Font,
            "Up/Down/Select | A/Start/Space | B/Esc",
            Function(cmd) cmd = Command.Down OrElse cmd = Command.Select,
            Function(cmd) cmd = Command.Up,
            Function(cmd) cmd = Command.A OrElse cmd = Command.Start,
            Function(cmd) cmd = Command.B)
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

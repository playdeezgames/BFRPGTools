Friend Class GameMenuState
    Inherits BaseMenuState(Of GameState, Hue, Sfx, HWModel, HWAssets)
    Const AbandonGameText = "Abandon Game"

    Public Sub New()
        MyBase.New("Game Menu", {AbandonGameText}, GameState.GameMenu, Hue.Black, Hue.Orange, Hue.LightBlue, Hue.DarkGray, Function(a) a.Font,
            "Up/Down/Select | A/Start/Space | B/Esc")
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

Friend Class MainMenuState
    Inherits BaseMenuState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Const EmbarkText = "Embark!"
    Const OptionsText = "Options..."
    Const AboutText = "About..."
    Const QuitText = "Quit"
    Sub New(config As MenuStateConfig(Of Hue, Command, HWAssets))
        MyBase.New("Main Menu",
                   {
                        EmbarkText,
                        OptionsText,
                        AboutText,
                        QuitText
                   },
                   GameState.MainMenu, config, Hue.Black, Hue.Orange, Hue.LightBlue, Hue.DarkGray, Function(a) a.Font,
                   "Up/Down/Select | A/Start/Space | B/Esc",
            Function(cmd) cmd = Command.Down OrElse cmd = Command.Select,
            Function(cmd) cmd = Command.Up,
            Function(cmd) cmd = Command.A OrElse cmd = Command.Start,
            Function(cmd) cmd = Command.B)
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        Select Case menuItem
            Case EmbarkText
                Return GameState.Embark
            Case OptionsText
                Return GameState.Options
            Case AboutText
                Return GameState.About
            Case QuitText
                Return GameState.ConfirmQuit
        End Select
        Return GameState.MainMenu
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        Return GameState.ConfirmQuit
    End Function
End Class

Friend Class MainMenuState
    Inherits BaseMenuState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Const EmbarkText = "Embark!"
    Const OptionsText = "Options..."
    Const AboutText = "About..."
    Const QuitText = "Quit"
    Sub New(menuConfig As MenuStateConfig(Of Hue, Command, HWAssets))
        MyBase.New("Main Menu",
                   {
                        EmbarkText,
                        OptionsText,
                        AboutText,
                        QuitText
                   },
                   GameState.MainMenu, menuConfig)
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

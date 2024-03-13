Friend Class MainMenuState
    Inherits BaseMenuState
    Const EmbarkText = "Embark!"
    Const OptionsText = "Options..."
    Const AboutText = "About..."
    Const QuitText = "Quit"
    Sub New()
        MyBase.New("Main Menu",
                   {
                        EmbarkText,
                        OptionsText,
                        AboutText,
                        QuitText
                   },
                   GameState.MainMenu)
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
End Class

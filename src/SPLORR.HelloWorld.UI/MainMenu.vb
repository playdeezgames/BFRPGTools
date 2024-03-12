Friend Class MainMenu
    Inherits BaseMenuState
    Const EmbarkText = "Embark!"
    Const ScumLoadText = "Scum Load"
    Const LoadText = "Load..."
    Const OptionsText = "Options..."
    Const AboutText = "About..."
    Const QuitText = "Quit"
    Sub New()
        MyBase.New("Main Menu",
                   {
                        EmbarkText,
                        ScumLoadText,
                        LoadText,
                        OptionsText,
                        AboutText,
                        QuitText
                   },
                   GameState.MainMenu)
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        Select Case menuItem
            Case QuitText
                Return GameState.Quit
        End Select
        Return GameState.MainMenu
    End Function
End Class

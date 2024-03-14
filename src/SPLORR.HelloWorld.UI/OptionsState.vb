Friend Class OptionsState
    Inherits BaseMenuState
    Const OptionsText = "Options"
    Const ToggleFullScreenText = "Toggle Full Screen"
    Const WindowSizeText = "Window Size..."
    Const SfxVolumeText = "Sfx Volume..."
    ReadOnly config As IHostConfig

    Public Sub New(config As IHostConfig)
        MyBase.New(OptionsText, {ToggleFullScreenText, WindowSizeText, SfxVolumeText}, GameState.Options)
        Me.config = config
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        Select Case menuItem
            Case ToggleFullScreenText
                config.IsFullScreen = Not config.IsFullScreen
            Case WindowSizeText
                Return GameState.ChangeWindowSize
        End Select
        Return GameState.Options
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        Return GameState.MainMenu
    End Function
End Class

Friend Class OptionsState
    Inherits BaseMenuState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Const OptionsText = "Options"
    Const ToggleFullScreenText = "Toggle Full Screen"
    Const WindowSizeText = "Window Size..."
    Const SfxVolumeText = "Sfx Volume..."
    ReadOnly hostConfig As IHostConfig

    Public Sub New(hostConfig As IHostConfig, menuConfig As MenuStateConfig(Of Hue, Command, HWAssets))
        MyBase.New(OptionsText, {ToggleFullScreenText, WindowSizeText, SfxVolumeText}, GameState.Options, menuConfig)
        Me.hostConfig = hostConfig
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        Select Case menuItem
            Case ToggleFullScreenText
                hostConfig.IsFullScreen = Not hostConfig.IsFullScreen
            Case WindowSizeText
                Return GameState.ChangeWindowSize
            Case SfxVolumeText
                Return GameState.ChangeSfxVolume
        End Select
        Return GameState.Options
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        Return GameState.MainMenu
    End Function
End Class

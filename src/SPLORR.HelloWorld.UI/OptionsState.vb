Imports System.Net.Http

Friend Class OptionsState
    Inherits BaseMenuState
    Const OptionsText = "Options"
    Const ToggleFullScreenText = "Toggle Full Screen"
    Const WindowSizeText = "Window Size..."
    Const SfxVolumeText = "Sfx Volume..."

    Public Sub New()
        MyBase.New(OptionsText, {ToggleFullScreenText, WindowSizeText, SfxVolumeText}, GameState.Options, GameState.MainMenu)
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String, context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets)) As GameState
        Select Case menuItem
            Case ToggleFullScreenText
                context.Config.IsFullScreen = Not context.Config.IsFullScreen
        End Select
        Return GameState.Options
    End Function
End Class

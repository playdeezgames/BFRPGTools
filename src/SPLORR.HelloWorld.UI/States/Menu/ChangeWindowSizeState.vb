Friend Class ChangeWindowSizeState
    Inherits BaseMenuState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)
    Private Shared Function ScaleToText(config As IHostConfig, scale As Integer) As String
        Return $"{config.ViewWidth * scale} x {config.ViewHeight * scale}"
    End Function
    Private ReadOnly table As IReadOnlyDictionary(Of String, Integer)
    Private ReadOnly hostConfig As IHostConfig
    Private needsInitialization As Boolean 'TODO: need a way to reset this on exit!

    Public Sub New(hostConfig As IHostConfig, scales As Integer(), menuconfig As MenuStateConfig(Of Hue, Command, HWAssets))
        MyBase.New("Window Size", scales.Select(Function(x) ScaleToText(hostConfig, x)).ToArray, GameState.ChangeWindowSize, menuconfig)
        table = scales.ToDictionary(Function(x) ScaleToText(hostConfig, x), Function(x) x)
        Me.hostConfig = hostConfig
        needsInitialization = True
    End Sub

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        If needsInitialization Then
            Dim index = 0
            For Each entry In table
                If entry.Value >= hostConfig.ViewScale Then
                    Exit For
                End If
                index += 1
            Next
            SetMenuItemIndex(index)
            needsInitialization = False
        End If
        Return MyBase.Update(context, elapsedTime)
    End Function

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        hostConfig.ViewScale = table(menuItem)
        Return GameState.ChangeWindowSize
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        needsInitialization = True
        Return GameState.Options
    End Function
End Class

Friend Class ChangeWindowSizeState
    Inherits BaseMenuState
    Private Shared Function ScaleToText(config As IHostConfig, scale As Integer) As String
        Return $"{config.ViewWidth * scale} x {config.ViewHeight * scale}"
    End Function
    Private ReadOnly table As IReadOnlyDictionary(Of String, Integer)

    Private ReadOnly config As IHostConfig

    Public Sub New(config As IHostConfig, scales As Integer())
        MyBase.New("Window Size", scales.Select(Function(x) ScaleToText(config, x)).ToArray, GameState.ChangeWindowSize, GameState.Options)
        table = scales.ToDictionary(Function(x) ScaleToText(config, x), Function(x) x)
        Me.config = config
    End Sub

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        config.ViewScale = table(menuItem)
        Return GameState.ChangeWindowSize
    End Function
End Class

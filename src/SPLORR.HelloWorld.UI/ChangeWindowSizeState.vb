﻿Friend Class ChangeWindowSizeState
    Inherits BaseMenuState
    Private Shared Function ScaleToText(config As IHostConfig, scale As Integer) As String
        Return $"{config.ViewWidth * scale} x {config.ViewHeight * scale}"
    End Function
    Private ReadOnly table As IReadOnlyDictionary(Of String, Integer)
    Private ReadOnly config As IHostConfig
    Private needsInitialization As Boolean 'TODO: need a way to reset this on exit!

    Public Sub New(config As IHostConfig, scales As Integer())
        MyBase.New("Window Size", scales.Select(Function(x) ScaleToText(config, x)).ToArray, GameState.ChangeWindowSize)
        table = scales.ToDictionary(Function(x) ScaleToText(config, x), Function(x) x)
        Me.config = config
        needsInitialization = True
    End Sub

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        If needsInitialization Then
            Dim index = 0
            For Each entry In table
                If entry.Value > config.ViewScale Then
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
        config.ViewScale = table(menuItem)
        Return GameState.ChangeWindowSize
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        Return GameState.Options
    End Function
End Class
﻿Friend Class ChangeSfxVolumeState
    Inherits BaseMenuState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)

    Private ReadOnly config As IHostConfig
    Private sfxTest As Action(Of Sfx)
    Private needsInitialization As Boolean = True
    Private Shared ReadOnly table As IReadOnlyDictionary(Of String, Single) =
        New Dictionary(Of String, Single) From
        {
            {"0%", 0.0F},
            {"10%", 0.1F},
            {"20%", 0.2F},
            {"30%", 0.3F},
            {"40%", 0.4F},
            {"50%", 0.5F},
            {"60%", 0.6F},
            {"70%", 0.7F},
            {"80%", 0.8F},
            {"90%", 0.9F},
            {"100%", 1.0F}
        }

    Public Sub New(config As IHostConfig, menuConfig As MenuStateConfig(Of Hue, Command, HWAssets))
        MyBase.New("Sfx Volume", table.Keys.ToArray, GameState.ChangeSfxVolume, menuConfig, Hue.Black, Hue.Orange, Hue.LightBlue, Hue.DarkGray, Function(a) a.Font,
            "Up/Down/Select | A/Start/Space | B/Esc",
            Function(cmd) cmd = Command.Down OrElse cmd = Command.Select,
            Function(cmd) cmd = Command.Up,
            Function(cmd) cmd = Command.A OrElse cmd = Command.Start,
            Function(cmd) cmd = Command.B)
        Me.config = config
    End Sub

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        If sfxTest Is Nothing Then
            sfxTest = AddressOf context.Play
        End If
        If needsInitialization Then
            SetMenuItemIndex(CInt(config.Volume * 10.0))
            needsInitialization = False
        End If
        Return MyBase.Update(context, elapsedTime)
    End Function

    Protected Overrides Function HandleMenuItem(menuItem As String) As GameState
        config.Volume = table(menuItem)
        If sfxTest IsNot Nothing Then
            sfxTest(Sfx.Ok)
        End If
        Return GameState.ChangeSfxVolume
    End Function

    Protected Overrides Function HandleGoBack() As GameState?
        needsInitialization = True
        Return GameState.Options
    End Function
End Class

Imports System.Resources

Public Class GameController
    Inherits BaseGameController(Of GameState, Hue, Command, Sfx, Model.HWModel, HWAssets)

    Public Sub New(config As IHostConfig, menuConfig As MenuStateConfig(Of Hue, Command, HWAssets))
        MyBase.New(
            config,
            "SPLORR!!",
            Function(x) x = GameState.Quit,
            New Dictionary(Of GameState, BaseGameState(Of GameState, Hue, Command, Sfx, Model.HWModel, HWAssets)) From
            {
                {GameState.Title, New TitleState()},
                {GameState.MainMenu, New MainMenuState(menuConfig)},
                {GameState.ConfirmQuit, New ConfirmQuitState(menuConfig)},
                {GameState.About, New AboutState()},
                {GameState.Options, New OptionsState(config, menuConfig)},
                {GameState.Embark, New EmbarkState()},
                {GameState.ChangeWindowSize, New ChangeWindowSizeState(config, {2, 4, 6, 8, 10, 12}, menuConfig)},
                {GameState.ChangeSfxVolume, New ChangeSfxVolumeState(config, menuConfig)},
                {GameState.Neutral, New NeutralState()},
                {GameState.InPlay, New InPlayState()},
                {GameState.GameMenu, New GameMenuState(menuConfig)},
                {GameState.ConfirmAbandon, New ConfirmAbandonState(menuConfig)}
            },
            GameState.Title,
            New Model.HWModel(),
            New HWAssets())
    End Sub
End Class

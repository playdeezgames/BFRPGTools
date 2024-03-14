Imports System.Resources

Public Class GameController
    Inherits BaseGameController(Of GameState, Hue, Command, Sfx, Model.HWModel, HWAssets)

    Public Sub New(config As IHostConfig)
        MyBase.New(
            config,
            "SPLORR!!",
            Function(x) x = GameState.Quit,
            New Dictionary(Of GameState, BaseGameState(Of GameState, Hue, Command, Sfx, Model.HWModel, HWAssets)) From
            {
                {GameState.Title, New TitleState()},
                {GameState.MainMenu, New MainMenuState()},
                {GameState.ConfirmQuit, New ConfirmQuitState()},
                {GameState.About, New AboutState()},
                {GameState.Options, New OptionsState(config)},
                {GameState.Embark, New EmbarkState()},
                {GameState.ChangeWindowSize, New ChangeWindowSizeState(config, {2, 4, 6, 8, 10, 12})},
                {GameState.ChangeSfxVolume, New ChangeSfxVolumeState(config)},
                {GameState.Neutral, New NeutralState()},
                {GameState.InPlay, New InPlayState()},
                {GameState.GameMenu, New GameMenuState()},
                {GameState.ConfirmAbandon, New ConfirmAbandonState()}
            },
            GameState.Title,
            New Model.HWModel(),
            New HWAssets())
    End Sub
End Class

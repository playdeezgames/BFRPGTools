Public Class GameController
    Inherits BaseGameController(Of GameState, Hue, Command, Sfx, Model.HWModel, HWAssets)

    Public Sub New(config As IHostConfig)
        MyBase.New(
            config,
            "SPLORR!!",
            Function(x) x = GameState.Quit,
            New Dictionary(Of GameState, Func(Of IUIContext(Of Hue, Command, Sfx, Model.HWModel, HWAssets), TimeSpan, GameState)) From
            {
                {GameState.Title, AddressOf TitleState.Update}
            },
            GameState.Title,
            New Model.HWModel(),
            New HWAssets())
    End Sub
End Class

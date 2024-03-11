Public Class GameController
    Inherits BaseGameController(Of GameState, Hue, Command, Sfx, Model)

    Public Sub New(config As IHostConfig)
        MyBase.New(
            config,
            "SPLORR!!",
            Function(x) x = GameState.Quit,
            New Dictionary(Of GameState, Func(Of IUIContext(Of Hue, Command, Sfx, Model), GameState)) From
            {
                {GameState.Title, AddressOf TitleState.Update}
            },
            GameState.Title,
            New Model(config))
    End Sub
End Class

Public Class GameController
    Inherits BaseGameController(Of GameState, Hue, Command, Sfx, Model.Model, Assets)

    Public Sub New(config As IHostConfig)
        MyBase.New(
            config,
            "SPLORR!!",
            Function(x) x = GameState.Quit,
            New Dictionary(Of GameState, Func(Of IUIContext(Of Hue, Command, Sfx, Model.Model, Assets), TimeSpan, GameState)) From
            {
                {GameState.Title, AddressOf TitleState.Update}
            },
            GameState.Title,
            New Model.Model(),
            New Assets())
    End Sub
End Class

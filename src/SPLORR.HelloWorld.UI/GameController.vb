Public Class GameController
    Inherits BaseGameController(Of GameState, Hue, Command, Sfx)

    Public Sub New(config As IHostConfig)
        MyBase.New(
            config,
            "SPLORR!!",
            Function(x) x = GameState.Quit,
            New Dictionary(Of GameState, Func(Of IUIContext(Of Hue, Command, Sfx), GameState)) From
            {
                {GameState.Title, AddressOf TitleState.Update}
            },
            GameState.Title)
    End Sub
End Class

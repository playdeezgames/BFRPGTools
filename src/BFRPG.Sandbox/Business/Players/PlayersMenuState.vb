Friend Class PlayersMenuState
    Inherits BaseState
    Implements IState

    Public Sub New(data As DataContext, ui As IUIContext, endState As IState)
        MyBase.New(data, ui, endState)
    End Sub

    Public Overrides Function Run() As IState Implements IState.Run
        ui.Clear()
        Dim menu = New List(Of String) From
                {
                    Choices.GoBack,
                    Choices.NewPlayer
                }
        Dim table =
                Players.All(data.Connection).
                ToDictionary(Function(x) x.UniqueName, Function(x) x.PlayerId)
        menu.AddRange(table.Keys)
        Dim answer = ui.Choose((Mood.Prompt, Prompts.PlayersMenu), menu.ToArray)
        Select Case answer
            Case Choices.GoBack
                Return endState
            Case Choices.NewPlayer
                NewPlayer.Run(data, ui)
            Case Else
                Dim playerId = table(answer)
                PlayerMenu.Run(data, ui, playerId)
        End Select
        Return Me
    End Function
End Class

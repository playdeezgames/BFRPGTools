Friend Module PlayersMenu
    Friend Sub Run(data As DataContext, ui As IUIContext)
        Do
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
                    Exit Do
                Case Choices.NewPlayer
                    NewPlayer.Run(data, ui)
                Case Else
                    Dim playerId = table(answer)
                    PlayerMenu.Run(data, ui, playerId)
            End Select
        Loop
    End Sub
End Module

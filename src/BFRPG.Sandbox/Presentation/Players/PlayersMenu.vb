Friend Module PlayersMenu
    Friend Sub Run(context As DataContext)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.PlayersMenu}
            prompt.AddChoice(Choices.GoBack)
            prompt.AddChoice(Choices.NewPlayer)
            Dim table As Dictionary(Of String, Integer) =
            Players.All(context.Connection).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.PlayerId)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case Choices.GoBack
                    Exit Do
                Case Choices.NewPlayer
                    NewPlayer.Run(context)
                Case Else
                    Dim playerId = table(answer)
                    PlayerMenu.Run(context, playerId)
            End Select
        Loop
    End Sub
End Module

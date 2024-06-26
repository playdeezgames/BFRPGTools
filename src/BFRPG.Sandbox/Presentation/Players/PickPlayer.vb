Friend Module PickPlayer
    Friend Sub Run(context As DataContext)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.WhichPlayer}
        prompt.AddChoice(Choices.GoBack)
        Dim table As Dictionary(Of String, Integer) = Players.All(context.Connection)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case Choices.GoBack
                Return
            Case Else
                Dim playerId = table(answer)
                PlayerMenu.Run(context, playerId)
        End Select
    End Sub

End Module

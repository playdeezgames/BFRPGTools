Friend Module PickPlayer

    Friend Sub Run(connection As MySqlConnection)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Player?[/]"}
        prompt.AddChoice(ChoiceGoBack)
        Dim table As Dictionary(Of String, Integer) = All(connection)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case ChoiceGoBack
                Return
            Case Else
                Dim playerId = table(answer)
                PlayerMenu.Run(connection, playerId)
        End Select
    End Sub

End Module

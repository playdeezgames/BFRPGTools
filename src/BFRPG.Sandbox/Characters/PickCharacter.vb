Friend Module PickCharacter
    Friend Sub Run(connection As MySqlConnection, playerId As Integer)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptWhichCharacter}
        prompt.AddChoice(ChoiceGoBack)
        Dim table As Dictionary(Of String, Integer) = Characters.AllForPlayer(connection, playerId)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case ChoiceGoBack
                Return
            Case Else
                Dim characterId = table(answer)
                CharacterMenu.Run(connection, characterId)
        End Select
    End Sub
End Module

Friend Module PickCharacter
    Friend Sub Run(context As DataContext, playerId As Integer)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptWhichCharacter}
        prompt.AddChoice(ChoiceGoBack)
        Dim table As Dictionary(Of String, Integer) = Characters.AllForPlayer(context.Connection, playerId)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case ChoiceGoBack
                Return
            Case Else
                Dim characterId = table(answer)
                CharacterMenu.Run(context, characterId)
        End Select
    End Sub
End Module

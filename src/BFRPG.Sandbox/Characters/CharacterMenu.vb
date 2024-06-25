Friend Module CharacterMenu
    Friend Sub Run(connection As MySqlConnection, characterId As Integer)
        Do
            AnsiConsole.Clear()
            Dim details = Characters.ReadDetails(connection, characterId)
            AnsiConsole.MarkupLine($"Character Id: {details.CharacterId}")
            AnsiConsole.MarkupLine($"Character Name: {details.CharacterName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptCharacterMenu}
            prompt.AddChoice(ChoiceGoBack)
            prompt.AddChoice(ChoiceDelete)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChoiceGoBack
                    Exit Do
                Case ChoiceDelete
                    If Confirm.Run(ConfirmDeleteCharacter) Then
                        Characters.Delete(connection, characterId)
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module

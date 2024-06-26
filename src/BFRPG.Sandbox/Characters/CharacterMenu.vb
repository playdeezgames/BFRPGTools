Friend Module CharacterMenu
    Friend Sub Run(context As DataContext, characterId As Integer)
        Do
            AnsiConsole.Clear()
            Dim details = Characters.ReadDetails(context.Connection, characterId)
            AnsiConsole.MarkupLine($"Player Name: {details.PlayerName}")
            AnsiConsole.MarkupLine($"Character Name: {details.CharacterName}")
            AnsiConsole.MarkupLine($"Race: {details.RaceName}")
            Dim abilityDetails = CharacterAbilities.ReadAllDetailsForCharacter(context.Connection, characterId)
            For Each abilityDetail In abilityDetails
                AnsiConsole.MarkupLine($"{abilityDetail.AbilityAbbreviation}: {abilityDetail.AbilityScore}")
            Next
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptCharacterMenu}
            prompt.AddChoice(ChoiceGoBack)
            prompt.AddChoice(ChoiceDelete)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChoiceGoBack
                    Exit Do
                Case ChoiceDelete
                    If Confirm.Run(ConfirmDeleteCharacter) Then
                        Characters.Delete(context.Connection, characterId)
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module

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
            Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.CharacterMenu}
            prompt.AddChoice(Choices.GoBack)
            prompt.AddChoice(Choices.Delete)
            Select Case AnsiConsole.Prompt(prompt)
                Case Choices.GoBack
                    Exit Do
                Case Choices.Delete
                    If Confirm.Run(Confirms.DeleteCharacter) Then
                        Characters.Delete(context.Connection, characterId)
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module

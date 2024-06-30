Friend Module CharacterMenu
    Friend Sub Run(context As DataContext, characterId As Integer)
        Do
            AnsiConsole.Clear()
            Dim details = Characters.ReadDetails(context.Connection, characterId)
            AnsiConsole.MarkupLine($"Player Name: {details.PlayerName}")
            AnsiConsole.MarkupLine($"Character Name: {details.UniqueName}")
            AnsiConsole.MarkupLine($"Race: {details.RaceName}")
            AnsiConsole.MarkupLine($"Class: {details.ClassName}")
            AnsiConsole.MarkupLine($"Level: {details.Level}")
            AnsiConsole.MarkupLine($"XP: {details.ExperiencePoints}")
            AnsiConsole.MarkupLine($"HP: {details.HitPoints}")
            Dim abilityDetails = CharacterAbilities.ReadAllDetailsForCharacter(context.Connection, characterId)
            For Each abilityDetail In abilityDetails
                AnsiConsole.MarkupLine($"{abilityDetail.AbilityAbbreviation}: {abilityDetail.AbilityScore} ({abilityDetail.Modifier})")
            Next
            Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.CharacterMenu}
            prompt.AddChoice(Choices.GoBack)
            prompt.AddChoice(Choices.Delete)
            prompt.AddChoice(Choices.Rename)
            prompt.AddChoice(Choices.Transfer)
            prompt.AddChoice(Choices.AddXP)
            Select Case AnsiConsole.Prompt(prompt)
                Case Choices.Transfer
                    If TransferCharacter.Run(context, characterId) Then
                        Exit Do
                    End If
                Case Choices.Rename
                    RenameCharacter.Run(context, characterId)
                Case Choices.GoBack
                    Exit Do
                Case Choices.Delete
                    If Confirm.Run(Confirms.DeleteCharacter) Then
                        Characters.Delete(context.Connection, characterId)
                        Exit Do
                    End If
                Case Choices.AddXP
                    AddExperiencePoints.Run(context, characterId)
            End Select
        Loop
    End Sub
End Module

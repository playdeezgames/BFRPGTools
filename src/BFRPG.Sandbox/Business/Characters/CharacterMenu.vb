Friend Module CharacterMenu
    Friend Sub Run(data As DataContext, ui As IUIContext, characterId As Integer)
        Do
            ui.Clear()
            Dim details = data.Characters.ReadDetails(characterId)
            ui.Write(
                (Mood.Info, $"Player Name: {details.PlayerName}"),
                (Mood.Info, $"Character Name: {details.UniqueName}"),
                (Mood.Info, $"Race: {details.RaceName}"),
                (Mood.Info, $"Class: {details.ClassName}"),
                (Mood.Info, $"Level: {details.Level}"),
                (Mood.Info, $"Description: {details.CharacterDescription}"),
                (Mood.Info, $"XP: {details.ExperiencePoints}"),
                (Mood.Info, $"HP: {details.HitPoints}"))
            Dim abilityDetails = data.Characters.Abilities(characterId).ReadAllDetailsForCharacter()
            For Each abilityDetail In abilityDetails
                ui.Write((Mood.Info, $"{abilityDetail.AbilityAbbreviation}: {abilityDetail.AbilityScore} ({abilityDetail.Modifier})"))
            Next

            Dim menu As New List(Of String) From {
                Choices.GoBack,
                Choices.Delete,
                Choices.Rename,
                Choices.Transfer,
                Choices.AddXP,
                Choices.CharacterSheet
            }
            Select Case ui.Choose((Mood.Prompt, Prompts.CharacterMenu), menu.ToArray)
                Case Choices.Transfer
                    If TransferCharacter.Run(data, ui, characterId) Then
                        Exit Do
                    End If
                Case Choices.Rename
                    RenameCharacter.Run(data, ui, characterId)
                Case Choices.GoBack
                    Exit Do
                Case Choices.Delete
                    If ui.Confirm((Mood.Danger, Confirms.DeleteCharacter)) Then
                        data.Characters.Delete(characterId)
                        Exit Do
                    End If
                Case Choices.AddXP
                    AddExperiencePoints.Run(data, ui, characterId)
                Case Choices.CharacterSheet
                    ExportCharacterSheet.Run(data, characterId)
            End Select
        Loop
    End Sub
End Module

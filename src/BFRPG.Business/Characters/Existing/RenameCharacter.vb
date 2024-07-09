﻿Friend Module RenameCharacter
    Friend Sub Run(context As IDataContext, ui As IUIContext, characterId As Integer)
        Dim characterName = ui.Ask((Mood.Prompt, Prompts.NewCharacterName), String.Empty).Trim
        If String.IsNullOrWhiteSpace(characterName) Then
            Return
        End If
        Dim playerId = context.Characters.ReadDetails(characterId).PlayerId
        If context.Characters.FindForPlayerAndName(playerId, characterName).HasValue Then
            ui.Message((Mood.Danger, Messages.DuplicateCharacterName))
            Return
        End If
        context.Characters.Rename(characterId, characterName)
    End Sub
End Module
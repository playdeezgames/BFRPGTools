﻿Friend Module RenameCharacter
    Friend Sub Run(context As DataContext, ui As IUIContext, characterId As Integer)
        Dim characterName = Trim(ui.Ask((Mood.Prompt, Prompts.NewCharacterName), String.Empty))
        If String.IsNullOrWhiteSpace(characterName) Then
            Return
        End If
        Dim playerId = Characters.ReadDetails(context.Connection, characterId).PlayerId
        If Characters.FindForPlayerAndName(context.Connection, playerId, characterName).HasValue Then
            OkPrompt.Run(Messages.DuplicateCharacterName)
            Return
        End If
        Characters.Rename(context.Connection, characterId, characterName)
    End Sub
End Module

﻿Friend Module AddExperiencePoints
    Friend Sub Run(context As IDataContext, ui As IUIContext, characterId As Integer)
        Dim xp As Integer = ui.Ask((Mood.Prompt, "How many XP?"), 0)
        If xp < 0 Then
            Return
        End If
        context.Characters.AddXP(characterId, xp)
    End Sub
End Module
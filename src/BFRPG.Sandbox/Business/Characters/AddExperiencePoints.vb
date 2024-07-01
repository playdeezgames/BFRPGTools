Friend Module AddExperiencePoints
    Friend Sub Run(context As DataContext, ui As IUIContext, characterId As Integer)
        Dim xp As Integer = ui.Ask((Mood.Prompt, "How many XP?"), 0)
        If xp < 0 Then
            Return
        End If
        Characters.AddXP(context.Connection, characterId, xp)
    End Sub
End Module

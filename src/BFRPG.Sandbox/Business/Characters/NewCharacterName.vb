Friend Module NewCharacterName
    Friend Sub Run(context As DataContext, ui As IUIContext, playerId As Integer)
        Dim characterName = Trim(ui.Ask((Mood.Prompt, Prompts.NewCharacterName), String.Empty))
        If Not String.IsNullOrWhiteSpace(characterName) Then
            If Characters.FindForPlayerAndName(context.Connection, playerId, characterName).HasValue Then
                ui.Message((Mood.Danger, Messages.DuplicateCharacterName))
                Return
            End If
            NewCharacterRaceClass.Run(context,ui, playerId, characterName)
        End If
    End Sub
End Module

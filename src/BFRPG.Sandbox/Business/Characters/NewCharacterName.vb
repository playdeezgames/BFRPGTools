Friend Module NewCharacterName
    Friend Sub Run(context As DataContext, ui As IUIContext, playerId As Integer)
        Dim characterName = Trim(AnsiConsole.Ask(Prompts.NewCharacterName, String.Empty))
        If Not String.IsNullOrWhiteSpace(characterName) Then
            If Characters.FindForPlayerAndName(context.Connection, playerId, characterName).HasValue Then
                OkPrompt.Run(Messages.DuplicateCharacterName)
                Return
            End If
            NewCharacterRaceClass.Run(context,ui, playerId, characterName)
        End If
    End Sub
End Module

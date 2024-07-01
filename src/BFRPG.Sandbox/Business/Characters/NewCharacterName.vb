Friend Module NewCharacterName
    Friend Sub Run(context As DataContext, playerId As Integer)
        Dim characterName = Trim(AnsiConsole.Ask(Prompts.NewCharacterName, String.Empty))
        If Not String.IsNullOrWhiteSpace(characterName) Then
            If Characters.FindForPlayerAndName(context.Connection, playerId, characterName).HasValue Then
                OkPrompt.Run(Messages.DuplicateCharacterName)
                Return
            End If
            NewCharacterRaceClass.Run(context, playerId, characterName)
        End If
    End Sub
End Module

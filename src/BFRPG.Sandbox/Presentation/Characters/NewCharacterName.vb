Friend Module NewCharacterName
    Friend Sub Run(context As DataContext, playerId As Integer)
        Dim characterName = Trim(AnsiConsole.Ask(PromptNewCharacterName, String.Empty))
        If Not String.IsNullOrWhiteSpace(characterName) Then
            If Characters.NameExists(context.Connection, playerId, characterName) Then
                OkPrompt.Run(Messages.DuplicateCharacterName)
                Return
            End If
            NewCharacterRace.Run(context, playerId, characterName)
        End If
    End Sub
End Module

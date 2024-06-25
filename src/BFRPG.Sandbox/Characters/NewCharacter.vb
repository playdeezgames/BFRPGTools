Friend Module NewCharacter
    Friend Sub Run(connection As MySqlConnection, playerId As Integer)
        Dim characterName = Trim(AnsiConsole.Ask(PromptNewCharacterName, String.Empty))
        If Not String.IsNullOrWhiteSpace(characterName) Then
            Dim characterId = Characters.Create(connection, playerId, characterName)
            If Not characterId.HasValue Then
                OkPrompt.Run(MessageDuplicateCharacterName)
                Return
            End If
            CharacterMenu.Run(connection, characterId.Value)
        End If
    End Sub
End Module

Friend Module NewCharacterName
    Friend Sub Run(connection As MySqlConnection, playerId As Integer)
        Dim characterName = Trim(AnsiConsole.Ask(PromptNewCharacterName, String.Empty))
        If Not String.IsNullOrWhiteSpace(characterName) Then
            If Characters.NameExists(connection, playerId, characterName) Then
                OkPrompt.Run(MessageDuplicateCharacterName)
                Return
            End If
            NewCharacterRace.Run(connection, playerId, characterName)
        End If
    End Sub
End Module

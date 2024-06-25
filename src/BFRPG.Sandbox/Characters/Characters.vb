Friend Module Characters
    Friend Sub Delete(connection As MySqlConnection, characterId As Integer)
        CharacterAbilities.DeleteForCharacter(connection, characterId)
        Using command = connection.CreateCommand
            command.CommandText = $"DELETE FROM `{TableCharacters}` WHERE `{ColumnCharacterId}`=@{ColumnCharacterId};"
            command.Parameters.AddWithValue(ColumnCharacterId, characterId)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Friend Function Create(connection As MySqlConnection, playerId As Integer, characterName As String) As Integer?
        Dim characterId As Integer = 0
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT IGNORE INTO {TableCharacters}
(
    {ColumnPlayerId}, 
    {ColumnCharacterName}
) 
VALUES
(
    @{ColumnPlayerId}, 
    @{ColumnCharacterName}
) 
RETURNING 
    {ColumnCharacterId};"
            command.Parameters.AddWithValue(ColumnPlayerId, playerId)
            command.Parameters.AddWithValue(ColumnCharacterName, Trim(characterName))
            Dim result = command.ExecuteScalar()
            If result Is Nothing Then
                Return Nothing
            End If
            characterId = CInt(result)
        End Using
        For Each abilityId In Abilities.AllIds(connection)
            CharacterAbilities.Write(connection, characterId, abilityId, RNG.RollDice(3, 6))
        Next
        Return characterId
    End Function

    Friend Function ReadDetails(connection As MySqlConnection, characterId As Integer) As CharacterDetails
        Using command = connection.CreateCommand()
            command.CommandText = $"
SELECT 
    `{ColumnCharacterName}` 
FROM 
    `{TableCharacters}` 
WHERE 
    `{ColumnCharacterId}`=@{ColumnCharacterId};"
            command.Parameters.AddWithValue(ColumnCharacterId, characterId)
            Using reader = command.ExecuteReader
                reader.Read()
                Return New CharacterDetails(characterId, reader.GetString(0))
            End Using
        End Using
    End Function
End Module

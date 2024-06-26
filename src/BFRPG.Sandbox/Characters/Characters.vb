Friend Module Characters
    Friend Sub Delete(connection As MySqlConnection, characterId As Integer)
        CharacterAbilities.DeleteForCharacter(connection, characterId)
        Using command = connection.CreateCommand
            command.CommandText = $"DELETE FROM `{TableCharacters}` WHERE `{ColumnCharacterId}`=@{ColumnCharacterId};"
            command.Parameters.AddWithValue(ColumnCharacterId, characterId)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Friend Function Create(
                          connection As MySqlConnection,
                          playerId As Integer,
                          characterName As String,
                          raceId As Integer) As Integer?
        Dim characterId As Integer = 0
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT IGNORE INTO {TableCharacters}
(
    {ColumnPlayerId}, 
    {ColumnCharacterName},
    {ColumnRaceId}
) 
VALUES
(
    @{ColumnPlayerId}, 
    @{ColumnCharacterName},
    @{ColumnRaceId}
) 
RETURNING 
    {ColumnCharacterId};"
            command.Parameters.AddWithValue(ColumnPlayerId, playerId)
            command.Parameters.AddWithValue(ColumnRaceId, raceId)
            command.Parameters.AddWithValue(ColumnCharacterName, Trim(characterName))
            Dim result = command.ExecuteScalar()
            If result Is Nothing Then
                Return Nothing
            End If
            characterId = CInt(result)
        End Using
        Return characterId
    End Function

    Friend Function ReadDetails(connection As MySqlConnection, characterId As Integer) As CharacterDetails
        Using command = connection.CreateCommand()
            command.CommandText = $"
SELECT 
    `{ColumnCharacterName}`,
    `{ColumnRaceId}`,
    `{ColumnRaceName}`,
    `{ColumnPlayerId}`,
    `{ColumnPlayerName}`
FROM 
    `{ViewCharacterDetails}` 
WHERE 
    `{ColumnCharacterId}`=@{ColumnCharacterId};"
            command.Parameters.AddWithValue(ColumnCharacterId, characterId)
            Using reader = command.ExecuteReader
                reader.Read()
                Return New CharacterDetails(characterId, reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4))
            End Using
        End Using
    End Function

    Friend Function AllForPlayer(connection As MySqlConnection, playerId As Integer) As Dictionary(Of String, Integer)
        Dim result As New Dictionary(Of String, Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT `{ColumnCharacterId}`, `{ColumnCharacterName}` FROM `{TableCharacters}` WHERE `{ColumnPlayerId}`=@{ColumnPlayerId};"
            command.Parameters.AddWithValue(ColumnPlayerId, playerId)
            Using reader = command.ExecuteReader
                While reader.Read
                    Dim characterId = reader.GetInt32(0)
                    Dim characterName = reader.GetString(1)
                    result($"{characterName}(Id={characterId})") = characterId
                End While
            End Using
        End Using
        Return result
    End Function

    Friend Function NameExists(connection As MySqlConnection, playerId As Integer, characterName As String) As Boolean
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT COUNT(1) FROM `{TableCharacters}` WHERE `{ColumnPlayerId}`=@{ColumnPlayerId} AND `{ColumnCharacterName}`=@{ColumnCharacterName};"
            command.Parameters.AddWithValue(ColumnCharacterName, characterName)
            command.Parameters.AddWithValue(ColumnPlayerId, playerId)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function
End Module

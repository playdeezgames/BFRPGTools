Friend Module Characters
    Friend Sub Delete(connection As MySqlConnection, characterId As Integer)
        CharacterAbilities.DeleteForCharacter(connection, characterId)
        Using command = connection.CreateCommand
            command.CommandText = $"DELETE FROM `{Tables.Characters}` WHERE `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
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
INSERT IGNORE INTO {Tables.Characters}
(
    {Columns.PlayerId}, 
    {Columns.CharacterName},
    {Columns.RaceId}
) 
VALUES
(
    @{Columns.PlayerId}, 
    @{Columns.CharacterName},
    @{Columns.RaceId}
) 
RETURNING 
    {Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            command.Parameters.AddWithValue(Columns.RaceId, raceId)
            command.Parameters.AddWithValue(Columns.CharacterName, Trim(characterName))
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
    `{Columns.CharacterName}`,
    `{Columns.RaceId}`,
    `{Columns.RaceName}`,
    `{Columns.PlayerId}`,
    `{Columns.PlayerName}`
FROM 
    `{Views.CharacterDetails}` 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            Using reader = command.ExecuteReader
                reader.Read()
                Return New CharacterDetails(characterId, reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4))
            End Using
        End Using
    End Function

    Friend Function AllForPlayer(connection As MySqlConnection, playerId As Integer) As Dictionary(Of String, Integer)
        Dim result As New Dictionary(Of String, Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.CharacterId}`, 
    `{Columns.CharacterName}` 
FROM 
    `{Tables.Characters}` 
WHERE 
    `{Columns.PlayerId}`=@{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
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
            command.CommandText = $"
SELECT 
    COUNT(1) 
FROM 
    `{Tables.Characters}` 
WHERE 
    `{Columns.PlayerId}`=@{Columns.PlayerId} AND 
    `{Columns.CharacterName}`=@{Columns.CharacterName};"
            command.Parameters.AddWithValue(Columns.CharacterName, characterName)
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function
End Module

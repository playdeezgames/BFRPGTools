Friend Module Characters
    Friend Sub Delete(connection As MySqlConnection, characterId As Integer)
        CharacterAbilities.DeleteForCharacter(connection, characterId)
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{Tables.Characters}` 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Friend Sub Rename(connection As MySqlConnection, characterId As Integer, characterName As String)
        Using command = connection.CreateCommand
            command.CommandText = $"
UPDATE 
    `{Tables.Characters}` 
SET 
    `{Columns.CharacterName}`=@{Columns.CharacterName} 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.Parameters.AddWithValue(Columns.CharacterName, characterName)
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

    Friend Function AllForPlayer(connection As MySqlConnection, playerId As Integer) As IEnumerable(Of CharacterDetails)
        Dim result As New List(Of CharacterDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.CharacterId}`,
    `{Columns.CharacterName}`,
    `{Columns.RaceId}`,
    `{Columns.RaceName}`,
    `{Columns.PlayerId}`,
    `{Columns.PlayerName}`
FROM 
    `{Views.CharacterDetails}` 
WHERE 
    `{Columns.PlayerId}`=@{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            Using reader = command.ExecuteReader
                While reader.Read()
                    result.Add(
                    New CharacterDetails(
                        reader(Columns.CharacterId),
                        reader(Columns.CharacterName),
                        reader(Columns.RaceId),
                        reader(Columns.RaceName),
                        reader(Columns.PlayerId),
                        reader(Columns.PlayerName)))
                End While
            End Using
        End Using
        Return result
    End Function

    Friend Function FindForPlayerAndName(connection As MySqlConnection, playerId As Integer, characterName As String) As Integer?
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.CharacterId}`
FROM 
    `{Tables.Characters}` 
WHERE 
    `{Columns.PlayerId}`=@{Columns.PlayerId} AND 
    `{Columns.CharacterName}`=@{Columns.CharacterName};"
            command.Parameters.AddWithValue(Columns.CharacterName, characterName)
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CInt(result)
        End Using
    End Function
End Module

Friend Module CharacterAbilities
    Friend Sub Write(
                    connection As MySqlConnection,
                    characterId As Integer,
                    abilityId As Integer,
                    abilityScore As Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT INTO `{Tables.CharacterAbilities}`
(
    `{Columns.CharacterId}`, 
    `{Columns.AbilityId}`, 
    `{Columns.AbilityScore}`
) 
VALUES
(
    @{Columns.CharacterId}, 
    @{Columns.AbilityId}, 
    @{Columns.AbilityScore}
) 
ON DUPLICATE KEY UPDATE 
    `{Columns.AbilityScore}`=@{Columns.AbilityScore};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.Parameters.AddWithValue(Columns.AbilityId, abilityId)
            command.Parameters.AddWithValue(Columns.AbilityScore, abilityScore)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Friend Sub DeleteForCharacter(connection As MySqlConnection, characterId As Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{Tables.CharacterAbilities}` 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Friend Function ReadAllDetailsForCharacter(connection As MySqlConnection, characterId As Integer) As IEnumerable(Of CharacterAbilityDetails)
        Dim result As New List(Of CharacterAbilityDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.CharacterId}`, 
    `{Columns.CharacterName}`, 
    `{Columns.AbilityId}`, 
    `{Columns.AbilityName}`, 
    `{Columns.AbilityAbbreviation}`, 
    `{Columns.AbilityScore}` 
FROM 
    `{Views.CharacterAbilityDetails}` 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(New CharacterAbilityDetails(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5)))
                End While
            End Using
        End Using
        Return result
    End Function
End Module

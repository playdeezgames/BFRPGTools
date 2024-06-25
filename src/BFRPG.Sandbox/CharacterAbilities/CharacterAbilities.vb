Friend Module CharacterAbilities
    Friend Sub Write(
                    connection As MySqlConnection,
                    characterId As Integer,
                    abilityId As Integer,
                    abilityScore As Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT INTO `{TableCharacterAbilities}`
(
    `{ColumnCharacterId}`, 
    `{ColumnAbilityId}`, 
    `{ColumnAbilityScore}`
) 
VALUES
(
    @{ColumnCharacterId}, 
    @{ColumnAbilityId}, 
    @{ColumnAbilityScore}
) 
ON DUPLICATE KEY UPDATE 
    `{ColumnAbilityScore}`=@{ColumnAbilityScore};"
            command.Parameters.AddWithValue(ColumnCharacterId, characterId)
            command.Parameters.AddWithValue(ColumnAbilityId, abilityId)
            command.Parameters.AddWithValue(ColumnAbilityScore, abilityScore)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Friend Sub DeleteForCharacter(connection As MySqlConnection, characterId As Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"DELETE FROM `{TableCharacterAbilities}` WHERE `{ColumnCharacterId}`=@{ColumnCharacterId};"
            command.Parameters.AddWithValue(ColumnCharacterId, characterId)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Friend Function ReadAllDetailsForCharacter(connection As MySqlConnection, characterId As Integer) As IEnumerable(Of CharacterAbilityDetails)
        Dim result As New List(Of CharacterAbilityDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT `{ColumnCharacterId}`, `{ColumnCharacterName}`, `{ColumnAbilityId}`, `{ColumnAbilityName}`, `{ColumnAbilityAbbreviation}`, `{ColumnAbilityScore}` FROM `{ViewCharacterAbilityDetails}` WHERE `{ColumnCharacterId}`=@{ColumnCharacterId};"
            command.Parameters.AddWithValue(ColumnCharacterId, characterId)
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(New CharacterAbilityDetails(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5)))
                End While
            End Using
        End Using
        Return result
    End Function
End Module

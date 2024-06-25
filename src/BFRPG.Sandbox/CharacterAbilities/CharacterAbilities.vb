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
End Module

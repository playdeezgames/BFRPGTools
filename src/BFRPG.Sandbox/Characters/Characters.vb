Friend Module Characters
    Friend Sub Delete(connection As MySqlConnection, characterId As Integer)
        Throw New NotImplementedException()
    End Sub

    Friend Function Create(connection As MySqlConnection, playerId As Integer, characterName As String) As Integer?
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
            Return CInt(result)
        End Using
    End Function

    Friend Function ReadDetails(connection As MySqlConnection, characterId As Integer) As CharacterDetails
        Throw New NotImplementedException()
    End Function
End Module

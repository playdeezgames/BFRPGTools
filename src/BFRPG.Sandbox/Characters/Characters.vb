Friend Module Characters
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
End Module

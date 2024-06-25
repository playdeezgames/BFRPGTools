Friend Module Players
    Friend Sub Delete(connection As MySqlConnection, playerId As Integer)
        Using command = connection.CreateCommand()
            command.CommandText = $"DELETE FROM {TablePlayers} WHERE {ColumnPlayerId}=@{ColumnPlayerId};"
            command.Parameters.AddWithValue(ColumnPlayerId, playerId)
            command.ExecuteNonQuery()
        End Using
    End Sub


    Friend Function Create(connection As MySqlConnection, playerName As String) As Integer?
        Using command = connection.CreateCommand
            command.CommandText = $"INSERT IGNORE INTO {TablePlayers}({ColumnPlayerName}) VALUES(@{ColumnPlayerName}) RETURNING {ColumnPlayerId};"
            command.Parameters.AddWithValue(ColumnPlayerName, Trim(playerName))
            Dim result = command.ExecuteScalar()
            If result Is Nothing Then
                Return Nothing
            End If
            Return CInt(result)
        End Using
    End Function

    Friend Sub ReadDetails(connection As MySqlConnection, playerId As Integer, ByRef playerName As String, ByRef characterCount As Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT {ColumnPlayerName},{ColumnCharacterCount} FROM {ViewPlayerDetails} WHERE {ColumnPlayerId}=@{ColumnPlayerId};"
            command.Parameters.AddWithValue(ColumnPlayerId, playerId)
            Using reader = command.ExecuteReader
                If Not reader.Read Then
                    Return
                End If
                playerName = reader.GetString(0)
                characterCount = reader.GetInt32(1)
            End Using
        End Using
    End Sub

    Friend Function All(connection As MySqlConnection) As Dictionary(Of String, Integer)
        Dim table As New Dictionary(Of String, Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT {ColumnPlayerId},{ColumnPlayerName} FROM {TablePlayers} ORDER BY {ColumnPlayerName};"
            Using reader = command.ExecuteReader
                While reader.Read
                    Dim playerId = reader.GetInt32(0)
                    Dim playerName = reader.GetString(1)
                    table($"{playerName}(Id={playerId})") = playerId
                End While
            End Using
        End Using
        Return table
    End Function
End Module

Friend Module Players
    Friend Sub Delete(connection As MySqlConnection, playerId As Integer)
        Using command = connection.CreateCommand()
            command.CommandText = $"DELETE FROM {Tables.Players} WHERE {Columns.PlayerId}=@{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            command.ExecuteNonQuery()
        End Using
    End Sub


    Friend Function Create(connection As MySqlConnection, playerName As String) As Integer?
        Using command = connection.CreateCommand
            command.CommandText = $"INSERT IGNORE INTO {Tables.Players}({Columns.PlayerName}) VALUES(@{Columns.PlayerName}) RETURNING {PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerName, Trim(playerName))
            Dim result = command.ExecuteScalar()
            If result Is Nothing Then
                Return Nothing
            End If
            Return CInt(result)
        End Using
    End Function

    Friend Function ReadDetails(connection As MySqlConnection, playerId As Integer) As PlayerDetails
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT {PlayerName},{CharacterCount} FROM {ViewPlayerDetails} WHERE {Columns.PlayerId}=@{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            Using reader = command.ExecuteReader
                If Not reader.Read Then
                    Return Nothing
                End If
                Return New PlayerDetails(playerId, reader.GetString(0), reader.GetInt32(1))
            End Using
        End Using
    End Function

    Friend Function All(connection As MySqlConnection) As Dictionary(Of String, Integer)
        Dim table As New Dictionary(Of String, Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT {PlayerId},{PlayerName} FROM {Tables.Players} ORDER BY {PlayerName};"
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

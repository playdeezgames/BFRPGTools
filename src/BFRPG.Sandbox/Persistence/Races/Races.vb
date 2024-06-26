Friend Module Races
    Friend Function All(connection As MySqlConnection) As IEnumerable(Of RaceDetails)
        Dim result As New List(Of RaceDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT `{RaceId}`, `{RaceName}` FROM `{TableRaces}`;"
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(New RaceDetails(reader.GetInt32(0), reader.GetString(1)))
                End While
            End Using
        End Using
        Return result
    End Function
End Module

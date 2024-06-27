Friend Module Races
    Friend Function All(connection As MySqlConnection) As IEnumerable(Of RaceDetails)
        Dim result As New List(Of RaceDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.RaceId}`, 
    `{Columns.RaceName}` 
FROM 
    `{Tables.Races}`;"
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(
                        New RaceDetails(
                            reader(Columns.RaceId),
                            reader(Columns.RaceName)))
                End While
            End Using
        End Using
        Return result
    End Function
End Module

Friend Module RaceClasses
    Friend Function All(connection As MySqlConnection) As IEnumerable(Of RaceClassDetails)
        Dim result As New List(Of RaceClassDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.RaceClassId}`, 
    `{Columns.RaceId}`, 
    `{Columns.RaceName}`,
    `{Columns.ClassId}`, 
    `{Columns.ClassName}` 
FROM 
    `{Views.RaceClassDetails}`;"
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(
                        New RaceClassDetails(
                            reader(Columns.RaceClassId),
                            reader(Columns.RaceId),
                            reader(Columns.RaceName),
                            reader(Columns.ClassId),
                            reader(Columns.ClassName)))
                End While
            End Using
        End Using
        Return result
    End Function

End Module

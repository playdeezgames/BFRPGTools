Friend Class RaceClasses
    Friend Shared Function ReadDetails(connection As MySqlConnection, raceClassId As Integer) As RaceClassDetails
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.RaceClassId}`, 
    `{Columns.RaceId}`, 
    `{Columns.RaceName}`,
    `{Columns.ClassId}`, 
    `{Columns.ClassName}` , 
    `{Columns.HitDieSize}` , 
    `{Columns.MaximumHitDice}` 
FROM 
    `{Views.RaceClassDetails}`
WHERE
    `{Columns.RaceClassId}`=@{Columns.RaceClassId};"
            command.Parameters.AddWithValue(Columns.RaceClassId, raceClassId)
            Using reader = command.ExecuteReader
                If reader.Read Then
                    Return New RaceClassDetails(
                            reader(Columns.RaceClassId),
                            reader(Columns.RaceId),
                            reader(Columns.RaceName),
                            reader(Columns.ClassId),
                            reader(Columns.ClassName),
                            reader(Columns.HitDieSize),
                            reader(Columns.MaximumHitDice))
                End If
                Return Nothing
            End Using
        End Using
    End Function
    Friend Shared Function All(connection As MySqlConnection) As IEnumerable(Of RaceClassDetails)
        Dim result As New List(Of RaceClassDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.RaceClassId}`, 
    `{Columns.RaceId}`, 
    `{Columns.RaceName}`,
    `{Columns.ClassId}`, 
    `{Columns.ClassName}` , 
    `{Columns.HitDieSize}` , 
    `{Columns.MaximumHitDice}` 
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
                            reader(Columns.ClassName),
                            reader(Columns.HitDieSize),
                            reader(Columns.MaximumHitDice)))
                End While
            End Using
        End Using
        Return result
    End Function

End Class

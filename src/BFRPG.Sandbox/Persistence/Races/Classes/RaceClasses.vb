Friend Class RaceClasses
    Implements IRaceClasses

    Private ReadOnly connection As MySqlConnection

    Public Sub New(connection As MySqlConnection)
        Me.connection = connection
    End Sub

    Function ReadDetails(raceClassId As Integer) As RaceClassDetails Implements IRaceClasses.ReadDetails
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
    Function All() As IEnumerable(Of RaceClassDetails) Implements IRaceClasses.All
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

    Public Function AbilityRanges(raceClassId As Integer) As IRaceClassAbilityRanges Implements IRaceClasses.AbilityRanges
        Return New RaceClassAbilityRanges(connection, raceClassId)
    End Function
End Class

Friend Class Races
    Implements IRaces

    Private ReadOnly connection As MySqlConnection

    Public Sub New(connection As MySqlConnection)
        Me.connection = connection
    End Sub

    Function All() As IEnumerable(Of RaceDetails) Implements IRaces.All
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
End Class

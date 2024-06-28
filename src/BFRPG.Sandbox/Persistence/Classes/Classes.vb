Friend Module Classes
    Friend Function All(connection As MySqlConnection) As IEnumerable(Of ClassDetails)
        Dim result As New List(Of ClassDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.ClassId}`, 
    `{Columns.ClassName}` 
FROM 
    `{Tables.Classes}`;"
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(
                        New ClassDetails(
                            reader(Columns.ClassId),
                            reader(Columns.ClassName)))
                End While
            End Using
        End Using
        Return result
    End Function
End Module

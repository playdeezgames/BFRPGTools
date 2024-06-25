Friend Module Abilities
    Friend Function AllIds(connection As MySqlConnection) As IEnumerable(Of Integer)
        Dim result As New List(Of Integer)
        Using command = connection.CreateCommand()
            command.CommandText = $"
SELECT 
    `{ColumnAbilityId}` 
FROM 
    `{TableAbilities}`;"
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(reader.GetInt32(0))
                End While
            End Using
        End Using
        Return result
    End Function
End Module

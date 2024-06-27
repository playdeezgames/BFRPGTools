﻿Friend Module Abilities
    Friend Function All(connection As MySqlConnection) As IEnumerable(Of AbilityDetails)
        Dim result As New List(Of AbilityDetails)
        Using command = connection.CreateCommand()
            command.CommandText = $"
SELECT 
    `{Columns.AbilityId}`,
    `{Columns.AbilityName}`,
    `{Columns.AbilityAbbreviation}`
FROM 
    `{Tables.Abilities}`;"
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(New AbilityDetails(
                                reader(Columns.AbilityId),
                                reader(Columns.AbilityName),
                                reader(Columns.AbilityAbbreviation)))
                End While
            End Using
        End Using
        Return result
    End Function
End Module

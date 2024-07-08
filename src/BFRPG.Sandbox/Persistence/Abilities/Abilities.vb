Friend Class Abilities
    Implements IAbilities
    Private ReadOnly connection As MySqlConnection
    Sub New(connection As MySqlConnection)
        Me.connection = connection
    End Sub

    Public Function All() As IEnumerable(Of AbilityDetails) Implements IAbilities.All
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
End Class

﻿Friend Class Store
    Implements IStore

    Private ReadOnly connection As MySqlConnection

    Public Sub New(connection As MySqlConnection)
        Me.connection = connection
    End Sub

    Public Function ReadAll(columns As IEnumerable(Of String), tableName As String) As IEnumerable(Of IReadOnlyDictionary(Of String, Object)) Implements IStore.ReadAll
        Dim result As New List(Of IReadOnlyDictionary(Of String, Object))
        Using command = connection.CreateCommand()

            command.CommandText = $"
SELECT 
    {String.Join(",", columns.Select(Function(x) $"`{x}`"))}
FROM 
    `{tableName}`;"
            Using reader = command.ExecuteReader
                While reader.Read
                    Dim record As New Dictionary(Of String, Object)
                    For Each column In columns
                        record(column) = reader(column)
                    Next
                    result.Add(record)
                End While
            End Using
        End Using
        Return result
    End Function
End Class
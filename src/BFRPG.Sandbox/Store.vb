Friend Class Store
    Implements IStore

    Private ReadOnly connection As MySqlConnection

    Public Sub New(connection As MySqlConnection)
        Me.connection = connection
    End Sub

    Public Sub Delete(tableName As String, forColumns As IReadOnlyDictionary(Of String, Object)) Implements IStore.Delete
        Using command = connection.CreateCommand()
            command.CommandText = $"
DELETE FROM 
    {tableName} 
{BuildWhereClause(forColumns)};"
            AddParameters(command, forColumns)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Function ReadAll(columns As IEnumerable(Of String), viewName As String, Optional forColumns As IReadOnlyDictionary(Of String, Object) = Nothing) As IEnumerable(Of IReadOnlyDictionary(Of String, Object)) Implements IStore.ReadAll
        Dim result As New List(Of IReadOnlyDictionary(Of String, Object))
        Dim whereClause = BuildWhereClause(forColumns)

        Using command = connection.CreateCommand()
            command.CommandText = $"
SELECT 
    {String.Join(",", columns.Select(Function(x) $"`{x}`"))}
FROM 
    `{viewName}`{whereClause};"
            AddParameters(command, forColumns)
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

    Private Shared Sub AddParameters(command As MySqlCommand, columns As IReadOnlyDictionary(Of String, Object))
        If columns IsNot Nothing Then
            For Each column In columns
                command.Parameters.AddWithValue(column.Key, column.Value)
            Next
        End If
    End Sub

    Private Shared Function BuildWhereClause(ByRef forColumns As IReadOnlyDictionary(Of String, Object)) As String
        Return If(forColumns IsNot Nothing AndAlso forColumns.Any, $" WHERE {String.Join(" AND ", forColumns.Keys.Select(Function(x) $"`{x}`=@{x}"))}", String.Empty)
    End Function

    Public Sub Update(tableName As String, updateColumns As IReadOnlyDictionary(Of String, Object), forColumns As IReadOnlyDictionary(Of String, Object)) Implements IStore.Update
        Using command = connection.CreateCommand
            command.CommandText = $"
UPDATE 
    `{tableName}` 
SET 
    {BuildUpdateList(updateColumns)}
    {BuildWhereClause(forColumns)};
"
            AddParameters(command, updateColumns)
            AddParameters(command, forColumns)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Shared Function BuildUpdateList(updateColumns As IReadOnlyDictionary(Of String, Object)) As Object
        Return String.Join(",", updateColumns.Keys.Select(Function(x) $"`{x}`=@{x}"))
    End Function
End Class

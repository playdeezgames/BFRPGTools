Public Class Store
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
        Dim whereClause = BuildWhereClause(forColumns)

        Using command = connection.CreateCommand()
            command.CommandText = $"
SELECT 
    {String.Join(",", columns.Select(Function(x) $"`{x}`"))}
FROM 
    `{viewName}`{whereClause};"
            AddParameters(command, forColumns)
            Return ReadResults(columns, command)
        End Using
    End Function

    Private Shared Function ReadResults(columns As IEnumerable(Of String), command As MySqlCommand) As IEnumerable(Of IReadOnlyDictionary(Of String, Object))
        Dim result As New List(Of IReadOnlyDictionary(Of String, Object))
        Using reader = command.ExecuteReader
            While reader.Read
                Dim record As New Dictionary(Of String, Object)
                For Each column In columns
                    record(column) = reader(column)
                Next
                result.Add(record)
            End While
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
    {BuildUpdateList(updateColumns.Keys)}
    {BuildWhereClause(forColumns)};
"
            AddParameters(command, updateColumns)
            AddParameters(command, forColumns)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Shared Function BuildUpdateList(updateColumns As IEnumerable(Of String)) As Object
        Return String.Join(",", updateColumns.Select(Function(x) $"`{x}`=@{x}"))
    End Function

    Public Function Insert(
                        tableName As String,
                        insertColumns As IReadOnlyDictionary(Of String, Object),
                        Optional returnColumns As IEnumerable(Of String) = Nothing,
                        Optional updateColumns As IEnumerable(Of String) = Nothing) As IReadOnlyDictionary(Of String, Object) Implements IStore.Insert
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT {If(updateColumns IsNot Nothing AndAlso updateColumns.Any, "", "IGNORE")} INTO {tableName}
(
    {BuildInsertList(insertColumns)}
) 
VALUES
(
    {BuildParameterList(insertColumns)}
) 
    {BuildUpdateClause(updateColumns)}
    {BuildReturningList(returnColumns)};"
            AddParameters(command, insertColumns)
            If returnColumns IsNot Nothing AndAlso returnColumns.Any Then
                Return ReadResults(returnColumns, command).FirstOrDefault
            End If
            command.ExecuteNonQuery()
            Return Nothing
        End Using
    End Function

    Private Function BuildUpdateClause(updateColumns As IEnumerable(Of String)) As Object
        If updateColumns IsNot Nothing AndAlso updateColumns.Any Then
            Return $"ON DUPLICATE KEY UPDATE {BuildUpdateList(updateColumns)}"
        End If
        Return String.Empty
    End Function

    Private Shared Function BuildInsertList(insertColumns As IReadOnlyDictionary(Of String, Object)) As Object
        Return String.Join(",", insertColumns.Keys.Select(Function(x) $"`{x}`"))
    End Function

    Private Shared Function BuildReturningList(returnColumns As IEnumerable(Of String)) As Object
        If returnColumns IsNot Nothing AndAlso returnColumns.Any Then
            Return $" RETURNING {String.Join(",", returnColumns.Select(Function(x) $"`{x}`"))}"
        End If
        Return String.Empty
    End Function

    Private Shared Function BuildParameterList(insertColumns As IReadOnlyDictionary(Of String, Object)) As Object
        Return String.Join(",", insertColumns.Keys.Select(Function(x) $"@{x}"))
    End Function
End Class

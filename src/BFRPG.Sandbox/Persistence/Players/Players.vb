Friend Class Players
    Implements IPlayers

    Public Sub New(connection As MySqlConnection, store As IStore)
        Me.connection = connection
        Me.store = store
    End Sub

    Private ReadOnly connection As MySqlConnection
    Private ReadOnly store As IStore

    Public Sub Delete(playerId As Integer) Implements IPlayers.Delete
        store.Delete(Tables.Players, New Dictionary(Of String, Object) From {{Columns.PlayerId, playerId}})
    End Sub

    Public Sub Rename(playerId As Integer, playerName As String) Implements IPlayers.Rename
        store.Update(
            Tables.Players,
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerName, playerName}
            },
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerId, playerId}
            })
    End Sub

    Public Function Create(playerName As String) As Integer? Implements IPlayers.Create
        Dim result = store.Insert(
            Tables.Players,
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerName, playerName}
            },
            {
                Columns.PlayerId
            })
        If result Is Nothing Then
            Return Nothing
        End If
        Return CInt(result(Columns.PlayerId))
    End Function

    Public Function ReadDetails(playerId As Integer) As PlayerDetails Implements IPlayers.ReadDetails
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.PlayerId}`,
    `{Columns.PlayerName}`,
    `{Columns.CharacterCount}`
FROM 
    `{Views.PlayerDetails}` 
WHERE 
    `{Columns.PlayerId}` = @{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            Using reader = command.ExecuteReader
                If Not reader.Read Then
                    Return Nothing
                End If
                Return New PlayerDetails(
                    reader(Columns.PlayerId),
                    reader(Columns.PlayerName),
                    reader(Columns.CharacterCount))
            End Using
        End Using
    End Function

    Public Function All() As IEnumerable(Of PlayerDetails) Implements IPlayers.All
        Dim result As New List(Of PlayerDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    {Columns.PlayerId},
    {Columns.PlayerName},
    {Columns.CharacterCount}
FROM 
    {Views.PlayerDetails} 
ORDER BY 
    {Columns.PlayerName};"
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(
                        New PlayerDetails(
                            reader(Columns.PlayerId),
                            reader(Columns.PlayerName),
                            reader(Columns.CharacterCount)))
                End While
            End Using
        End Using
        Return result
    End Function

    Public Function FindForName(playerName As String) As Integer? Implements IPlayers.FindForName
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.PlayerId}` 
FROM 
    `{Tables.Players}` 
WHERE 
    `{Columns.PlayerName}`=@{Columns.PlayerName};"
            command.Parameters.AddWithValue(Columns.PlayerName, playerName)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CInt(result)
        End Using
    End Function
End Class

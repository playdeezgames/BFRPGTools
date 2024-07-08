﻿Friend Class Players
    Implements IPlayers

    Public Sub New(connection As MySqlConnection)
        Me.Connection = connection
    End Sub

    Private ReadOnly connection As MySqlConnection

    Public Sub Delete(playerId As Integer) Implements IPlayers.Delete
        Using command = connection.CreateCommand()
            command.CommandText = $"
DELETE FROM 
    {Tables.Players} 
WHERE {Columns.PlayerId}=@{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub Rename(playerId As Integer, playerName As String) Implements IPlayers.Rename
        Using command = connection.CreateCommand()
            command.CommandText = $"
UPDATE 
    `{Tables.Players}` 
SET 
    `{Columns.PlayerName}`=@{Columns.PlayerName} 
WHERE 
    `{Columns.PlayerId}`=@{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            command.Parameters.AddWithValue(Columns.PlayerName, playerName)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Function Create(playerName As String) As Integer? Implements IPlayers.Create
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT IGNORE INTO {Tables.Players}
(
    {Columns.PlayerName}
) 
VALUES
(
    @{Columns.PlayerName}
) 
RETURNING 
    {PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerName, Trim(playerName))
            Dim result = command.ExecuteScalar()
            If result Is Nothing Then
                Return Nothing
            End If
            Return CInt(result)
        End Using
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

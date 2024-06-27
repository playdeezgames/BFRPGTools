﻿Friend Module Players
    Friend Sub Delete(
                     connection As MySqlConnection,
                     playerId As Integer)
        Using command = connection.CreateCommand()
            command.CommandText = $"
DELETE FROM 
    {Tables.Players} 
WHERE {Columns.PlayerId}=@{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            command.ExecuteNonQuery()
        End Using
    End Sub


    Friend Function Create(
                          connection As MySqlConnection,
                          playerName As String) As Integer?
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

    Friend Function ReadDetails(
                               connection As MySqlConnection,
                               playerId As Integer) As PlayerDetails
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

    Friend Function All(
                       connection As MySqlConnection) As IEnumerable(Of PlayerDetails)
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
End Module

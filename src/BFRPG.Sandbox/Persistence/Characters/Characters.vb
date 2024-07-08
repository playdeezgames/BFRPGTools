Friend Class Characters
    Implements ICharacters
    Private ReadOnly connection As MySqlConnection
    Sub New(connection As MySqlConnection)
        Me.connection = connection
    End Sub

    Public Sub Delete(characterId As Integer) Implements ICharacters.Delete
        CharacterAbilities.DeleteForCharacter(connection, characterId)
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{Tables.Characters}` 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub Rename(characterId As Integer, characterName As String) Implements ICharacters.Rename
        Using command = connection.CreateCommand
            command.CommandText = $"
UPDATE 
    `{Tables.Characters}` 
SET 
    `{Columns.CharacterName}`=@{Columns.CharacterName} 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.Parameters.AddWithValue(Columns.CharacterName, characterName)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub Transfer(characterId As Integer, playerId As Integer) Implements ICharacters.Transfer
        Using command = connection.CreateCommand
            command.CommandText = $"
UPDATE 
    `{Tables.Characters}` 
SET 
    `{Columns.PlayerId}`=@{Columns.PlayerId} 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub AddXP(characterId As Integer, experiencePoints As Integer) Implements ICharacters.AddXP
        Using command = connection.CreateCommand
            command.CommandText = $"
UPDATE 
    `{Tables.Characters}` 
SET 
    `{Columns.ExperiencePoints}`=`{Columns.ExperiencePoints}`+@{Columns.ExperiencePoints} 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};
"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.Parameters.AddWithValue(Columns.ExperiencePoints, experiencePoints)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Function Create(playerId As Integer, characterName As String, raceClassId As Integer, experiencePoints As Integer, characterDescription As String) As Integer? Implements ICharacters.Create
        Dim characterId As Integer = 0
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT IGNORE INTO {Tables.Characters}
(
    `{Columns.PlayerId}`, 
    `{Columns.CharacterName}`,
    `{Columns.RaceClassId}`,
    `{Columns.ExperiencePoints}`,
    `{Columns.CharacterDescription}`
) 
VALUES
(
    @{Columns.PlayerId}, 
    @{Columns.CharacterName},
    @{Columns.RaceClassId},
    @{Columns.ExperiencePoints},
    @{Columns.CharacterDescription}
) 
RETURNING 
    {Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            command.Parameters.AddWithValue(Columns.RaceClassId, raceClassId)
            command.Parameters.AddWithValue(Columns.ExperiencePoints, experiencePoints)
            command.Parameters.AddWithValue(Columns.CharacterName, Trim(characterName))
            command.Parameters.AddWithValue(Columns.CharacterDescription, Trim(characterDescription))
            Dim result = command.ExecuteScalar()
            If result Is Nothing Then
                Return Nothing
            End If
            characterId = CInt(result)
        End Using
        Return characterId
    End Function

    Public Function ReadDetails(characterId As Integer) As CharacterDetails Implements ICharacters.ReadDetails
        Using command = connection.CreateCommand()
            command.CommandText = $"
SELECT 
    `{Columns.CharacterId}`,
    `{Columns.CharacterName}`,
    `{Columns.RaceId}`,
    `{Columns.RaceName}`,
    `{Columns.PlayerId}`,
    `{Columns.PlayerName}`,
    `{Columns.ClassId}`,
    `{Columns.ClassName}`,
    `{Columns.ExperiencePoints}`,
    `{Columns.Level}`,
    `{Columns.HitPoints}`,
    `{Columns.CharacterDescription}`
FROM 
    `{Views.CharacterDetails}` 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            Using reader = command.ExecuteReader
                reader.Read()
                Return New CharacterDetails(
                    reader(Columns.CharacterId),
                    reader(Columns.CharacterName),
                    reader(Columns.RaceId),
                    reader(Columns.RaceName),
                    reader(Columns.PlayerId),
                    reader(Columns.PlayerName),
                    reader(Columns.ClassId),
                    reader(Columns.ClassName),
                    reader(Columns.ExperiencePoints),
                    reader(Columns.Level),
                    reader(Columns.HitPoints),
                    reader(Columns.CharacterDescription))
            End Using
        End Using
    End Function

    Public Function AllForPlayer(playerId As Integer) As IEnumerable(Of CharacterDetails) Implements ICharacters.AllForPlayer
        Dim result As New List(Of CharacterDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.CharacterId}`,
    `{Columns.CharacterName}`,
    `{Columns.RaceId}`,
    `{Columns.RaceName}`,
    `{Columns.PlayerId}`,
    `{Columns.PlayerName}`,
    `{Columns.ClassId}`,
    `{Columns.ClassName}`,
    `{Columns.ExperiencePoints}`,
    `{Columns.Level}`,
    `{Columns.HitPoints}`,
    `{Columns.CharacterDescription}`
FROM 
    `{Views.CharacterDetails}` 
WHERE 
    `{Columns.PlayerId}`=@{Columns.PlayerId};"
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            Using reader = command.ExecuteReader
                While reader.Read()
                    result.Add(
                    New CharacterDetails(
                        reader(Columns.CharacterId),
                        reader(Columns.CharacterName),
                        reader(Columns.RaceId),
                        reader(Columns.RaceName),
                        reader(Columns.PlayerId),
                        reader(Columns.PlayerName),
                        reader(Columns.ClassId),
                        reader(Columns.ClassName),
                        reader(Columns.ExperiencePoints),
                        reader(Columns.Level),
                        reader(Columns.HitPoints),
                        reader(Columns.CharacterDescription)))
                End While
            End Using
        End Using
        Return result
    End Function

    Public Function FindForPlayerAndName(playerId As Integer, characterName As String) As Integer? Implements ICharacters.FindForPlayerAndName
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.CharacterId}`
FROM 
    `{Tables.Characters}` 
WHERE 
    `{Columns.PlayerId}`=@{Columns.PlayerId} AND 
    `{Columns.CharacterName}`=@{Columns.CharacterName};"
            command.Parameters.AddWithValue(Columns.CharacterName, characterName)
            command.Parameters.AddWithValue(Columns.PlayerId, playerId)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CInt(result)
        End Using
    End Function
End Class

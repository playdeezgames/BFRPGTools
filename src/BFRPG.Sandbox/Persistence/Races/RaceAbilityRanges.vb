﻿Friend Module RaceAbilityRanges
    Friend Function Valid(connection As MySqlConnection, raceId As Integer, abilityId As Integer, abilityScore As Integer) As Boolean
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    COUNT(1) 
FROM 
    `{Views.RaceAbilityRanges}` 
WHERE 
    `{Columns.RaceId}`=@{Columns.RaceId} AND 
    `{Columns.AbilityId}`=@{Columns.AbilityId} AND 
    `{Columns.MaximumScore}`>=@{Columns.AbilityScore} AND 
    `{Columns.MinimumScore}`<=@{Columns.AbilityScore};"
            command.Parameters.AddWithValue(Columns.AbilityScore, abilityScore)
            command.Parameters.AddWithValue(Columns.AbilityId, abilityId)
            command.Parameters.AddWithValue(Columns.RaceId, raceId)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function
End Module

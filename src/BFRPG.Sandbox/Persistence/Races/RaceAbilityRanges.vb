Friend Module RaceClassAbilityRanges
    Friend Function Valid(
                         connection As MySqlConnection,
                         raceClassId As Integer,
                         abilityId As Integer,
                         abilityScore As Integer) As Boolean
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    COUNT(1) 
FROM 
    `{Views.RaceClassAbilityRanges}` 
WHERE 
    `{Columns.RaceClassId}`=@{Columns.RaceClassId} AND 
    `{Columns.AbilityId}`=@{Columns.AbilityId} AND 
    `{Columns.MaximumScore}`>=@{Columns.AbilityScore} AND 
    `{Columns.MinimumScore}`<=@{Columns.AbilityScore};"
            command.Parameters.AddWithValue(Columns.AbilityScore, abilityScore)
            command.Parameters.AddWithValue(Columns.AbilityId, abilityId)
            command.Parameters.AddWithValue(Columns.RaceClassId, raceClassId)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function
End Module

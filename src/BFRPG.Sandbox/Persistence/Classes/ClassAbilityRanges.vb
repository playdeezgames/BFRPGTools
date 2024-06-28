Friend Module ClassAbilityRanges
    Friend Function Valid(connection As MySqlConnection, classId As Integer, abilityId As Integer, abilityScore As Integer) As Boolean
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    COUNT(1) 
FROM 
    `{Views.ClassAbilityRanges}` 
WHERE 
    `{Columns.ClassId}`=@{Columns.ClassId} AND 
    `{Columns.AbilityId}`=@{Columns.AbilityId} AND 
    `{Columns.MaximumScore}`>=@{Columns.AbilityScore} AND 
    `{Columns.MinimumScore}`<=@{Columns.AbilityScore};"
            command.Parameters.AddWithValue(Columns.AbilityScore, abilityScore)
            command.Parameters.AddWithValue(Columns.AbilityId, abilityId)
            command.Parameters.AddWithValue(Columns.ClassId, classId)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function
End Module

Friend Module RaceAbilityRanges
    Friend Function Valid(connection As MySqlConnection, raceId As Integer, abilityId As Integer, abilityScore As Integer) As Boolean
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT COUNT(1) FROM `{ViewRaceAbilityRanges}` WHERE `{ColumnRaceId}`=@{ColumnRaceId} AND `{ColumnAbilityId}`=@{ColumnAbilityId} AND `{ColumnMaximumScore}`>=@{ColumnAbilityScore} AND `{ColumnMinimumScore}`<=@{ColumnAbilityScore};"
            command.Parameters.AddWithValue(ColumnAbilityScore, abilityScore)
            command.Parameters.AddWithValue(ColumnAbilityId, abilityId)
            command.Parameters.AddWithValue(ColumnRaceId, raceId)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function
End Module
